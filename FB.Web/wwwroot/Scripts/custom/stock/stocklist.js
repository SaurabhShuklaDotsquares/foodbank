(function ($) {
    function MyReferralIndex() {
        var $this = this, myReferralGrid, formDeleteUserDefinedField;

        function initializeModalWithForm() {
            $("#modal-delete-stock").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-stock").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        $('#grid-my-stock').find("tr.selected").remove();
                        Global.ShowMessage("Stock deleted successfully.", Global.MessageType.Success);
                        initializeGridMyReferral();
                    }
                    else {
                        Global.ShowMessage("You can't delete this stock  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-stock").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }

        function initializeGridMyReferral() {
            if ($.fn.DataTable.isDataTable($this.myReferralGrid)) {
                $($this.myReferralGrid).DataTable().destroy();
            }
            $this.myReferralGrid = new Global.GridAjaxHelper('#grid-my-stock', {
                "aoColumns": [
                    { "sName": "Event" },
                    { "sName": "Id" },
                    { "sName": "Food.Name" },
                    { "sName": "TotalQuantity" },
                    { "sName": "Unit" },
                    { "sName": "PricePerItem" },
                    { "sName": "IsItemLowInStock" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 7] }, { 'visible': false, 'aTargets': [0] }],

            }, "Foodbank/Stock/StockList",
            );
            $("#grid-my-stock").parent("div").parent("div").addClass("table-responsive");
            $this.myReferralGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Stock/StockList";
            });
        }
        $this.init = function () {
            initializeGridMyReferral();
            initializeModalWithForm();
        }
    }
    

    $(
        function () {
            var self = new MyReferralIndex();
            self.init();
        }
    )
})(jQuery)