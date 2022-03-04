/** view */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const showDeleted = document.getElementById(`showDeleted`);
const searchInput = document.getElementById(`searchInput`);
const searchBtn = document.getElementById(`searchBtn`);
const $areaGrid = $(`#areaGrid`);

/** create */
const cAreaName = document.getElementById(`cAreaName`);
const cAreaCategoryId = document.getElementById(`cAreaCategoryId`);
const cAreaDescription = document.getElementById(`cAreaDescription`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/** modify */
const mAreaName = document.getElementById(`mAreaName`);
const mAreaCategoryId = document.getElementById(`mAreaCategoryId`);
const mAreaDescription = document.getElementById(`mAreaDescription`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/** delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var areaId = 0;
var createSelectedAreaCategory;
var modifySelectedAreaCategory;

async function Initialize() {
    await AreaGrid();
    await ReloadAreaGrid();
    await GetAreaCategories();
    createSelectedAreaCategory = new SlimSelect({
        select: '#cAreaCategoryId',
        placeholder: 'Select Area Category',
        hideSelectedOption: true
    });
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

//AreaCategory GRID
async function AreaGrid() {
    "use strict";
    $areaGrid.jqGrid({
        colModel: [
            { name: "AreaId", label: "", key: true, hidden: true },
            { name: "AreaCategoryId", label: "", hidden: true },

            { name: "AreaName", label: "Name", width: 100, align: 'center', search: false },
            { name: "AreaCategoryName", label: "Category", width: 100, align: 'center', search: false },
            { name: "AreaDescription", label: "Description", width: 100, align: 'center', search: false },
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
            $(`#gbox_areaGrid`).block({
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
        caption: 'Area',
        loadComplete: function () {
            $(`#gbox_areaGrid`).unblock();
            let ids = $areaGrid.getDataIDs();
            for (let i of ids) {
                let row = $areaGrid.getRowData(i);
                if (row.Active === "NO") {
                    $areaGrid.setCell(i, 'AreaName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == areaId) {
                areaId = 0;
            }
            else {
                areaId = parseInt(rowid);
            }
            let row = $areaGrid.getRowData(rowid);
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

//RELOAD LOCATION CATEGORY GRID
async function ReloadAreaGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Area/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Area/SearchActive?keyWord=${keyWord}`;
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
                        $areaGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        areaId = 0;
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

async function GetAreaCategories() {
    $.ajax({
        url: `/Area/GetAreaCategories`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.AreaCategoryName < b.AreaCategoryName) return -1
                    return a.AreaCategoryName > b.AreaCategoryName ? 1 : 0
                });
                $.each(response.Data, function (key, item) {
                    if (item.Active == true) {
                        html += `<option value="${item.AreaCategoryId}">${item.AreaCategoryName}</option>`;
                    }

                });
                $(`#cAreaCategoryId`).html(html);
                $(`#mAreaCategoryId`).html(html);
            }
        });
}