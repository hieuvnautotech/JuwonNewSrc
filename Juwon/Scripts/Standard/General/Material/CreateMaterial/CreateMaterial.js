async function CreateMaterial() {
    let MaterialName = cMaterialName.value;
    let MaterialInOutId = cMaterialInOut.value;
    let MaterialSectionId = cMaterialSection.value;
    let MaterialTypeId = cMaterialType.value;
    let PartId = cMaterialPart.value;
    let ColorId = cMaterialColor.value;
    let MaterialUnitId = cMaterialUnit.value;
    let MoldId = cMaterialMold.value;
    let MaterialRemark = cMaterialRemark.value;

    if (!MaterialName || MaterialName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        MaterialName,
        MaterialTypeId,
        MaterialInOutId,
        MaterialSectionId,
        MaterialUnitId,
        MaterialRemark,
        PartId,
        ColorId,
        MoldId,
    }

    $.ajax({
        url: `/Material/CreateMaterial`,
        type: "POST",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $materialGrid.addRowData(response.Data.MaterialId, response.Data, `first`);
                $materialGrid.setRowData(response.Data.MaterialId, false, { background: '#39FF14' });
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