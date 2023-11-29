(function ($) {
    function ParcelTypeIndex() {
        var $this = this, foodDonationGrid, cashcardDonationGrid;

        function initializeModalWithForm() {
            HideLoader();
            //$("#FoodCategoryId, #ProductApiId").select2();
            //$("#FoodCategoryId").select2().select2("val", null);
            //$("#FoodCategoryId").select2().select2("val", $("#hdnFoodCategoryId").val());

            //setTimeout(function () {
            //    $("#FoodCategoryId").trigger('change');
            //}, 100);

            $("#FoodCategoryId").off("change").on("change", function () {
                if ($(this).val() != "") {
                    ShowLoader();
                    $("#FoodItemId").html('');
                    $.get(Global.DomainName + "stock/GetFoodItemList", { categoryId: $(this).val() }, function (data) {
                        var html;
                        $.each(data.data, function (index, item) {
                            html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $("#FoodItemId").append('<option value="" >Select</option>');
                        $("#FoodItemId").append(html);

                        $("#FoodItemId").select2().select2("val", null);
                        $("#FoodItemId").select2().select2("val", $("#hdnFoodProductId").val());
                        HideLoader();
                        setTimeout(function () {
                            $("#FoodItemId").trigger('change');
                        }, 100);
                    });
                }
            });

            //$("#ProductApiId").off("change").on("change", function () {
            //    var foodid = $("#ProductApiId option:selected").val();
            //    if (foodid != "") {
            //        $('#FoodItemName').val($("#ProductApiId option:selected").text());
            //    }
            //});


            $("#modal-view-donations").on('hidden.bs.modal', function () {
                $("#modal-view-donations").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }

        function initializeParcelTypeGrid() {
            if ($.fn.DataTable.isDataTable($this.parcelTypeGrid)) {
                $($this.parcelTypeGrid).DataTable().destroy();
            }
            $this.parcelTypeGrid = new Global.GridAjaxHelper('#grid-parceltype-master', {
                "aoColumns": [
                    { "sName": "#" },
                    { "sName": "S.NO" },
                    { "sName": "Name" },
                    { "sName": "ParcelFoodItem.Count" },
                    { "sName": "Adddate" },
                    {
                        "sName": ""
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [ 1,3, 5] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/ParcelType/BindPacelTypeList",
            );
            $("#grid-parceltype-master").parent("div").parent("div").addClass("table-responsive");
            $this.parcelTypeGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/ParcelType/BindPacelTypeList";
            });
            $this.parcelTypeGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/ParcelType/BindPacelTypeList";
            });
        }

        $this.init = function () {
            initializeParcelTypeGrid();
            initializeModalWithForm();
        }
    }

    $(
        function () {
            var self = new ParcelTypeIndex();
            self.init();
        }
    )
})(jQuery)