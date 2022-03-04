async function OpenModifyModal() {
    if (locationId != 0) {
        await ModifySlimSelect();
        let obj = $locationGrid.getRowData(locationId);
        mLocationName.value = obj.LocationName;
        modifySelectedLocationCategory.set([obj.LocationCategoryId]);
        modifySelectedArea.set([obj.AreaId]);
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}

$(`#modifyModal`).on(`hide.bs.modal`, function (e) {
    modifySelectedLocationCategory.destroy();
    modifySelectedArea.destroy();
})

async function ModifySlimSelect() {
    modifySelectedLocationCategory = new SlimSelect({
        select: '#mLocationCategoryId',
        hideSelectedOption: true
    });

    modifySelectedArea = new SlimSelect({
        select: '#mAreaId',
        hideSelectedOption: true
    });
}

async function ModifyLocation() {
    let LocationId = locationId;
    let LocationName = !mLocationName.value ? "" : mLocationName.value.trim();
    let LocationDescription = mLocationDescription.value;
    let LocationCategoryId = !mLocationCategoryId.value ? "" : mLocationCategoryId.value.trim();
    let AreaId = !mAreaId.value ? "" : mAreaId.value.trim();

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
        LocationId,
        LocationName,
        LocationDescription,
        LocationCategoryId,
        AreaId,
    }

    $.ajax({
        url: `/Location/ModifyLocation`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $locationGrid.setRowData(response.Data.LocationId, response.Data);
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
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToModifyModal`).modal(`hide`);
            return false;
        });
}