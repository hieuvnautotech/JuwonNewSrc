async function OpenModifyModal() {
    if (recycleId != 0) {
        let obj = $recycleGrid.getRowData(recycleId);
        mRecycleName.value = obj.RecycleName;
        mRecycleDescription.value = obj.RecycleDescription
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}
async function ModifyRecycle() {
    let RecycleId = recycleId == null ? 0 : parseInt(recycleId);
    let RecycleName = mRecycleName.value;
    let RecycleDescription = mRecycleDescription.value;

    if (RecycleId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    if (!RecycleName || RecycleName.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    let obj = {
        RecycleId,
        RecycleName,
        RecycleDescription
    }

    $.ajax({
        url: `/Recycle/ModifyRecycle`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $recycleGrid.setRowData(response.Data.RecycleId, response.Data);
              
                SuccessAlert(response.ResponseMessage);

                $(`#confirmToModifyModal`).modal(`hide`);
                $(`#modifyModal`).modal(`hide`);
                return true;
            }
            else {
                $(`#confirmToModifyModal`).modal(`hide`);
                ErrorAlert(response.ResponseMessage);
                return false;
            }
        })

        .fail(function () {
            $(`#confirmToModifyModal`).modal(`hide`);
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return false;
        });
}