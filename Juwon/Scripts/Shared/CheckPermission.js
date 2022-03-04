var userSession = undefined;
var permissions = undefined;
var ml_BtnCreate = undefined;
var ml_BtnModify = undefined;
var ml_BtnBlock = undefined;
var ml_BtnDelete = undefined;
var ml_BtnSub = undefined;
var ml_BtnDetail = undefined;
var ml_BtnStart = undefined;
var ml_BtnPacking = undefined;

function GetSessionInfo()
{
    userSession = JSON.parse(window.localStorage.getItem(`userSession`));
    if (!userSession)
    {
        $.ajax({
            url: `/LoginBase/GetSessionData`,
            type: `GET`,
            datatype: `json`,
            data: {},
            async: false
        })
            .done(function (response)
            {
                window.localStorage.setItem("userSession", JSON.stringify(response));
                userSession = JSON.parse(window.localStorage.getItem(`userSession`));
                permissions = userSession.Permissions;
            });
    }
    else
    {
        permissions = userSession.Permissions;
    }

    //$.ajax({
    //    url: `/LoginBase/GetSessionData`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {},
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        window.localStorage.setItem("userSession", JSON.stringify(response));
    //        userSession = JSON.parse(window.localStorage.getItem(`userSession`));
    //        permissions = userSession.Permissions;
    //    });

}
GetSessionInfo();

function returnMLValueBtn_Create(e)
{
    ml_BtnCreate = JSON.parse(window.localStorage.getItem(`ml_BtnCreate`));
    if (!ml_BtnCreate)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnCreate`, JSON.stringify(response));
                    ml_BtnCreate = JSON.parse(window.localStorage.getItem(`ml_BtnCreate`));
                }
                else
                {
                    ml_BtnCreate = e;
                }
            })
            .fail(function ()
            {
                ml_BtnCreate = e;
            });
    }
    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnCreate = response;
    //        }
    //    });
}

function returnMLValueBtn_Modify(e)
{
    ml_BtnModify = JSON.parse(window.localStorage.getItem(`ml_BtnModify`));
    if (!ml_BtnModify)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnModify`, JSON.stringify(response));
                    ml_BtnModify = JSON.parse(window.localStorage.getItem(`ml_BtnModify`));
                }
                else
                {
                    ml_BtnModify = e;
                }
            })
            .fail(function ()
            {
                ml_BtnModify = e;
            });
    }

    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnModify = response;
    //        }
    //    });

}

function returnMLValueBtn_Block(e)
{
    ml_BtnBlock = JSON.parse(window.localStorage.getItem(`ml_BtnBlock`));
    if (!ml_BtnBlock)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnBlock`, JSON.stringify(response));
                    ml_BtnBlock = JSON.parse(window.localStorage.getItem(`ml_BtnBlock`));
                }
                else
                {
                    ml_BtnBlock = e;
                }
            })
            .fail(function ()
            {
                ml_BtnBlock = e;
            });
    }

    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnBlock = response;
    //        }
    //    });

}

function returnMLValueBtn_Delete(e)
{
    ml_BtnDelete = JSON.parse(window.localStorage.getItem(`ml_BtnDelete`));
    if (!ml_BtnDelete)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnDelete`, JSON.stringify(response));
                    ml_BtnDelete = JSON.parse(window.localStorage.getItem(`ml_BtnDelete`));
                }
                else
                {
                    ml_BtnDelete = e;
                }
            })
            .fail(function ()
            {
                ml_BtnDelete = e;
            });
    }

    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnDelete = response;
    //        }
    //    });
}

function returnMLValueBtn_Sub(e)
{
    ml_BtnSub = JSON.parse(window.localStorage.getItem(`ml_BtnSub`));
    if (!ml_BtnSub)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnSub`, JSON.stringify(response));
                    ml_BtnSub = JSON.parse(window.localStorage.getItem(`ml_BtnSub`));
                }
                else
                {
                    ml_BtnSub = e;
                }
            })
            .fail(function ()
            {
                ml_BtnSub = e;
            });
    }

    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnSub = response;
    //        }
    //    });
}

function returnMLValueBtn_Detail(e)
{

    ml_BtnDetail = JSON.parse(window.localStorage.getItem(`ml_BtnDetail`));
    if (!ml_BtnDetail)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnDetail`, JSON.stringify(response));
                    ml_BtnDetail = JSON.parse(window.localStorage.getItem(`ml_BtnDetail`));
                }
                else
                {
                    ml_BtnDetail = e;
                }
            })
            .fail(function ()
            {
                ml_BtnDetail = e;
            });
    }

    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnDetail = response;
    //        }
    //    });
}

function returnMLValueBtn_Start(e)
{
    ml_BtnStart = JSON.parse(window.localStorage.getItem(`ml_BtnStart`));
    if (!ml_BtnStart)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnStart`, JSON.stringify(response));
                    ml_BtnStart = JSON.parse(window.localStorage.getItem(`ml_BtnStart`));
                }
                else
                {
                    ml_BtnStart = e;
                }
            })
            .fail(function ()
            {
                ml_BtnStart = e;
            });
    }

    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnStart = response;
    //        }
    //    });
}

function returnMLValueBtn_Packing(e)
{
    ml_BtnPacking = JSON.parse(window.localStorage.getItem(`ml_BtnPacking`));
    if (!ml_BtnPacking)
    {
        $.ajax({
            url: `/Base/GetResourceValue`,
            type: `GET`,
            datatype: `json`,
            data: {
                input: e
            },
            async: false
        })
            .done(function (response)
            {
                if (response !== `notExisted`)
                {
                    window.localStorage.setItem(`ml_BtnPacking`, JSON.stringify(response));
                    ml_BtnPacking = JSON.parse(window.localStorage.getItem(`ml_BtnPacking`));
                }
                else
                {
                    ml_BtnPacking = e;
                }
            })
            .fail(function ()
            {
                ml_BtnPacking = e;
            });
    }

    //$.ajax({
    //    url: `/Base/GetResourceValue`,
    //    type: `GET`,
    //    datatype: `json`,
    //    data: {
    //        input: e
    //    },
    //    async: false
    //})
    //    .done(function (response)
    //    {
    //        if (response !== `notExisted`)
    //        {
    //            ml_BtnPacking = response;
    //        }
    //    });
}

function CheckPermissions()
{
    $(`button`).each(function ()
    {
        let classList = $(this).attr(`class`);
        let classArr = classList.split(/\s+/);
        let n = classArr.includes(`ui-dialog-titlebar-close`);
        let m = classArr.includes(`rounded-circle`);
        let iSearch = classArr.includes(`iBtnSearch`);
        if (!permissions)
        {
            $(this).remove();
        }
        else
        {
            let flag = classArr.some(x => permissions.indexOf(x) !== -1);
            if (!flag && !n && !iSearch & !m)
            {
                $(this).remove();
            }
        }

    });
}

function CheckDivPermissions()
{
    $(`div .divPermission`).each(function ()
    {
        let classList = $(this).attr(`class`);
        let classArr = classList.split(/\s+/);
        if (!permissions)
        {
            $(this).remove();
        }
        else
        {
            let flag = classArr.some(x => permissions.indexOf(x) !== -1);
            if (!flag)
            {
                $(this).remove();
            }
        }

    });
}

$(document).ready(function ()
{
    //userSession = JSON.parse(window.localStorage.getItem(`userSession`));
    //if (!userSession)
    //{
    //    $.ajax({
    //        url: "/LoginBase/GetSessionData",
    //        type: "GET",
    //        datatype: "json",
    //        data: {},
    //        success: function (n)
    //        {
    //            window.localStorage.setItem("userSession", JSON.stringify(n));
    //            userSession = JSON.parse(window.localStorage.getItem(`userSession`));
    //            permissions = userSession.Permissions;
    //        },
    //        error: function () { }
    //    });
    //}

    CheckDivPermissions();
    CheckPermissions();
    returnMLValueBtn_Create(`Btn_Create`);
    returnMLValueBtn_Modify(`Btn_Modify`);
    returnMLValueBtn_Block(`Btn_Block`);
    returnMLValueBtn_Delete(`Btn_Delete`);
    returnMLValueBtn_Sub(`Btn_Sub`);
    returnMLValueBtn_Detail(`Btn_Detail`);
    returnMLValueBtn_Start(`Btn_Start`);
    returnMLValueBtn_Packing(`Btn_Packing`);
});

$(`#ddlCulture`).on(`change`, function ()
{
    localStorage.removeItem(`userSession`);
    localStorage.removeItem(`ml_BtnCreate`);
    localStorage.removeItem(`ml_BtnModify`);
    localStorage.removeItem(`ml_BtnBlock`);
    localStorage.removeItem(`ml_BtnDelete`);
    localStorage.removeItem(`ml_BtnSub`);
    localStorage.removeItem(`ml_BtnDetail`);
    localStorage.removeItem(`ml_BtnStart`);
    localStorage.removeItem(`ml_BtnPacking`);
})

function GetCurrentDateFormat()
{
    let today = new Date();

    let date = String(today.getDate()).padStart(2, '0');
    let month = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    let year = today.getFullYear();

    today = `${year}-${month}-${date}`;
    return today;
}

function GetCurrentDateTimeFormat()
{
    let today = new Date();

    let date = String(today.getDate()).padStart(2, '0');
    let month = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    let year = today.getFullYear();

    let hour = String(today.getHours()).padStart(2, '0');
    let minute = String(today.getMinutes()).padStart(2, '0');
    let second = String(today.getSeconds()).padStart(2, '0');

    today = `${year}-${month}-${date} ${hour}:${minute}:${second}`;
    return today;
}

function DateTimeToString(e)
{
    //let today = new Date();

    let date = String(e.getDate()).padStart(2, '0');
    let month = String(e.getMonth() + 1).padStart(2, '0'); //January is 0!
    let year = e.getFullYear();

    let hour = String(e.getHours()).padStart(2, '0');
    let minute = String(e.getMinutes()).padStart(2, '0');
    let second = String(e.getSeconds()).padStart(2, '0');

    let str = `${year}-${month}-${date} ${hour}:${minute}:${second}`;
    return str;
}

function DateToString(e)
{
    //let today = new Date();

    let date = String(e.getDate()).padStart(2, '0');
    let month = String(e.getMonth() + 1).padStart(2, '0'); //January is 0!
    let year = e.getFullYear();

    let str = `${year}-${month}-${date}`;
    return str;
}

function DateToStringLotCode()
{
    let today = new Date();

    let date = String(today.getDate()).padStart(2, '0');
    let month = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    let year = today.getFullYear();

    let y = year.toString().slice(-2);

    let hour = String(today.getHours()).padStart(2, '0');
    let minute = String(today.getMinutes()).padStart(2, '0');
    let second = String(today.getSeconds()).padStart(2, '0');

    let str = `${y}${month}${date}${hour}${minute}${second}`;
    return str;
}

function StringLength4(e)
{
    switch (e.toString().length)
    {
        case 1:
            return `000${e.toString()}`;
        case 2:
            return `00${e.toString()}`;
        case 3:
            return `0${e.toString()}`;
        default:
            return e.toString();
    }
}

function ThousandsSeperated(x)
{
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function IsBeginsWith(check, str)
{
    return (str.substr(0, check.length) == check);
}

function checkProperties(obj)
{
    for (var key in obj)
    {
        if (obj[key] === null || obj[key] === `` || obj[key] === undefined)
            return false;
    }
    return true;
}

function isInt(value)
{
    return !isNaN(value) &&
        parseInt(Number(value)) == value &&
        !isNaN(parseInt(value, 10));
}
