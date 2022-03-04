/** view */
const modifyBtn = document.getElementById(`modifyBtn`);
const deleteBtn = document.getElementById(`deleteBtn`);
const searchBtn = document.getElementById(`searchBtn`);
const searchInput = document.getElementById(`searchInput`);
const showDeleted = document.getElementById(`showDeleted`);
const $vendorGrid = $(`#vendorGrid`);

/** create */
const cVendorCode = document.getElementById(`cVendorCode`);
const cVendorName = document.getElementById(`cVendorName`);
const cVendorAddress = document.getElementById(`cVendorAddress`);
const cVendorPhone = document.getElementById(`cVendorPhone`);
const cDestinationId = document.getElementById(`cDestinationId`);
const cVendorCategoryId = document.getElementById(`cVendorCategoryId`);
const btnConfirmToCreate = document.getElementById(`btnConfirmToCreate`);

/** modify */
const mVendorCode = document.getElementById(`mVendorCode`);
const mVendorName = document.getElementById(`mVendorName`);
const mVendorAddress = document.getElementById(`mVendorAddress`);
const mVendorPhone = document.getElementById(`mVendorPhone`);
const mDestinationId = document.getElementById(`mDestinationId`);
const mVendorCategoryId = document.getElementById(`mVendorCategoryId`);
const btnConfirmToModify = document.getElementById(`btnConfirmToModify`);

/** delete */
const btnConfirmToDelete = document.getElementById(`btnConfirmToDelete`);

var vendorId = 0;
var createSelectedDestination;
var modifySelectedDestination;
var createSelectedVendorCategory;
var modifySelectedVendorCategory;
var createSelectedBuyers;
var modifySelectedBuyers;

async function Initialize() {
    await VendorGrid();
    await ReloadVendorGrid();
    await GetDestination();
    await GetVendorCategory();
    await GetBuyers();

    createSelectedDestination = new SlimSelect({
        select: '#cDestinationId',
        placeholder: 'Select Destination',
        hideSelectedOption: true
    });

    createSelectedVendorCategory = new SlimSelect({
        select: '#cVendorCategoryId',
        placeholder: 'Select Destination',
        hideSelectedOption: true
    });

    createSelectedBuyers = new SlimSelect({
        select: '#cBuyer',
        placeholder: 'Select Buyers',
        hideSelectedOption: true
    });
    
}

async function GetDestination() {
    $.ajax({
        url: `/Vendor/GetDestination`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.DestinationName < b.DestinationName) return -1
                    return a.DestinationName > b.DestinationName ? 1 : 0
                });

                $.each(response.Data, function (key, item) {
                    if (item.Active == true) {
                        html += `<option  value="${item.DestinationId}">${item.DestinationName}</option>`;
                    }

                });
                $(`#cDestinationId`).html(html);
                $(`#mDestinationId`).html(html);
            }
        });
}

async function GetVendorCategory() {
    $.ajax({
        url: `/Vendor/GetVendorCategory`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.VendorCategoryName < b.VendorCategoryName) return -1
                    return a.VendorCategoryName > b.VendorCategoryName ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    if (item.Active == true) {
                        html += `<option value="${item.VendorCategoryId}">${item.VendorCategoryName}</option>`;
                    }

                });
                $(`#cVendorCategoryId`).html(html);
                $(`#mVendorCategoryId`).html(html);
            }
        });
}

async function GetBuyers() {
    $.ajax({
        url: `/Vendor/GetBuyers`,
    })
        .done(function (response) {
            let html = ``;
            if (response.Data.length > 0) {
                response.Data.sort((a, b) => {
                    if (a.BuyerName < b.BuyerName) return -1
                    return a.BuyerName > b.BuyerName ? 1 : 0
                });


                $.each(response.Data, function (key, item) {
                    if (item.Active == true) {
                        html += `<option value="${item.BuyerId}">${item.BuyerName}</option>`;
                    }

                });
                $(`#cBuyer`).html(html);
                $(`#mBuyer`).html(html);
            }
        });
}

$(`#searchInput`).on(`keypress`, function (e) {
    if (e.which == 13) {
        $(`#searchBtn`).trigger(`click`);
    }
});

//PROCESS GRID
async function VendorGrid() {
    "use strict";
    $vendorGrid.jqGrid({
        colModel: [
            { name: "VendorId", label: "", key: true, hidden: true },
            { name: "VendorCode", width: 100, align: 'left', searchoptions: { sopt: ['cn'] } },
            { name: "VendorName", label: "Name", width: 250, align: 'left', search: false },
            { name: "VendorAddress", label: "Address", width: 200, align: 'left', search: false },
            { name: "VendorPhone", label: "Phone", width: 100, align: 'center', search: false },
            { name: "DestinationName", label: "Destination", width: 100, align: 'center', search: false },
            { name: "DestinationId", label: "", hidden: true },
            { name: "VendorCategoryId", label: "", hidden: true },
            { name: "VendorCategoryName", label: "Keyword", width: 100, align: 'center', search: false },
            {
                name: "CreatedDate", width: 80, align: 'center', formatter: 'date', formatoptions:
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
                name: "ModifiedDate", width: 80, align: 'center', formatter: 'date', formatoptions:
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
            $(`#gbox_vendorGrid`).block({
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
        caption: 'Vendor',
        loadComplete: function () {
            $(`#gbox_vendorGrid`).unblock();
            let ids = $vendorGrid.getDataIDs();
            for (let i of ids) {
                let row = $vendorGrid.getRowData(i);
                if (row.Active === "NO") {
                    $vendorGrid.setCell(i, 'VendorCode', '', { 'background-color': '#ffcc99' }, '');
                    $vendorGrid.setCell(i, 'VendorName', '', { 'background-color': '#ffcc99' }, '');
                }
            }
        },
        onSelectRow: function (rowid, status, e, iRow, iCol) {
            if (parseInt(rowid) == vendorId) {
                vendorId = 0;
            }
            else {
                vendorId = parseInt(rowid);
            }
            let row = $vendorGrid.getRowData(rowid);
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

//RELOAD PROCESS GRID
async function ReloadVendorGrid() {
    return new Promise(resolve => {
        let keyWord = searchInput.value == null ? "" : searchInput.value;
        let requestUrl = ``;
        if (showDeleted.checked) {
            requestUrl = `/Vendor/SearchAll?keyWord=${keyWord}`;
        }
        else {
            requestUrl = `/Vendor/Search?keyWord=${keyWord}`;
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
                        $vendorGrid
                            .clearGridData()
                            .setGridParam({
                                data: response.Data === null ? "" : response.Data,
                                datatype: 'local',
                            }).trigger(`reloadGrid`);
                        vendorId = 0;
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
