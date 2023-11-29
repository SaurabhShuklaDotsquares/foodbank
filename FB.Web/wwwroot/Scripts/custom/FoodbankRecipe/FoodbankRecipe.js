
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
            
            if ($.fn.DataTable.isDataTable($this.myRecipeGrid)) {
                $($this.myRecipeGrid).DataTable().destroy();
            }
            $this.myRecipeGrid = new Global.GridAjaxHelper('#recipe-grid', {
                "aoColumns": [
                    { "sName": "Event" },
                    { "sName": "Id" },
                    { "sName": "RecipeTitle" },
                    { "sName": "Ingredients" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1,3,4] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/Recipe/RecipeList",
            );
           
            $("#recipe-grid").parent("div").parent("div").addClass("table-responsive");
            $this.myRecipeGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Recipe/RecipeList";
            });
            $("#modal-view-donations").on('hidden.bs.modal', function () {
                $("#modal-view-donations").find(".modal-content").html("");
                $(this).removeData('bs.modal');
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


            $("#modal-delete-recipe").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-recipe").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        $('#grid-my-recipe').find("tr.selected").remove();
                        Global.ShowMessage("Recipe deleted successfully.", Global.MessageType.Success);
                        InitalizeRecipeGrid();
                       // window.location.reload();
                    }
                    else {
                        Global.ShowMessage("You can't delete this Recipe  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-recipe").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }

        $this.init = function () {
            InitializeOnLoadSection();
            InitalizeRecipeGrid();
            initializeModalWithForm();
            //initializeParcelTypeFoodItemGrid();
        };
    }

    $(function () {
        var self = new PersonIndex();
        self.init();
    });
}(jQuery));
