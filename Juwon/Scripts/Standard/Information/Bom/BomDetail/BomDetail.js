const createBomDetailBtn = document.getElementById(`createBomDetailBtn`);
const modifyBomDetailBtn = document.getElementById(`modifyBomDetailBtn`);
const deleteBomDetailBtn = document.getElementById(`deleteBomDetailBtn`);
const showBomDetailDeleted = document.getElementById(`showBomDetailDeleted`);
const searchBomDetailInput = document.getElementById(`searchBomDetailInput`);
const searchBomDetailBtn = document.getElementById(`searchBomDetailBtn`);
const $bomDetailGrid = $(`#bomDetailGrid`);

//Create
const cPartId = document.getElementById(`cPartId`);
const cMaterialId = document.getElementById(`cMaterialId`);
const cWeight = document.getElementById(`cWeight`);
const cAmount = document.getElementById(`cAmount`);
const cBomDetailFile = document.getElementById(`cBomDetailFile`);
const btnConfirmToCreateBomDetail = document.getElementById(`btnConfirmToCreateBomDetail`);

//Modify
const mPartId = document.getElementById(`mPartId`);
const mMaterialId = document.getElementById(`mMaterialId`);
const mWeight = document.getElementById(`mWeight`);
const mAmount = document.getElementById(`mAmount`);
const mBomDetailFile = document.getElementById(`mBomDetailFile`);
const btnConfirmToModifyBomDetail = document.getElementById(`btnConfirmToModifyBomDetail`);

var PartList = [];
var MaterialList = [];
var bomDetailId = 0;

var cPartSelectedList;
var mPartSelectedList;
var cMaterialSelectedList;
var mMaterialSelectedList;

// BOMDETAIL GRID
async function BomDetailGrid() {
    "use strict";
    $bomDetailGrid.jqGrid({
        colModel: [
            { name: "BomDetailId", label: "", key: true, hidden: true, search: false },
            { name: "BomId", label: "", width: 100, align: 'center', search: false, hidden: true },
            { name: "PartId", label: "", width: 100, align: 'center', search: false, hidden: true },
            { name: "MaterialId", label: "", width: 100, align: 'center', search: false, hidden: true },
            { name: "BomDetailCode", label: "Code", width: 100, align: 'center', searchoptions: { sopt: ['cn'] } },

            { name: "Image", label: "File", width: 100, align: 'center', search: false },
            { name: "Weight", label: "Weight", width: 100, align: 'center', search: false },
            { name: "Amount", label: "Amount", width: 100, align: 'center', search: false },
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
        height: 200,
        cmTemplate: { resizable: false },
        beforeRequest: function () {
            $(`#gbox_bomDetailGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {

        },
        loadonce: true,
        caption: 'Bom Detail',
        loadComplete: function () {
            $(`#gbox_bomDetailGrid`).unblock();
            let ids = $bomDetailGrid.getDataIDs();
            for (let i of ids) {
                let row = $bomDetailGrid.getRowData(i);
                if (row.Active === "NO") {
                    $bomDetailGrid.setCell(i, 'BomDetailCode', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },

        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == bomDetailId) {
                bomDetailId = 0;
            }
            else {
                bomDetailId = parseInt(rowid);
            }
            let row = $bomDetailGrid.getRowData(rowid);
            if (row.Active === "NO") {
                deleteBomDetailBtn.innerHTML = `<i class="fa fa-toggle-on"></i>&nbsp;Active`;
            }
            else {
                deleteBomDetailBtn.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
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

//RELOAD BOMDETAIL GRID
async function ReloadBomDetailGrid() {
    if (bomId && bomId != 0) {
        return new Promise(resolve => {
            let keyWord = searchBomDetailInput.value == null ? "" : searchBomDetailInput.value;
            let requestUrl = ``;
            if (showBomDetailDeleted.checked) {
                requestUrl = `/Bom/SearchAllByBomId`;
            }
            else {
                requestUrl = `/Bom/SearchActiveByBomId`;
            }
            $.ajax({
                url: requestUrl,
                type: `GET`,
                data: {
                    'bomId': bomId,
                    'keyWord': keyWord
                }
            })
                .done(function (response) {
                    if (response) {
                        if (response.HttpResponseCode == 100) {
                            WarningAlert(response.ResponseMessage);
                        }
                        else {
                            $bomDetailGrid
                                .clearGridData()
                                .setGridParam({
                                    data: response.Data === null ? "" : response.Data,
                                    datatype: 'local',
                                }).trigger(`reloadGrid`);
                            bomDetailId = 0;
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
    else {
        return new Promise(resolve => {
            //WarningAlert(response.ResponseMessage);
            resolve(false);
        })
    }

}

async function GetPart() {
    $.ajax({
        url: `/Bom/GetPart`,
    })
        .done(function (response) {
            PartList = [];
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.PartName < b.PartName) return -1
                    return a.PartName > b.PartName ? 1 : 0
                });

                $.each(response.Data, function (key, item) {
                    //html += `<option  value="${item.PartId}">${item.PartName}</option>`;
                    PartList.push(item);
                });
                //$(`#cPartId`).html(html);
                //$(`#mPartId`).html(html);
            }
        });
}

async function GetMaterial() {
    $.ajax({
        url: `/Bom/GetMaterial`,
    })
        .done(function (response) {
            MaterialList = [];
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.MaterialName < b.MaterialName) return -1
                    return a.MaterialName > b.MaterialName ? 1 : 0
                });

                $.each(response.Data, function (key, item) {
                    //html += `<option  value="${item.MaterialId}">${item.MaterialName}</option>`;
                    MaterialList.push(item);
                });
                //$(`#cMaterialId`).html(html);
                //$(`#mMaterialId`).html(html);
            }
        });
}