$(`#modifyModal`).on(`hide.bs.modal`, function (e) {
    modifySelectedAreaCategory.destroy();
})

async function ModifySlimSelect() {
    modifySelectedAreaCategory = new SlimSelect({
        select: '#mAreaCategoryId',
        hideSelectedOption: true
    });
}

async function OpenModifyModal() {
    if (areaId != 0) {
        await ModifySlimSelect();
        let obj = $areaGrid.getRowData(areaId);
        mAreaName.value = obj.AreaName;
        mAreaDescription.value = obj.AreaDescription;
        modifySelectedAreaCategory.set([obj.AreaCategoryId]);
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyArea() {

    let AreaId = areaId == null ? 0 : parseInt(areaId);
    let AreaName = mAreaName.value;
    let AreaDescription = mAreaDescription.value;
    let AreaCategoryId = mAreaCategoryId.value;

    if (AreaId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }

    if (!AreaName || AreaName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        AreaId,
        AreaName,
        AreaCategoryId,
        AreaDescription
    }

    $.ajax({
        url: `/Area/ModifyArea`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $areaGrid.setRowData(response.Data.AreaId, response.Data);
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