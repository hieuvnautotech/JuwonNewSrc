async function OpenModifyModal() {
    if (departmentId != 0) {
        let obj = $departmentGrid.getRowData(departmentId);
        mDepartmentName.value = obj.DepartmentName;
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyDepartment() {

    let DepartmentId = departmentId == null ? 0 : parseInt(departmentId);
    let DepartmentName = mDepartmentName.value;

    if (DepartmentId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    if (!DepartmentName || DepartmentName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        DepartmentId,
        DepartmentName,
    }

    $.ajax({
        url: `/Department/ModifyDepartment`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $departmentGrid.setRowData(response.Data.DepartmentId, response.Data);
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