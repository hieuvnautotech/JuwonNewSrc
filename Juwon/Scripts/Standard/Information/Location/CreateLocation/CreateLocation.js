async function CreateLocation() {
    let LocationName = !cLocationName.value ? "" : cLocationName.value.trim();
    let LocationDescription = cLocationDescription.value;
    let LocationCategoryId = !cLocationCategoryId.value ? "" : cLocationCategoryId.value.trim();
    let AreaId = !cAreaId.value ? "" : cAreaId.value.trim();

    if (!LocationName || LocationName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    if (!LocationCategoryId || LocationCategoryId.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    if (!AreaId || AreaId.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        LocationName,
        LocationDescription,
        LocationCategoryId,
        AreaId,
    }

    $.ajax({
        url: `/Location/CreateLocation`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {

                $locationGrid.addRowData(response.Data.LocationId, response.Data, `first`);
                $locationGrid.setRowData(response.Data.LocationId, false, { background: '#39FF14' });
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