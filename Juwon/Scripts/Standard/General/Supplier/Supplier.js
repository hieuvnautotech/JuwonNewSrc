/* View */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $supplierGrid = $(`#supplierGrid`);


/* Create */
const cSupplierName = document.getElementById(`cSupplierName`);
const cSupplierAddress = document.getElementById(`cSupplierAddress`);
const cSupplierUrl = document.getElementById(`cSupplierUrl`);
const cSupplierEmail = document.getElementById(`cSupplierEmail`);
const cSupplierPhone = document.getElementById(`cSupplierPhone`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/* Modify */
const mSupplierName = document.getElementById(`mSupplierName`);
const mSupplierAddress = document.getElementById(`mSupplierAddress`);
const mSupplierUrl = document.getElementById(`mSupplierUrl`);
const mSupplierEmail = document.getElementById(`mSupplierEmail`);
const mSupplierPhone = document.getElementById(`mSupplierPhone`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/* Delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var supplierId = 0;
async function Initialize() {
    await SupllierGrid();
    await ReloadSupplierGrid();
}
//PROCESS GRID
async function SupllierGrid() {
    "use strict";
    $supplierGrid.jqGrid({
        colModel: [
            { name: "SupplierId", label: "", key: true, hidden: true },
            { name: "SupplierName", label: "Name", width: 100, align: 'center', searchoptions: { sopt: ['cn'] } },
            { name: "SupplierAddress", label: "Address", width: 100, align: 'center', search: false },
            { name: "SupplierUrl", label: "Url", width: 100, align: 'center', search: false },
            { name: "SupplierEmail", label: "Email", width: 100, align: 'center', search: false },
            { name: "SupplierPhone", label: "Phone", width: 100, align: 'center', search: false },
            {
                name: "CreatedDate", width: 100, align: 'center', formatter: 'date', formatoptions:
                {
                    srcformat: "ISO8601Long", newformat: "Y-m-d"
                },
                sorttype: 'date',
                label: "Created Date",
                searchoptions: {
                    sopt: ['ge'],
                    dataInit: function (elem) {
                        $(elem).datepicker({
                            dateFormat: 'yy-mm-dd',
                            autoSize: true,
                            changeYear: true,
                            changeMonth: true,
                            showButtonPanel: true,
                            showWeek: true,
                            onSelect: function () {
                                $(this).keydown();
                            },
                        });
                    }
                }
            },
            {
                name: "ModifiedDate", width: 100, align: 'center', formatter: 'date', formatoptions:
                {
                    srcformat: "ISO8601Long", newformat: "Y-m-d"
                },
                sorttype: 'date',
                label: "ModifiedDate",
                searchoptions: {
                    sopt: ['ge'],
                    dataInit: function (elem) {
                        $(elem).datepicker({
                            dateFormat: 'yy-mm-dd',
                            autoSize: true,
                            changeYear: true,
                            changeMonth: true,
                            showButtonPanel: true,
                            showWeek: true,
                            onSelect: function () {
                                $(this).keydown();
                            },
                        });
                    }
                }
            },
            { name: "Active", label: "Actived", hidden: true, align: 'center', formatter: ShowActiveStatus },
        ],
        jsonReader:
        {
            root: "result",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        iconSet: "fontAwesome",
        rownumbers: true,
        sortname: "Name",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        pager: true,
        rowNum: 20,
        viewrecords: true,
        shrinkToFit: false,
        height: 600,
        cmTemplate: { resizable: false },
        beforeRequest: function () {
            $(`#gbox_supplierGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
          
        },
        loadonce: true,
        caption: 'Supplier',
        loadComplete: function () {
            $(`#gbox_supplierGrid`).unblock();
            let ids = $supplierGrid.getDataIDs();
            for (let i of ids) {
                let row = $supplierGrid.getRowData(i);
                if (row.Active === "NO") {
                    $supplierGrid.setCell(i, 'SupplierName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == supplierId) {
                supplierId = 0;
            }
            else {
                supplierId = parseInt(rowid);
            }
            let row = $supplierGrid.getRowData(rowid);
            if (row.Active === "NO") {
                deleteBtn.innerHTML = `<i class="fa fa-toggle-on"></i>&nbsp;Active`;
            }
            else {
                deleteBtn.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
            }
        }
    })
        .filterToolbar({
            searchOperators: true,
            searchOnEnter: false,
            loadFilterDefaults: false,
            afterSearch: function () {
            }
        });
}
//RELOAD PROCESS GRID
async function ReloadSupplierGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Supplier/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Supplier/Search?keyWord=${keyWord}`;
        }
        $.ajax({
            url: requestUrl,
            type: `GET`,
        })
            .done(function (response) {
                if (response) {
                    if (response.HttpResponseCode == 100) {
                        WarningAlert(response.ResponseMessage);
                    }
                    else {
                        $supplierGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        supplierId = 0;
                        deleteBtn.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
                    }
                    resolve(true);
                }
            })
            .fail(function () {
                ErrorAlert(`System error - Please contact IT`);
                resolve(false);
            })
    })
}
$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

