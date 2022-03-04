var $grid = $(`#permissionGrid`);
var id;
// CREATE
$(`#openCreateDialogBtn`).on(`click`, function () {
    $('#dialogCreate').dialog('open');
});
$(`#dialogCreate`).dialog({
    //width: 550,
    //height: 400,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "550px");
        //$(this).css("maxheight", "600px");
    },

    classes: {
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    open: function (event, ui) {
    },
    close: function (event, ui) {
        $(`#createName`).val(``);
    },
});
$(`#btnCancelCreate`).on(`click`, function () {
    $('#dialogCreate').dialog('close');
});
$(`#btnCreate`).on(`click`, function () {
    let name = $(`#createName`).val() === null ? `` : $(`#createName`).val().trim();
    let perCategoryCode = $(`#createPermissionCategory`).val() === null ? `` : $(`#createPermissionCategory`).val().trim();
    let des = $(`#createDescription`).val() === null ? `` : $(`#createDescription`).val().trim();
    if (name == `` || perCategoryCode == `` || !perCategoryCode) {
        WarningAlert(`ERROR_FullFillTheForm`);
    }
    else {

        var permission = new Object();
        permission.Name = name;
        permission.PermissionCategory = perCategoryCode;
        permission.Description = des;

        $.ajax({
            url: `/Permission/Create`,
            type: `POST`,
            data: JSON.stringify(permission),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.Data && response.HttpResponseCode != 100) {
                    $grid.addRowData(response.Data.ID, response.Data, "first");
                    $grid.jqGrid('setRowData', response.Data.ID, false, { background: '#39FF14' });
                    $('#dialogCreate').dialog('close');
                    SuccessAlert(response.ResponseMessage);
                }
                else {
                    ErrorAlert(response.ResponseMessage);
                }
                return;
            })

            .fail(function () {
                ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
                return;
            });
    }
    return;
});

//MODIFY
function ModifyPermission(e) {
    id = e.attr(`data-id`);

    let rowData = $grid.getRowData(id);
    $(`#modifyName`).val(rowData.Name);
    $(`#modifyPermissionCategory`).val(rowData.PermissionCategory);
    $(`#modifyDescription`).val(rowData.Description);
    if (rowData.Active == `true`) {
        $(`#modifyActive`).val(`1`);
    }
    $(`#dialogModify`).dialog(`open`);
}
$(`#dialogModify`).dialog({
    //width: 550,
    //height: 420,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "550px");
        //$(this).css("maxheight", "600px");
    },
    classes: {
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    open: function (event, ui) {
    },
    close: function (event, ui) {
    },
});
$(`#btnCancelModify`).on(`click`, function () {
    $(`#dialogModify`).dialog(`close`);
});
$(`#btnModify`).on(`click`, function () {
    let name = $(`#modifyName`).val() === null ? `` : $(`#modifyName`).val().trim();
    let permissionCategory = $(`#modifyPermissionCategory`).val() === null ? `` : $(`#modifyPermissionCategory`).val().trim();
    let description = $(`#modifyDescription`).val() === null ? `` : $(`#modifyDescription`).val().trim();
    let active = $(`#modifyActive`).val() == `1` ? true : false;

    if (name == `` || permissionCategory == ``) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return;
    }
    else {
        var permission = new Object();
        permission.ID = id;
        permission.Name = name;
        permission.PermissionCategory = permissionCategory;
        permission.Description = description;
        permission.Active = active;

        $.ajax({
            url: `/Permission/Modify`,
            type: `PUT`,
            data: JSON.stringify(permission),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.Data && response.HttpResponseCode != 100) {
                    $grid.setRowData(response.Data.ID, response.Data, { background: '#39FF14' });
                    $(`#dialogModify`).dialog('close');
                    SuccessAlert(response.ResponseMessage);
                }
                else {
                    ErrorAlert(response.ResponseMessage);
                }
                return;
            })

            .fail(function () {
                ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
                return;
            });
    }
});

//DELETE
function DeletePermission(e) {
    id = e.attr(`data-id`);
    let rowData = $grid.getRowData(id);
    $(`#deleteName`).val(rowData.Name);
    $(`#deletePermissionCategory`).val(rowData.PermissionCategory);
    $(`#deleteDescription`).val(rowData.Description);
    $(`#deleteActive`).val(rowData.Active);

    $(`#dialogDelete`).dialog(`open`);
}
$(`#dialogDelete`).dialog({
    width: 550,
    height: 420,
    resizable: false,
    fluid: true,
    modal: true,
    autoOpen: false,
    classes: {
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    open: function (event, ui) {
    },
    close: function (event, ui) {
    },
});
$(`#btnCancelDelete`).on(`click`, function () {
    $(`#dialogDelete`).dialog(`close`);
});
$(`#btnDelete`).on(`click`, function () {
    $.ajax({
        url: `/Permission/Delete/${id}`,
        type: `DELETE`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`
    })
        .done(function (response) {
            if (response.IsSuccess) {
                $grid.delRowData(id);
                $('#dialogDelete').dialog('close');
                SuccessAlert(response.ResponseMessage);
            }
            else {
                ErrorAlert(response.ResponseMessage);
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
        });

    return;
});