var grid = $('#mainGrid');

//GRID
function Grid() {
    grid.jqGrid
        ({
            url: `/StandardDocument/getGuidingList`,
            datatype: `json`,
            mtype: `GET`,
            colModel: [
                { name: 'Id', key: true, sortable: false, hidden: true },
                { name: 'MenuCode', sortable: false, hidden: true },
                { name: 'LanguageCode', sortable: false, hidden: true },

                { name: 'Name', sortable: true, width: 100 },
                { name: 'FullName', sortable: true, width: 250 },
                { name: 'MenuLevel', width: 50, sortable: true, align: 'center' },
                { name: 'Language', sortable: false, align: 'center', formatter: LanguageFormatter, width: 50 },
                { label: '', name: 'Modify', sortable: false, width: 50, align: 'center', formatter: ModifyBtnClick },

                {
                    name: "deleteBtn", width: 50, align: "center", label: "", resizable: false, title: false,
                    formatter: ShowDeleteBtnGrid
                },
            ],

            jsonReader:
            {
                root: "Data",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            iconSet: "fontAwesome",
            //idPrefix: "ug_",
            rownumbers: true,
            sortname: "Name",
            sortorder: "asc",
            threeStateSort: true,
            sortIconsBeforeText: true,
            headertitles: true,
            //toppager: true,
            pager: true,
            rowNum: 50,
            rowList: [50, 100, 200, 500, 1000],
            viewrecords: true,
            //width: null,
            autowidth: true,
            shrinkToFit: false,
            searching: {
                defaultSearch: "Id"
            },
            height: 500,
            loadonce: true,
            caption: `Guiding List Infomation`,
            emptyrecords: 'No Data',
            beforeProcessing: function (data) {
                //var model = data.multiLangModel, name, $colHeader, $sortingIcons;
                //if (model) {
                //    for (name in model) {
                //        if (model.hasOwnProperty(name)) {
                //            $colHeader = $("#jqgh_" + $.jgrid.jqID(this.id + "_" + name));
                //            $sortingIcons = $colHeader.find(">span.s-ico");
                //            $colHeader.text(model[name].label);
                //            $colHeader.append($sortingIcons);
                //        }
                //    }
                //}
            },
            multiselect: false,


            loadComplete: function () {
                //CheckPermissions();

                //let rowIds = grid.getDataIDs();
                //let rowData = grid.getRowData(rowIds[0]);
                //if (rowData.modifyBtn == ``) {
                //    grid.hideCol("modifyBtn");
                //}
            },
            onSelectRow: function (rowid, status, e, iRow, iCol) {
                let selRowId = grid.getGridParam(`selrow`);
                let rowData = grid.getRowData(selRowId);
                let masterCode = rowData.Code;
            }
        });
}
function LanguageFormatter(cellValue, options, rowdata, action) {
    if (rowdata.LanguageCode == `EN`) {
        return `English`;
    }
    return `Vietnamese`;
}
function ModifyBtnClick(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-warning button-srh Permission_modifyMenuUserGuide" data-id="${rowObject.Id}" onclick="ModifyUserGuide(${rowObject.Id})" style="cursor: pointer;"><i class="fa fa-edit" aria-hidden="true"></i>&nbsp;${ml_BtnModify}</button>`;
}
function ShowDeleteBtnGrid(cellvalue, options, rowObject) {
    return `<button class="btn btn-sm btn-danger button-srh Permission_modifyMenuUserGuide" data-id="${rowObject.Id}" onclick="DeletePermission($(this))" style="cursor: pointer;"><i class="fa fa-trash" aria-hidden="true"></i>&nbsp;${ml_BtnDelete}</button>`;
}


var getIdModify;
function ModifyUserGuide(id) {
    getIdModify = id;
    $(`.dialog_MODIFY`).dialog(`open`);
}
var getIdDelete;
function DeleteUserGuide(id) {
    getIdDelete = id;
    $(`#dialogDelete`).dialog(`open`);
}
//DOCUMENT READY
$(document).ready(function () {
    Grid();
});

//MENU LIST DIALOG
var mnCode = ``;
var mnName = ``;
var mnLevel = ``;
var mnUrl = ``;
$(`.popupDialogMenuBtn`).click(function () {
    $(`.dialog_MENULIST`).dialog(`open`);
});
$(`.dialog_MENULIST`).dialog({
    width: 1200,
    height: 600,
    resizable: false,
    fluid: true,
    modal: true,
    autoOpen: false,

    //width: 'auto',
    //height: 'auto',
    //autoResize: true,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    //create: function (event, ui) {
    //    // Set maxWidth
    //    $(this).css("maxWidth", "1200px");
    //    $(this).css("maxHeight", "600px");
    //},

    classes: {
        "ui-dialog": "ui-dialog",
        "ui-dialog-titlebar": "ui-dialog ui-dialog-titlebar-sm",
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    resize: function (event, ui) {
        $('.ui-dialog-content').addClass('m-0 p-0');
    },
    open: function (event, ui) {
        var $grid = $(`#menuListGrid`);
        $grid.jqGrid({
            //url: `/Menu/GetAll`,
            url: `/StandardDocument/GetAllMenu`,
            mtype: `GET`,
            datatype: `json`,
            colModel: [
                { name: "ID", label: "", key: true, hidden: true },
                { name: "MenuCategory", hidden: true },
                { name: "MenuLevel", hidden: true },
                { name: "PrimaryMenu", hidden: true },
                { name: "MenuOrderly", hidden: true },
                { name: "SecondaryMenu", hidden: true },
                { name: "MenuLevel2Orderly", hidden: true },
                { name: "TertiaryMenu", hidden: true },
                { name: "MenuLevel3Orderly", hidden: true },
                { name: "Code", width: 120, align: 'center' },
                { name: "Name", width: 150 },
                { name: "FullName", width: 250 },
                { name: "CategoryName", width: 150 },
                { name: "Link", width: 150 },
                { name: "Active", width: 100, align: 'center' },

            ],
            jsonReader:
            {
                root: "Data",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },

            pager: `#menuListGridPager`,
            rowNum: 50,
            rowList: [50, 100, 200, 500, 1000],
            rownumbers: true,
            autowidth: true,
            shrinkToFit: true,
            viewrecords: true,
            height: 350,
            width: null,
            loadonce: true,
            caption: `Menu List Infomation`,
            emptyrecords: `No Data`,
            beforeProcessing: function (data) {
                //var model = data.multiLangModel, name, $colHeader, $sortingIcons;
                //if (model) {
                //    for (name in model) {
                //        if (model.hasOwnProperty(name)) {
                //            $colHeader = $("#jqgh_" + $.jgrid.jqID(this.id + "_" + name));
                //            $sortingIcons = $colHeader.find(">span.s-ico");
                //            $colHeader.text(model[name].label);
                //            $colHeader.append($sortingIcons);
                //        }
                //    }
                //}
            },
            loadComplete: function () {

            },
            onSelectRow: function (rowid, selected, status, e) {
                var selectedRowId = $grid.jqGrid(`getGridParam`, `selrow`);
                var rowData = $grid.getRowData(selectedRowId);
                mnCode = rowData.Code;
                mnName = rowData.Name;
                mnLevel = rowData.MenuLevel;
                mnUrl = rowData.Link;
            },
        });
    },
    close: function (event, ui) {
    },
});
$(`#dialog_MENULIST_SelectBtn`).on(`click`, function () {
    $(`.dialog_MENULIST`).dialog(`close`);
    $(`#cName`).val(mnName);
    $(`#mName`).val(mnName);
    $(`#createBtn`).prop(`disabled`, false);
});

//CREATE DIALOG
$(`#registerBtn`).click(function () {
    $(`.dialog_CREATE`).dialog(`open`);
});
$(`.dialog_CREATE`).dialog({
    //width: 1200,
    //height: 850,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,

    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "1200px");
        $(this).css("maxHeight", "850px");
    },
    classes: {
        "ui-dialog": "ui-dialog",
        "ui-dialog-titlebar": "ui-dialog ui-dialog-titlebar-sm",
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    resize: function (event, ui) {
        $('.ui-dialog-content').addClass('m-0 p-0');
    },
    open: function (event, ui) {
        $(`#createBtn`).prop(`disabled`, true);
        $('#cContent').summernote({
            placeholder: 'Please give me content ....',
            tabsize: 2,
            height: 350,
            //minHeight: 100,
            //maxHeight: 500,
            focus: true,
            toolbar: [
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']],
            ],
            popover: {
                image: [
                    ['image', ['resizeFull', 'resizeHalf', 'resizeQuarter', 'resizeNone']],
                    ['float', ['floatLeft', 'floatRight', 'floatNone']],
                    ['remove', ['removeMedia']]
                ],
                link: [
                    ['link', ['linkDialogShow', 'unlink']]
                ],
                table: [
                    ['add', ['addRowDown', 'addRowUp', 'addColLeft', 'addColRight']],
                    ['delete', ['deleteRow', 'deleteCol', 'deleteTable']],
                ],
                air: [
                    ['color', ['color']],
                    ['font', ['bold', 'underline', 'clear']],
                    ['para', ['ul', 'paragraph']],
                    ['table', ['table']],
                    ['insert', ['link', 'picture']]
                ]
            },
            codemirror: {
                theme: 'paper'
            }
        });

    },

});

//MODIFY DIALOG
$(`.dialog_MODIFY`).dialog({
    //width: 1200,
    //height: 900,
    //resizable: false,
    //fluid: true,
    //modal: true,
    //autoOpen: false,
    /////
    width: 'auto',
    height: 'auto',
    autoResize: true,
    fluid: true,
    modal: true,
    autoOpen: false,
    create: function (event, ui) {
        // Set maxWidth
        $(this).css("maxWidth", "1200px");
        $(this).css("maxHeight", "850px");
    },

    /////

    classes: {
        "ui-dialog": "ui-dialog",
        "ui-dialog-titlebar": "ui-dialog ui-dialog-titlebar-sm",
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    resize: function (event, ui) {
        $('.ui-dialog-content').addClass('m-0 p-0');
    },
    open: function (event, ui) {

        $('#mContent').summernote({
            placeholder: 'Please give me content ....',
            tabsize: 2,
            height: 500,
            minHeight: 100,
            maxHeight: 600,
            focus: true,
            toolbar: [
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['fontsize', ['fontsize']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']],
            ],
            popover: {
                image: [
                    ['image', ['resizeFull', 'resizeHalf', 'resizeQuarter', 'resizeNone']],
                    ['float', ['floatLeft', 'floatRight', 'floatNone']],
                    ['remove', ['removeMedia']]
                ],
                link: [
                    ['link', ['linkDialogShow', 'unlink']]
                ],
                table: [
                    ['add', ['addRowDown', 'addRowUp', 'addColLeft', 'addColRight']],
                    ['delete', ['deleteRow', 'deleteCol', 'deleteTable']],
                ],
                air: [
                    ['color', ['color']],
                    ['font', ['bold', 'underline', 'clear']],
                    ['para', ['ul', 'paragraph']],
                    ['table', ['table']],
                    ['insert', ['link', 'picture']]
                ]
            },
            codemirror: {
                theme: 'paper'
            }
        });
        $.ajax({
            url: `/StandardDocument/GetGuidingById?Id=${getIdModify}`,
            type: `GET`,
            dataType: `json`
        })
            .done(function (response) {
                //console.log(response.result);
                $(`#mLanguageCode`).val(response.Data.LanguageCode);
                $(`#mName`).val(response.Data.Name);
                //$(`#mDescription`).val(response.result.Description);
                $(`#mContent`).summernote('code', response.Data.Content);
                $(`#mContent`).summernote({ focus: true });
                mnCode = response.Data.MenuCode;
                mnName = response.Data.Name;
                mnLevel = response.Data.MenuLevel;
                return;

            })
            .fail(function () {
                ErrorAlert(`System error`);
                return;
            });
    },
    //close: function (event, ui) {
    //    $('#mContent').summernote('code', '');
    //},
});

$(`#createBtn`).on(`click`, function () {

    var languageCode = $(`#cLanguageCode`).val();
    var name = $(`#cName`).val();
    var content = $(`#cContent`).summernote(`code`);
    var checkArray = {};
    checkArray[`languageCode`] = languageCode;
    checkArray[`name`] = name;
    checkArray[`content`] = content;

    for (var item in checkArray) {
        if (!checkArray[item] || checkArray[item] == `<p><br></p>`) {
            ErrorAlert(`Full fill the create form to continue`);
            return;
        }
    }

    var formData = new FormData();
    formData.append(`Name`, name);
    formData.append(`Content`, content);
    formData.append(`MenuCode`, mnCode);
    formData.append(`MenuLevel`, mnLevel);
    formData.append(`LanguageCode`, languageCode);
    $.ajax({
        method: `POST`,
        processData: false,
        contentType: false,
        cache: false,
        data: formData,
        datatype: `json`,
        url: `/StandardDocument/Insert`
    })
        .done(function (response) {
            $(`.dialog_CREATE`).dialog(`close`);
            if (response.flag) {


                $(`#cName`).val("");
                $(`#cWriter`).val("");

                $('#cContent').summernote('reset');

                //grid.jqGrid(`addRowData`, response.result[0].Id, response.result[0], `first`);
                grid.jqGrid(`addRowData`, response.result.Data[0].Id, response.result.Data[0], `first`);
                grid.setRowData(response.result.Data[0].Id, false, { background: `#28a745`, color: `white`, 'font-size': `1.2em` });


                //grid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.result }).trigger("reloadGrid");

                //SuccessAlert(response.message);
                SuccessAlert(response.ResponseMessage);
                return;
            }
            ErrorAlert(response.message);
            //ErrorAlert(response.ResponseMessage);
            return;
        })
        .fail(function () {
            ErrorAlert(`System error`);
            return;
        });
});

$(`#modifyBtn`).on(`click`, function () {
    var languageCode = $(`#mLanguageCode`).val();
    var name = $(`#mName`).val();
    var content = $(`#mContent`).summernote(`code`);
    var checkArray = {};
    checkArray[`languageCode`] = languageCode;
    checkArray[`name`] = name;
    checkArray[`content`] = content;

    for (var item in checkArray) {
        if (!checkArray[item] || checkArray[item] == `<p><br></p>`) {
            ErrorAlert(`Full fill the create form to continue`);
            return;
        }
    }

    var formData = new FormData();
    formData.append(`Id`, getIdModify);
    formData.append(`Name`, name);
    formData.append(`Content`, content);
    formData.append(`MenuCode`, mnCode);
    formData.append(`MenuLevel`, mnLevel);
    formData.append(`LanguageCode`, languageCode);
    $.ajax({
        method: `PUT`,
        processData: false,
        contentType: false,
        cache: false,
        data: formData,
        datatype: `json`,
        url: `/StandardDocument/Update`
    })
        .done(function (response) {
            $(`.dialog_MODIFY`).dialog(`close`);
            if (response.flag) {
                var row = response.result.Data;
                //console.log(row)
                var modifyRow = grid.getRowData(getIdModify);
                if (row.LanguageCode == `EN`) {
                    modifyRow.Language = `English`;
                }
                else {
                    modifyRow.Language = `Vietnamese`;
                }

                //grid.setRowData(response.result.Id, modifyRow, { background: `#28a745`, color: `#fff`, 'font-size': `1.2em` });
                grid.setRowData(response.result.Data.Id, modifyRow, { background: `#28a745`, color: `#fff`, 'font-size': `1.2em` });
                //SuccessAlert(response.message);
                SuccessAlert(response.result.ResponseMessage);
                return;
            }
            //ErrorAlert(response.message);
            ErrorAlert(response.result.ResponseMessage);
            return;
        })
        .fail(function () {
            ErrorAlert(`System error`);
            return;
        });
});
//////////////
var idGuidingList;
function DeletePermission(e) {
    idGuidingList = e.attr(`data-id`);
    //let rowData = grid.getRowData(id);

    $(`#dialogDelete`).dialog(`open`);
}
$(`#dialogDelete`).dialog({
    width: 450,
    height: 150,
    resizable: false,
    fluid: true,
    modal: true,
    autoOpen: false,
    classes: {
        "ui-dialog-titlebar-close": "visibility: hidden",
    },
    open: function (event, ui) {
    },
    close: function (event, ui) {
    },
});
$(`#btnCancelDelete`).on(`click`, function () {
    $(`#dialogDelete`).dialog(`close`);
});
$(`#btnDelete`).on(`click`, function () {
    $.ajax({
        url: `/StandardDocument/Delete/${idGuidingList}`,
        type: `DELETE`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`
    })
        .done(function (response) {
            if (response.flag) {
                grid.delRowData(idGuidingList);
                $('#dialogDelete').dialog('close');
                SuccessAlert(response.message);
            }
            else {
                ErrorAlert(response.message);
            }
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
        });

    return;
});



///////////////
$('#excuseDelete').click(function (e) {

    if (!getIdDelete) {
        //alert
        ErrorAlert('Please select  on grid to delete !');
        return;
    }
    else {
        {
            $.ajax({
                url: "/UserGuide/DeleteUserGuide",
                type: "DELETE",
                dataType: "json",
                data: {
                    id: getIdDelete
                },
                success: function (response) {
                    if (response.result) {
                        debugger
                        grid.jqGrid('delRowData', getIdDelete);
                        SuccessAlert(response.message);
                        $('#dialogDelete').dialog('close');

                        return;
                    }
                    else {
                        ErrorAlert(response.message);
                        $('#dialogDelete').dialog('close');
                        return;
                    }
                },
                error: function (e) {
                    ErrorAlert('Script is in the wrong way !');
                    $('#dialogDelete').dialog('close');
                    return;
                }
            });
        }
    }
});

$(`#searchBtn`).on(`click`, function () {

    let slanguageCode = $(`#sLanguageCode`).val() == null ? `` : $(`#sLanguageCode`).val().trim();
    let smenuCode = $(`#sMenuCode`).val() == null ? `` : $(`#sMenuCode`).val().trim();
    let smenuName = $(`#sMenuName`).val() == null ? `` : $(`#sMenuName`).val().trim();
    $.ajax({
        url: `/StandardDocument/Search?languageCode=${slanguageCode}&menuCode=${smenuCode}&menuName=${smenuName}`,
        type: `GET`,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                grid.jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
            }
            else {
                grid.jqGrid('clearGridData');
                WarningAlert(`ERROR_NotFound`);
            }
            return;
        })
        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});


$(`#searchMenuBtn`).on(`click`, function () {
    let keyWord = $(`#searchMenuInput`).val() === null ? `` : $(`#searchMenuInput`).val().trim();

    $.ajax({
        //url: `/Menu/Search?s=${keyWord}`,
        url: `/StandardDocument/SearchMenu?s=${keyWord}`,
        type: `GET`,
        //data: keyWord,
        contentType: `application/json; charset=utf-8`,
        dataType: `json`,
        cache: false,
    })
        .done(function (response) {
            if (response.Data && response.HttpResponseCode != 100) {
                $(`#menuListGrid`).jqGrid('clearGridData').jqGrid('setGridParam', { datatype: 'local', data: response.Data }).trigger("reloadGrid");
                //SuccessAlert(response.message);
            }
            else {
                $(`#menuListGrid`).jqGrid('clearGridData');
                WarningAlert(`ERROR_NotFound`);
            }
            return;
        })

        .fail(function () {
            ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
            return;
        });
});
$(`#searchMenuInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchMenuBtn`).trigger(`click`);
    }
});