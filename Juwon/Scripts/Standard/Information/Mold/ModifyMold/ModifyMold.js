async function ModifySlimSelect() {
    modifySelectedCurrency = new SlimSelect({
        select: '#mCurrency',
        hideSelectedOption: true
    });
}

$(`#modifyModal`).on(`hide.bs.modal`, function (e) {
    modifySelectedCurrency.destroy();
    ClickCloseModify();
})

async function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {

            var el = document.querySelector('#modifyImage');
            if (el != null) {
                el.remove();
            }
            var el = document.querySelector('#ShowFile');
            if (el != null) {
                el.remove();
            }

            let ListAceptFile = ["IMAGE/JPG", "IMAGE/JPE", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/GIF", "IMAGE/PNG"];
            var extension = input.files[0].type.toUpperCase()

            if (ListAceptFile.includes(extension)) {

                const image = document.createElement('img');
                image.style.width = "200px";
                image.style.height = "200px";
                image.id = "modifyImage";
                image.src = e.target.result;
                image.attr = e.target.result;
                document.querySelector('#mShowImg').appendChild(image)
            }

        };

        reader.readAsDataURL(input.files[0]);
        $("#mCloseImg").removeClass("hidden");
    }
}

async function ClickCloseModify() {
    var el = document.querySelector('#modifyImage');
    if (el != null) {
        el.remove();
        $("#mCloseImg").addClass("hidden");
        document.getElementById("mMoldFile").value = "";
        $("#mMoldFile").val('');
        //$("#mShowMoldFile").val(''); 
    }
}

async function OpenModifyModal() {
    if (moldId != 0) {
        await ModifySlimSelect();

        //Clear input image 
        $("#mMoldFile").val('');

        //Gán các giá trị vào input modify
        let obj = $moldGrid.getRowData(moldId);
        mMoldCode.value = obj.MoldCode.slice(0, 9);
        mMoldName.value = obj.MoldName;
        mMoldUnitPrice.value = obj.UnitPrice;
        modifySelectedCurrency.set([obj.Currency]);

        //Show img
        if (obj.Image != null && obj.Image != "") {

            var el = document.querySelector('#modifyImage');
            if (el != null) {
                el.remove();
            }
            var el = document.querySelector('#ShowFile');
            if (el != null) {
                el.remove();
            }
            if (obj.Image.includes(".pdf")) {

                const File = document.createElement('span');
                File.id = "ShowFile";
                File.style.color = "blue";
                File.style.borderBottom = "2px solid"
                File.style.cursor = "pointer"
                File.onclick = function () {
                    window.open(
                        window.location.origin + "/Img/Standard/Information/Mold/" + obj.Image, "_blank");
                };
                File.innerHTML = obj.Image;

                document.querySelector('#mShowFile').appendChild(File);
            }
            else {
                const image = document.createElement('img');
                image.style.width = "200px";
                image.style.height = "200px";
                image.id = "modifyImage";
                image.src = "../Img/Standard/Information/Mold/" + obj.Image;
                image.attr = obj.Image;
                document.querySelector('#mShowImg').appendChild(image);

                $("#mCloseImg").removeClass("hidden");
            }

        }
        else {
            $("#showImg").html("");
        }
        $("#modifyModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
}

async function ModifyMold() {
    let obj = $moldGrid.getRowData(moldId);
    let MoldId = moldId == null ? 0 : parseInt(moldId);
    

   /* let MoldId = mMoldId.value;*/
    let MoldName = mMoldName.value;
    let UnitPrice = mMoldUnitPrice.value;
    let Currency = mCurrency.value;
    let MoldCode = mMoldCode.value;

    if (MoldId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }

    //if (!MoldId || MoldId.trim().length === 0) {
    //    WarningAlert(`WARN_NotSelectOnGrid`);
    //    return false;
    //}
        if (!MoldName || MoldName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
        if (MoldCode.length != 9) {
        WarningAlert(`ERROR_MoldCode`);
        return false;
    }

    let formData = new FormData();
    let files = $("#mMoldFile").get(0).files;
    formData.append("file", files[0]);

    formData.append("MoldId", MoldId);
    formData.append("MoldCode", MoldCode);
    formData.append("MoldName", MoldName);
    formData.append("UnitPrice", UnitPrice);
    formData.append("Currency", Currency);
    formData.append("Image", obj.Image);

    $.ajax({
        url: `/Mold/ModifyMold`,
        type: "PUT",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $moldGrid.setRowData(response.Data.MoldId, response.Data);

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

async function ShowImageModal() {
    if (moldId != 0) {


        let obj = $moldGrid.getRowData(moldId);

        //Show img
        if (obj.Image != null) {

            var el = document.querySelector('#modifyImageBig');
            if (el != null) {
                el.remove();
            }

            const image = document.createElement('img');
            image.id = "modifyImageBig";
            image.style.margin = "5% auto";
            image.style.display = "block";
            image.style.width = "90%";
            image.style.height = "80vh";
            image.src = "../Img/Standard/Information/Mold/" + obj.Image;
            image.attr = obj.Image;
            document.querySelector('#ShowImageBig').appendChild(image);

        }
        else {
            WarningAlert(`WARN_NotSelectOnGrid`);
            return false;
        }
        $("#ShowImageModal").modal();
    }
    else {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }

}
