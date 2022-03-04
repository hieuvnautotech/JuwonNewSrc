var $masterGrid = $(`#commonMasterGrid`);
var $detailGrid = $(`#commonDetailGrid`);
var commonMasterID;
var commonDetailID;
//CREATE CommonMaster Popup
$(`#createCommonMasterBtn`).on(`click`, function () {
    $('#dialogCreateCommonMaster').dialog('open');
});
$(`#dialogCreateCommonMaster`).dialog({
    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "500px");
        //$(this).css("maxheight", "600px");
    },


    //width: 450,
    //height: 300,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    classes: {
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    open: function (event, ui) {
    },
    close: function (event, ui) {
        $(`#createCommonMasterCode`).val(``);
        $(`#createCommonMasterName`).val(``);
    },
});
$(`#btnCancelCreateCommonMaster`).on(`click`, function () {
    $('#dialogCreateCommonMaster').dialog('close');
});
$(`#btnCreateCommonMaster`).on(`click`, function () {
    let code = $(`#createCommonMasterCode`).val() === null ? `` : $(`#createCommonMasterCode`).val().trim();
    let name = $(`#createCommonMasterName`).val() === null ? `` : $(`#createCommonMasterName`).val().trim();

    if (code == `` || name == ``) {
        WarningAlert(`ERROR_FullFillTheForm`);
    }
    else {

        var commonMaster = new Object();
        commonMaster.Code = code;
        commonMaster.Name = name;

        $.ajax({
            url: `/Common/CreateCommonMaster`,
            type: `POST`,
            data: JSON.stringify(commonMaster),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.Data) {
                    $masterGrid.addRowData(response.Data.ID, response.Data, "first");
                    $masterGrid.jqGrid('setRowData', response.Data.ID, false, { background: '#39FF14' });
                    $('#dialogCreateCommonMaster').dialog('close');
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


//MODIFY CommonMaster Popup
function ModifyCommonMaster(e) {
    commonMasterID = e.attr(`data-id`);

    let rowData = $masterGrid.getRowData(commonMasterID);
    $(`#modifyCommonMasterCode`).val(rowData.Code);
    $(`#modifyCommonMasterName`).val(rowData.Name);
    $(`#dialogModifyCommonMaster`).dialog(`open`);
}
$(`#dialogModifyCommonMaster`).dialog({
    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "450px");
        //$(this).css("maxheight", "600px");
    },


    //width: 450,
    //height: 300,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    classes: {
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    open: function (event, ui) {
    },
    close: function (event, ui) {
        $(`#modifyCommonMasterCode`).val(``);
        $(`#modifyCommonMasterName`).val(``);
    },
});
$(`#btnCancelModifyCommonMaster`).on(`click`, function () {
    $('#dialogModifyCommonMaster').dialog('close');
});
$(`#btnModifyCommonMaster`).on(`click`, function () {
    let code = $(`#modifyCommonMasterCode`).val() === null ? `` : $(`#modifyCommonMasterCode`).val().trim();
    let name = $(`#modifyCommonMasterName`).val() === null ? `` : $(`#modifyCommonMasterName`).val().trim();

    if (code == `` || name == ``) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return;
    }
    else {
        var commonMaster = new Object();
        commonMaster.Code = code;
        commonMaster.Name = name;
        commonMaster.ID = commonMasterID;

        $.ajax({
            url: `/Common/ModifyCommonMaster`,
            type: `POST`,
            data: JSON.stringify(commonMaster),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.Data) {
                    $masterGrid.setRowData(response.Data.ID, response.Data, { background: '#39FF14' });
                    $(`#dialogModifyCommonMaster`).dialog('close');
                    $detailGrid.setGridParam({ url: `/Common/GetCommonDetailByMasterCode?masterCode=${response.Data.Code}`, datatype: "json" }).trigger("reloadGrid");
                    SuccessAlert(response.ResponseMessage);
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

//DELETE CommonMaster Popup
function DeleteCommonMaster(e) {
    commonMasterID = e.attr(`data-id`);

    var rowData = $masterGrid.getRowData(commonMasterID);
    $(`#deleteCommonMasterCode`).val(rowData.Code);
    $(`#deleteCommonMasterName`).val(rowData.Name);
    $(`#dialogDeleteCommonMaster`).dialog(`open`);
}
$(`#dialogDeleteCommonMaster`).dialog({
    width: 450,
    height: 300,
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
$(`#btnCancelDeleteCommonMaster`).on(`click`, function () {
    $('#dialogDeleteCommonMaster').dialog('close');
});
$(`#btnDeleteCommonMaster`).on(`click`, function () {
    $.ajax({
        url: `/Common/DeleteCommonMaster/${commonMasterID}`,
        type: `POST`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`
    })
        .done(function (response) {
            if (response.flag) {
                $masterGrid.delRowData(commonMasterID);
                $('#dialogDeleteCommonMaster').dialog('close');
                SuccessAlert(response.message);
            }
            else {
                ErrorAlert(response.message);
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
        });

    return;
});

//CREATE CommonDetail Popup
$(`#createCommonDetailBtn`).on(`click`, function () {
    $('#dialogCreateCommonDetail').dialog('open');
});
$(`#dialogCreateCommonDetail`).dialog({
    //width: 550,
    //height: 350,
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
        //GetCommonMaster();
    },
    close: function (event, ui) {
        $(`#createCommonDetailCode`).val(``);
        $(`#createCommonDetailName`).val(``);
    },
});
$(`#btnCancelCreateCommonDetail`).on(`click`, function () {
    $('#dialogCreateCommonDetail').dialog('close');
});
$(`#btnCreateCommonDetail`).on(`click`, function () {
    let code = $(`#createCommonDetailCode`).val() == null ? `` : $(`#createCommonDetailCode`).val().trim();
    let name = $(`#createCommonDetailName`).val() == null ? `` : $(`#createCommonDetailName`).val().trim();
    let masterCode = $(`#createCommonDetailMasterCode`).val() == null ? `` : $(`#createCommonDetailMasterCode`).val().trim();

    if (code == `` || name == `` || masterCode == ``) {
        WarningAlert(`ERROR_FullFillTheForm`);
    }
    else {
        var commonDetail = new Object();
        commonDetail.Code = code;
        commonDetail.Name = name;
        commonDetail.MasterCode = masterCode;

        $.ajax({
            url: `/Common/CreateCommonDetail`,
            type: `POST`,
            data: JSON.stringify(commonDetail),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.Data && response.HttpResponseCode != 100) {
                    $detailGrid.addRowData(response.Data.ID, response.Data, "first");
                    $detailGrid.jqGrid('setRowData', response.Data.ID, false, { background: '#39FF14' });
                    $(`#dialogCreateCommonDetail`).dialog('close');
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

//MODIFY CommonDetail Popup
function ModifyCommonDetail(e) {
    commonDetailID = e.attr(`data-id`);

    let rowData = $detailGrid.getRowData(commonDetailID);
    $(`#modifyCommonDetailCode`).val(rowData.Code);
    $(`#modifyCommonDetailName`).val(rowData.Name);
    $(`#modifyCommonDetailMasterCode`).val(rowData.MasterName);
    $(`#dialogModifyCommonDetail`).dialog(`open`);
}
$(`#dialogModifyCommonDetail`).dialog({
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


    //width: 550,
    //height: 420,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    classes: {
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    open: function (event, ui) {
        //GetCommonMaster();
        let rowData = $detailGrid.getRowData(commonDetailID);
        if (rowData.Active == `true`) {
            $(`#modifyCommonDetailActive`).val(`1`);
        }
        else {
            $(`#modifyCommonDetailActive`).val(`0`);
        }
    },

    close: function (event, ui) {
        $(`#modifyCommonDetailCode`).val(``);
        $(`#modifyCommonDetailName`).val(``);
    },
});
$(`#btnCancelModifyCommonDetail`).on(`click`, function () {
    $(`#dialogModifyCommonDetail`).dialog(`close`);
});
$(`#btnModifyCommonDetail`).on(`click`, function () {
    let code = $(`#modifyCommonDetailCode`).val() == null ? `` : $(`#modifyCommonDetailCode`).val().trim();
    let name = $(`#modifyCommonDetailName`).val() == null ? `` : $(`#modifyCommonDetailName`).val().trim();
    let rowData = $detailGrid.getRowData(commonDetailID);
    let masterCode = rowData.MasterCode;
    let active = $(`#modifyCommonDetailActive`).val() == null ? `` : $(`#modifyCommonDetailActive`).val().trim();
    if (code == `` || name == `` || masterCode == `` || !active) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return;
    }
    else {
        var commonDetail = new Object();
        commonDetail.Code = code;
        commonDetail.Name = name;
        commonDetail.MasterCode = masterCode;
        if (active == `1`) {
            commonDetail.Active = true;
        }
        else {
            commonDetail.Active = false;
        }


        $.ajax({
            url: `/Common/ModifyCommonDetail`,
            type: `PUT`,
            data: JSON.stringify(commonDetail),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.Data && response.HttpResponseCode != 100) {
                    $detailGrid.setRowData(response.Data.ID, response.Data, { background: '#39FF14' });
                    $(`#dialogModifyCommonDetail`).dialog(`close`);
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
