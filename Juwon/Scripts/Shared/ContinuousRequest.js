$('#i_EN').click(function () {
    var menuCode = $(`#keepMenuCode`).val();
    var menuCodeLv3 = ``;
    if (!menuCode || menuCode == 'undefined') {
        menuCodeLv3 = $('div>').find('a.collapse-item.active').data('code');
        $.get(`/StandardDocument/Search2?menuCode=${menuCodeLv3}&languageCode=EN`, function (response) {
            //console.log(response)
            if (response.Data && response.HttpResponseCode != 100) {
                if (response.Data[0].Content) {
                    var html = ``;
                    html += `<h4>Name:  ${response.Data[0].Name}</h4>`;
                    html += `<h4>Full Direction:  ${response.Data[0].FullName}</h4>`;
                    //html += `<h5>Writer: ${response.result[0].Writer}</h5>`;
                    //html += `<h5>Description: ${response.result[0].Description}</h5>`;
                    html += `<div id="content"><h4>Content: </h4>${response.Data[0].Content}</div>`;


                    //html += '<div class="col-md-12">';
                    //html += '<table class="table table-bordered ">';
                    //html += '<tbody>';
                    //html += '<tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Name</th>';
                    //html += `<td class="col-md-12 border border-secondary"> ${response.result[0].Name}</td>`;
                    //html += '</tr>';
                    //html += ' <tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Full Direction</th>';
                    //html += `<td class="col-md-12 border border-secondary">${response.result[0].FullName}</td>`;
                    //html += '</tr>';
                    //html += '<tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Content</th>';
                    //html += `<td id="content" class="col-md-12 border border-secondary">${response.result[0].Content}</td>`;
                    //html += ' </tr>';
                    //html += '</tbody>';
                    //html += '</table>';
                    //html += '</div>';

                    $(`#info_detail`).html(html);
                    $(`.Manual_detail_page`).dialog(`open`);
                }
            }
            else {
                ErrorAlert("No data");
            }
        });
    }
    else {
        $.get(`/StandardDocument/Search2?menuCode=${menuCode}&languageCode=EN`, function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                if (response) {
                    var html = ``;

                    html += `<h4>Name:  ${response.Data[0].Name}</h4>`;
                    html += `<h4>Full Direction:  ${response.Data[0].FullName}</h4>`;
                    //html += `<h5>${response.result[0].Writer}</h5>`;
                    //html += `<h5>${response.result[0].Description}</h5>`;
                    html += `<div id="content"><h4>Content: </h4>${response.Data[0].Content}</div>`;

                    //html += '<div class="col-md-12">';
                    //html += '<table class="table table-bordered ">';
                    //html += '<tbody>';
                    //html += '<tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Name</th>';
                    //html += `<td class="col-md-12 border border-secondary"> ${response.result[0].Name}</td>`;
                    //html += '</tr>';
                    //html += ' <tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Full Direction</th>';
                    //html += `<td class="col-md-12 border border-secondary">${response.result[0].FullName}</td>`;
                    //html += '</tr>';
                    //html += '<tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Content</th>';
                    //html += `<td id="content" class="col-md-12 border border-secondary">${response.result[0].Content}</td>`;
                    //html += ' </tr>';
                    //html += '</tbody>';
                    //html += '</table>';
                    //html += '</div>';

                    $(`#info_detail`).html(html);
                    $(`.Manual_detail_page`).dialog(`open`);
                }
                else {
                    ErrorAlert("No data");
                }
            }
        });
    }
});

$('#i_VN').click(function () {
    var menuCode = $(`#keepMenuCode`).val();
    var menuCodeLv3 = ``;
    if (!menuCode || menuCode == 'undefined') {
        menuCodeLv3 = $('div>').find('a.collapse-item.active').data('code');
        //menuCodeLv3 = $('aside.main-sidebar div.sidebar nav.side-menu ul.side-menu-item li.menu-open ul.nav-item-item li.nav-item').find('a.active').data('mn_cd');
        $.get(`/StandardDocument/Search2?menuCode=${menuCodeLv3}&languageCode=VN`, function (response) {
            //console.log(response)
            if (response.Data && response.HttpResponseCode != 100) {
                if (response.Data[0].Content) {
                    var html = ``;

                    html += `<h4>Name:  ${response.Data[0].Name}</h4>`;
                    html += `<h4>Full Direction:  ${response.Data[0].FullName}</h4>`;
                    //html += `<h5>${response.result[0].Writer}</h5>`;
                    //html += `<h5>${response.result[0].Description}</h5>`;
                    html += `<div id="content"><h4>Content: </h4>${response.Data[0].Content}</div>`;


                    //html += '<div class="col-md-12">';
                    //html += '<table class="table table-bordered ">';
                    //html += '<tbody>';
                    //html += '<tr>';

                    //html += `<td class="col-md-12 border border-secondary" scope="col" > ${response.result[0].Name}</td>`;
                    //html += '</tr>';
                    //html += ' <tr>';
                    ////html += '<th class="col-md-0 border border-secondary" scope="col">Full Direction</th>';
                    //html += `<td class="col-md-12 border border-secondary" scope="col" >${response.result[0].FullName}</td>`;
                    //html += '</tr>';
                    //html += '<tr>';
                    ////html += '<th class="col-md-0 border border-secondary" scope="col">Content</th>';
                    //html += `<td style="width: 200px" id="content" class="col-md-12 border border-secondary" scope="col">${response.result[0].Content}</td>`;
                    //html += ' </tr>';
                    //html += '</tbody>';
                    //html += '</table>';
                    //html += '</div>';

                    $(`#info_detail`).html(html);
                    $(`.Manual_detail_page`).dialog(`open`);
                }
            }
            else {
                ErrorAlert("No data");
            }
        });
    }
    else {
        $.get(`/StandardDocument/Search2?menuCode=${menuCode}&languageCode=VN`, function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                if (response) {
                    var html = ``;

                    html += `<h4>Name:  ${response.Data[0].Name}</h4>`;
                    html += `<h4>Full Direction:  ${response.Data[0].FullName}</h4>`;
                    //html += `<h5>${response.result[0].Writer}</h5>`;
                    //html += `<h5>${response.result[0].Description}</h5>`;
                    html += `<div id="content"><h4>Content: </h4>${response.Data[0].Content}</div>`;


                    //html += '<div class="col-md-12">';
                    //html += '<table class="table table-bordered ">';
                    //html += '<tbody>';
                    //html += '<tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Name</th>';
                    //html += `<td class="col-md-12 border border-secondary"> ${response.result[0].Name}</td>`;
                    //html += '</tr>';
                    //html += ' <tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Full Direction</th>';
                    //html += `<td class="col-md-12 border border-secondary">${response.result[0].FullName}</td>`;
                    //html += '</tr>';
                    //html += '<tr>';
                    //html += '<th class="col-md-0 border border-secondary" scope="col">Content</th>';
                    //html += `<td style="width: 200px" id="content" class="col-md-12 border border-secondary">${response.result[0].Content}</td>`;
                    //html += ' </tr>';
                    //html += '</tbody>';
                    //html += '</table>';
                    //html += '</div>';

                    $(`#info_detail`).html(html);
                    $(`.Manual_detail_page`).dialog(`open`);
                }
            }
            else {
                ErrorAlert("No data");
            }
        });
    }
});

$(`.Manual_detail_page`).dialog({
    width: 1200,
    height: 900,
    maxWidth: `100%`,
    maxHeight: 800,
    minWidth: `50%`,
    minHeight: 400,
    resizable: false,
    fluid: true,
    modal: true,
    autoOpen: false,
    classes: {
        "ui-dialog": "ui-dialog",
        "ui-dialog-titlebar": "ui-dialog ui-dialog-titlebar-sm",
        "ui-dialog-titlebar-close": "visibility: visible;",
    },
    resize: function (event, ui) {
        $(`.ui-dialog-content`).addClass(`m-0 p-0`);
    },

});