async function OpenModifyModal() {
    if (supplierId != 0) {
        let obj = $supplierGrid.getRowData(supplierId);
        mSupplierName.value = obj.SupplierName;
        mSupplierAddress.value = obj.SupplierAddress;
        mSupplierUrl.value = obj.SupplierUrl;
        mSupplierEmail.value = obj.SupplierEmail;
        mSupplierPhone.value = obj.SupplierPhone;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}
async function ModifySupplier() {
    let SupplierId = supplierId == null ? 0 : parseInt(supplierId);
    let SupplierName = mSupplierName.value;
    let SupplierAddress = mSupplierAddress.value;
    let SupplierUrl = mSupplierUrl.value;
    let SupplierEmail = mSupplierEmail.value;
    let SupplierPhone = mSupplierPhone.value;

    if (SupplierId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    if (!SupplierName || SupplierName.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    let obj = {
        SupplierId,
        SupplierName,
        SupplierAddress,
        SupplierUrl,
        SupplierEmail,
        SupplierPhone
    }

  

    $.ajax({
        url: `/Supplier/ModifySupplier`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $supplierGrid.setRowData(response.Data.SupplierId, response.Data);
              
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
            $(`#confirmToModifyModal`).modal(`hide`);
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return false;
        });
}