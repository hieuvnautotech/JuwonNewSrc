
/** view */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $vendorCategoryGrid = $(`#vendorCategoryGrid`);

/** create */
const cVendorCategoryName = document.getElementById(`cVendorCategoryName`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/** modify */
const mVendorCategoryName = document.getElementById(`mVendorCategoryName`);
const btnModify = document.getElementById(`btnModify`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/** delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var vendorCategoryId = 0;

async function Initialize() {
    await VendorCategoryGrid();
    await ReloadVendorCategoryGrid();
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

//VendorCategory GRID
async function VendorCategoryGrid() {
    "use strict";
    $vendorCategoryGrid.jqGrid({
        colModel: [
            { name: "VendorCategoryId", label: "", key: true, hidden: true },
            { name: "VendorCategoryName", label: "Vendor Category Name", width: 100, align: 'center', search: false },
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
        //loadui: 'disable',
        //footerrow: true,
        beforeRequest: function () {
            $(`#gbox_vendorCategoryGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
            //var model = data.multiLangModel, name, $colHeader, $sortingIcons;
            //if (model)
            //{
            //    for (name in model)
            //    {
            //        if (model.hasOwnProperty(name))
            //        {
            //            $colHeader = $("#jqgh_" + $.jgrid.jqID(this.id + "_" + name));
            //            $sortingIcons = $colHeader.find(">span.s-ico");
            //            $colHeader.text(model[name].label);
            //            $colHeader.append($sortingIcons);
            //        }
            //    }
            //}
        },
        loadonce: true,
        caption: 'VendorCategory',
        loadComplete: function () {
            $(`#gbox_vendorCategoryGrid`).unblock();
            let ids = $vendorCategoryGrid.getDataIDs();
            for (let i of ids) {
                let row = $vendorCategoryGrid.getRowData(i);
                if (row.Active === "NO") {
                    $vendorCategoryGrid.setCell(i, 'VendorCategoryName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == vendorCategoryId) {
                vendorCategoryId = 0;
            }
            else {
                vendorCategoryId = parseInt(rowid);
            }
            let row = $vendorCategoryGrid.getRowData(rowid);
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

//RELOAD VENDOR CATEGORY GRID
async function ReloadVendorCategoryGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/VendorCategory/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/VendorCategory/Search?keyWord=${keyWord}`;
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
                        $vendorCategoryGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        vendorCategoryId = 0;
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
