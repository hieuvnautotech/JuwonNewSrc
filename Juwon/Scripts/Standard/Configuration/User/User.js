var $grid = $(`#userGrid`);
var $roleGrid = $(`#roleGrid`);
var userID;

// MODIFY, BLOCK, DELETE BUTTONS
function ShowModifyBtnGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-warning button-srh Permission_modifyUser" data-id="${rowObject.ID}" onclick="Modify($(this))" style="cursor: pointer;"><i class="fa fa-edit" aria-hidden="true"></i>&nbsp;${ml_BtnModify}</button>`;
}

//SEARCH USER
$(`#searchBtn`).on(`click`, function () {
    let keyWord = $(`#searchInput`).val() == null ? `` : $(`#searchInput`).val().trim();

    $.ajax({
        url: `/User/Search?s=${keyWord}`,
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
$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

// GRID
function Grid() {
    "use strict";
    $grid.jqGrid({
        url: `/User/GetAll`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "LocationID", width: 250, hidden: true },
            {
                name: "modifyBtn", width: 100, align: "center", label: "", resizable: false, title: false,
                formatter: ShowModifyBtnGrid
            },
            { name: "UserName", width: 100 },
            { name: "Name", width: 150 },
            { name: "Email", width: 150 },
            { name: "Phone", width: 100 },
            { name: "Address", width: 250 },
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
        rowNum: 10,
        viewrecords: true,
        //width: null,
        autowidth: true,
        shrinkToFit: false,
        searching: {
            defaultSearch: "Id"
        },
        beforeRequest: function () {
            $(`#gbox_userGrid`).block({
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
        height: "auto",
        maxHeight: 265,
        loadComplete: function () {
            $(`#gbox_userGrid`).unblock();
            //CheckPermissions();
            let rowIds = $grid.getDataIDs();
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            let selRowId = $grid.getGridParam(`selrow`);
            if (selRowId) {
                let rowData = $grid.getRowData(selRowId);
                userID = rowData.ID;

                GetRoleByUserID(userID);
                $roleGrid.jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
            }

        }
    });
};

//SEARCH ROLE
$(`#searchRoleBtn`).on(`click`, function () {
    let keyWord = $(`#searchRoleInput`).val() == null ? `` : $(`#searchRoleInput`).val().trim();

    $.ajax({
        url: `/User/SearchRole?s=${keyWord}`,
        type: `GET`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {
            if (response.Data.length > 0 && response.HttpResponseCode != 100) {
                $roleGrid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
            }
            else {
                $roleGrid.jqGrid('clearGridData');
                WarningAlert(response.ResponseMessage);
            }
            return;
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});
$(`#searchRoleInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchRoleBtn`).trigger(`click`);
    }
});

// MENU GRID
function RoleGrid() {
    "use strict";
    $roleGrid.jqGrid({
        url: `/User/GetAllRoles`,
        mtype: `GET`,
        datatype: `json`,
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            
            { name: "RoleCategory", hidden: true },

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
        rowNum: 1000,
        viewrecords: true,
        //width: null,
        autowidth: true,
        shrinkToFit: false,
        multiselect: true,
        searching: {
            defaultSearch: "Id"
        },
        beforeRequest: function () {
            $(`#gbox_roleGrid`).block({
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
        height: `auto`,
        maxHeight: 300,
        loadComplete: function () {
            $(`#gbox_roleGrid`).unblock();

            let rowIds = $roleGrid.getDataIDs();

            if (listRoleID) {
                for (let i = 0; i < listRoleID.length; i++) {
                    if (rowIds.indexOf(listRoleID[i]) > -1) {
                        $roleGrid.setSelection(listRoleID[i], true);
                    }
                    else {
                        $roleGrid.setSelection(listRoleID[i], false);
                    }
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {

        }
    });
};

var listRoleID;
function GetRoleByUserID(e) {
    $.ajax({
        url: `/User/GetRoleIDByUserID?id=${e}`,
        type: `GET`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        //cache: false,
        async: false
    })
        .done(function (response) {
            listRoleID = [];
            for (let i of response) {
                listRoleID.push(i);
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
        });
    return;
};

//MODIFY ROLE FOR USER
$(`#modifyRoleUserBtn`).on(`click`, function () {
    let roleIDs = $roleGrid.jqGrid('getGridParam', 'selarrrow');
    if (!roleIDs || !userID) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return;
    }
    else {
        let authorizeModel = {};
        authorizeModel.ID = userID;
        authorizeModel.AuthorizeIDs = (roleIDs.length == 0) ? '' : roleIDs;

        $.ajax({
            url: `/User/AuthorizeRoles`,
            type: `POST`,
            data: JSON.stringify(authorizeModel),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.flag) {
                    $grid.setSelection(userID, true);
                    SuccessAlert(response.message);
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

// DOCUMENT READY
$(document).ready(function () {
    Grid();
    RoleGrid();
});