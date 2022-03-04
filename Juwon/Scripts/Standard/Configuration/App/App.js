const $appGrid = $(`#appGrid`);
var selRowData;
var poGridDetail_KeepSelectedID;
var poGridDetail_SelectedID;
const uploadAppBtn = document.getElementById(`uploadAppBtn`);
uploadAppBtn.disabled = true;
const $fileUploadApp = $(`#fileUploadApp`);
const fileUploadApp = document.getElementById(`fileUploadApp`);

// GRID
async function Grid() {
    "use strict";
    $appGrid.jqGrid({
        colModel: [
            { name: "ID", label: "", key: true, hidden: true },
            { name: "Name" },
            { name: "Type" },
            { name: "UrlApp", label: "Directory" },
            { name: "VesionApp", label: "Ver.", align: 'center' },
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
        //idPrefix: "ug_",
        rownumbers: true,
        //sortname: "Name",
        sortorder: "asc",
        threeStateSort: true,
        sortIconsBeforeText: true,
        headertitles: true,
        //toppager: true,
        pager: true,
        rowNum: 4,
        viewrecords: true,
        //width: null,
        autowidth: true,
        shrinkToFit: false,

        loadonce: true,
        height: "auto",
        maxHeight: 500,
        loadComplete: function () {
            CheckPermissions();
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            //selRowData = $appGrid.getRowData(rowid);
            poGridDetail_KeepSelectedID = null;
            //
            if (rowid == poGridDetail_SelectedID) {
                poGridDetail_SelectedID = null;
                uploadAppBtn.disabled = true;
            }
            else {
                poGridDetail_SelectedID = rowid;
                uploadAppBtn.disabled = false;
            }
        }
    });
};
Grid();
//RELOAD WO GRID 
async function ReloadAppGrid() {
    return new Promise(resolve => {
        $.ajax({
            url: `/App/Grid`,
        })
            .done(function (response) {
                if (response) {
                    if (response.Data.HttpResponseCode == 100) {
                        WarningAlert(response.ResponseMessage);
                    }
                    else {
                        $appGrid
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
                resolve(false);
            })
    })
}

//UPLOAD APP
async function UploadApp() {
    if (!fileUploadApp.value) {
        alert(`no`)
    }
    else {
        if (!poGridDetail_SelectedID) {
            return false;
        }

        selRowData = $appGrid.getRowData(poGridDetail_SelectedID);
        let obj = {
            ID: selRowData.ID,
            Name: selRowData.Name,
            Type: selRowData.Type,
            //File: $fileUploadApp[0].files,
        }

        let formData = new FormData();
        formData.append("data", JSON.stringify(obj));
        var file = fileUploadApp.files[0];
        formData.append("httpPostedFileBase", file);

        $.ajax({
            url: `/App/UploadApp`,
            type: "POST",
            data: formData,
            dataType: "json",
            contentType: false,
            processData: false,
        })
            .done(function (response) {
                if (response.Data && response.Data.HttpResponseCode != 100) {
                    SuccessAlert(response.ResponseMessage);
                    fileUploadApp.value = null;
                    ReloadAppGrid();
                    return true;
                }
                else {
                    ErrorAlert(response.ResponseMessage);
                    return false;
                }
            })

            .fail(function () {
                ErrorAlert(`Lỗi hệ thống - Vui lòng liên hệ IT.`);
                return false;
            });
    }
}