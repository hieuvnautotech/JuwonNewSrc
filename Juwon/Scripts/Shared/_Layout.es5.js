"use strict";

var MONTH_ARR = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var FULLMONTH_ARR = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var DAY_ARR = ["1<sup>st</sup>", "2<sup>nd</sup>", "3<sup>rd</sup>", "4<sup>th</sup>", "5<sup>th</sup>", "6<sup>th</sup>", "7<sup>th</sup>", "8<sup>th</sup>", "9<sup>th</sup>", "10<sup>th</sup>", "11<sup>th</sup>", "12<sup>th</sup>", "13<sup>th</sup>", "14<sup>th</sup>", "15<sup>th</sup>", "16<sup>th</sup>", "17<sup>th</sup>", "18<sup>th</sup>", "19<sup>th</sup>", "20<sup>th</sup>", "21<sup>st</sup>", "22<sup>nd</sup>", "23<sup>rd</sup>", "24<sup>th</sup>", "25<sup>th</sup>", "26<sup>th</sup>", "27<sup>th</sup>", "28<sup>th</sup>", "29<sup>th</sup>", "30<sup>th</sup>", "31<sup>st</sup>"];

function SuccessAlert(content) {
    var output = undefined;
    $.ajax({
        url: "/Base/GetResourceValue",
        type: "GET",
        datatype: "json",
        data: {
            input: content
        }
    }).done(function (response) {
        if (response !== "notExisted") {
            output = response;
            toastr.success(output);
        } else {
            toastr.success(content);
        }
        toastr.options = {
            "closeButton": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }).fail(function () {
        output = "Success.";
        toastr.success(output);
        toastr.options = {
            "closeButton": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    });
}

function ErrorAlert(content) {
    var output = undefined;
    $.ajax({
        url: "/Base/GetResourceValue",
        type: "GET",
        datatype: "json",
        data: {
            input: content
        }
    }).done(function (response) {
        if (response !== "notExisted") {
            output = response;
            toastr.error(output);
        } else {
            toastr.error(content);
        }
        toastr.options = {
            "closeButton": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }).fail(function () {
        output = "Error.";
        toastr.error(output, 'Error');
        toastr.options = {
            "closeButton": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    });
}

function WarningAlert(content) {
    var output = undefined;
    $.ajax({
        url: "/Base/GetResourceValue",
        type: "GET",
        datatype: "json",
        data: {
            input: content
        }
    }).done(function (response) {
        if (response !== "notExisted") {
            output = response;
            toastr.warning(output);
        } else {
            toastr.warning(content);
        }
        toastr.options = {
            "closeButton": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }).fail(function () {
        output = "Warning";
        toastr.warning(output);
        toastr.options = {
            "closeButton": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-bottom-left",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "3000",
            "hideDuration": "3000",
            "timeOut": "3000",
            "extendedTimeOut": "3000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    });
}

$(function () {
    setInterval(KeepSessionAlive, 1000 * 60 * 5);
    //$.unblockUI();
});

function KeepSessionAlive() {
    $.post("/KeepSessionAlive.ashx", null, function () {});
}

//SHOW ACTIVE STATUS
function ShowActiveStatus(cellvalue, options, rowObject) {
    return rowObject.Active === true ? "YES" : "NO";
}

//SHOW HANGER STATUS
function ShowHangerStatus(cellvalue, options, rowObject) {
    return rowObject.HasHanger === true ? "YES" : "NO";
}

//REMOVE HTML TAG
function RemoveHtmlTag(e) {
    return e.replace(/<\/?[^>]+(>|$)/g, "");
}

$("#BtnChangePass").on("click", function () {
    var Password = $("#Password").val();
    var RepeatPassword = $("#RepeatPassword").val();

    var model = {
        Password: Password,
        RepeatPassword: RepeatPassword
    };
    $.ajax({
        url: "/Login/ChangePassword",
        type: "POST",
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false
    }).done(function (response) {
        if (response.flag) {
            SuccessAlert(response.message);
            $('#ChangePasswordModal').modal('toggle');
            return true;
        } else {
            ErrorAlert(response.message);
            return false;
        }
    }).fail(function () {
        ErrorAlert("Lỗi hệ thống - Vui lòng liên hệ IT.");
        return false;
    });
});

