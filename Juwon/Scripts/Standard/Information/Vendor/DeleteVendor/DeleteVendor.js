async function OpenDeleteModal() {
    if (vendorId != 0) {
        $("#confirmToDeleteModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}

async function DeleteVendor() {
    let VendorId = vendorId == null ? 0 : parseInt(vendorId);
    if (VendorId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    let requestUrl = `/Vendor/DeleteVendor`;
    let requestType = `DELETE`;
    if (deleteBtn.innerText.trim().toUpperCase() == `ACTIVE`) {
        requestUrl = `/Vendor/ActiveVendor`;
        requestType = `PUT`
    }
    $.ajax({
        url: requestUrl,
        type: requestType,
        data: { VendorId },
    })
        .done(function (response) {
            if (response.IsSuccess) {
                SuccessAlert(response.ResponseMessage);
                searchInput.value == null;
                $(`#confirmToDeleteModal`).modal(`hide`);
                ReloadVendorGrid();
                return true;
            }
            else {
                ErrorAlert(response.ResponseMessage);
                $(`#confirmToDeleteModal`).modal(`hide`);
                return false;
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToDeleteModal`).modal(`hide`);
            return false;
        });
}