//MODIFY
function Modify(e) {
    userID = e.attr(`data-id`);

    let rowData = $grid.getRowData(userID);
    $(`#modifyUsername`).val(rowData.UserName);
    $(`#modifyName`).val(rowData.Name);
    $(`#modifyEmail`).val(rowData.Email);
    $(`#modifyPhone`).val(rowData.Phone);
    $(`#modifyAddress`).val(rowData.Address);
    $(`#modifyProdLine`).val(rowData.LocationID);
    if (rowData.Active == `true`) {
        $(`#modifyActive`).val(`1`);
    }
    $(`#dialogModify`).dialog(`open`);
}
$(`#dialogModify`).dialog({
    //width: 550,
    //height: 640,
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
        $(`#modifyPassword`).val(``);
    },
});
$(`#btnCancelModify`).on(`click`, function () {
    $(`#dialogModify`).dialog(`close`);
});
$(`#btnModify`).on(`click`, function () {
    let userName = $(`#modifyUsername`).val() === null ? `` : $(`#modifyUsername`).val().trim();
    let pass = $(`#modifyPassword`).val() === null ? `` : $(`#modifyPassword`).val().trim();
    let name = $(`#modifyName`).val() === null ? `` : $(`#modifyName`).val().trim();
    let email = $(`#modifyEmail`).val() === null ? `` : $(`#modifyEmail`).val().trim();
    let phone = $(`#modifyPhone`).val() === null ? `` : $(`#modifyPhone`).val().trim();
    let address = $(`#modifyAddress`).val() === null ? `` : $(`#modifyAddress`).val().trim();
    let locationID = $(`#modifyProdLine`).val() === null ? `0` : $(`#modifyProdLine`).val().trim();
    let active = $(`#modifyActive`).val() == `1` ? true : false;

    if (name == `` || userName == ``) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return;
    }
    else {
        let user = new Object();
        user.ID = userID;
        user.UserName = userName;
        user.Password = pass;
        user.Name = name;
        user.Email = email;
        user.Phone = phone;
        user.Address = address;
        user.Active = active;
        user.LocationID = locationID;

        $.ajax({
            url: `/User/Modify`,
            type: `PUT`,
            data: JSON.stringify(user),
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

//CREATE
$(`#openCreateDialogBtn`).on(`click`, function () {
    $(`#dialogCreate`).dialog(`open`);
});

$(`#dialogCreate`).dialog({
    //width: 550,
    //height: 580,
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
        $(`#createPassword`).val(``);
        $(`#createUsername`).val(``);
    },
});
$(`#btnCancelCreate`).on(`click`, function () {
    $(`#dialogCreate`).dialog(`close`);
});
$(`#btnCreate`).on(`click`, function () {
    let userName = $(`#createUsername`).val() === null ? `` : $(`#createUsername`).val().trim();
    let pass = $(`#createPassword`).val() === null ? `` : $(`#createPassword`).val().trim();
    let name = $(`#createName`).val() === null ? `` : $(`#createName`).val().trim();
    let email = $(`#createEmail`).val() === null ? `` : $(`#createEmail`).val().trim();
    let phone = $(`#createPhone`).val() === null ? `` : $(`#createPhone`).val().trim();
    let address = $(`#createAddress`).val() === null ? `` : $(`#createAddress`).val().trim();
    let line = $(`#createProdLine`).val() === null ? `0` : $(`#createProdLine`).val().trim();
    if (!userName || !pass) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return;
    }
    else {
        let user = new Object();
        user.ID = userID;
        user.UserName = userName;
        user.Password = pass;
        user.Name = name;
        user.Email = email;
        user.Phone = phone;
        user.Address = address;
        user.LocationID = line;

        $.ajax({
            url: `/User/Create`,
            type: `POST`,
            data: JSON.stringify(user),
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
});

