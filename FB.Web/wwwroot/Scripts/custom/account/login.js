(function ($) {
    'use strict';
    function Login() {
        var $this = this, formForgotCredentials;
        function initializeModalWithForm() {
            $("#modal-forgot-credentials").on('loaded.bs.modal', function () {

                formForgotCredentials = new Global.FormHelper($("#frm-forgot-cred form"), { updateTargetId: "validation-summary" }, function (result) {
                    if (result.isSuccess) {
                        Global.Alert("Info", result.data, function () {
                            $("#modal-forgot-credentials").modal("hide");
                        });
                    }
                    else {
                        Global.Alert("Error", result.data);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
        }

        $this.init = function () {
            $("#frmLogin").validate();
            initializeModalWithForm();
        };
    }

    $(function () {
        var self = new Login();
        self.init();
    });

}(jQuery));