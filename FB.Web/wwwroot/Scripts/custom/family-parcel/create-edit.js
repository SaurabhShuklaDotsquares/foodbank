(function ($) {
    function CreateEditFamilyParcel() {
        var $this = this;
        function OnlyIntvaluepacel(textbox) {
            if (textbox != undefined) {
                if (textbox.value.length > 5) {

                    textbox.value = '';//textbox.value.replace(/(\..*)\./g, '$1');;
                }
                textbox.value = textbox.value.replace(/[^0-9]/g, '');
                textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
            }
            

        }
        function initializeForm() {
            if ($("#FamilyId").val() == 0) {
                $(".divfamily").hide();
            }
           
           
            if ($(".ddlParcelTypeId").val() != "" && $(".ddlParcelTypeId").val() == 1) {
                $("#divStandardType").show();
            } else {
                $("#divStandardType").hide();
            }

            if ($("#RecipeId").val() != "" || $("#RecipeId").val() != 0) {
                $("#IncludeRecipe").prop("checked", true);
                $(".divIsInclude").show();
            } else {
                $("#IncludeRecipe").prop("checked", false);
                $(".divIsInclude").hide();
            }

            $(".food-items").off("change").on("change", function () {
                if ($(this).val() != "") {
                    ShowLoader();
                    $.get(Global.DomainName + "parceltype/getfooditemunit", { foodId: $(this).val() }, function (result) {
                        if (result.data != '') {
                            $("#QuantityUnit").val(result.data.unit);
                            $("#AvailableQuantity").val(result.data.totalQuantity);
                            HideLoader();
                        }
                        HideLoader();
                    });
                }
            });

            $(".datepicker").datepicker({ format: "dd/mm/yyyy", startDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });

            $(".ddlParcelTypeId").off("change").on("change", function () {
                if ($(this).val() == 1) {
                    $(".divfamily").show();
                    $("#divStandardType").show();

                } else if ($(this).val() == 2) {
                    $(".divfamily").hide();
                    $("#FamilyId").val("");
                    $("#StandardParcelId").val("");
                    $("#divStandardType").hide();
                    $("#fooditemtable").html("");
                }
                else {
                    $(".divfamily").show();
                    $("#StandardParcelId").val("");
                    $("#divStandardType").hide();
                    $("#fooditemtable").html("");
                }
            });

            $("#Quantity").off("blur").on("blur", function () {
                if ($("#FoodItemId").val() != "" && $("#FoodItemId").val() != 0 && $(this).val() != "") {
                    $.get(Global.DomainName + "familyparcel/CheckStockItemAvailabilty", { foodId: $("#FoodItemId").val(), quantity: $(this).val() }, function (result) {
                        if (result.data != true) {
                            alertify.dismissAll();
                            alertify.error("Selected food quantity is low in stock. Please enter sufficient  quantity.");
                            return false;
                        }
                    });

                }
            });

            //$(".tbl-quantity").off("blur").on("blur", function () {
            //    if ($(this).attr("data-foodId") != "" && $(this).attr("data-foodId") != 0 && $(this).val() != "") {
            //        $.get(Global.DomainName + "familyparcel/CheckStockItemAvailabilty", { foodId: $(this).attr("data-foodId"), quantity: $(this).val() }, function (result) {
            //            if (result.data != true) {
            //                alertify.dismissAll();
            //                alertify.error("Selected food quantity is low in stock. Please enter sufficient  quantity.");
            //                isFalse = true;
            //                return false;
            //            }
            //        });
            //    }
            //});

            $("#IncludeRecipe").off("click").on("click", function () {
                if ($(this).prop("checked")) {
                    $(".divIsInclude").show();
                } else {
                    $("#RecipeId").val("");
                    $(".divIsInclude").hide();
                }
            });

            $(".ddlStandardParcelTypeId").off("change").on("change", function () {
                var empTab2 = document.getElementById('fooditemtable');
                var countrow2 = empTab2.rows;
                for (var i = 0; i < countrow2.length; i++) {
                    if (countrow2[i].className == 'odd norecord') {
                        countrow2[i].remove();
                    }
                }
                var empTab = document.getElementById('fooditemtable');
                var countrow = empTab.rows;
                for (var i = 0; i < countrow.length; i++) {
                    if (countrow[i].className == 'Standard') {
                        countrow[i].remove();
                    }
                }

                $.get(Global.DomainName + "familyparcel/getfooditemlistbyparceltypeid", { standardParcelTypeId: $(this).val() }, function (result) {
                    if (result.data != '') {
                        ShowLoader();
                       
                        $.each(result.data, function (index, item) {
                            var html555 = ' ';
                            html555 += '   <tr class="Standard">';
                            html555 += '<td width="50%"><input name="hdnfoodItemId" type="hidden" value="' + item.foodItemId + '" /><input class="form-control" placeholder="Food Item" name="subfamilyname" type="text" value="' + item.foodItemName + '" disabled></td>';
                            html555 += '  <td><input class="form-control" onkeypress="OnlyIntvaluepacel(this)" onkeyup="OnlyIntvaluepacel(this)" placeholder="Quantity" name="itemquantity" value="' + item.quantity + '" type="number" min="1" max="99999"></td>';
                            html555 += '<td style="">';
                            html555 += '  <a class="deleterow" style="display:block;" href="">';
                            html555 += '       <img src="/Content/images/delete.png" width="22px"/>';
                            html555 += '    </a>';
                            html555 += ' </td>';
                            html555 += ' </tr>';
                            $("#fooditemtable").append(html555);
                            OnlyIntvaluepacel();
                        });
                        HideLoader();
                    }
                    else {
                        HideLoader();
                    }
                });



                $("#FoodItemId").val("");
                $("#Quantity").val("");
                $("#QuantityUnit").val("");
            });
            $(".ddlRecipeId").off("change").on("change", function () {
                debugger;

                var empTab2 = document.getElementById('fooditemtable');
                var countrow2 = empTab2.rows;
                for (var i = 0; i < countrow2.length; i++) {
                    if (countrow2[i].className == 'odd norecord') {
                        countrow2[i].remove();
                    }
                }
                var empTab = document.getElementById('fooditemtable');
                var countrow = empTab.rows;
                for (var i = 0; i < countrow.length; i++) {
                    if (countrow[i].className == 'Recipe') {
                        countrow[i].remove();
                    }
                }
                $.get(Global.DomainName + "familyparcel/GetFoodItemListByRecipeId", { recipeId: $(this).val() }, function (result) {
                    if (result.data != '') {
                        ShowLoader();
                        debugger;
                      
                      

                        $.each(result.data, function (index, item) {
                            var html555 = ' ';
                            html555 += '   <tr class="Recipe">';
                            html555 += '<td width="50%"><input name="hdnfoodItemId" type="hidden" value="' + item.foodItemId + '" /><input class="form-control" placeholder="Food Item" name="subfamilyname" type="text" value="' + item.foodItemName + '" disabled></td>';
                            html555 += '  <td><input class="form-control" onkeypress="OnlyIntvaluepacel(this)" onkeyup="OnlyIntvaluepacel(this)" placeholder="Quantity" name="itemquantity" value="' + item.quantity + '" type="number" min="1" max="99999"></td>';
                            html555 += '<td style="">';
                            html555 += '  ';
                            html555 += '      ' ;
                            html555 += '    ';
                            html555 += ' </td>';
                            html555 += ' </tr>';
                            $("#fooditemtable").append(html555);
                            OnlyIntvaluepacel();
                        });
                        HideLoader();
                        $(".ddlStandardParcelTypeId").trigger("change");
                    }
                    else {
                        HideLoader();
                    }
                });


               
                $("#FoodItemId").val("");
                $("#Quantity").val("");
                $("#QuantityUnit").val("");
            });


            $("#btnaddfooditem").off("click").on("click", function () {

                if ($("#FoodItemId").val() == "" || $("#FoodItemId").val() == 0) {
                    alertify.dismissAll();
                    alertify.error("Please select food item.");
                    return false;
                }

                if ($("#Quantity").val() == "") {
                    alertify.dismissAll();
                    alertify.error("Please enter the quantity.");
                    return false;
                }
                if ($("#Quantity").val() == 0) {
                    alertify.dismissAll();
                    alertify.error("Please enter the quantity greater than 0.");
                    return false;
                }

                $.get(Global.DomainName + "familyparcel/CheckFoodItemIsAlreadyExits", { standardParcelTypeId: $("#ParcelId").val(), foodId: $("#FoodItemId").val() }, function (result) {
                    if (result.data == true) {
                        alertify.dismissAll();
                        alertify.error("Selected food item is already in list. Please add another item.");
                        return false;
                    }
                });

                if ($("#FoodItemId").val() != "" && $("#FoodItemId").val() != 0 && $("#Quantity").val() != "") {
                    $.get(Global.DomainName + "familyparcel/CheckStockItemAvailabilty", { foodId: $("#FoodItemId").val(), quantity: $("#Quantity").val() }, function (result) {
                        if (result.data != true) {
                            alertify.dismissAll();
                            alertify.error("Selected food quantity is low in stock. Please enter sufficient   quantity.");
                            return false;
                        }
                        else {
                            if ($("#fooditemtable").html().includes('No records found')) {
                                $("#fooditemtable").html("");
                            }
                            var html555 = ' ';
                            html555 += '   <tr>';
                            html555 += '<td width="50%"><input name="hdnfoodItemId" type="hidden" value="' + $("#FoodItemId").val() + '" /><input class="form-control" placeholder="Food Item" name="subfamilyname" type="text" value="' + $("#FoodItemId option:selected").text() + '" readonly></td>';
                            html555 += '  <td><input class="form-control" onkeypress="OnlyIntvaluepacel(this)" onkeyup="OnlyIntvaluepacel(this)" placeholder="Quantity" name="itemquantity" value="' + $("#Quantity").val() + '" type="number" min="1" max="99999"></td>';
                            html555 += '<td style="">';
                            html555 += '  <a class="deleterow" style="display:block;" href="">';
                            html555 += '       <img src="/Content/images/delete.png" width="22px"/>';
                            html555 += '    </a>';
                            html555 += ' </td>';
                            html555 += ' </tr>';
                            $("#fooditemtable").append(html555);

                            $("#FoodItemId").val("");
                            $("#Quantity").val("");
                            $("#QuantityUnit").val("");
                            OnlyIntvaluepacel();
                        }
                    });
                }
            });

            $('body').delegate('.deleterow', 'click', function () {
                var empTab = document.getElementById('tablefamilyfooditem');
                empTab.deleteRow(this.parentNode.parentNode.rowIndex);
            });

            $("#btn-submit").click(function (e) {
                if ($("form").valid() == false) {
                    return false;
                }
                if ($("#fooditemtable").html().includes('No records found')) {
                    alertify.dismissAll();
                    alertify.error("Please add atleast one food item in parcel.");
                    return false;
                }
                
                if ($("#ParcelTypeId").val() != 2)
                {
                    if ($("#FamilyId").val() == "")
                    {
                        alertify.dismissAll();
                        alertify.error("Please select family.");
                        return false;
                    }

                }
                
                var empTab = document.getElementById('tablefamilyfooditem');
                var countrow = empTab.rows.length-1;
                if (countrow == 0) {
                    alertify.dismissAll();
                    alertify.error("Please add atleast one food item in parcel.");
                    return false;
                }
                $.ajax({
                    method: 'post',
                    data: $('form').serialize(),
                    success: function (data) {
                        if (data.isSuccess) {
                            alertify.dismissAll();
                            alertify.success(data.data);
                        } else {
                            alertify.dismissAll();
                            alertify.error(data.data);
                        }
                    }
                });
            })
        }

        function initializeFamilyParcelItemGrid() {
        }

        $this.init = function () {
            initializeFamilyParcelItemGrid();
            initializeForm();
            OnlyIntvaluepacel();
        }
    }

    $(
        function () {
            var self = new CreateEditFamilyParcel();
            self.init();
        }
    )
})(jQuery)