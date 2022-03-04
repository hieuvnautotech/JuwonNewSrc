/** view */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $buyerGrid = $(`#buyerGrid`);

/** create */
const cBuyerCode = document.getElementById(`cBuyerCode`);
const cBuyerName = document.getElementById(`cBuyerName`);
const cBuyerDescription = document.getElementById(`cBuyerDescription`);
const cBuyerEmail = document.getElementById(`cBuyerEmail`);
const cBuyerPhone = document.getElementById(`cBuyerPhone`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/** modify */
const mBuyerCode = document.getElementById(`mBuyerCode`);
const mBuyerName = document.getElementById(`mBuyerName`);
const mBuyerDescription = document.getElementById(`mBuyerDescription`);
const mBuyerEmail = document.getElementById(`mBuyerEmail`);
const mBuyerPhone = document.getElementById(`mBuyerPhone`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/** delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var buyerId = 0;

async function Initialize() {
    await BuyerGrid();
    await ReloadBuyerGrid();
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});
//Buyer GRID
async function BuyerGrid() {
    "use strict";
    $buyerGrid.jqGrid({
        colModel: [
            { name: "BuyerId", label: "", key: true, hidden: true },
            { name: "BuyerCode", width: 200, align: 'center', searchoptions: { sopt: ['cn'] } },
            { name: "BuyerName", label: "Name", width: 100, align: 'left', search: false },
            { name: "BuyerUrl", label: "Url", width: 100, align: 'center', search: false, hidden: true },
            { name: "BuyerEmail", label: "Email", width: 100, align: 'left', search: false },
            { name: "BuyerPhone", label: "Phone", width: 100, align: 'center', search: false },
            { name: "BuyerDescription", label: "Description", width: 100, align: 'center', search: false },
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
            $(`#gbox_buyerGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {

        },
        loadonce: true,
        caption: 'Buyer',
        loadComplete: function () {
            $(`#gbox_buyerGrid`).unblock();
            let ids = $buyerGrid.getDataIDs();
            for (let i of ids) {
                let row = $buyerGrid.getRowData(i);
                if (row.Active === "NO") {
                    $buyerGrid.setCell(i, 'BuyerCode', '', { 'background-color': '#ffcc99' }, '');
                    $buyerGrid.setCell(i, 'BuyerName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == buyerId) {
                buyerId = 0;
            }
            else {
                buyerId = parseInt(rowid);
            }
            let row = $buyerGrid.getRowData(rowid);
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
async function ReloadBuyerGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Buyer/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Buyer/Search?keyWord=${keyWord}`;
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
                        $buyerGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        buyerId = 0;
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
