/** view */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $colorGrid = $(`#colorGrid`);

/** create */
const cColorCode = document.getElementById(`cColorCode`);
const cColorName = document.getElementById(`cColorName`);
const cColorDescription = document.getElementById(`cColorDescription`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/** modify */
const mColorCode = document.getElementById(`mColorCode`);
const mColorName = document.getElementById(`mColorName`);
const mColorDescription = document.getElementById(`mColorDescription`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/** delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var colorId = 0;

async function Initialize() {
    await ColorGrid();
    await ReloadColorGrid();
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

//PROCESS GRID
async function ColorGrid() {
    "use strict";
    $colorGrid.jqGrid({
        colModel: [
            { name: "ColorId", label: "", key: true, hidden: true },
            { name: "ColorCode", width: 200, align: 'center', searchoptions: { sopt: ['cn'] }, label: 'Color Code' },
            { name: "ColorName", label: "Color Name", width: 100, align: 'center', search: false },
            { name: "ColorDescription", label: "Description", width: 100, align: 'center', search: false },
            //{ name: "ProcessNote", label: "Note", width: 100, align: 'center', search: false },
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
            //{ name: "CreatedBy", label: "Created By", width: 100, align: 'center', search: false },
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
            //{ name: "ModifiedBy", label: "Modified By", width: 100, align: 'center', search: false },
            { name: "Active", label: "Actived", hidden: true, align: 'center', formatter: ShowActiveStatus },
        ],
        jsonReader:
        {
            root: "result",
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
            $(`#gbox_colorGrid`).block({
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
        caption: 'Color',
        loadComplete: function () {
            $(`#gbox_colorGrid`).unblock();
            let ids = $colorGrid.getDataIDs();
            for (let i of ids) {
                let row = $colorGrid.getRowData(i);
                if (row.Active === "NO") {
                    $colorGrid.setCell(i, 'ColorCode', '', { 'background-color': '#ffcc99' }, '');
                    $colorGrid.setCell(i, 'ColorName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == colorId) {
                colorId = 0;
            }
            else {
                colorId = parseInt(rowid);
            }
            let row = $colorGrid.getRowData(rowid);
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

//RELOAD COLOR GRID
async function ReloadColorGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Color/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Color/Search?keyWord=${keyWord}`;
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
                        $colorGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        colorId = 0;
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
