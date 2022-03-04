async function OpenDeleteModal() {
    if (buyerId != 0) {
        if (deleteBtn.innerText.trim().toUpperCase() == `ACTIVE`) {
            $(`#confirmToDeleteModal`).find('.modal-title').text('Confirm to Active');
            btnConfirmToDelete.innerHTML = `<i class="fa fa-toggle-on"></i>&nbsp;Active`;
        }
        else {
            $(`#confirmToDeleteModal`).find('.modal-title').text('Confirm to Delete');
            btnConfirmToDelete.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
        }
        $(`#confirmToDeleteModal`).modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
};

async function DeleteBuyer() {
    let BuyerId = buyerId == null ? 0 : parseInt(buyerId);
    if (BuyerId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    let requestUrl = `/Buyer/DeleteBuyer`;
    let requestType = `DELETE`;
    if (deleteBtn.innerText.trim().toUpperCase() == `ACTIVE`) {
        requestUrl = `/Buyer/ActiveBuyer`;
        requestType = `PUT`
    }

    $.ajax({
        url: requestUrl,
        type: requestType,
        data: {
            buyerId
        },
    })
        .done(function (response) {
            if (response.IsSuccess) {
                SuccessAlert(response.ResponseMessage);
                $(`#confirmToDeleteModal`).modal(`hide`);
                searchInput.value == null;
                ReloadBuyerGrid();
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