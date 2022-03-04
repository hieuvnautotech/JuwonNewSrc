async function OpenModifyModal() {
    if (areaCategoryId != 0) {
        let obj = $areaCategoryGrid.getRowData(areaCategoryId);
        mAreaCategoryName.value = obj.AreaCategoryName;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyAreaCategory() {

    let AreaCategoryId = areaCategoryId == null ? 0 : parseInt(areaCategoryId);
    let AreaCategoryName = mAreaCategoryName.value;

    if (AreaCategoryId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!AreaCategoryName || AreaCategoryName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        AreaCategoryId,
        AreaCategoryName,
    }

    $.ajax({
        url: `/AreaCategory/ModifyAreaCategory`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $areaCategoryGrid.setRowData(response.Data.AreaCategoryId, response.Data);
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