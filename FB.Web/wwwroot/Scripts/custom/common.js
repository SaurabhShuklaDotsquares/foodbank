(function ($) {
    function CommonIndex() {
        var $this = this, formAddPledgeDonation;

        function initializeModalWithForm() {
            $("#pledge-popup").on('loaded.bs.modal', function (e) {
                
                $(".datepicker").datepicker({ format: "dd/mm/yyyy", startDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
                $('.datepicker').datepicker("setDate", new Date());

                formAddPledgeDonation = new Global.FormHelper($("#pledge-popup form"), { updateTargetId: "validation-summary" }, function (data) {
                    
                    if (data.isSuccess) {
                        $(".close").trigger('click');
                        alertify.dismissAll();
                        alertify.success(data.data);
                    }
                    else {
                        alertify.dismissAll();
                        alertify.error(data.data);
                    }
                });
            }).on('hidden.bs.modal', function (e) {
                $("#pledge-popup").find(".modal-content").html("");
                $(this).removeData('bs.modal');
                
            });
        }

        $this.init = function () {
            initializeModalWithForm();
        }
    }

    $(
        function () {
            var self = new CommonIndex();
            self.init();
        }
    )
})(jQuery)