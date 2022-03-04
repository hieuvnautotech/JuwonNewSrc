
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
    let roleCategoryCode = $(`#createRoleCategory`).val() === null ? `` : $(`#createRoleCategory`).val().trim();
    let des = $(`#createDescription`).val() === null ? `` : $(`#createDescription`).val().trim();
    if (name == `` || roleCategoryCode == `` || !roleCategoryCode) {
        WarningAlert(`ERROR_FullFillTheForm`);
    }
    else {

        var role = new Object();
        role.Name = name;
        role.RoleCategory = roleCategoryCode;
        role.Description = des;

        $.ajax({
            url: `/Role/Create`,
            type: `POST`,
            data: JSON.stringify(role),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.flag) {
                    $grid.addRowData(response.result.Data.ID, response.result.Data, "first");
                    $grid.jqGrid('setRowData', response.result.Data.ID, false, { background: '#39FF14' });
                    $('#dialogCreate').dialog('close');
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
    return;
});
