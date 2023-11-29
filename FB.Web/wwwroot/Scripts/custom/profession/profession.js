(function ($) {
    function MyFoodDonation() {
        var $this = this;

        function initializeModalWithForm() {
            $("#modal-add-Profession").on('loaded.bs.modal', function (e) {

                formAddEditDonation = new Global.FormHelper($(this).find("form"), {}, function (data) {
                    if (data.isSuccess) {
                        $("#btn-cancel").trigger('click');
                        initializeProfession();
                    }
                    else {
                        alertify.dismissAll();
                        alertify.error(data.data);
                    }
                });

            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-Profession").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });
        }

        function initializeProfession() {
            if ($.fn.DataTable.isDataTable($this.ProfessionGrid)) {
                $($this.ProfessionGrid).DataTable().destroy();
            }
            $this.ProfessionGrid = new Global.GridAjaxHelper('#grid-Profession', {
                "aoColumns": [
                    { "sName": "FoodItemId" },//0
                    { "sName": "S.NO" },//1
                    { "sName": "Title" },//2
                    { "sName": "ModifiedDate" },//3
                    { "sName": "Action" },//4
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 4] }, { 'visible': false, 'aTargets': [0] }],
            }, "FoodBank/Profession/Index?foodBankId=" + $("#hdnFoodBankId").val(),
                Global.DeleteMasters);
            $("#grid-Profession").parent("div").parent("div").addClass("table-responsive");
            $this.ProfessionGrid.on('search.dt', function () { // for searching
                Global.DataServer.dataURL = "FoodBank/Profession/Index?foodbankId=" + $("#hdnFoodBankId").val();
            });
            $this.ProfessionGrid.on('length.dt', function () { // for pagination
                Global.DataServer.dataURL = "FoodBank/Profession/Index?foodbankId=" + $("#hdnFoodBankId").val();
            });
        }

        $this.init = function () {
            initializeProfession();
            initializeModalWithForm();
        }
    }

    $(
        function () {
            var self = new MyFoodDonation();
            self.init();
        }
    )
})(jQuery)