
// CREATE
$(`#openCreateDialogBtn`).on(`click`, function () {
    $('#dialogCreate').dialog('open');
});
$(`#dialogCreate`).dialog({
    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "900px");
        //$(this).css("maxheight", "600px");
    },

    //width: 900,
    //height: 450,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    //classes: {
    //    "ui-dialog-titlebar-close": "visibility: hidden",
    //},
    open: function (event, ui) {
        GetMenuCategory();
    },
    close: function (event, ui) {
        $(`#createName`).val(``);
    },
});
$(`#btnCancelCreate`).on(`click`, function () {
    $('#dialogCreate').dialog('close');
});
$(`#btnCreate`).on(`click`, function () {
    let sel = document.getElementById(`createPrimaryMenu`);
    let primaryMenu = sel.options[sel.selectedIndex].text;
    let secondaryMenu = $(`#createSecondaryMenu`).val() === null ? `` : $(`#createSecondaryMenu`).val().trim();
    let tertiaryMenu = $(`#createTertiaryMenu`).val() === null ? `` : $(`#createTertiaryMenu`).val().trim();
    let menuLevel = $(`#createMenuLevel`).val() === null ? 2 : parseInt($(`#createMenuLevel`).val().trim());

    //let primaryMenuOrder = $(`#createPrimaryMenuOrder`).val() === null ? `` : parseInt($(`#createPrimaryMenuOrder`).val().trim());
    //let secondaryMenuOrder = $(`#createSecondaryMenuOrder`).val() === null ? `` : parseInt($(`#createSecondaryMenuOrder`).val().trim());
    //let tertiaryMenuOrder = $(`#createTertiaryMenuOrder`).val() === null ? `` : parseInt($(`#createTertiaryMenuOrder`).val().trim());
    let link = $(`#createLink`).val() === null ? `` : $(`#createLink`).val().trim();
    let menuCategory = $(`#createPrimaryMenu`).val() === null ? `` : $(`#createPrimaryMenu`).val().trim();

    if (primaryMenu == ``) {
        WarningAlert(`ERROR_FullFillTheForm`);
    }
    else {
        var menu = new Object();
        menu.PrimaryMenu = primaryMenu;
        menu.SecondaryMenu = secondaryMenu;
        menu.TertiaryMenu = tertiaryMenu;
        menu.MenuLevel = menuLevel;
        //menu.MenuOrderly = primaryMenuOrder;
        //menu.MenuLevel2Orderly = secondaryMenuOrder;
        //menu.MenuLevel3Orderly = tertiaryMenuOrder;
        menu.Link = link;
        menu.MenuCategory = menuCategory;

        $.ajax({
            url: `/Menu/Create`,
            type: `POST`,
            data: JSON.stringify(menu),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.flag) {
                    $grid.addRowData(response.result.ID, response.result, "first");
                    $grid.jqGrid('setRowData', response.result.ID, false, { background: '#39FF14' });
                    $('#dialogCreate').dialog('close');
                    SuccessAlert(response.message);
                }
                else {
                    ErrorAlert(response.message);
                }
                return;
            })

            .fail(function () {
                ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
                return;
            });
    }
    return;
});

//MODIFY
var menuID;
var menuLevel;
function Modify(e) {
    let id = e.attr(`data-id`);

    let rowData = $grid.getRowData(id);
    menuID = rowData.ID;
    menuLevel = rowData.MenuLevel;
    $(`#modifyPrimaryMenu`).val(rowData.MenuCategory);
    $(`#modifyPrimaryMenuOrder`).val(rowData.MenuOrderly);
    $(`#modifySecondaryMenu`).val(rowData.SecondaryMenu);
    $(`#modifySecondaryMenuOrder`).val(rowData.MenuLevel2Orderly);
    $(`#modifyTertiaryMenu`).val(rowData.TertiaryMenu);
    $(`#modifyTertiaryMenuOrder`).val(rowData.MenuLevel3Orderly);
    $(`#modifyMenuLevel`).val(rowData.MenuLevel);
    $(`#modifyLink`).val(rowData.Link);
    if (rowData.Active == `true`) {
        $(`#modifyActive`).val(`1`);
    }

    $(`#dialogModify`).dialog(`open`);
}
$(`#dialogModify`).dialog({
    //width: 1200,
    //height: 480,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    //classes: {
    //    "ui-dialog-titlebar-close": "visibility: hidden",
    //},
    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "1200px");
        //$(this).css("maxheight", "600px");
    },

    open: function (event, ui) {
        switch (parseInt(menuLevel)) {
            case 1:
                $(`#modifyPrimaryMenu`).prop(`disabled`, true);
                $(`#modifyPrimaryMenuOrder`).prop(`readonly`, false);
                $(`#modifySecondaryMenu`).prop(`readonly`, true);
                $(`#modifySecondaryMenuOrder`).prop(`readonly`, true);
                $(`#modifyTertiaryMenu`).prop(`readonly`, true);
                $(`#modifyTertiaryMenuOrder`).prop(`readonly`, true);
                $(`#modifyMenuLevel`).prop(`readonly`, true);
                $(`#modifyLink`).prop(`readonly`, true);
                break;
            case 2:
                $(`#modifyPrimaryMenu`).prop(`disabled`, true);
                $(`#modifyPrimaryMenuOrder`).prop(`readonly`, true);
                $(`#modifySecondaryMenu`).prop(`readonly`, true);
                $(`#modifySecondaryMenuOrder`).prop(`readonly`, false);
                $(`#modifyTertiaryMenu`).prop(`readonly`, true);
                $(`#modifyTertiaryMenuOrder`).prop(`readonly`, true);
                $(`#modifyMenuLevel`).prop(`readonly`, true);
                $(`#modifyLink`).prop(`readonly`, true);
                break;
            default:
                $(`#modifyPrimaryMenu`).prop(`disabled`, true);
                $(`#modifyPrimaryMenuOrder`).prop(`readonly`, true);
                $(`#modifySecondaryMenu`).prop(`readonly`, true);
                $(`#modifySecondaryMenuOrder`).prop(`readonly`, true);
                $(`#modifyTertiaryMenu`).prop(`readonly`, false);
                $(`#modifyTertiaryMenuOrder`).prop(`readonly`, false);
                $(`#modifyMenuLevel`).prop(`readonly`, true);
                $(`#modifyLink`).prop(`readonly`, false);
                break;
        }
    },
    close: function (event, ui) {
    },
});
$(`#btnCancelModify`).on(`click`, function () {
    $(`#dialogModify`).dialog(`close`);
});
$(`#btnModify`).on(`click`, function () {

    let sel = document.getElementById(`modifyPrimaryMenu`);
    let primaryMenu = sel.options[sel.selectedIndex].text;
    let secondaryMenu = $(`#modifySecondaryMenu`).val() === null ? `` : $(`#modifySecondaryMenu`).val().trim();
    let tertiaryMenu = $(`#modifyTertiaryMenu`).val() === null ? `` : $(`#modifyTertiaryMenu`).val().trim();
    let menuLevel = $(`#modifyMenuLevel`).val() === null ? 2 : parseInt($(`#modifyMenuLevel`).val().trim());

    let primaryMenuOrder = $(`#modifyPrimaryMenuOrder`).val() === null ? `` : parseInt($(`#modifyPrimaryMenuOrder`).val().trim());
    let secondaryMenuOrder = $(`#modifySecondaryMenuOrder`).val() === null ? `` : parseInt($(`#modifySecondaryMenuOrder`).val().trim());
    let tertiaryMenuOrder = $(`#modifyTertiaryMenuOrder`).val() === null ? `` : parseInt($(`#modifyTertiaryMenuOrder`).val().trim());

    let link = $(`#modifyLink`).val() === null ? `` : $(`#modifyLink`).val().trim();
    let menuCategory = $(`#modifyPrimaryMenu`).val() === null ? `` : $(`#modifyPrimaryMenu`).val().trim();
    let active = $(`#modifyActive`).val() == `1` ? true : false;

    if (primaryMenu == ``) {
        WarningAlert(`ERROR_FullFillTheForm`);
    }
    else {
        var menu = new Object();
        menu.ID = menuID;

        menu.MenuCategory = menuCategory;

        menu.PrimaryMenu = primaryMenu;
        menu.MenuOrderly = primaryMenuOrder;

        menu.SecondaryMenu = secondaryMenu;
        menu.MenuLevel2Orderly = secondaryMenuOrder;

        menu.TertiaryMenu = tertiaryMenu;
        menu.MenuLevel3Orderly = tertiaryMenuOrder;

        menu.Link = link;
        menu.MenuLevel = menuLevel;
        menu.Active = active;

        $.ajax({
            url: `/Menu/Modify`,
            type: `PUT`,
            data: JSON.stringify(menu),
            contentType: `application/json; charset=utf-8`,
            dataType: `json`
        })
            .done(function (response) {
                if (response.flag) {
                    $(`#dialogModify`).dialog('close');
                    SuccessAlert(response.message);
                }
                else {
                    ErrorAlert(response.message);
                }
            })

            .fail(function () {
                ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            });
    }

    //WarningAlert(`Function đang phát triển.`)
    return;
});

