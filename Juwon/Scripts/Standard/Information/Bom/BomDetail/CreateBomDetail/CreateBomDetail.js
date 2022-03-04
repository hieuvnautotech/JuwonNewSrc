async function readURLCreateBomDetail(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {

            var el = document.querySelector('#imgBomDetail');
            if (el != null) {
                el.remove();

            }
            let ListAceptFile = ["IMAGE/JPG", "IMAGE/JPE", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/GIF", "IMAGE/PNG"];
            var extension = input.files[0].type.toUpperCase()

            if (ListAceptFile.includes(extension)) {
                const image = document.createElement('img');
                image.style.width = "300px";
                image.style.height = "200px";
                image.id = "imgBomDetail";
                image.src = e.target.result;
                image.attr = e.target.result;
                document.querySelector('#showImgBomDetail').appendChild(image)
            }
        };

        reader.readAsDataURL(input.files[0]);
        $("#CloseImgBomDetail").removeClass("hidden");
    }
}

async function OpenCreateBomDetailModal() {
    if (!bomId || bomId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
    else {
        $('#cPartId').empty();
        $('#cMaterialId').empty();

        if (PartList && PartList.length > 0) {

            await SetPartDropDownList();
            await SetMaterialDropDownList();
        }
        else {
            await GetPart();
            await GetMaterial();
            await SetPartDropDownList();
            await SetMaterialDropDownList();
        }

        $("#createBomDetailModal").modal();
    }
}

async function SetPartDropDownList() {
    let html = ``;
    PartList.sort((a, b) => {
        if (a.PartName < b.PartName) return -1
        return a.PartName > b.PartName ? 1 : 0
    });

    $.each(PartList, function (key, item) {
        html += `<option  value="${item.PartId}">${item.PartName}</option>`;
    });
    $(`#cPartId`).html(html);
    if (!cPartSelectedList) {
        cPartSelectedList = new SlimSelect({
            select: '#cPartId',
            hideSelectedOption: true
        });
    }

    //$(`#mPartId`).html(html);
}

async function SetMaterialDropDownList() {
    let html = ``;
    MaterialList.sort((a, b) => {
        if (a.MaterialCode < b.MaterialCode) return -1
        return a.MaterialCode > b.MaterialCode ? 1 : 0
    });

    $.each(MaterialList, function (key, item) {
        html += `<option  value="${item.MaterialId}">${item.MaterialName}</option>`;
    });
    $(`#cMaterialId`).html(html);
    if (!cMaterialSelectedList) {
        cMaterialSelectedList = new SlimSelect({
            select: '#cMaterialId',
            hideSelectedOption: true
        });
    }

    //$(`#mMaterialId`).html(html);

}

async function ClickCloseCreateBomDetail() {
    var el = document.querySelector('#imgBomDetail');
    if (el != null) {
        el.remove();
        $("#CloseImgBomDetail").addClass("hidden");
        document.getElementById("cBomDetailFile").value = "";
    }
}

$(`#createBomDetailModal`).on(`hide.bs.modal`, function (e) {
    ClickCloseCreateBomDetail();
})

async function CreateBomDetail() {
    let BomId = bomId;
    let PartId = cPartId.value;
    let MaterialId = cMaterialId.value;
    let Weight = parseFloat(cWeight.value);
    let Amount = cAmount.value;

    if (!BomId || BomId == 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    let formData = new FormData();
    let files = $("#cBomDetailFile").get(0).files;

    formData.append("File", files[0]);
    formData.append("BomId", BomId);
    formData.append("PartId", PartId);
    formData.append("MaterialId", MaterialId);
    formData.append("Weight", Weight.toFixed(5));
    formData.append("Amount", Amount);

    $.ajax({
        url: `/Bom/CreateBomDetail`,
        type: "POST",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {

                $bomDetailGrid.addRowData(response.Data.BomDetailId, response.Data, `first`);
                $bomDetailGrid.setRowData(response.Data.BomDetailId, false, { background: '#39FF14' });

                $(`#confirmToCreateBomDetailModal`).modal(`hide`);
                $(`#createBomDetailModal`).modal(`hide`);
                SuccessAlert(response.ResponseMessage);

                return true;
            }
            else {
                ErrorAlert(response.ResponseMessage);
                $(`#confirmToCreateBomDetailModal`).modal(`hide`);
                return false;
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            $(`#confirmToCreateBomDetailModal`).modal(`hide`);
            return false;
        });
}