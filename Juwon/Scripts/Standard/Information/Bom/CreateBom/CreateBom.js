async function readURLCreate(input) {
   
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {

            var el = document.querySelector('#img1');
            if (el != null) {
                el.remove();
              
            }
            let ListAceptFile = ["IMAGE/JPG", "IMAGE/JPE", "IMAGE/JPEG", "IMAGE/BMP", "IMAGE/GIF", "IMAGE/PNG"];
            var extension = input.files[0].type.toUpperCase()
           
            if (ListAceptFile.includes(extension)) {
                const image = document.createElement('img');
                image.style.width = "200px";
                image.style.height = "200px";
                image.id = "img1";
                image.src = e.target.result;
                image.attr = e.target.result;
                document.querySelector('#showImg').appendChild(image)
            }
        };

        reader.readAsDataURL(input.files[0]);
        $("#CloseImg").removeClass("hidden");
    }
}

async function ClickCloseCreate() {
    var el = document.querySelector('#img1');
    if (el != null) {
        el.remove();
        $("#CloseImg").addClass("hidden");
        document.getElementById("cBomFile").value = "";

    }
}

async function CreateBom() {
   
    let MoldId = cMoldId.value;
  
  
    if (!MoldId || MoldId.trim().length  === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }
  
    let formData = new FormData();
    let files = $("#cBomFile").get(0).files;
    formData.append("file", files[0]);
  
    formData.append("MoldId", MoldId);

    $.ajax({
        url: `/Bom/CreateBom`,
        type: "POST",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                
                $bomGrid.addRowData(response.Data.BomId, response.Data, `first`);
                $bomGrid.setRowData(response.Data.BomId, false, { background: '#39FF14' });

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