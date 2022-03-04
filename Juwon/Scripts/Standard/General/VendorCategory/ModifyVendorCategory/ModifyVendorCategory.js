async function OpenModifyModal() {
    if (vendorCategoryId != 0) {
        let obj = $vendorCategoryGrid.getRowData(vendorCategoryId);
        mVendorCategoryName.value = obj.VendorCategoryName;
        mVendorCategoryName.value = obj.VendorCategoryName;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyVendorCategory() {

    let VendorCategoryId = vendorCategoryId == null ? 0 : parseInt(vendorCategoryId);
    let VendorCategoryName = mVendorCategoryName.value;

    if (VendorCategoryId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!VendorCategoryName || VendorCategoryName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        VendorCategoryId,
        VendorCategoryName
    }

    $.ajax({
        url: `/VendorCategory/ModifyVendorCategory`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $vendorCategoryGrid.setRowData(response.Data.VendorCategoryId, response.Data);
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