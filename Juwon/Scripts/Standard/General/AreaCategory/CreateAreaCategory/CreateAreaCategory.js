async function CreateAreaCategory() {
    let AreaCategoryName = cAreaCategoryName.value;
    if (!AreaCategoryName || AreaCategoryName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    let obj = {
        AreaCategoryName,
    }

    $.ajax({
        url: `/AreaCategory/CreateAreaCategory`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $areaCategoryGrid.addRowData(response.Data.AreaCategoryId, response.Data, `first`);
                $areaCategoryGrid.setRowData(response.Data.AreaCategoryId, false, { background: '#39FF14' });
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