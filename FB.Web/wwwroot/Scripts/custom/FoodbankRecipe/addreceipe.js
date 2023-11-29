
(function ($) {

    function PersonIndex() {
        var $this = this, myRecipeGrid;

        function initializeModalWithForm() {
            $(".datepicker").datepicker({ format: "dd/mm/yyyy", endDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $('.datepicker').datepicker("setDate", new Date());
        }

        function InitializeOnLoadSection() {
            $(".Add-Recipe").select2({
                placeholder: "Select Recipe"
            });
        }

        function InitalizeRecipeGrid() {
            
        
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
            }, "Foodbank/Recipe/BindRecipeTypeFoodItemList?RecipeTypeId=" + $("#hdnParcelTypeId").val()
            );
            $("#grid-parceltype-fooditem").parent("div").parent("div").addClass("table-responsive");
            $this.foodItemGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Recipe/BindRecipeTypeFoodItemList?RecipeTypeId=" + $("#hdnParcelTypeId").val()
            });
            $this.foodItemGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Recipe/BindRecipeTypeFoodItemList?RecipeTypeId=" + $("#hdnParcelTypeId").val()
            });
        }
        $("#btn-submit").click(function (e) {
            $('form').valid();
          
            var Ingredients = $('Select[name="Ingredients"]').map(function () {
                return this
            }).get();
            var valueIngredients = '';
            for (var i = 0; i < Ingredients.length; i++) {
                var sub = '';
                if (Ingredients[i].selectedOptions.length == 0) {
                    //$("#Ingredientsddl").text('*required')
                    alertify.dismissAll();
                    alertify.error("Please select ingredients");
                    return false;
                }
                for (var j = 0; j < Ingredients[i].selectedOptions.length; j++) {
                    sub += Ingredients[i].selectedOptions[j].value + ',';
                }
                valueIngredients += sub;
            }
            $('#Ingredientsids').val(valueIngredients);
            $('form').submit();
        });

        function initializeModalWithForm() {

            $(".food-items").off("change").on("change", function () {
                if ($(this).val() != "") {
                    ShowLoader();
                    $.get(Global.DomainName + "Recipe/getfooditemunit", { foodId: $(this).val() }, function (result) {
                        if (result.data != '') {
                            HideLoader();
                            $("#QuantityUnit").val(result.data.unit);
                        
                        }
                        HideLoader();
                    });
                }
            });
            $("#modal-delete-recipe").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-recipe").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        $('#grid-my-recipe').find("tr.selected").remove();
                        Global.ShowMessage("Recipe deleted successfully.", Global.MessageType.Success);
                    }
                    else {
                        Global.ShowMessage("You can't delete this Recipe  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-parceltype-fooditem").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-parceltype-fooditem").modal("hide");

                    if (result.isSuccess) {
                        alertify.dismissAll();
                        Global.ShowMessage(result.data, Global.MessageType.Success);
                        initializeParcelTypeFoodItemGrid();

                        $(".img-loading-div").hide();


                        Global.ModalClear($("#modal-delete-parceltype-fooditem"));
                    }
                    else {
                        alertify.dismissAll();
                        Global.ShowMessage(result.data, Global.MessageType.Error);
                    }
                });
               


            }).on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }

        $this.init = function () {
            InitializeOnLoadSection();
            InitalizeRecipeGrid();
            initializeModalWithForm();
            initializeParcelTypeFoodItemGrid();
        };
    }

    $(function () {
        var self = new PersonIndex();
        self.init();
    });
}(jQuery));
