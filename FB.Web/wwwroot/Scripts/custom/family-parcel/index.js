(function ($) {
    function FamilyParcelIndex() {
        var $this = this;

        function initializeForm() {
            $("#modal-download-qrcode").on('hidden.bs.modal', function () {
                $("#modal-download-qrcode").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }

        function initializeFamilyParcelGrid() {
            if ($.fn.DataTable.isDataTable($this.familyparcelGrid)) {
                $($this.familyparcelGrid).DataTable().destroy();
            }
            $this.familyparcelGrid = new Global.GridAjaxHelper('#grid-family-parcel-master', {
                "aoColumns": [
                    { "sName": "ParcelTypeId" },
                    { "sName": "S.NO" },
                    { "sName": "ParcelTypeId" },
                    { "sName": "Family.FamilyName" },
                    { "sName": "DeliveredDate" },
                    { "sName": "DeliveryDate" },
                    { "sName": "Status" },
                    {
                        "sName": ""
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 7] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/FamilyParcel/BindFamilyPacelList",
            );
            $("#grid-family-parcel-master").parent("div").parent("div").addClass("table-responsive");
            $this.familyparcelGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/FamilyParcel/BindFamilyPacelList";
            });
            $this.familyparcelGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/FamilyParcel/BindFamilyPacelList";
            });
        }

        $this.init = function () {
            initializeFamilyParcelGrid();
            initializeForm();
        }
    }

    $(
        function () {
            var self = new FamilyParcelIndex();
            self.init();
        }
    )
})(jQuery)