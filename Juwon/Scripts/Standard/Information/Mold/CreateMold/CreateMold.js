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
        document.getElementById("cMoldFile").value = "";

    }
}
function CheckSpace(event) {
    if (event.which == 32) {
        event.preventDefault();
        return false;
    }
}
//async function CreateMold() {
//    let moldCode = cMoldCode.value;
//    let moldName = cMoldName.value;
//    let UnitPrice = cMoldUnitPrice.value;
//    let cCurrency = document.getElementById("cCurrency");
//    let Currency = cCurrency.value;

//    if (moldCode.length != 9) {
//        WarningAlert(`ERROR_MoldCode`);
//        return false;
//    }
//    if (!moldName || moldName.trim().length === 0) {
//        WarningAlert(`ERROR_FullFillTheForm`);
//        return false;
//    }

//    let obj = {
//        moldCode,
//        moldName,
//        UnitPrice,
//        Currency,

//    }

//    $.ajax({
//        url: `/Mold/CreateMold`,
//        type: "POST",
//        data: JSON.stringify(obj),
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        traditonal: true,
//        cache: false,
//    })
//        .done(function (response) {
//            if (response.Data && response.HttpResponseCode != 100) {
//                $moldGrid.addRowData(response.Data.MoldId, response.Data, `first`);
//                $moldGrid.setRowData(response.Data.MoldId, false, { background: '#39FF14' });
//                SuccessAlert(response.ResponseMessage);
//                $(`#confirmToCreateModal`).modal(`hide`);
//                $(`#createModal`).modal(`hide`);
//                return true;
//            }
//            else {
//                ErrorAlert(response.ResponseMessage);
//                $(`#confirmToCreateModal`).modal(`hide`);
//                return false;
//            }
//        })

//        .fail(function () {
//            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
//            $(`#confirmToCreateModal`).modal(`hide`);
//            return false;
//        });
//}


async function CreateMold() {

    //let MoldId = cMoldId.value;
    let MoldCode = cMoldCode.value;
    let MoldName = cMoldName.value;
    let UnitPrice = cMoldUnitPrice.value;
    let cCurrency = document.getElementById("cCurrency");
    let Currency = cCurrency.value;

    if (MoldCode.length != 9) {
        WarningAlert(`ERROR_MoldCode`);
        return false;
    }
    if (!MoldName || MoldName.trim().length === 0) {
        WarningAlert(`ERROR_FullFillTheForm`);
        return false;
    }

    //if (!MoldId || MoldId.trim().length === 0) {
    //    WarningAlert(`ERROR_FullFillTheForm`);
    //    return false;
    //}

    let formData = new FormData();
    let files = $("#cMoldFile").get(0).files;
    formData.append("file", files[0]);

    //formData.append("MoldId", MoldId);
    formData.append("MoldCode", MoldCode);
    formData.append("MoldName", MoldName);
    formData.append("UnitPrice", UnitPrice);
    formData.append("Currency", Currency);


    $.ajax({
        url: `/Mold/CreateMold`,
        type: "POST",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {

                $moldGrid.addRowData(response.Data.MoldId, response.Data, `first`);
                $moldGrid.setRowData(response.Data.MoldId, false, { background: '#39FF14' });

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