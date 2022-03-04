async function CreateVendorCategory() {
    let VendorCategoryName = cVendorCategoryName.value;

    if (!VendorCategoryName || VendorCategoryName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        VendorCategoryName
    }

    $.ajax({
        url: `/VendorCategory/CreateVendorCategory`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $vendorCategoryGrid.addRowData(response.Data.VendorCategoryId, response.Data, `first`);
                $vendorCategoryGrid.setRowData(response.Data.VendorCategoryId, false, { background: '#39FF14' });
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