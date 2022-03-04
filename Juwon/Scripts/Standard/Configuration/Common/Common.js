var $masterGrid = $(`#commonMasterGrid`);
var $detailGrid = $(`#commonDetailGrid`);
var MasterCode = null;

//function SetCaptionCommonMasterGrid()
//{
//    let $grid = $(`#commonMasterGrid`);
//    $grid.jqGrid('setCaption', 'newCaption');
//}
//------------------------------------------------------------------------------------------------//
//DROP DOWN LIST
function GetCommonMaster() {
    $.ajax({
        url: `/Common/GetCommonMasterList`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.Code}">${item.Name}</option>`;
                });
                $(`#createCommonDetailMasterCode`).html(html);
                //$(`#modifyCommonDetailMasterCode`).html(html);
            }

        });
}

// MASTER GRID
function ShowModifyBtnMasterGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-warning button-srh Permission_modifyCommonMaster" data-id="${rowObject.ID}" onclick="ModifyCommonMaster($(this))" style="cursor: pointer;"><i class="fa fa-edit" aria-hidden="true"></i>&nbsp;${ml_BtnModify}</button>`;
}
function ShowDeleteBtnMasterGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-danger button-srh Permission_deleteCommonMaster" data-id="${rowObject.ID}" onclick="DeleteCommonMaster($(this))" style="cursor: pointer;"><i class="fa fa-trash" aria-hidden="true"></i>&nbsp;${ml_BtnDelete}</button>`;
}
//SEARCH
$(`#searchCommonMaster`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchCommonMasterBtn`).trigger(`click`);
    }
});
$(`#searchCommonMasterBtn`).on(`click`, function () {
    let keyWord = $(`#searchCommonMaster`).val() == null ? `` : $(`#searchCommonMaster`).val().trim();

    $.ajax({
        url: `/Common/SearchCommonMaster?s=${keyWord}`,
        type: `GET`,
        //data: keyWord,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {

            if (response.Data != null && response.Data.length > 0) {
                $masterGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
            }
            else {
                $masterGrid.jqGrid('clearGridData');
                WarningAlert(`ERROR_NotFound`);
            }

            return;
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});
function ShowCommonMasterGrid() {
    "use strict";
    $masterGrid.jqGrid({
        url: `/Common/GetCommonMasterList`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            {
                name: "modifyBtn", width: 150, align: "center", label: "", resizable: false, title: false,
                formatter: ShowModifyBtnMasterGrid
            },
            //{
            //    name: "deleteBtn", width: 150, align: "center", label: "", resizable: false, title: false,
            //    formatter: ShowDeleteBtnMasterGrid
            //}
            { name: "Code", width: 200, align: 'center' },
            { name: "Name", width: 200 },
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
        //idPrefix: "ug_",
        rownumbers: true,
        sortname: "Code",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        //toppager: true,
        pager: true,
        rowNum: 7,
        viewrecords: true,
        //width: null,
        //width: "100%",
        //autowidth: true,
        shrinkToFit: false,
        height: `auto`,
        maxHeight: 223,
        searching: {
            defaultSearch: "Id"
        },
        beforeRequest: function () {
            $(`#gbox_commonMasterGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
            //var model = data.multiLangModel, name, $colHeader, $sortingIcons;
            //if (model) {
            //    for (name in model) {
            //        if (model.hasOwnProperty(name)) {
            //            $colHeader = $("#jqgh_" + $.jgrid.jqID(this.id + "_" + name));
            //            $sortingIcons = $colHeader.find(">span.s-ico");
            //            $colHeader.text(model[name].label);
            //            $colHeader.append($sortingIcons);
            //        }
            //    }
            //}
        },
        loadonce: true,

        caption: 'Master',
        loadComplete: function () {
            $(`#gbox_commonMasterGrid`).unblock();
            //CheckPermissions();

            let rowIds = $masterGrid.getDataIDs();
            let rowData = $masterGrid.getRowData(rowIds[0]);
            if (rowData.modifyBtn == ``) {
                $masterGrid.hideCol("modifyBtn");
            }
            //if (rowData.deleteBtn == ``)
            //{
            //    $masterGrid.hideCol("deleteBtn");
            //}
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            let selRowId = $masterGrid.getGridParam(`selrow`);
            let rowData = $masterGrid.getRowData(selRowId);
            MasterCode = rowData.Code;

            $detailGrid.setGridParam({
                url: `/Common/GetCommonDetailByMasterCode?masterCode=${MasterCode}`,
                datatype: "json",
                page: 1
            }).trigger("reloadGrid");

        }
    });
}

// DETAIL GRID
function ShowModifyBtnDetailGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-warning button-srh Permission_modifyCommonMaster" data-id="${rowObject.ID}" onclick="ModifyCommonDetail($(this))" style="cursor: pointer;"><i class="fa fa-edit" aria-hidden="true"></i>&nbsp;${ml_BtnModify}</button>`;
}
function ShowDeleteBtnDetailGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-danger button-srh Permission_deleteCommonMaster" data-id="${rowObject.ID}" onclick="DeleteCommonDetail($(this))" style="cursor: pointer;"><i class="fa fa-trash" aria-hidden="true"></i>&nbsp;${ml_BtnDelete}</button>`;
}

// SEARCH DETAIL
$(`#searchCommonDetail`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchCommonDetailBtn`).trigger(`click`);
    }
});
$(`#searchCommonDetailBtn`).on(`click`, function () {
    let keyWord = $(`#searchCommonDetail`).val() == null ? `` : $(`#searchCommonDetail`).val().trim();

    $.ajax({
        url: `/Common/SearchCommonDetail?s=${keyWord}&MasterCode=${MasterCode}`,
        type: `GET`,
        //data: keyWord,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {
            //if (response.result.length > 0) {
            //    $detailGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.result }).trigger("reloadGrid");
            //    //SuccessAlert(response.message);
            //}
            //else {
            //    $detailGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.result }).trigger("reloadGrid");
            //    WarningAlert(`ERROR_NotFound`);
            //}
            if (response.Data != null && response.Data.length > 0) {
                $detailGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
            }
            else {
                $detailGrid.jqGrid('clearGridData');
                WarningAlert(`ERROR_NotFound`);
            }
            return;
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});
function ShowCommonDetailGrid() {
    "use strict";
    $detailGrid.jqGrid({
        url: `/Common/GetCommonDetailList`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "MasterCode", width: 200, align: 'center', hidden: true },
            {
                name: "modifyBtn", width: 150, align: "center", label: "", resizable: false, title: false,
                formatter: ShowModifyBtnDetailGrid
            },
            //{
            //    name: "deleteBtn", width: 150, align: "center", label: "", resizable: false, title: false,
            //    formatter: ShowDeleteBtnDetailGrid
            //}
            { name: "Code", width: 200, align: 'center' },
            { name: "Name", width: 200 },
            { name: "MasterName", width: 200 },
            { name: "Active", width: 100, align: 'center' },
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
        //idPrefix: "ug_",
        rownumbers: true,
        sortname: "MasterCode",
        sortorder: "desc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        //toppager: true,
        pager: true,
        rowNum: 9,
        viewrecords: true,
        //width: null,
        //width: "100%",
        //autowidth: true,
        height: `auto`,
        maxHeight: 280,
        shrinkToFit: false,

        searching: {
            defaultSearch: "Id"
        },
        beforeRequest: function () {
            $(`#gbox_commonDetailGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
            //var model = data.multiLangModel, name, $colHeader, $sortingIcons;
            //if (model) {
            //    for (name in model) {
            //        if (model.hasOwnProperty(name)) {
            //            $colHeader = $("#jqgh_" + $.jgrid.jqID(this.id + "_" + name));
            //            $sortingIcons = $colHeader.find(">span.s-ico");
            //            $colHeader.text(model[name].label);
            //            $colHeader.append($sortingIcons);
            //        }
            //    }
            //}
        },
        loadonce: true,

        caption: 'Detail',
        loadComplete: function () {
            $(`#gbox_commonDetailGrid`).unblock();
            //CheckPermissions();

            let rowIds = $detailGrid.getDataIDs();
            let rowData = $detailGrid.getRowData(rowIds[0]);
            if (rowData.modifyBtn == ``) {
                $detailGrid.hideCol("modifyBtn");
            }
            //if (rowData.deleteBtn == ``) {
            //    $detailGrid.hideCol("deleteBtn");
            //}
            //CheckPermissions();
        }

    });
}

// DOCUMENT READY
$(document).ready(function () {
    ShowCommonMasterGrid();
    ShowCommonDetailGrid();
    GetCommonMaster();
});
