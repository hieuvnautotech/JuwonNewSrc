function LoadApp() {
    $.getJSON(`/API/LoadApp`, function (response) {
        if (response.Data && response.Data.length > 0) {
            $.each(response.Data, function (key, item) {
                if (item.Type === 'MMS') {
                    $('#appMMS').attr({
                        //target: '_blank',
                        download: item.Name,
                        href: item.UrlApp
                    });
                    //break;
                }
                if (item.Type === 'WMS') {
                    $('#appWMS').attr({
                        //target: '_blank',
                        download: item.Name,
                        href: item.UrlApp
                    });
                }
            });
        }
        else {
            
        }

        return;
    })
};

$(document).ready(function () {
    localStorage.removeItem(`userSession`);
    localStorage.removeItem(`ml_BtnCreate`);
    localStorage.removeItem(`ml_BtnModify`);
    localStorage.removeItem(`ml_BtnBlock`);
    localStorage.removeItem(`ml_BtnDelete`);
    localStorage.removeItem(`ml_BtnSub`);
    localStorage.removeItem(`ml_BtnDetail`);
    localStorage.removeItem(`ml_BtnStart`);
    localStorage.removeItem(`ml_BtnPacking`);
    LoadApp();
});