async function OpenModifyModal() {
    if (partId != 0) {
        let obj = $partGrid.getRowData(partId);
        mPartCode.value = obj.PartCode;
        mPartName.value = obj.PartName;
        mPartDescription.value = obj.PartDescription;
     
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyPart() {

    let PartId = partId == null ? 0 : parseInt(partId);
    let PartCode = mPartCode.value;
    let PartName = mPartName.value;
    let PartDescription = mPartDescription.value;
 

    if (PartId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!PartCode || PartCode.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    if (!PartName || PartName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        PartId,
        PartCode,
        PartName,
        PartDescription,
      
    }

    $.ajax({
        url: `/Part/ModifyPart`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $partGrid.setRowData(response.Data.PartId, response.Data);
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