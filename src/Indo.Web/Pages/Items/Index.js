﻿$(function () {
    /* Localization */
    var l = abp.localization.getResource('Indo');

    /* load grid once */
    loadGrid();

    /* Syncfusion */
    ej.base.enableRipple(window.ripple);
    var grid = new ej.grids.Grid({
        enableInfiniteScrolling: false,
        allowPaging: true,
        pageSettings: { currentPage: 1, pageSize: 100, pageSizes: ["10", "20", "100", "200", "All"] },
        allowSorting: true,
        allowFiltering: true,
        filterSettings: { type: 'Excel' },
        allowSelection: true,
        selectionSettings: { persistSelection: true, type: 'Single' },
        enableHover: true,
        allowExcelExport: true,
        allowPdfExport: true,
        allowTextWrap: false,
        toolbar: [
            'ExcelExport', 'Search',
            { type: 'Separator' },
            { text: 'Add', tooltipText: 'Add', prefixIcon: 'e-add', id: 'AddCustom' },
            { text: 'Edit', tooltipText: 'Edit', prefixIcon: 'e-edit', id: 'EditCustom' },
            { text: 'Delete', tooltipText: 'Delete', prefixIcon: 'e-delete', id: 'DeleteCustom' },
            { type: 'Separator' },
            { text: 'Help', tooltipText: 'Help', id: 'HelpCustom' },
        ],
        columns: [
            { type: 'checkbox', width: 30 },
            {
                field: 'id', isPrimaryKey: true, headerText: 'ID', visible: false
            },
            {
                field: 'name', headerText: 'Item', textAlign: 'Left', width: 100,
            },
            {
                field: 'ItemShortCode', headerText: 'Item Code', textAlign: 'Left', width: 150
            },
            {
                field: 'CostforCompany', headerText: 'Cost for Company', textAlign: 'Left', width: 100,
                
            },
            {
                field: 'Quantity', headerText: 'Quantity', textAlign: 'Left', width: 100,
            },
            {
                field: 'ComeUnderWhichProduct', headerText: 'ComesUnder Which Product', textAlign: 'Left', width: 100,
            },
        ],
        beforeDataBound: () => {
            grid.showSpinner();
        },
        dataBound: () => {
            grid.element.querySelector('.e-toolbar-left').style.left = grid.element.querySelector('.e-toolbar-right').getBoundingClientRect().width + 'px';
            grid.autoFitColumns();
            grid.hideSpinner();
        },
        excelExportComplete: () => {
            grid.hideSpinner();
        },
        created: () => {
            var gridHeight = ($(".pcoded-main-container").height()) - ($(".e-gridheader").outerHeight()) - ($(".e-toolbar").outerHeight()) - ($(".e-gridpager").outerHeight());
            grid.height = gridHeight;
        },
        rowSelecting: () => {
            if (grid.getSelectedRecords().length) {
                grid.clearSelection();
            }
        },
        toolbarClick: (args) => {
            if (args.item.id === 'HelpCustom') {
                helpModal.open();
            }
            if (args.item.id === 'Grid_excelexport') {
                grid.showSpinner();
                grid.excelExport();
            }
            if (args.item.id === 'AddCustom') {
                createModal.open();
            }
            if (args.item.id === 'EditCustom') {
                if (grid.getSelectedRecords().length) {
                    var selectedId = grid.getSelectedRecords()[0].id;
                    updateModal.open({ id: selectedId });
                }
                else {
                    abp.notify.error(l('SelectRecordError'));
                }
            }
            if (args.item.id === 'DeleteCustom') {
                if (grid.getSelectedRecords().length) {
                    var selectedRow = grid.getSelectedRecords()[0];
                    abp.message.confirm('Delete this data: ' + selectedRow.name)
                        .then(function (confirmed) {
                            if (confirmed) {
                                indo.item.item
                                    .delete(selectedRow.id)
                                    .then(function () {
                                        abp.notify.success(l('DeleteSuccess'));
                                        reloadGrid();
                                    })
                                    .catch(function (error) {
                                        abp.notify.error("Error: " + error.message + " " + error.details);
                                    });
                            }
                        });
                }
                else {
                    abp.notify.error(l('SelectRecordError'));
                }
            }

        },
    }
});
function reloadGrid() {
    indo.item.item.getList()
        .then(function (data) {
            grid.dataSource = data;
        });
}
function loadGrid() {
    indo.item.item.getList()
        .then(function (data) {
            grid.dataSource = data;
            grid.appendTo('#Grid');
        });
}


/* Popup Modal */
abp.modals.EmployeeModal = function () {

    function initModal(modalManager, args) {
        $(".custom-select").select2({
            dropdownParent: $('.modal-body')
        });
    };

    return {
        initModal: initModal
    };
}
var helpModal = new abp.ModalManager(abp.appPath + 'Item/Help');
var createModal = new abp.ModalManager({
    viewUrl: abp.appPath + 'Item/Create',
    modalClass: 'ItemModal'
});
var updateModal = new abp.ModalManager({
    viewUrl: abp.appPath + 'Item/Update',
    modalClass: 'ItemModal'
});
createModal.onResult(function () {
    abp.notify.success(l('CreateSuccess'));
    reloadGrid();
});
updateModal.onResult(function () {
    abp.notify.success(l('UpdateSuccess'));
    reloadGrid();
});

});
