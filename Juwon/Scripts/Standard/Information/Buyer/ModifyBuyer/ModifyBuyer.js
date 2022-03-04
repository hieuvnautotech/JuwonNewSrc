async function OpenModifyModal() {
    if (buyerId != 0) {
        let obj = $buyerGrid.getRowData(buyerId);
        mBuyerCode.value = obj.BuyerCode;
        mBuyerName.value = obj.BuyerName;
        mBuyerDescription.value = obj.BuyerDescription;
        mBuyerEmail.value = obj.BuyerEmail;
        mBuyerPhone.value = obj.BuyerPhone;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyBuyer() {

    let BuyerId = buyerId == null ? 0 : parseInt(buyerId);
    let BuyerCode = mBuyerCode.value;
    let BuyerName = mBuyerName.value;
    let BuyerDescription = mBuyerDescription.value;
    let BuyerEmail = mBuyerEmail.value;
    let BuyerPhone = mBuyerPhone.value;

    if (BuyerId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!BuyerCode || BuyerCode.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    if (!BuyerName || BuyerName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        BuyerId,
        BuyerCode,
        BuyerName,
        BuyerDescription,
        BuyerEmail,
        BuyerPhone
    }

    $.ajax({
        url: `/Buyer/ModifyBuyer`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $buyerGrid.setRowData(response.Data.BuyerId, response.Data);
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