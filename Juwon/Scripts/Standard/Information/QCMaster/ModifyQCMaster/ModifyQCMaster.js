
async function OpenModifyModal() {
    if (qcMasterId != 0) {
        let obj = $qcMasterGrid.getRowData(qcMasterId);
        mQCMasterName.value = obj.QCMasterName;
        mQCMasterDescription.value = obj.QCMasterDescription;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyQCMaster() {

    let QCMasterId = qcMasterId == null ? 0 : parseInt(qcMasterId);
    let QCMasterName = mQCMasterName.value;
    let QCMasterDescription = mQCMasterDescription.value;
    let QCDetailIds = [...qcDetailIdList];

    if (QCMasterId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!QCMasterName || QCMasterName.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }

    let obj = {
        QCMasterId,
        QCMasterName,
        QCMasterDescription,
        QCDetailIds
    }

    $.ajax({
        url: `/QCMaster/ModifyQCMaster`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $qcMasterGrid.setRowData(response.Data.QCMasterId, response.Data);
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