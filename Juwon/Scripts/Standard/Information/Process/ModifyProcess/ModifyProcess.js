
async function OpenModifyModal() {
    if (processId != 0) {
        let obj = $processGrid.getRowData(processId);
        mProcessCode.value = obj.ProcessCode;
        mProcessName.value = obj.ProcessName;
        mProcessDescription.value = obj.ProcessDescription;
        mProcessNote.value = obj.ProcessNote;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyProcess() {

    let ProcessId = processId == null ? 0 : parseInt(processId);
    let ProcessCode = mProcessCode.value;
    let ProcessName = mProcessName.value;
    let ProcessDescription = mProcessDescription.value;
    let ProcessNote = mProcessNote.value;

    if (ProcessId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!ProcessCode || ProcessCode.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    if (!ProcessName || ProcessName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        ProcessId,
        ProcessCode,
        ProcessName,
        ProcessDescription,
        ProcessNote,
    }

    $.ajax({
        url: `/Process/ModifyProcess`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $processGrid.setRowData(response.Data.ProcessId, response.Data);
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