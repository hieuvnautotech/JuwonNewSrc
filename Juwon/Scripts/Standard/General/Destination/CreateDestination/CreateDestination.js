async function CreateDestination() {

    let DestinationCode = cDestinationCode.value;
    let DestinationName = cDestinationName.value;
    let DestinationDescription = cDestinationDescription.value;
    if (!DestinationCode || DestinationCode.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    if (!DestinationName || DestinationName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        DestinationCode,
        DestinationName,
        DestinationDescription,
    }

    $.ajax({
        url: `/Destination/CreateDestination`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {

                $destinationGrid.addRowData(response.Data.DestinationId, response.Data, `first`);
                $destinationGrid.setRowData(response.Data.DestinationId, false, { background: '#39FF14' });
                SuccessAlert(response.ResponseMessage);
                $(`#confirmToCreateModal`).modal(`hide`);
                $(`#createModal`).modal(`hide`);
                return true;
            }
            else {
                ErrorAlert(response.ResponseMessage);
                $(`#confirmToCreateModal`).modal(`hide`);
                return false;
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToCreateModal`).modal(`hide`);
            return false;
        });
}