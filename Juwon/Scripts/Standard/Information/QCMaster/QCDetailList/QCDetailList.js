//QCMaster GRID
async function QCDetailGrid() {
    "use strict";
    $qcDetailGrid.jqGrid({
        colModel: [
            { name: "QCDetailId", label: "", key: true, hidden: true },
            { name: "QCDetailName", width: 200, align: 'center'},
            { name: "QCDetailDescription", label: "Name", width: 200, align: 'center', search: false },
            {
                name: "CreatedDate", width: 100, align: 'center', formatter: 'date', formatoptions:
                {
                    srcformat: "ISO8601Long", newformat: "Y-m-d"
                },
                sorttype: 'date',
                label: "Created Date",
            },
            {
                name: "ModifiedDate", width: 100, align: 'center', formatter: 'date', formatoptions:
                {
                    srcformat: "ISO8601Long", newformat: "Y-m-d"
                },
                sorttype: 'date',
                label: "ModifiedDate",
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
        rowNum: -1,
        viewrecords: true,
        shrinkToFit: false,
        height: 600,
        cmTemplate: { resizable: false },
        multiselect: true,
        //footerrow: true,
        beforeRequest: function () {
            $(`#gbox_qcDetailGrid`).block({
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
        caption: 'QC Detail',
        loadComplete: function () {
            $(`#gbox_qcDetailGrid`).unblock();
            let rowIds = $qcDetailGrid.getDataIDs();
            if (qcDetailIdList.length > 0) {
                for (let i = 0; i < qcDetailIdList.length; i++) {
                    if (rowIds.indexOf(qcDetailIdList[i]) > -1) {
                        $qcDetailGrid.setSelection(qcDetailIdList[i], true);
                    }
                    else {
                        $qcDetailGrid.setSelection(qcDetailIdList[i], false);
                    }
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == qcDetailId) {
                qcDetailId = 0;
            }
            else {
                qcDetailId = parseInt(rowid);
            }
        }
    })
}
QCDetailGrid();

//RELOAD QCDetail GRID
async function ReloadQCDetailGrid() {
    return new Promise(resolve => {
        let requestUrl = `/QCMaster/GetQCDetails`;
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
                        $qcDetailGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
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

async function DestroyQCDetailGrid() {
    delete $qcDetailGrid;
    $('#qcDetailGrid').GridUnload('#qcDetailGrid');
}

$(`#qcDetailListModal`).on(`show.bs.modal`, async function (e) {
    //await DestroyQCDetailGrid();
    //await QCDetailGrid();
    if (qcMasterId) {
        await GetQCDetailIds();
    }
    else {
        await SaveQCDetailIds();
    }
    await ReloadQCDetailGrid();
})

async function GetQCDetailIds() {
    if (qcMasterId) {
        $.ajax({
            url: `/QCMaster/GetQCDetailsByQCMasterId`,
            type: `GET`,
            data: {
                qcMasterId
            }
        })
            .done(function (response) {
                if (response.Data) {
                    if (response.HttpResponseCode == 100) {
                        WarningAlert(response.ResponseMessage);
                    }
                    else {
                        qcDetailIdList = [...response.Data.map(function (e) {
                            return e.QCDetailId;
                        })];
                    }
                }
            })
            .fail(function () {
                ErrorAlert(`System error - Please contact IT`);
            })
    }
}

async function SaveQCDetailIds() {
    let ids = $qcDetailGrid.jqGrid('getGridParam', 'selarrrow');
    qcDetailIdList = [...ids];
}
