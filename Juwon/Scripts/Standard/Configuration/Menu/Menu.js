var $grid = $(`#menuGrid`);

// MODIFY, BLOCK, DELETE BUTTONS
function ShowModifyBtnGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-warning button-srh Permission_modifyMenu" data-id="${rowObject.ID}" onclick="Modify($(this))" style="cursor: pointer;"><i class="fa fa-edit" aria-hidden="true"></i>&nbsp;${ml_BtnModify}</button>`;
}
function ShowDeleteBtnGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-danger button-srh Permission_deleteMenu" data-id="${rowObject.ID}" onclick="Delete($(this))" style="cursor: pointer;"><i class="fa fa-trash" aria-hidden="true"></i>&nbsp;${ml_BtnDelete}</button>`;
}

// GRID
function Grid() {
    "use strict";
    $grid.jqGrid({
        url: `/Menu/GetAll`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "MenuCategory", hidden: true },
            { name: "MenuLevel", hidden: true },
            { name: "PrimaryMenu", hidden: true },
            { name: "MenuOrderly", hidden: true },
            { name: "SecondaryMenu", hidden: true },
            { name: "MenuLevel2Orderly", hidden: true },
            { name: "TertiaryMenu", hidden: true },
            { name: "MenuLevel3Orderly", hidden: true },
            {
                name: "modifyBtn", width: 100, align: "center", label: "", resizable: false, title: false,
                formatter: ShowModifyBtnGrid
            },
            //{
            //    name: "deleteBtn", width: 100, align: "center", label: "", resizable: false, title: false,
            //    formatter: ShowDeleteBtnGrid
            //},
            { name: "Code", width: 120, align: 'center' },
            { name: "Name", width: 150 },
            { name: "FullName", width: 250 },
            { name: "CategoryName", width: 150 },
            { name: "Link", width: 150 },
            { name: "Active", width: 100, align: 'center' },

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
        //idPrefix: "ug_",
        rownumbers: true,
        //sortname: "Name",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        //toppager: true,
        pager: true,
        rowNum: 21,
        viewrecords: true,

        autowidth: true,
        shrinkToFit: false,
        searching: {
            defaultSearch: "Id"
        },
        beforeRequest: function ()
        {
            $(`#gbox_menuGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
            var model = data.multiLangModel, name, $colHeader, $sortingIcons;
            if (model) {
                for (name in model) {
                    if (model.hasOwnProperty(name)) {
                        $colHeader = $("#jqgh_" + $.jgrid.jqID(this.id + "_" + name));
                        $sortingIcons = $colHeader.find(">span.s-ico");
                        $colHeader.text(model[name].label);
                        $colHeader.append($sortingIcons);
                    }
                }
            }
        },
        loadonce: true,
        height: "auto",
        maxHeight: 660,
        loadComplete: function ()
        {
            $(`#gbox_menuGrid`).unblock();
            CheckPermissions();

            let rowIds = $grid.getDataIDs();
            let rowData = $grid.getRowData(rowIds[0]);
            if (rowData.modifyBtn == ``) {
                $grid.hideCol("modifyBtn");
            }
            //if (rowData.blockBtn == ``) {
            //    $grid.hideCol("blockBtn");
            //}
            //if (rowData.deleteBtn == ``) {
            //    $grid.hideCol("deleteBtn");
            //}
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            let selRowId = $grid.getGridParam(`selrow`);
            let rowData = $grid.getRowData(selRowId);
        }
    });
}

//DROP DOWN LIST
function GetMenuCategory() {
    $.ajax({
        url: `/Menu/GetMenuCategory`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data && response.HttpResponseCode != 100) {
                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.Code}">${item.Name}</option>`;
                });
                $(`#createPrimaryMenu`).html(html);
                $(`#modifyPrimaryMenu`).html(html);
            }
        });
}

//SEARCH
$(`#searchBtn`).on(`click`, function ()
{
    let keyWord = $(`#searchInput`).val() === null ? `` : $(`#searchInput`).val().trim();

    $.ajax({
        url: `/Menu/Search?s=${keyWord}`,
        type: `GET`,
        //data: keyWord,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response)
        {
            if (response.length > 0)
            {
                $grid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response }).trigger("reloadGrid");
                //SuccessAlert(response.message);
            }
            else
            {
                $grid.jqGrid('clearGridData');
                WarningAlert(`ERROR_NotFound`);
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

// DOCUMENT READY
$(document).ready(function () {
    Grid();
    GetMenuCategory();
});