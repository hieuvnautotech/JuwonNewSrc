

async function OpenModifyModal() {
    if (locationCategoryId != 0) {
        let obj = $locationCategoryGrid.getRowData(locationCategoryId);
        mLocationCategoryName.value = obj.LocationCategoryName;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyLocationCategory() {

    let LocationCategoryId = locationCategoryId == null ? 0 : parseInt(locationCategoryId);
    let LocationCategoryName = mLocationCategoryName.value;

    if (LocationCategoryId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }

    if (!LocationCategoryName || LocationCategoryName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        LocationCategoryId,
        LocationCategoryName
    }

    $.ajax({
        url: `/LocationCategory/ModifyLocationCategory`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $locationCategoryGrid.setRowData(response.Data.LocationCategoryId, response.Data);
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