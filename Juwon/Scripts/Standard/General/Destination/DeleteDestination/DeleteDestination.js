async function OpenDeleteModal() {
    if (destinationId != 0) {
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

async function DeleteDestination() {
    let DestinationId = destinationId == null ? 0 : parseInt(destinationId);
    if (DestinationId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    let requestUrl = `/Destination/DeleteDestination`;
    let requestType = `DELETE`;
    if (deleteBtn.innerText.trim().toUpperCase() == `ACTIVE`) {
        requestUrl = `/Destination/ActiveDestination`;
        requestType = `PUT`
    }

    $.ajax({
        url: requestUrl,
        type: requestType,
        data: {
            destinationId
        },
    })
        .done(function (response) {
            if (response.IsSuccess) {
                SuccessAlert(response.ResponseMessage);
                $(`#confirmToDeleteModal`).modal(`hide`);
                searchInput.value == null;
                ReloadDestinationGrid();
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
