async function OpenModifyModal() {
    if (qCDetailId != 0) {
        let obj = $qCDetailGrid.getRowData(qCDetailId);
        mQCDetailName.value = obj.QCDetailName;
        mQCDetailDescription.value = obj.QCDetailDescription;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}

async function ModifyQCDetail() {
    let QCDetailId = qCDetailId;
    let QCDetailName = !mQCDetailName.value ? "" : mQCDetailName.value.trim();
    let QCDetailDescription = mQCDetailDescription.value;

    if (!QCDetailName || QCDetailName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        QCDetailId,
        QCDetailName,
        QCDetailDescription,
    }

    $.ajax({
        url: `/QCDetail/ModifyQCDetail`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $qCDetailGrid.setRowData(response.Data.QCDetailId, response.Data);
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