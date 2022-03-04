async function ModifySlimSelect() {
    modifySelectedDestination = new SlimSelect({
        select: '#mDestinationId',
        hideSelectedOption: true
    });

    modifySelectedVendorCategory = new SlimSelect({
        select: '#mVendorCategoryId',
        hideSelectedOption: true
    });

    modifySelectedBuyers = new SlimSelect({
        select: '#mBuyer'
    });
}

$(`#modifyModal`).on(`hide.bs.modal`, function (e) {
    modifySelectedDestination.destroy();
    modifySelectedVendorCategory.destroy();
    modifySelectedBuyers.destroy();
})

async function OpenModifyVendorModal() {
    if (vendorId != 0) {

        await ModifySlimSelect();

        let obj = $vendorGrid.getRowData(vendorId);
        mVendorCode.value = obj.VendorCode;
        mVendorName.value = obj.VendorName;
        mVendorAddress.value = obj.VendorAddress;
        mVendorPhone.value = obj.VendorPhone;
        modifySelectedDestination.set([obj.DestinationId]);
        modifySelectedVendorCategory.set([obj.VendorCategoryId]);

        await GetBuyersByVendorId(vendorId);
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function GetBuyersByVendorId(e) {
    let modifyVendor_Buyers = [];
    $.ajax({
        url: `/Vendor/GetBuyersByVendorId?vendorId=${e}`,
        type: "GET",
    })
        .done(function (response) {
            
            if (response.Data && response.HttpResponseCode != 100) {
                for (let item of response.Data) {
                    modifyVendor_Buyers.push(item.BuyerId);
                }
            }
            else {
                ErrorAlert(response.ResponseMessage);
            }
            modifySelectedBuyers.set(modifyVendor_Buyers);
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
        });
}


async function ModifyVendor() {

    let VendorId = vendorId == null ? 0 : parseInt(vendorId);
    let VendorCode = mVendorCode.value;
    let VendorName = mVendorName.value;
    let VendorAddress = mVendorAddress .value;
    let VendorPhone = mVendorPhone.value;
    let DestinationId = mDestinationId.value;
    let VendorCategoryId = mVendorCategoryId.value;

    let BuyerIds = modifySelectedBuyers.selected();
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

    if (VendorId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!VendorCode || VendorCode.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    if (!VendorName || VendorName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        VendorId,
        VendorCode,
        VendorName,
        VendorAddress,
        VendorPhone,
        DestinationId,
        VendorCategoryId,
        Buyers
    }

    $.ajax({
        url: `/Vendor/ModifyVendor`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $vendorGrid.setRowData(response.Data.VendorId, response.Data);
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