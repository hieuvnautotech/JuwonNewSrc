async function CreateProcess() {
    let ProcessCode = cProcessCode.value;
    let ProcessName = cProcessName.value;
    let ProcessDescription = cProcessDescription.value;
    let ProcessNote = cProcessNote.value;
    if (!ProcessCode || ProcessCode.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    if (!ProcessName || ProcessName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        ProcessCode,
        ProcessName,
        ProcessDescription,
        ProcessNote,
    }

    $.ajax({
        url: `/Process/CreateProcess`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                
                $processGrid.addRowData(response.Data.ProcessId, response.Data, `first`);
                $processGrid.setRowData(response.Data.ProcessId, false, { background: '#39FF14' });

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