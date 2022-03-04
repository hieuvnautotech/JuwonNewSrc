/* View */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const showDeleted = document.getElementById(`showDeleted`);
const searchInput = document.getElementById(`searchInput`);
const searchBtn = document.getElementById(`searchBtn`);
const $locationGrid = $(`#locationGrid`);

/* Create */
const cLocationName = document.getElementById(`cLocationName`);
const cLocationCategoryId = document.getElementById(`cLocationCategoryId`);
const cAreaId = document.getElementById(`cAreaId`);
const cLocationDescription = document.getElementById(`cLocationDescription`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/* Modify */
const mLocationName = document.getElementById(`mLocationName`);
const mLocationCategoryId = document.getElementById(`mLocationCategoryId`);
const mAreaId = document.getElementById(`mAreaId`);
const mLocationDescription = document.getElementById(`mLocationDescription`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/* Delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var locationId = 0;

var createSelectedLocationCategory;
var createSelectedArea;

var modifySelectedLocationCategory;
var modifySelectedArea;

async function Initialize() {
    await LocationGrid();
    await ReloadLocationGrid();
    await GetLocationCategories();
    await GetAreas();

    createSelectedLocationCategory = new SlimSelect({
        select: '#cLocationCategoryId',
        placeholder: 'Select Category',
        hideSelectedOption: true
    });

    createSelectedArea = new SlimSelect({
        select: '#cAreaId',
        placeholder: 'Select Area',
        hideSelectedOption: true
    });
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

//LOCATION GRID
async function LocationGrid() {
    "use strict";
    $locationGrid.jqGrid({
        colModel: [
            { name: "LocationId", label: "", key: true, hidden: true },
            { name: "LocationCategoryId", label: "", hidden: true },
            { name: "AreaId", label: "", hidden: true },
            { name: "LocationName", label: "Name", width: 100, align: 'center', search: false },
            { name: "LocationCategoryName", label: "Category", width: 100, align: 'center', search: false },
            { name: "AreaName", label: "Area", width: 100, align: 'center', search: false },
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
            $(`#gbox_locationGrid`).block({
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
        caption: 'Location',
        loadComplete: function () {
            $(`#gbox_locationGrid`).unblock();
            let ids = $locationGrid.getDataIDs();
            for (let i of ids) {
                let row = $locationGrid.getRowData(i);
                if (row.Active === "NO") {
                    $locationGrid.setCell(i, 'LocationName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == locationId) {
                locationId = 0;
            }
            else {
                locationId = parseInt(rowid);
            }
            let row = $locationGrid.getRowData(rowid);
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
            searchOnEnter: false,
            loadFilterDefaults: false,
            afterSearch: function () {
            }
        });
}

//RELOAD LOCATION GRID
async function ReloadLocationGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Location/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Location/SearchActive?keyWord=${keyWord}`;
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
                        $locationGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        locationId = 0;
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

async function GetLocationCategories() {
    $.ajax({
        url: `/Location/GetLocationCategories`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.LocationCategoryName < b.LocationCategoryName) return -1
                    return a.LocationCategoryName > b.LocationCategoryName ? 1 : 0
                });

                $.each(response.Data, function (key, item) {
                    if (item.Active == true) {
                        html += `<option value="${item.LocationCategoryId}">${item.LocationCategoryName}</option>`;
                    }

                });
                $(`#cLocationCategoryId`).html(html);
                $(`#mLocationCategoryId`).html(html);
            }
        });
}

async function GetAreas() {
    $.ajax({
        url: `/Location/GetAreas`,
        type: `GET`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.AreaName < b.AreaName) return -1
                    return a.AreaName > b.AreaName ? 1 : 0
                });

                $.each(response.Data, function (key, item) {
                    if (item.Active == true) {
                        html += `<option value="${item.AreaId}">${item.AreaName}</option>`;
                    }

                });
                $(`#cAreaId`).html(html);
                $(`#mAreaId`).html(html);
            }
        });
}