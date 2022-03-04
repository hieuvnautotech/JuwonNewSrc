async function ModifySlimSelectBomDetail() {
    mPartSelectedList = new SlimSelect({
        select: '#mPartId',
        hideSelectedOption: true
    });

    mMaterialSelectedList = new SlimSelect({
        select: '#mMaterialId',
        hideSelectedOption: true
    });
}

async function readURLModifyBomDetail(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {

            var el = document.querySelector('#mImgBomDetail');
            if (el != null) {
                el.remove();
            }
            var el = document.querySelector('#mShowFile');
            if (el != null) {
                el.remove();
            }

            let ListAceptFile = ["IMAGE/JPG", "IMAGE/JPE", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/GIF", "IMAGE/PNG"];
            var extension = input.files[0].type.toUpperCase()

            if (ListAceptFile.includes(extension)) {

                const image = document.createElement('img');
                image.style.width = "200px";
                image.style.height = "200px";
                image.id = "modifyBomDetailImage";
                image.src = e.target.result;
                image.attr = e.target.result;
                document.querySelector('#mShowImgBomDetail').appendChild(image)
            }

        };

        reader.readAsDataURL(input.files[0]);
        $("#mCloseImgBomDetail").removeClass("hidden");
    }
}

//async function OpenModifyBomDetailModal() {
//    if (!bomDetailId || bomDetailId == 0) {
//        WarningAlert(`WARN_NotSelectOnGrid`);
//    }
//    else {
//        $('#mPartId').empty();
//        $('#mMaterialId').empty();

//        if (PartList && PartList.length > 0) {

//            await SetPartDropDownList();
//            await SetMaterialDropDownList();
//        }
//        else {
//            await GetPart();
//            await GetMaterial();
//            await SetPartDropDownList();
//            await SetMaterialDropDownList();
//        }

//        $("#modifyBomDetailModal").modal();
//    }
//}

async function OpenModifyBomDetailModal() {
    if (bomDetailId != 0) {
        await ModifySlimSelectBomDetail();

        //Clear input image 
        $("#mBomDetailFile").val('');

        //Gán các giá trị vào input modify
        let obj = $bomDetailGrid.getRowData(bomDetailId);
        mPartSelectedList.set([obj.PartId]);
        mMaterialSelectedList.set([obj.MaterialId]);

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
            if (obj.FileUpload.includes(".pdf")) {

                const File = document.createElement('span');
                File.id = "ShowFile";
                File.style.color = "blue";
                File.style.borderBottom = "2px solid"
                File.style.cursor = "pointer"
                File.onclick = function () {
                    window.open(
                        window.location.origin + "/Img/Standard/Information/Bom/" + obj.FileUpload, "_blank");
                };
                File.innerHTML = obj.FileUpload;

                document.querySelector('#mShowFile').appendChild(File);
            }
            else {
                const image = document.createElement('img');
                image.style.width = "200px";
                image.style.height = "200px";
                image.id = "modifyImage";
                image.src = "../Img/Standard/Information/Bom/" + obj.FileUpload;
                image.attr = obj.FileUpload;
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

async function ClickCloseModifyBomDetail() {
    var el = document.querySelector('#modifyBomDetailImage');
    if (el != null) {
        el.remove();
        $("#mCloseImgBomDetail").addClass("hidden");
        document.getElementById("mBomDetailFile").value = "";
    }
}

$(`#modifyBomDetailModal`).on(`hide.bs.modal`, function (e) {
    ClickCloseModifyBomDetail();
})