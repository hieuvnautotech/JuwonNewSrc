async function CreateSeason() {
    let SeasonCode = cSeasonCode.value;
    let SeasonName = cSeasonName.value;
    let SeasonDescription = cSeasonDescription.value;
    if (!SeasonCode || SeasonCode.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    if (!SeasonName || SeasonName.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
  
    let obj = {
        SeasonCode,
        SeasonName,
        SeasonDescription
    }

    $.ajax({
        url: `/Season/CreateSeason`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                
                $seasonGrid.addRowData(response.Data.SeasonId, response.Data, `first`);
                $seasonGrid.setRowData(response.Data.SeasonId, false, { background: '#39FF14' });

                $(`#confirmToCreateModal`).modal(`hide`);
                $(`#createModal`).modal(`hide`);
                SuccessAlert(response.ResponseMessage);

                return true;
            }
            else {
                $(`#confirmToCreateModal`).modal(`hide`);
                ErrorAlert(response.ResponseMessage);
                return false;
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToCreateModal`).modal(`hide`);
            return false;
        });
}