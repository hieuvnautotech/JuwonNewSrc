/** view */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $materialGrid = $(`#materialGrid`);

/** create */
const cMaterialName = document.getElementById(`cMaterialName`);
const cMaterialInOut = document.getElementById(`cMaterialInOut`);
const cMaterialSection = document.getElementById(`cMaterialSection`);
const cMaterialType = document.getElementById(`cMaterialType`);
const cMaterialPart = document.getElementById(`cMaterialPart`);
const cMaterialColor = document.getElementById(`cMaterialColor`);
const cMaterialUnit = document.getElementById(`cMaterialUnit`);
const cMaterialRemark = document.getElementById(`cMaterialRemark`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/** modify */
const mMaterialCode = document.getElementById(`mMaterialCode`);
const mMaterialName = document.getElementById(`mMaterialName`);
const mMaterialInOut = document.getElementById(`mMaterialInOut`);
const mMaterialSection = document.getElementById(`mMaterialSection`);
const mMaterialType = document.getElementById(`mMaterialType`);
const mMaterialPart = document.getElementById(`mMaterialPart`);
const mMaterialColor = document.getElementById(`mMaterialColor`);
const mMaterialUnit = document.getElementById(`mMaterialUnit`);
const mMaterialRemark = document.getElementById(`mMaterialRemark`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/** delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var materialId = 0;

var createSelectedSection;
var modifySelectedSection;

var createSelectedUnit;
var modifySelectedUnit;

var createSelectedInOut;
var modifySelectedInOut;

var createSelectedType;
var modifySelectedType;

var createSelectedPart;
var modifySelectedPart;

var createSelectedColor;
var modifySelectedColor;

var createSelectedMold;
var modifySelectedMold;

async function Initialize() {
    await MaterialGrid();
    await ReloadMaterialGrid();

    await GetMaterialSections();
    await GetMaterialUnits();
    await GetMaterialInOut();
    await GetMaterialType();
    await GetMaterialPart();
    await GetMaterialColor();

    await GetMaterialMold();

    createSelectedSection = new SlimSelect({
        select: '#cMaterialSection',
        placeholder: 'Select Section',
        hideSelectedOption: true
    });

    createSelectedUnit = new SlimSelect({
        select: '#cMaterialUnit',
        placeholder: 'Select Section',
        hideSelectedOption: true
    });

    createSelectedInOut = new SlimSelect({
        select: '#cMaterialInOut',
        placeholder: 'Select Section',
        hideSelectedOption: true
    });

    createSelectedType = new SlimSelect({
        select: '#cMaterialType',
        placeholder: 'Select Section',
        hideSelectedOption: true
    });

    createSelectedPart = new SlimSelect({
        select: '#cMaterialPart',
        placeholder: 'Select Section',
        hideSelectedOption: true
    });

    createSelectedColor = new SlimSelect({
        select: '#cMaterialColor',
        placeholder: 'Select Section',
        hideSelectedOption: true
    });

    createSelectedMold = new SlimSelect({
        select: '#cMaterialMold',
        placeholder: 'Select Section',
        hideSelectedOption: true
    });
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

//MATERIAL GRID
async function MaterialGrid() {
    "use strict";
    $materialGrid.jqGrid({
        colModel: [
            { name: "MaterialId", label: "", key: true, hidden: true, search: false },
            { name: "MaterialTypeId", label: "", hidden: true, search: false },
            { name: "MaterialInOutId", label: "", hidden: true, search: false},
            { name: "MaterialSectionId", label: "", hidden: true, search: false },
            { name: "MaterialUnitId", label: "", hidden: true, search: false },
            { name: "PartId", label: "", hidden: true, search: false },
            { name: "ColorId", label: "", hidden: true, search: false },
            { name: "MoldId", label: "", hidden: true, search: false},

            { name: "MaterialCode", label: "Code", width: 150, align: 'center', searchoptions: { sopt: ['cn'] }},
            { name: "MaterialName", label: "Name", width: 200, align: 'center', searchoptions: { sopt: ['cn'] }},
            
            { name: "MaterialInOutName", label: "IN/OUT", width: 50, align: 'center', search: false },
            { name: "MaterialSectionName", label: "Section", width: 50, align: 'center', search: false },
            { name: "MaterialTypeName", label: "Type", width: 50, align: 'center', search: false },
            
            { name: "MoldCode", label: "Mold", width: 70, align: 'center', search: false },
            { name: "ColorName", label: "Color", width: 100, align: 'center', search: false },
            { name: "MaterialRemark", label: "Remark", width: 70, align: 'center', search: false },
            { name: "MaterialUnitName", label: "Unit", width: 70, align: 'center', search: false },
            {
                name: "CreatedDate", width: 100, align: 'center', formatter: 'date', formatoptions:
                {
                    srcformat: "ISO8601Long", newformat: "Y-m-d"
                },
                sorttype: 'date',
                label: "Created Date",
                searchoptions: {
                    sopt: ['ge'],
                    dataInit: function (elem) {
                        $(elem).datepicker({
                            dateFormat: 'yy-mm-dd',
                            autoSize: true,
                            changeYear: true,
                            changeMonth: true,
                            showButtonPanel: true,
                            showWeek: true,
                            onSelect: function () {
                                $(this).keydown();
                            },
                        });
                    }
                }
            },
            {
                name: "ModifiedDate", width: 100, align: 'center', formatter: 'date', formatoptions:
                {
                    srcformat: "ISO8601Long", newformat: "Y-m-d"
                },
                sorttype: 'date',
                label: "ModifiedDate",
                searchoptions: {
                    sopt: ['ge'],
                    dataInit: function (elem) {
                        $(elem).datepicker({
                            dateFormat: 'yy-mm-dd',
                            autoSize: true,
                            changeYear: true,
                            changeMonth: true,
                            showButtonPanel: true,
                            showWeek: true,
                            onSelect: function () {
                                $(this).keydown();
                            },
                        });
                    }
                }
            },
            { name: "Active", label: "Actived", hidden: true, align: 'center', formatter: ShowActiveStatus },
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
        rownumbers: true,
        sortname: "Name",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        pager: true,
        rowNum: 20,
        viewrecords: true,
        shrinkToFit: false,
        height: 600,
        cmTemplate: { resizable: false },
        //loadui: 'disable',
        //footerrow: true,
        beforeRequest: function () {
            $(`#gbox_materialGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
            //var model = data.multiLangModel, name, $colHeader, $sortingIcons;
            //if (model)
            //{
            //    for (name in model)
            //    {
            //        if (model.hasOwnProperty(name))
            //        {
            //            $colHeader = $("#jqgh_" + $.jgrid.jqID(this.id + "_" + name));
            //            $sortingIcons = $colHeader.find(">span.s-ico");
            //            $colHeader.text(model[name].label);
            //            $colHeader.append($sortingIcons);
            //        }
            //    }
            //}
        },
        loadonce: true,
        caption: 'Material',
        loadComplete: function () {
            $(`#gbox_materialGrid`).unblock();
            let ids = $materialGrid.getDataIDs();
            for (let i of ids) {
                let row = $materialGrid.getRowData(i);
                if (row.Active === "NO") {
                    $materialGrid.setCell(i, 'MaterialCode', '', { 'background-color': '#ffcc99' }, '');
                    $materialGrid.setCell(i, 'MaterialName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == materialId) {
                materialId = 0;
            }
            else {
                materialId = parseInt(rowid);
            }

            let row = $materialGrid.getRowData(rowid);
            if (row.Active === "NO") {
                deleteBtn.innerHTML = `<i class="fa fa-toggle-on"></i>&nbsp;Active`;
            }
            else {
                deleteBtn.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
            }
        }
    })
        .filterToolbar({
            searchOperators: true,
            searchOnEnter: true,
            loadFilterDefaults: false,
            afterSearch: function () {
                materialId = 0;
                $materialGrid.jqGrid('resetSelection');
                ReloadMaterialGrid();
            }
        });
}

//RELOAD MATERIAL GRID
async function ReloadMaterialGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Material/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Material/SearchActive?keyWord=${keyWord}`;
        }
        $.ajax({
            url: requestUrl,
            type: `GET`,
        })
            .done(function (response) {
                if (response) {
                    if (response.HttpResponseCode == 100) {
                        WarningAlert(response.ResponseMessage);
                    }
                    else {
                        $materialGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        materialId = 0;
                        deleteBtn.innerHTML = `<i class="fa fa-trash"></i>&nbsp;Delete`;
                    }

                    resolve(true);
                }
            })
            .fail(function () {
                ErrorAlert(`System error - Please contact IT`);
                resolve(false);
            })
    })
}

async function GetMaterialSections() {
    $.ajax({
        url: `/Material/GetMaterialSections`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.Name < b.Name) return -1
                    return a.Name > b.Name ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.ID}">${item.Name}</option>`;
                });
                $(`#cMaterialSection`).html(html);
                $(`#mMaterialSection`).html(html);
            }
        });
}

async function GetMaterialUnits() {
    $.ajax({
        url: `/Material/GetMaterialUnits`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.Name < b.Name) return -1
                    return a.Name > b.Name ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.ID}">${item.Name}</option>`;
                });
                $(`#cMaterialUnit`).html(html);
                $(`#mMaterialUnit`).html(html);
            }
        });
}

async function GetMaterialInOut() {
    $.ajax({
        url: `/Material/GetMaterialInOut`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.Name < b.Name) return -1
                    return a.Name > b.Name ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.ID}">${item.Name}</option>`;
                });
                $(`#cMaterialInOut`).html(html);
                $(`#mMaterialInOut`).html(html);
            }
        });
}

async function GetMaterialType() {
    $.ajax({
        url: `/Material/GetMaterialType`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.Name < b.Name) return -1
                    return a.Name > b.Name ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.ID}">${item.Name}</option>`;
                });
                $(`#cMaterialType`).html(html);
                $(`#mMaterialType`).html(html);
            }
        });
}

async function GetMaterialPart() {
    $.ajax({
        url: `/Material/GetMaterialPart`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.PartName < b.PartName) return -1
                    return a.PartName > b.PartName ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.PartId}">${item.PartName}</option>`;
                });
                $(`#cMaterialPart`).html(html);
                $(`#mMaterialPart`).html(html);
            }
        });
}

async function GetMaterialColor() {
    $.ajax({
        url: `/Material/GetMaterialColor`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.ColorName < b.ColorName) return -1
                    return a.ColorName > b.ColorName ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.ColorId}">${item.ColorName}</option>`;
                });
                $(`#cMaterialColor`).html(html);
                $(`#mMaterialColor`).html(html);
            }
        });
}

async function GetMaterialMold() {
    $.ajax({
        url: `/Material/GetMaterialMold`,
    })
        .done(function (response) {
            let html = `<option value="0">--No Mold--</option>`;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.MoldCode < b.MoldCode) return -1
                    return a.MoldCode > b.MoldCode ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    html += `<option value="${item.MoldId}">${item.MoldCode}</option>`;
                });
                $(`#cMaterialMold`).html(html);
                $(`#mMaterialMold`).html(html);
            }
        });
}