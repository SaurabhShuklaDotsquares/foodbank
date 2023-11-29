(function ($) {
    function DonationIndex() {
        var $this = this, foodDonationGrid, paymentDonationGrid;

        function initializeModalWithForm() {
            $("#modal-add-food-donations").on('loaded.bs.modal', function (e) {
                HideLoader();
                $(".datepicker").datepicker({ format: "dd/mm/yyyy", startDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
                $('.datepicker').datepicker("setDate", new Date());

                $(".food-items").select2();
                $("#FoodCategoryId").select2().select2("val", null);
                $("#FoodCategoryId").select2().select2("val", $("#hdnFoodCategoryId").val());

                setTimeout(function () {
                    $("#FoodCategoryId").trigger('change');
                }, 100);

                $("#FoodCategoryId").off("change").on("change", function () {
                    if ($(this).val() != "") {
                        ShowLoader();
                        $("#ProductApiId").html('');
                        $.get(Global.DomainName + "donor/GetFoodItemList", { categoryId: $(this).val() }, function (data) {
                            var html;
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#ProductApiId").append('<option value="" >Select</option>');
                            $("#ProductApiId").append(html);

                            $("#ProductApiId").select2().select2("val", null);
                            $("#ProductApiId").select2().select2("val", $("#hdnFoodProductId").val());
                            HideLoader();
                            setTimeout(function () {
                                $("#ProductApiId").trigger('change');
                            }, 100);
                        });
                    }
                });

                $("#ProductApiId").off("change").on("change", function () {
                    var foodid = $("#ProductApiId option:selected").val();
                    if (foodid != "") {
                        $('#FoodItemName').val($("#ProductApiId option:selected").text());
                    }
                });

                formAddEditDonation = new Global.FormHelper($(this).find("form"),
                    {
                        updateTargetId: "validation-summary", beforeSubmit: function () {
                            if ($('form').valid()) {
                                if ($('#FoodCategoryId').val() == "" || $('#FoodCategoryId').val() == null) {
                                    alertify.dismissAll();
                                    alertify.error("Please select food category.");
                                    return false;
                                }
                                if ($('#ProductApiId').val() == "" || $('#ProductApiId').val() == null) {
                                    alertify.dismissAll();
                                    alertify.error("Please select food item.");
                                    return false;
                                }
                                return true;
                            }
                        }
                    }, function onSuccess(data) {
                        if (data.isSuccess) {
                            $(".close").trigger('click');
                            alertify.dismissAll();
                            alertify.success(data.data);
                            initializeGrid();
                        }
                        else {
                            alertify.dismissAll();
                            alertify.error(data.data);
                        }

                    });
            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-food-donations").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }

        function initializeGrid() {
            if ($.fn.DataTable.isDataTable($this.foodDonationGrid)) {
                $($this.foodDonationGrid).DataTable().destroy();
            }
            $this.foodDonationGrid = new Global.GridAjaxHelper('#grid-food-donation', {
                "aoColumns": [{ "sName": "FoodItemId" },
                { "sName": "S.NO" },
                { "sName": "AddedDate" },
                { "sName": "Food.Name" },
                { "sName": "Quntity" },
                { "sName": "Status" }
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 4] }, { 'visible': false, 'aTargets': [0] }],
            }, "donor/mydonation?personId=" + $("#hdnPersonID").val(),
                Global.DeleteMasters);
            $("#grid-food-donation").parent("div").parent("div").addClass("table-responsive");
            $this.foodDonationGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "donor/mydonation?personId=" + $("#hdnPersonID").val();
            });
            $this.foodDonationGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "donor/mydonation?personId=" + $("#hdnPersonID").val();
            });
        }

        function initializeGridPayment() {
            if ($.fn.DataTable.isDataTable($this.paymentDonationGrid)) {
                $($this.paymentDonationGrid).DataTable().destroy();
            }
            $this.paymentDonationGrid = new Global.GridAjaxHelper('#grid-payment-donation', {
                "aoColumns": [
                    { "sName": "PaymentGateway" },
                    { "sName": "S.No" },
                    { "sName": "CreatedDate" },
                    { "sName": "" },
                    { "sName": "PaymentGateway" },
                    { "sName": "Amount" },
                    { "sName": "GASDSEligible" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 3] }, { 'visible': false, 'aTargets': [0] }],
            }, "donor/MyDonationPayment?personId=" + $("#hdnPersonID").val(),
            );
            $("#grid-payment-donation").parent("div").parent("div").addClass("table-responsive");
            $this.paymentDonationGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "donor/MyDonationPayment?personId=" + $("#hdnPersonID").val();
            });
            $this.paymentDonationGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "donor/MyDonationPayment?personId=" + $("#hdnPersonID").val();
            });
        }

        $this.init = function () {
            initializeGrid();
            initializeGridPayment();
            initializeModalWithForm();
        }
    }

    $(
        function () {
            var self = new DonationIndex();
            self.init();
        }
    )
})(jQuery)