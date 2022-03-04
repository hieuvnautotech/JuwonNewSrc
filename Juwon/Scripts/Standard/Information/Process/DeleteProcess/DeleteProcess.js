async function OpenDeleteModal() {
    if (processId != 0) {
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

async function DeleteProcess() {
    let ProcessId = processId == null ? 0 : parseInt(processId);
    if (ProcessId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    let requestUrl = `/Process/DeleteProcess`;
    let requestType = `DELETE`;
    if (deleteBtn.innerText.trim().toUpperCase() == `ACTIVE`) {
        requestUrl = `/Process/ActiveProcess`;
        requestType = `PUT`
    }
    $.ajax({
        url: requestUrl,
        type: requestType,
        data: { processId },
    })
        .done(function (response) {
            if (response.IsSuccess) {
                SuccessAlert(response.ResponseMessage);
                $(`#confirmToDeleteModal`).modal(`hide`);
                searchInput.value == null;
                ReloadProcessGrid();
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