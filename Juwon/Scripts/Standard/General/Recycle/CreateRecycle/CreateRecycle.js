async function CreateRecycle() {
 
    let RecycleName = cRecycleName.value;
    let RecycleDescription = cRecycleDescription.value;
    if (!RecycleName || RecycleName.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
   

    let obj = {
       
        RecycleName,
        RecycleDescription
    }

    $.ajax({
        url: `/Recycle/CreateRecycle`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                
                $recycleGrid.addRowData(response.Data.RecycleId, response.Data, `first`);
                $recycleGrid.setRowData(response.Data.RecycleId, false, { background: '#39FF14' });

                $(`#confirmToCreateModal`).modal(`hide`);
                $(`#createModal`).modal(`hide`);
                SuccessAlert(response.ResponseMessage);

                return true;
            }
            else {
                $(`#confirmToCreateModal`).modal(`hide`);
                ErrorAlert(response.ResponseMessage);
                return false;
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToCreateModal`).modal(`hide`);
            return false;
        });
}