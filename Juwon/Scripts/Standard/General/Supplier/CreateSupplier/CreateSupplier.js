async function CreateSupplier() {
 
    let SupplierName = cSupplierName.value;
    let SupplierAddress = cSupplierAddress.value;
    let SupplierUrl = cSupplierUrl.value;
    let SupplierEmail = cSupplierEmail.value;
    let SupplierPhone = cSupplierPhone.value;
    if (!SupplierName || SupplierName.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
   

    let obj = {
       
        SupplierName,
        SupplierAddress,
        SupplierUrl,
        SupplierEmail,
        SupplierPhone
    }

    $.ajax({
        url: `/Supplier/CreateSupplier`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                
                $supplierGrid.addRowData(response.Data.SupplierId, response.Data, `first`);
                $supplierGrid.setRowData(response.Data.SupplierId, false, { background: '#39FF14' });

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