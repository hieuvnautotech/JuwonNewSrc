

async function CreateQCDetail() {
    let QCDetailName = !cQCDetailName.value ? "" : cQCDetailName.value.trim();
    let QCDetailDescription = cQCDetailDescription.value;

    if (!QCDetailName || QCDetailName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        QCDetailName,
        QCDetailDescription,
    }

    $.ajax({
        url: `/QCDetail/CreateQCDetail`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {

                $qCDetailGrid.addRowData(response.Data.QCDetailId, response.Data, `first`);
                $qCDetailGrid.setRowData(response.Data.QCDetailId, false, { background: '#39FF14' });
                SuccessAlert(response.ResponseMessage);
                $(`#confirmToCreateModal`).modal(`hide`);
                $(`#createModal`).modal(`hide`);
                cQCDetailName.value = null;
                cQCDetailDescription.value = null;
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