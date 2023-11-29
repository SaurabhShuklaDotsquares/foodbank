(function ($) {
    function MyFoodDonation() {
        var $this = this, foodDonationGrid, cashcardDonationGrid;

        function initializeModalWithForm() {
            $("#modal-add-food-donations").on('loaded.bs.modal', function (e) {
                HideLoader();
                $(".datepicker").datepicker({ format: "dd/mm/yyyy", startDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
                $('.datepicker').datepicker("setDate", new Date());

                $("#FoodCategoryId, #ProductApiId").select2();
                $("#FoodCategoryId").select2().select2("val", null);
                $("#FoodCategoryId").select2().select2("val", $("#hdnFoodCategoryId").val());

                setTimeout(function () {
                    $("#FoodCategoryId").trigger('change');
                }, 100);


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


                $('#CharityID3').off("change").on('change', function () {
                    
                    $('#BranchID3').html("<option value=''>Select Branch</option>");

                    if ($(this).val() != "") {
                        $("#BranchID3").html('');
                        var html = "<option value=''>Select Branch</option>";
                        var param = $('select#CharityID3 option:selected').val();

                        $.get('BindBranches', { charityID: param }, function (data) {

                            if (data.data.length > 1) {


                                $.each(data.data, function (index, item) {
                                    html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                                });
                                $("#BranchID3").html(html);
                            }
                            else {
                                $('#BranchID3').removeAttr('disabled');
                                $.each(data.data, function (index, item) {
                                    html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                });
                                $("#BranchID3").html(html);
                            }
                        });
                    }
                    binddonorlist();
                });
                $('#BranchID3').off("change").on('change', function () {
                    binddonorlist();
                });

            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-food-donations").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
            //$("#modal-delete-donations").on('loaded.bs.modal', function () {
            //    formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
            //        if (result.isSuccess) {

            //        }
            //    });
            //}).on('hidden.bs.modal', function () {
            //    Global.ModalClear($(this));
            //});
            $('#CharityID').off("change").on('change', function () {
                
                $('#BranchID').html("<option value=''>Select Branch</option>");

                if ($(this).val() != "") {
                    $("#BranchID").html('');
                    var html = "<option value=''>Select Branch</option>";
                    var param = $('select#CharityID option:selected').val();

                    $.get('BindBranches', { charityID: param }, function (data) {

                        if (data.data.length > 1) {


                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#BranchID").html(html);
                        }
                        else {
                            $('#BranchID').removeAttr('disabled');
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#BranchID").html(html);
                        }
                    });
                }
                initializeGridFoodDonation();
            });
            $('#BranchID').off("change").on('change', function () {
                initializeGridFoodDonation();
            });
            $('#CharityID2').off("change").on('change', function () {
                
                $('#BranchID2').html("<option value=''>Select Branch</option>");

                if ($(this).val() != "") {
                    $("#BranchID2").html('');
                    var html = "<option value=''>Select Branch</option>";
                    var param = $('select#CharityID2 option:selected').val();

                    $.get('BindBranches', { charityID: param }, function (data) {

                        if (data.data.length > 1) {


                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#BranchID2").html(html);
                        }
                        else {
                            $('#BranchID2').removeAttr('disabled');
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#BranchID2").html(html);
                        }
                    });
                }
                initializeGridPayment();
            });
            $('#BranchID2').off("change").on('change', function () {
                initializeGridPayment();
            });


            $("#modal-view-donations").on('hidden.bs.modal', function () {
                $("#modal-view-donations").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }
        function binddonorlist() {
            
            var param = $('select#BranchID3 option:selected').val();
            var param2 = $('select#CharityID3 option:selected').val();

            $.get('BindDonorList', { charityID: param2, Branchid: param,}, function (data) {

                var html = "<option value=''>Select </option>";
                if (data.data.length > 1) {
                    $.each(data.data, function (index, item) {
                        html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                    });
                    $("#DonorId").html(html);
                }
                else {
                    $('#DonorId').removeAttr('disabled');
                    $.each(data.data, function (index, item) {
                        html = "<option value='" + item.value + "'>" + item.text + "</option>";
                    });
                    $("#DonorId").html(html);
                }
            });
        }
        function initializeGridFoodDonation() {
            if ($.fn.DataTable.isDataTable($this.foodDonationGrid)) {
                $($this.foodDonationGrid).DataTable().destroy();
            }
            $this.foodDonationGrid = new Global.GridAjaxHelper('#grid-food-donation', {
                "aoColumns": [{ "sName": "FoodItemId" },
                { "sName": "S.NO" },
                { "sName": "Donor.Forenames,Donor.Surname" },
                { "sName": "ExpiryDate" },
                { "sName": "Food.Name" },
                { "sName": "Quntity" },
                { "sName": "Status" },
                { "sName": " " }
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 5, 7] }, { 'visible': false, 'aTargets': [0] }],
            }, "FoodBank/Donation/MyFoodDonation?foodbankId=" + $("#hdnFoodBankId").val() +"&charitID=" + $("#CharityID").val() + " &BranchID=" + $("#BranchID").val(),
                Global.DeleteMasters);
            $("#grid-food-donation").parent("div").parent("div").addClass("table-responsive");
            $this.foodDonationGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "FoodBank/Donation/MyFoodDonation?foodbankId=" + $("#hdnFoodBankId").val() + "&charitID=" + $("#CharityID").val() + " &BranchID=" + $("#BranchID").val();
            });
            $this.foodDonationGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "FoodBank/Donation/MyFoodDonation?foodbankId=" + $("#hdnFoodBankId").val() + "&charitID=" + $("#CharityID").val() + " &BranchID=" + $("#BranchID").val();
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
            }, "foodbank/donation/MyDonationPayment?foodbankId=" + $("#hdnFoodbankId").val() + "&charitID=" + $("#CharityID2").val() + " &BranchID=" + $("#BranchID2").val(),
            );
            $("#grid-payment-donation").parent("div").parent("div").addClass("table-responsive");
            $this.paymentDonationGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "foodbank/donation/MyDonationPayment?foodbankId=" + $("#hdnFoodbankId").val() + "&charitID=" + $("#CharityID2").val() + " &BranchID=" + $("#BranchID2").val();
            });
            $this.paymentDonationGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "foodbank/donation/MyDonationPayment?foodbankId=" + $("#hdnFoodbankId").val() + "&charitID=" + $("#CharityID2").val() + " &BranchID=" + $("#BranchID2").val();
            });
        }

        $this.init = function () {
            initializeGridFoodDonation();
            initializeModalWithForm();
            initializeGridPayment();
           // binddonorlist();
        }
    }

    $(
        function () {
            var self = new MyFoodDonation();
            self.init();
        }
    )
})(jQuery)