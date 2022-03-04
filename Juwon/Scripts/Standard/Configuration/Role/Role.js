var $grid = $(`#roleGrid`);
var $menuGrid = $(`#menuGrid`);
var $gbmenuGrid = $(`#gbox_menuGrid`);
var $permissionGrid = $(`#permissionGrid`);
var $gbpermissionGrid = $(`#gbox_permissionGrid`);
var roleID;

// MODIFY, BLOCK, DELETE BUTTONS
function ShowModifyBtnGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-warning button-srh Permission_modifyRole" data-id="${rowObject.ID}" onclick="Modify($(this))" style="cursor: pointer;"><i class="fa fa-edit" aria-hidden="true"></i>&nbsp;${ml_BtnModify}</button>`;
}

//DROP DOWN LIST
function GetRoleCategory() {
    $.ajax({
        url: `/Role/GetRoleCategory`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                $.each(response.Data, function (key, item) {
                    if (item.Code != `0001`) {
                        html += `<option value="${item.Code}">${item.Name}</option>`;
                    }
                });
                $(`#createRoleCategory`).html(html);
            }

        });
}

//SEARCH ROLE
$(`#searchBtn`).on(`click`, function () {
    let keyWord = $(`#searchInput`).val() == null ? `` : $(`#searchInput`).val().trim();

    $.ajax({
        url: `/Role/Search?s=${keyWord}`,
        type: `GET`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $grid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
            }
            else {
                $grid.jqGrid('clearGridData');
                WarningAlert(response.ResponseMessage);
            }
            return;
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});

//SEARCH MENU
$(`#searchMenuBtn`).on(`click`, function () {
    let keyWord = $(`#searchMenuInput`).val() === null ? `` : $(`#searchMenuInput`).val().trim();

    $.ajax({
        url: `/Menu/Search?s=${keyWord}`,
        type: `GET`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {
            if (response.result.length > 0) {
                $menuGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.result }).trigger("reloadGrid");
                //SuccessAlert(response.message);
            }
            else {
                $menuGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.result }).trigger("reloadGrid");
                WarningAlert(`ERROR_NotFound`);
            }
            return;
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});

//MODIFY MENU FOR ROLE
$(`#modifyRoleMenuBtn`).on(`click`, function () {
    let menuIDs = $menuGrid.jqGrid('getGridParam', 'selarrrow');
    if (!menuIDs || !roleID) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return;
    }
    else {
        let roleMenuModel = {};
        roleMenuModel.RoleID = roleID;
        roleMenuModel.MenuIDs = menuIDs;

        $.ajax({
            url: `/Role/AuthorizeMenus`,
            type: `POST`,
            data: JSON.stringify(roleMenuModel),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.flag) {
                    $grid.setSelection(roleID, true);
                    //$menuGrid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                }
                else {
                    ErrorAlert(response.message);
                }
                return;
            })

            .fail(function () {
                ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
                return;
            });
    }
});

//SEARCH PERMISSION
$(`#searchPermissionBtn`).on(`click`, function () {
    let keyWord = $(`#searchPermissionInput`).val() == null ? `` : $(`#searchPermissionInput`).val().trim();

    $.ajax({
        url: `/Permission/Search?s=${keyWord}`,
        type: `GET`,
        //data: keyWord,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {
            if (response.Data.length > 0) {
                $permissionGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
                //SuccessAlert(response.message);
            }
            else {
                $permissionGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
                WarningAlert(`ERROR_NotFound`);
            }
            return;
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});

//MODIFY PERMISSION FOR ROLE
$(`#modifyRolePermissionBtn`).on(`click`, function () {
    let permissionIDs = $permissionGrid.jqGrid('getGridParam', 'selarrrow');
    if (!permissionIDs || !roleID) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return;
    }
    else {
        let roleMenuModel = {};
        roleMenuModel.RoleID = roleID;
        roleMenuModel.PermissionIDs = permissionIDs;

        $.ajax({
            url: `/Role/AuthorizePermissions`,
            type: `POST`,
            data: JSON.stringify(roleMenuModel),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response)
            {
                if (response.flag)
                {
                    $grid.setSelection(roleID, true);
                    //$menuGrid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                }
                else
                {
                    ErrorAlert(response.message);
                }
                return;
            })

            .fail(function ()
            {
                ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
                return;
            });
    }
});

// GRID
function Grid() {
    "use strict";
    $grid.jqGrid({
        url: `/Role/GetAll`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "RoleCategory", hidden: true },
            //{
            //    name: "modifyBtn", width: 100, align: "center", label: "", resizable: false, title: false,
            //    formatter: ShowModifyBtnGrid
            //},
            //{
            //    name: "deleteBtn", width: 100, align: "center", label: "", resizable: false, title: false,
            //    formatter: ShowDeleteBtnGrid
            //},
            { name: "Name", width: 150 },

            { name: "CategoryName", width: 150 },
            { name: "Description", width: 250 },
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
        //sortname: "Name",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        //toppager: true,
        pager: true,
        rowNum: 4,
        viewrecords: true,
        //width: null,
        autowidth: true,
        shrinkToFit: false,
        searching: {
            defaultSearch: "Id"
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
        height: "auto",
        maxHeight: 135,
        loadComplete: function () {
            CheckPermissions();
            let rowIds = $grid.getDataIDs();
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            let selRowId = $grid.getGridParam(`selrow`);
            if (selRowId) {
                let rowData = $grid.getRowData(selRowId);
                roleID = rowData.ID;

                //$gbmenuGrid.block({
                //    message: '<h1><img src="../../../Img/loading/Preloader_6.gif" /></h1>',
                //    css: { border: '3px solid #a00' }
                //});
                $(`#gbox_menuGrid`).block({
                    message: '<img src="../../../Img/loading/hourglass.gif" />'
                });
                GetMenuByRoleID(roleID);
                $menuGrid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');

                $(`#gbox_permissionGrid`).block({
                    message: '<img src="../../../Img/loading/hourglass.gif" />'
                });
                GetPermissionByRoleID(roleID);
                $permissionGrid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
            }
            else {
                //GetMenuByRoleID(0);
                //GetPermissionByRoleID(0);
            }
        }
    });
};

// MENU GRID
function MenuGrid() {
    "use strict";
    $menuGrid.jqGrid({
        url: `/Menu/GetAll`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "Code", width: 120, align: 'center' },
            { name: "Name", width: 150 },
            { name: "FullName", width: 250 },
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
        rowNum: 1000,
        rowList: [1000],
        viewrecords: true,
        height: `auto`,
        maxHeight: 430,
        autowidth: true,
        shrinkToFit: false,
        multiselect: true,
        searching: {
            defaultSearch: "Id"
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
        caption: 'Menu',
        loadComplete: function () {
            let rowIds = $menuGrid.getDataIDs();

            if (listMenuID) {
                for (let i = 0; i < listMenuID.length; i++) {
                    if (rowIds.indexOf(listMenuID[i]) > -1) {
                        $menuGrid.setSelection(listMenuID[i], true);
                    }
                    else {
                        $menuGrid.setSelection(listMenuID[i], false);
                    }
                }
            }
            $(`#gbox_menuGrid`).unblock();
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {

        }
    });
};

var listMenuID;
function GetMenuByRoleID(e) {
    $.ajax({
        url: `/Role/GetMenuByRoleID?id=${e}`,
        type: `GET`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        //cache: false,
        async: false
    })
        .done(function (response) {
            listMenuID = [];
            for (let i of response) {
                listMenuID.push(i);
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
        });
    return;
};

// PERMISSION GRID
function PermissionGrid() {
    "use strict";
    $permissionGrid.jqGrid({
        url: `/Permission/GetAll`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "PermissionCategory", width: 200, hidden: true },
            { name: "Name", width: 250 },
            { name: "CategoryName", width: 200 },
            { name: "Description", width: 200 },
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
        sortname: "Name",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        //toppager: true,
        pager: true,
        rowNum: 1000,
        rowList: [1000],
        viewrecords: true,
        height: `auto`,
        maxHeight: 430,
        autowidth: true,
        shrinkToFit: false,
        multiselect: true,
        searching: {
            defaultSearch: "Id"
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
        caption: 'Permission',
        loadComplete: function () {
            //for (let i of listPermissionID) {
            //    $permissionGrid.setSelection(i, true);
            //}
            let rowIds = $permissionGrid.getDataIDs();
            if (listPermissionID) {
                for (let i = 0; i < listPermissionID.length; i++) {
                    if (rowIds.indexOf(listPermissionID[i]) > -1) {
                        $permissionGrid.setSelection(listPermissionID[i], true);
                    }
                    else {
                        $permissionGrid.setSelection(listPermissionID[i], false);
                    }
                }
            }
            $(`#gbox_permissionGrid`).unblock();
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
        }
    });
};

var listPermissionID;
function GetPermissionByRoleID(e) {
    $.ajax({
        url: `/Role/GetPermissionByRoleID?id=${e}`,
        type: `GET`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        //cache: false,
        async: false
    })
        .done(function (response) {
            listPermissionID = [];
            for (let i of response) {
                listPermissionID.push(i);
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
        });
    return;
};

// DOCUMENT READY
$(document).ready(function () {
    Grid();
    MenuGrid();
    PermissionGrid();
    GetRoleCategory();
});