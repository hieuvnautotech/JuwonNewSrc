
async function OpenModifyModal() {
    if (colorId != 0) {
        let obj = $colorGrid.getRowData(colorId);
        mColorCode.value = obj.ColorCode;
        mColorName.value = obj.ColorName;
        mColorDescription.value = obj.ColorDescription;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyColor() {

    let ColorId = colorId == null ? 0 : parseInt(colorId);
    let ColorCode = mColorCode.value;
    let ColorName = mColorName.value;
    let ColorDescription = mColorDescription.value;

    if (ColorId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!ColorCode || ColorCode.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    if (!ColorName || ColorName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        ColorId,
        ColorCode,
        ColorName,
        ColorDescription
    }

    $.ajax({
        url: `/Color/ModifyColor`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $colorGrid.setRowData(response.Data.ColorId, response.Data);
                SuccessAlert(response.ResponseMessage);
                $(`#confirmToModifyModal`).modal(`hide`);
                $(`#modifyModal`).modal(`hide`);
                return true;
            }
            else {
                ErrorAlert(response.ResponseMessage);
                $(`#confirmToModifyModal`).modal(`hide`);
                return false;
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToModifyModal`).modal(`hide`);
            return false;
        });
}