async function OpenModifyModal() {
    if (destinationId != 0) {
        let obj = $destinationGrid.getRowData(destinationId);
        mDestinationCode.value = obj.DestinationCode;
        mDestinationName.value = obj.DestinationName;
        mDestinationDescription.value = obj.DestinationDescription;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyDestination() {

    let DestinationId = destinationId == null ? 0 : parseInt(destinationId);
    let DestinationCode = mDestinationCode.value;
    let DestinationName = mDestinationName.value;
    let DestinationDescription = mDestinationDescription.value;

    if (DestinationId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!DestinationCode || DestinationCode.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    if (!DestinationName || DestinationName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        DestinationId,
        DestinationCode,
        DestinationName,
        DestinationDescription,
    }

    $.ajax({
        url: `/Destination/ModifyDestination`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $destinationGrid.setRowData(response.Data.DestinationId, response.Data);
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