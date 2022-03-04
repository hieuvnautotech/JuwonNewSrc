async function OpenDeleteModal() {
    if (supplierId != 0) {
        if (deleteBtn.innerText.trim().toUpperCase() == `ACTIVE`) {
            $(`#confirmToDeleteModal`).find('.modal-title').text('Confirm to Active');
            btnConfirmToDelete.innerHTML = `<i class="fa fa-toggle-on"></i>&nbsp;Active`;
        }
        else {
            $(`#confirmToDeleteModal`).find('.modal-title').text('Confirm to Delete');
            btnConfirmToDelete.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
        }
        $("#confirmToDeleteModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}

async function DeleteLocation() {
    let SupplierId = supplierId == null ? 0 : parseInt(supplierId);
    if (SupplierId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }

    let requestUrl = `/Supplier/DeleteSupplier`;
    let requestType = `DELETE`;
    if (deleteBtn.innerText.trim().toUpperCase() == `ACTIVE`) {
        requestUrl = `/Supplier/ActiveSupplier`;
        requestType = `PUT`
    }

    $.ajax({
        url: requestUrl,
        type: requestType,
        data: { supplierId },
    })
        .done(function (response) {
            if (response.IsSuccess) {
                SuccessAlert(response.ResponseMessage);
               searchInput.value == null;
                ReloadSupplierGrid();
                $(`#confirmToDeleteModal`).modal(`hide`);
                return true;
            }
            else {
                $(`#confirmToDeleteModal`).modal(`hide`);
                ErrorAlert(response.ResponseMessage);
                return false;
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToDeleteModal`).modal(`hide`);
            return false;
        });
}