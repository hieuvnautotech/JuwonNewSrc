/* View */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $bomGrid = $(`#bomGrid`);

/* Create */
const cBomCode = document.getElementById(`cBomCode`);
const cMoldId = document.getElementById(`cMoldId`);
const cBomFile = document.getElementById(`cBomFile`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/* Modify */
const mBomCode = document.getElementById(`mBomCode`);
const mMoldId = document.getElementById(`mMoldId`);
const mBomFile = document.getElementById(`mBomFile`);
const mShowBomFile = document.getElementById(`mShowBomFile`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/* Delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var bomId = 0;
var createSelectedMoldId;
var modifySelectedMoldId;

async function Initialize() {
    await BomGrid();
    await ReloadBomGrid();
    await GetMold();
    await GetPart();
    await GetMaterial();

    createSelectedMoldId = new SlimSelect({
        select: '#cMoldId',
        placeholder: 'Select Mold',
        hideSelectedOption: true
    });
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});
async function GetMold() {
    $.ajax({
        url: `/Bom/GetMold`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.MoldCode < b.MoldCode) return -1
                    return a.MoldCode > b.MoldCode ? 1 : 0
                });

                $.each(response.Data, function (key, item) {
                    if (item.Active == true) {
                        html += `<option  value="${item.MoldId}">${item.MoldCode}</option>`;
                    }
                });
                $(`#cMoldId`).html(html);
                $(`#mMoldId`).html(html);
            }
        });
}
//Bom GRID
async function BomGrid() {
    "use strict";
    $bomGrid.jqGrid({
        colModel: [
            { name: "BomId", label: "", key: true, hidden: true },
            { name: "BomCode", label: "Code", width: 100, align: 'center', search: false, hidden: true },
            { name: "MoldId", label: "Mold", width: 100, align: 'center', search: false, hidden: true },
            { name: "MoldCode", label: "Mold", width: 100, align: 'center', searchoptions: { sopt: ['cn'] } },
            { name: "FileUpload", label: "File", width: 100, align: 'center', search: false },
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
            root: "Data",
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
        rowNum: 10,
        viewrecords: true,
        shrinkToFit: false,
        height: 250,
        cmTemplate: { resizable: false },
        beforeRequest: function () {
            $(`#gbox_bomGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {

        },
        loadonce: true,
        caption: 'Bom',
        loadComplete: function () {
            $(`#gbox_bomGrid`).unblock();
            let ids = $bomGrid.getDataIDs();
            for (let i of ids) {
                let row = $bomGrid.getRowData(i);
                if (row.Active === "NO") {
                    $bomGrid.setCell(i, 'BomCode', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },

        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == bomId) {
                bomId = 0;
            }
            else {
                bomId = parseInt(rowid);
            }
            let row = $bomGrid.getRowData(rowid);
            if (row.Active === "NO") {
                deleteBtn.innerHTML = `<i class="fa fa-toggle-on"></i>&nbsp;Active`;
            }
            else {
                deleteBtn.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
            }

            ReloadBomDetailGrid();
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
//RELOAD BOM GRID
async function ReloadBomGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Bom/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Bom/Search?keyWord=${keyWord}`;
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
                        $bomGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        bomId = 0;
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

