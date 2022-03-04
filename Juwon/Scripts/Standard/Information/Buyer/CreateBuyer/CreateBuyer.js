async function CreateBuyer() {
    let BuyerCode = cBuyerCode.value;
    let BuyerName = cBuyerName.value;
    let BuyerDescription = cBuyerDescription.value;
    let BuyerEmail = cBuyerEmail.value;
    let BuyerPhone = cBuyerPhone.value;
    if (!BuyerCode || BuyerCode.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    if (!BuyerName || BuyerName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        BuyerCode,
        BuyerName, 
        BuyerDescription,
        BuyerEmail, 
        BuyerPhone,
    }

    $.ajax({
        url: `/Buyer/CreateBuyer`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $buyerGrid.addRowData(response.Data.BuyerId, response.Data, `first`);
                $buyerGrid.setRowData(response.Data.BuyerId, false, { background: '#39FF14' });
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