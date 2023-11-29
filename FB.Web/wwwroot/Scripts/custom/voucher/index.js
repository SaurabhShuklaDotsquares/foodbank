(function ($) {
    function VoucherIndex() {
        var $this = this, voucherGrid;

        function initializeVoucherGrid() {
            if ($.fn.DataTable.isDataTable($this.voucherGrid)) {
                $($this.voucherGrid).DataTable().destroy();
            }
            $this.voucherGrid = new Global.GridAjaxHelper('#grid-voucher-master', {
                "aoColumns": [
                    { "sName": "VoucherId" },
                    { "sName": "S.NO" },
                    { "sName": "Family.FamilyName" },
                    { "sName": "Referrer.Name" },
                    { "sName": "AddedDate" },
                    { "sName": "VoucherToken" },
                    {
                        "sName": ""
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 6, 7] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/Voucher/BindVoucherList",
            );
            $("#grid-voucher-master").parent("div").parent("div").addClass("table-responsive");
            $this.voucherGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Voucher/BindVoucherList";
            });
            $this.voucherGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Voucher/BindVoucherList";
            });
        }

        $this.init = function () {
            initializeVoucherGrid();
        }
    }

    $(
        function () {
            var self = new VoucherIndex();
            self.init();
        }
    )
})(jQuery)