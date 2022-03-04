async function CreateLocationCategory() {
    let locationCategoryName = cLocationCategoryName.value;

    if (!locationCategoryName || locationCategoryName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        locationCategoryName
    }

    $.ajax({
        url: `/LocationCategory/CreateLocationCategory`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $locationCategoryGrid.addRowData(response.Data.LocationCategoryId, response.Data, `first`);
                $locationCategoryGrid.setRowData(response.Data.LocationCategoryId, false, { background: '#39FF14' });
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