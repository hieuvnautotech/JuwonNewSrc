async function CreateDepartment() {
    let DepartmentName = cDepartmentName.value;
    if (!DepartmentName || DepartmentName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
    let obj = {
        DepartmentName,
    }

    $.ajax({
        url: `/Department/CreateDepartment`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $departmentGrid.addRowData(response.Data.DepartmentId, response.Data, `first`);
                $departmentGrid.setRowData(response.Data.DepartmentId, false, { background: '#39FF14' });
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