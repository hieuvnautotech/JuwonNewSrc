async function CreateVendor() {
    let VendorCode = cVendorCode.value;
    let VendorName = cVendorName.value;
    let VendorAddress = cVendorAddress.value;
    let VendorPhone = cVendorPhone.value;
    let DestinationId = cDestinationId.value;
    let VendorCategoryId = cVendorCategoryId.value;
    let BuyerIds = createSelectedBuyers.selected();
    let Buyers = []

    if (!BuyerIds) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    else {
        for (let item of BuyerIds) {
            let obj = {
                BuyerId: item,
                Active: true
            }
            Buyers.push(obj);
        }
    }

    if (!VendorCode || VendorCode.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    if (!VendorName || VendorName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        VendorCode,
        VendorName,
        VendorAddress,
        VendorPhone,
        DestinationId,
        VendorCategoryId,
        Buyers
    }

    $.ajax({
        url: `/Vendor/CreateVendor`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                
                $vendorGrid.addRowData(response.Data.VendorId, response.Data, `first`);
                $vendorGrid.setRowData(response.Data.VendorId, false, { background: '#39FF14' });
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