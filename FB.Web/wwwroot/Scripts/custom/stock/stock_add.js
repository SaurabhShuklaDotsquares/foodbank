(function ($) {
    'use strict';

    function GrantorIndex() {
        var $this = this, grantorStockGrid;

        function initializeModal() {
            $("#modal-grantor-stock").on('loaded.bs.modal', function (e) {
                HideLoader();
                $(".datepicker").datepicker({ format: "dd/mm/yyyy", startDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
                $('.datepicker').datepicker("setDate", new Date());

                formAddEditDonation = new Global.FormHelper($(this).find("form"),
                    {
                        updateTargetId: "validation-summary", beforeSubmit: function () {
                            if ($('form').valid()) {
                                return true;
                            }
                        }
                    }, function (data) {

                        if (data.isSuccess) {
                            $(".close").trigger('click');
                            alertify.dismissAll();
                            alertify.success(data.data);
                            initializeGridFoodDonation();
                        }
                        else {
                            alertify.dismissAll();
                            alertify.error(data.data);
                        }

                    });
            }).on('hidden.bs.modal', function (e) {
                $("#modal-grantor-stock").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }

        function initializeGridMyGrantor() {
            grantorStockGrid = new Global.GridAjaxHelper('#grid-grantor-stock', {
                "aoColumns": [
                    { "sName": "Id" },
                    { "sName": "SNo" },
                    { "sName": "Person.ForeName,Person.Surname" },
                    { "sName": "Quntity" },
                    { "sName": "TotalPrice" },
                    { "sName": "AddedDate" },
                    {
                        "sName": " "
                    },
                ],
                "bStateSave": true,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 6] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/Stock/GrantorStockQuantityList?stockId=" + $('#hndId').val(),
            );
            $("#grid-grantor-stock").parent("div").parent("div").addClass("table-responsive");
            grantorStockGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Stock/GrantorStockQuantityList?stockId=" + $('#hndId').val();
            });
            grantorStockGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Stock/GrantorStockQuantityList?stockId=" + $('#hndId').val();
            });
        }

        function InitializeOnLoadSection() {
            if ($("#hndId").val() == 0) {
                $("#ProductApiId").select2();
            }

            $("#FoodCategoryId").select2();
            $("#FoodCategoryId").select2().select2("val", null);
            $("#FoodCategoryId").select2().select2("val", $("#hdnFoodCategoryId").val());

            setTimeout(function () {
                $("#FoodCategoryId").trigger('change');
            }, 100);

            if ($("#hndId").val() == 0) {
                $("#FoodCategoryId").off("change").on("change", function () {
                    if ($(this).val() != "") {
                        ShowLoader();
                        $("#ProductApiId").html('');
                        $.get(Global.DomainName + "stock/GetFoodItemList", { categoryId: $(this).val() }, function (data) {
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
            }

            $("#ProductApiId").off("change").on("change", function () {
                var foodid = $("#ProductApiId option:selected").val();
                if (foodid != "") {
                    $('#FoodName').val($("#ProductApiId option:selected").text());
                    $.get(Global.DomainName + 'Foodbank/Stock/GetStockAvalability', { Foodid: foodid }, function (data) {
                        $("#AvailableQuantity").val(data);
                    });

                    $.get(Global.DomainName + 'Foodbank/Stock/GetFoodItemAllergyList', { foodItemId: foodid }, function (data) {
                        $("#AvailableQuantity").val(data);
                    });
                }
                else {
                }
            });

            if ($('#GrantorId').val() != null & $('#GrantorId').val() > 0) {
                $('.available-grantor').prop("checked", true);
            }

            $("input[name='IsGrantorMoney']").change(function () {
                if ($(this).val() == 'false') {
                    $("#Grantordiv").css("display", "none");
                }
                else {
                    $("#Grantordiv").css({ "display": "block" });
                }


            });

            $("#btn-submit").click(function (e) {
                $('form').valid();
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
                if ($("input[name='IsGrantorMoney']:checked").val() == "true") {
                    if ($("#GrantorId").val() == "") {
                        Global.Alert("Warning ", "Please select Grantor ");
                        return false;
                    }
                    else {
                        $('form').submit();
                    }
                }
                else {
                    $('form').submit();
                }


            })
        }


        $this.init = function () {
            InitializeOnLoadSection();
            initializeModal();
            initializeGridMyGrantor();
        };
    }

    $(function () {
        var self = new GrantorIndex();
        self.init();
    });
}(jQuery));