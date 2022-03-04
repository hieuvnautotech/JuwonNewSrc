/** view */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $destinationGrid = $(`#destinationGrid`);

/** create */
const cDestinationCode = document.getElementById(`cDestinationCode`);
const cDestinationName = document.getElementById(`cDestinationName`);
const cDestinationDescription = document.getElementById(`cDestinationDescription`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/** modify */
const mDestinationCode = document.getElementById(`mDestinationCode`);
const mDestinationName = document.getElementById(`mDestinationName`);
const mDestinationDescription = document.getElementById(`mDestinationDescription`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/** delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var destinationId = 0;

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

async function Initialize() {
    await DestinationGrid();
    await ReloadDestinationGrid();
}

//PROCESS GRID
async function DestinationGrid() {
    "use strict";
    $destinationGrid.jqGrid({
        colModel: [
            { name: "DestinationId", label: "", key: true, hidden: true },
            { name: "DestinationCode", width: 200, align: 'center', searchoptions: { sopt: ['cn'] } },
            { name: "DestinationName", label: "Name", width: 100, align: 'center', search: false },
            { name: "DestinationDescription", label: "Description", width: 100, align: 'center', search: false },
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
            $(`#gbox_destinationGrid`).block({
                message: '<img src="../../../Img/loading/hourglass.gif" />'
            });
        },
        beforeProcessing: function (data) {
        },
        loadonce: true,
        caption: 'Destination',
        loadComplete: function () {
            $(`#gbox_destinationGrid`).unblock();
            let ids = $destinationGrid.getDataIDs();
            for (let i of ids) {
                let row = $destinationGrid.getRowData(i);
                if (row.Active === "NO") {
                    $destinationGrid.setCell(i, 'DestinationCode', '', { 'background-color': '#ffcc99' }, '');
                    $destinationGrid.setCell(i, 'DestinationName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == destinationId) {
                destinationId = 0;
            }
            else {
                destinationId = parseInt(rowid);
            }
            let row = $destinationGrid.getRowData(rowid);
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

//RELOAD DESTINATION GRID
async function ReloadDestinationGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Destination/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Destination/Search?keyWord=${keyWord}`;
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
                        $destinationGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        destinationId = 0;
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