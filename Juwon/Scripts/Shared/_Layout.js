const MONTH_ARR = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
const FULLMONTH_ARR = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
const DAY_ARR = ["1<sup>st</sup>", "2<sup>nd</sup>", "3<sup>rd</sup>", "4<sup>th</sup>", "5<sup>th</sup>", "6<sup>th</sup>", "7<sup>th</sup>", "8<sup>th</sup>", "9<sup>th</sup>", "10<sup>th</sup>", "11<sup>th</sup>", "12<sup>th</sup>", "13<sup>th</sup>", "14<sup>th</sup>", "15<sup>th</sup>", "16<sup>th</sup>", "17<sup>th</sup>", "18<sup>th</sup>", "19<sup>th</sup>", "20<sup>th</sup>", "21<sup>st</sup>", "22<sup>nd</sup>", "23<sup>rd</sup>", "24<sup>th</sup>", "25<sup>th</sup>", "26<sup>th</sup>", "27<sup>th</sup>", "28<sup>th</sup>", "29<sup>th</sup>", "30<sup>th</sup>", "31<sup>st</sup>"];

function SuccessAlert(content)
{
    let output = undefined;
    $.ajax({
        url: `/Base/GetResourceValue`,
        type: `GET`,
        datatype: `json`,
        data: {
            input: content
        }
    }).done(function (response)
    {
        if (response !== `notExisted`)
        {
            output = response;
            toastr.success(output);
        }
        else
        {
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
    }).fail(function ()
    {
        output = `Success.`;
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

function ErrorAlert(content)
{
    let output = undefined;
    $.ajax({
        url: `/Base/GetResourceValue`,
        type: `GET`,
        datatype: `json`,
        data: {
            input: content
        }
    })
        .done(function (response)
        {
            if (response !== `notExisted`)
            {
                output = response;
                toastr.error(output);
            }
            else
            {
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
        })
        .fail(function ()
        {
            output = `Error.`;
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

function WarningAlert(content)
{
    let output = undefined;
    $.ajax({
        url: `/Base/GetResourceValue`,
        type: `GET`,
        datatype: `json`,
        data: {
            input: content
        }
    })
        .done(function (response)
        {
            if (response !== `notExisted`)
            {
                output = response;
                toastr.warning(output);
            }
            else
            {
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
        })
        .fail(function ()
        {
            output = `Warning`;
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

$(function ()
{
    setInterval(KeepSessionAlive, (1000 * 60 * 5));
    //$.unblockUI();
});

function KeepSessionAlive()
{
    $.post("/KeepSessionAlive.ashx", null, function (){});
}

//SHOW ACTIVE STATUS
function ShowActiveStatus(cellvalue, options, rowObject)
{
    return rowObject.Active === true ? "YES" : "NO";
}

//SHOW HANGER STATUS
function ShowHangerStatus(cellvalue, options, rowObject)
{
    return rowObject.HasHanger === true ? "YES" : "NO";
}

//REMOVE HTML TAG
function RemoveHtmlTag(e)
{
    return e.replace(/<\/?[^>]+(>|$)/g, "")
}

$(`#BtnChangePass`).on(`click`, function () {
    let Password=  $(`#Password`).val();
    let RepeatPassword = $(`#RepeatPassword`).val();

    let model = {
        Password: Password,
        RepeatPassword: RepeatPassword
    }
    $.ajax({
        url: `/Login/ChangePassword`,
        type: "POST",
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        traditonal: true,
        cache: false,
    }).done(function (response) {
        if (response.IsSuccess) {
            SuccessAlert(response.ResponseMessage);
            $('#ChangePasswordModal').modal('toggle');
            return true;
        }
        else {
            ErrorAlert(response.ResponseMessage);
            return false;
        }
    })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return false;
        });

});

async function autocomplete(inp, arr) {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });

    async function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        await removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    async function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    async function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}