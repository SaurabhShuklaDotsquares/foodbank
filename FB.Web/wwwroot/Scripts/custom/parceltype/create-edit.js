(function ($) {
    function CreateEditParcelTypeIndex() {
        var $this = this, foodDonationGrid, cashcardDonationGrid;

        function initializeModalWithForm() {
            $(".food-items").off("change").on("change", function () {
                if ($(this).val() != "") {
                    ShowLoader();
                    $.get(Global.DomainName + "parceltype/getfooditemunit", { foodId: $(this).val() }, function (result) {
                        if (result.data != '') {
                        
                            $("#QuantityUnit").val(result.data.unit);
                            HideLoader();
                        }
                        HideLoader();
                    });
                }
            });

            $("#modal-view-donations").on('hidden.bs.modal', function () {
                $("#modal-view-donations").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }

        function initializeParcelTypeFoodItemGrid() {
            if ($.fn.DataTable.isDataTable($this.foodItemGrid)) {
                $($this.foodItemGrid).DataTable().destroy();
            }
            $this.foodItemGrid = new Global.GridAjaxHelper('#grid-parceltype-fooditem', {
                "aoColumns": [
                    { "sName": "ParcelTypeId" },
                    { "sName": "S.NO" },
                    { "sName": "Food.Name" },
                    { "sName": "Quantity" },
                    {
                        "sName": ""
                    },
                ],
                "bStateSave": false,
                "bFilter": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 4] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/ParcelType/BindPacelTypeFoodItemList?parcelTypeId=" + $("#hdnParcelTypeId").val()
            );
            $("#grid-parceltype-fooditem").parent("div").parent("div").addClass("table-responsive");
            $this.foodItemGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/ParcelType/BindPacelTypeFoodItemList?parcelTypeId=" + $("#hdnParcelTypeId").val()
            });
            $this.foodItemGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/ParcelType/BindPacelTypeFoodItemList?parcelTypeId=" + $("#hdnParcelTypeId").val()
            });
        }

        $this.init = function () {
            initializeParcelTypeFoodItemGrid();
            initializeModalWithForm();
        }
    }

    $(
        function () {
            var self = new CreateEditParcelTypeIndex();
            self.init();
        }
    )
})(jQuery)