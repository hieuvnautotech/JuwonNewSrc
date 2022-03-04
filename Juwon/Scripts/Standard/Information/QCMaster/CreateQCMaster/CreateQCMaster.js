async function CreateQCMaster() {
    let QCMasterName = cQCMasterName.value;
    let QCMasterDescription = cQCMasterDescription.value;
    
    if (!QCMasterName || QCMasterName.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let QCDetailIds = [...qcDetailIdList];

    let obj = {
        QCMasterName,
        QCMasterDescription,
        QCDetailIds
    }

    $.ajax({
        url: `/QCMaster/CreateQCMaster`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                
                $qcMasterGrid.addRowData(response.Data.QCMasterId, response.Data, `first`);
                $qcMasterGrid.setRowData(response.Data.QCMasterId, false, { background: '#39FF14' });

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