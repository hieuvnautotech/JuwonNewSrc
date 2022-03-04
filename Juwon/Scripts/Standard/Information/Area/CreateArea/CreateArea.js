async function CreateArea() {
    let AreaName = cAreaName.value;
    let AreaCategoryId = cAreaCategoryId.value;
    let AreaDescription = cAreaDescription.value;

    if (!AreaName || AreaName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        AreaName,
        AreaCategoryId,
        AreaDescription
    }

    $.ajax({
        url: `/Area/CreateArea`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $areaGrid.addRowData(response.Data.AreaId, response.Data, `first`);
                $areaGrid.setRowData(response.Data.AreaId, false, { background: '#39FF14' });
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