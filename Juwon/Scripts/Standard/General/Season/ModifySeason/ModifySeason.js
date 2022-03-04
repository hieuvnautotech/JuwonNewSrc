async function OpenModifyModal() {
    if (seasonId != 0) {
        let obj = $seasonGrid.getRowData(seasonId);
        mSeasonCode.value = obj.SeasonCode;
        mSeasonName.value = obj.SeasonName;
        mSeasonDescription.value = obj.SeasonDescription
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}
async function ModifySeason() {
    let SeasonId = seasonId == null ? 0 : parseInt(seasonId);
    let SeasonCode = mSeasonCode.value;
    let SeasonName = mSeasonName.value;
    let SeasonDescription = mSeasonDescription.value;

    if (SeasonId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    if (!SeasonCode || SeasonCode.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    if (!SeasonName || SeasonName.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    let obj = {
        SeasonId,
        SeasonCode,
        SeasonName,
        SeasonDescription
    }

    $.ajax({
        url: `/Season/ModifySeason`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $seasonGrid.setRowData(response.Data.SeasonId, response.Data);
              
                SuccessAlert(response.ResponseMessage);

                $(`#confirmToModifyModal`).modal(`hide`);
                $(`#modifyModal`).modal(`hide`);
                return true;
            }
            else {
                $(`#confirmToModifyModal`).modal(`hide`);
                ErrorAlert(response.ResponseMessage);
                return false;
            }
        })

        .fail(function () {
            $(`#confirmToModifyModal`).modal(`hide`);
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return false;
        });
}