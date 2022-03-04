/* View */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $seasonGrid = $(`#seasonGrid`);


/* Create */
const cRecycleName = document.getElementById(`cRecycleName`);
const cRecycleDescription = document.getElementById(`cRecycleDescription`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/* Modify */
const mRecycleName = document.getElementById(`mRecycleName`);
const mRecycleDescription = document.getElementById(`mRecycleDescription`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/* Delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var seasonId = 0;
async function Initialize() {
    await SeasonGrid();
    await ReloadSeasonGrid();
}
$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

//PROCESS GRID
async function SeasonGrid() {
    "use strict";
    $seasonGrid.jqGrid({
        colModel: [
            { name: "SeasonId", label: "", key: true,hidden: true},
            { name: "SeasonCode", label: "Code", width: 100, align: 'center', searchoptions: { sopt: ['cn'] } },
            { name: "SeasonName", label: "Name", width: 100, align: 'center', search: false },
            { name: "SeasonDescription", label: "Description", width: 100, align: 'center', search: false },
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
        beforeRequest: function () {
            $(`#gbox_seasonGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
          
        },
        loadonce: true,
        caption: 'Season',
        loadComplete: function () {
            $(`#gbox_seasonGrid`).unblock();
            let ids = $seasonGrid.getDataIDs();
            for (let i of ids) {
                let row = $seasonGrid.getRowData(i);
                if (row.Active === "NO") {
                    $seasonGrid.setCell(i, 'SeasonCode', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },

        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == seasonId) {
                seasonId = 0;
            }
            else {
                seasonId = parseInt(rowid);
            }
            let row = $seasonGrid.getRowData(rowid);
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
//RELOAD SEASON GRID
async function ReloadSeasonGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Season/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Season/Search?keyWord=${keyWord}`;
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
                        $seasonGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        seasonId = 0;
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

