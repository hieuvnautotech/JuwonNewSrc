var $grid = $(`#permissionGrid`);

// MODIFY, BLOCK, DELETE BUTTONS
function ShowModifyBtnGrid(cellvalue, options, rowObject)
{
    return `<button class="btn btn-sm btn-warning button-srh Permission_modifyPermission" data-id="${rowObject.ID}" onclick="ModifyPermission($(this))" style="cursor: pointer;"><i class="fa fa-edit" aria-hidden="true"></i>&nbsp;${ml_BtnModify}</button>`;
}
function ShowDeleteBtnGrid(cellvalue, options, rowObject)
{
    return `<button class="btn btn-sm btn-danger button-srh Permission_deletePermission" data-id="${rowObject.ID}" onclick="DeletePermission($(this))" style="cursor: pointer;"><i class="fa fa-trash" aria-hidden="true"></i>&nbsp;${ml_BtnDelete}</button>`;
}
//function ShowBlockBtnGrid(cellvalue, options, rowObject)
//{
//    return `<button class="btn btn-sm btn-warning button-srh Permission_blockPermission" data-id="${rowObject.ID}" onclick="BlockPermission($(this))" style="cursor: pointer;"><i class="fa fa-ban" aria-hidden="true"></i>&nbsp;${ml_BtnBlock}</button>`;
//}

//SEARCH
$(`#searchBtn`).on(`click`, function ()
{
    let keyWord = $(`#searchInput`).val() == null ? `` : $(`#searchInput`).val().trim();

    $.ajax({
        url: `/Permission/Search?s=${keyWord}`,
        type: `GET`,
        //data: keyWord,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response)
        {
            if (response.Data && response.HttpResponseCode != 100)
            {
                $grid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
                SuccessAlert(response.ResponseMessage);
            }
            else
            {
                //$grid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
                WarningAlert(response.ResponseMessage);
            }
            return;
        })

        .fail(function ()
        {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});
$(`#searchInput`).on(`keypress`, function (e)
{
    if (e.which == 13)
    {
        $(`#searchBtn`).trigger(`click`);
    }
});

//DROP DOWN LIST
function GetPermissionCategory()
{
    $.ajax({
        //url: `/Common/GetCommonDetailByMasterCode?masterCode=0003`,
        url: `/Permission/GetPermissions`,
    })
        .done(function (response)
        {
            let html = ``;
            if (response.Data)
            {
                $.each(response.Data, function (key, item)
                {
                    html += `<option value="${item.Code}">${item.Name}</option>`;
                });
                $(`#createPermissionCategory`).html(html);
                $(`#modifyPermissionCategory`).html(html);
            }

        });
}

// GRID
function ShowGrid()
{
    "use strict";
    $grid.jqGrid({
        url: `/Permission/GetAll`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "PermissionCategory", width: 200, hidden: true },
            {
                name: "modifyBtn", width: 100, align: "center", label: "", resizable: false, title: false,
                formatter: ShowModifyBtnGrid
            },
            {
                name: "deleteBtn", width: 100, align: "center", label: "", resizable: false, title: false,
                formatter: ShowDeleteBtnGrid
            },
            { name: "Name", width: 250 },
            { name: "CategoryName", width: 200 },
            { name: "Description", width: 200 },
            { name: "Active", width: 100 },
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
        sortname: "Name",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        //toppager: true,
        pager: true,
        rowNum: 21,
        viewrecords: true,
        //width: null,
        autowidth: true,
        shrinkToFit: false,
        searching: {
            defaultSearch: "Id"
        },
        beforeRequest: function ()
        {
            $(`#gbox_permissionGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data)
        {
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
        height: "auto",
        maxHeight: 660,
        caption: 'Permission',
        loadComplete: function ()
        {

            $(`#gbox_permissionGrid`).unblock();
            //CheckPermissions();

            //let rowIds = $grid.getDataIDs();
            //let rowData = $grid.getRowData(rowIds[0]);
            //if (rowData.modifyBtn == ``)
            //{
            //    $grid.hideCol("modifyBtn");
            //}
            //if (rowData.blockBtn == ``)
            //{
            //    $grid.hideCol("blockBtn");
            //}
            //if (rowData.deleteBtn == ``)
            //{
            //    $grid.hideCol("deleteBtn");
            //}
        },
        onSelectRow: function (rowid, status, e, iRow, iCol)
        {
            let selRowId = $grid.getGridParam(`selrow`);
            let rowData = $grid.getRowData(selRowId);
        }
    });
}

// DOCUMENT READY
$(document).ready(function ()
{
    ShowGrid();
    GetPermissionCategory();
});