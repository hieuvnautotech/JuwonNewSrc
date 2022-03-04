async function ModifySlimSelect() {
    modifySelectedMoldId = new SlimSelect({
        select: '#mMoldId',
        hideSelectedOption: true
    });
}
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
        document.getElementById("mBomFile").value = "";
        $("#mBomFile").val(''); 
        //$("#mShowBomFile").val(''); 
    }
}

$(`#modifyModal`).on(`hide.bs.modal`, function (e) {
    modifySelectedMoldId.destroy();
    ClickCloseModify();
})

async function OpenModifyModal() {
    if (bomId != 0) {
        await ModifySlimSelect();

        //Clear input image 
        $("#mBomFile").val(''); 
        
        //Gán các giá trị vào input modify
        let obj = $bomGrid.getRowData(bomId);
        modifySelectedMoldId.set([obj.MoldId]);

        //Show img
        if (obj.FileUpload != null && obj.FileUpload != "") {

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
                        window.location.origin + "/Img/Standard/Information/Bom/" + obj.FileUpload , "_blank");
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


async function ModifyBom() {
    let BomId = bomId == null ? 0 : parseInt(bomId);

    let MoldId = mMoldId.value;

    if (BomId == 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
    }
   
    if (!MoldId || MoldId.trim().length === 0) {
        WarningAlert(`WARN_NotSelectOnGrid`);
        return false;
    }
    let obj = $bomGrid.getRowData(bomId);

    let formData = new FormData();
    let files = $("#mBomFile").get(0).files;
    formData.append("file", files[0]);

    formData.append("MoldId", MoldId);
    formData.append("BomId", BomId);
    formData.append("FileUpload", obj.FileUpload);

    $.ajax({
        url: `/Bom/ModifyBom`,
        type: "PUT",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $bomGrid.setRowData(response.Data.BomId, response.Data);
              
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

async function ShowImageModal()
{
    if (bomId != 0) {

        
        let obj = $bomGrid.getRowData(bomId);
     
        //Show img
        if (obj.FileUpload != null) {

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
            image.src = "../Img/Standard/Information/Bom/" + obj.FileUpload;
            image.attr = obj.FileUpload;
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

