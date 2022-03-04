async function ModifySlimSelect() {
    modifySelectedSection = new SlimSelect({
        select: '#mMaterialSection',
        hideSelectedOption: true
    });

    modifySelectedUnit = new SlimSelect({
        select: '#mMaterialUnit',
        hideSelectedOption: true
    });

    modifySelectedInOut = new SlimSelect({
        select: '#mMaterialInOut',
        hideSelectedOption: true
    });

    modifySelectedType = new SlimSelect({
        select: '#mMaterialType',
        hideSelectedOption: true
    });

    modifySelectedPart = new SlimSelect({
        select: '#mMaterialPart',
        hideSelectedOption: true
    });

    modifySelectedColor = new SlimSelect({
        select: '#mMaterialColor',
        hideSelectedOption: true
    });

    modifySelectedMold = new SlimSelect({
        select: '#mMaterialMold',
        hideSelectedOption: true
    });
}

$(`#modifyModal`).on(`hide.bs.modal`, function (e) {
    modifySelectedSection.destroy();
    modifySelectedUnit.destroy();
    modifySelectedInOut.destroy();
    modifySelectedType.destroy();
    modifySelectedPart.destroy();
    modifySelectedColor.destroy();
    modifySelectedMold.destroy();
})

async function OpenModifyModal() {
    if (materialId != 0) {
        await ModifySlimSelect();

        let obj = $materialGrid.getRowData(materialId);
        mMaterialCode.value = obj.MaterialCode;
        mMaterialName.value = obj.MaterialName;
        mMaterialRemark.value = obj.MaterialRemark;

        modifySelectedSection.set([obj.MaterialSectionId]);
        modifySelectedUnit.set([obj.MaterialUnitId]);
        modifySelectedInOut.set([obj.MaterialInOutId]);
        modifySelectedType.set([obj.MaterialTypeId]);
        modifySelectedPart.set([obj.PartId]);
        modifySelectedColor.set([obj.ColorId]);
        modifySelectedMold.set([obj.MoldId]);

        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    return;
}

async function ModifyMaterial() {

    let MaterialId = materialId == null ? 0 : parseInt(materialId);
    let MaterialCode = mMaterialCode.value;
    let MaterialName = mMaterialName.value ? mMaterialName.value.trim() : "";
    let MaterialTypeId = mMaterialType.value;
    let MaterialInOutId = mMaterialInOut.value;
    let MaterialSectionId = mMaterialSection.value;
    let MaterialUnitId = mMaterialUnit.value;
    let PartId = mMaterialPart.value;
    let ColorId = mMaterialColor.value;
    let MoldId = mMaterialMold.value;
    let MaterialRemark = mMaterialRemark.value;

    if (MaterialId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }

    if (!MaterialName || MaterialName.length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let obj = {
        MaterialId,
        MaterialCode,
        MaterialName,
        MaterialTypeId,
        MaterialInOutId,
        MaterialSectionId,
        MaterialUnitId,
        MaterialRemark,
        PartId,
        ColorId,
        MoldId
    }

    $.ajax({
        url: `/Material/ModifyMaterial`,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $materialGrid.setRowData(response.Data.MaterialId, response.Data);
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