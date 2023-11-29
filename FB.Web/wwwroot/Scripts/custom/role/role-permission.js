
(function ($) {
    'use strict';
    function RolePermission() {
        var $this = this, roleGrid, formRolePermission;
        var assignPermission = new AssignPermission();

        function initializeModalWithForm() {
            $("#modal-assign-permission").on('loaded.bs.modal', function () {
                assignPermission.initializeRoleMenuWithEvent();
                formRolePermission = Global.FormHelper($("#frm-assign-permission form"), { updateTargetId: "validation-summary" });
                return formRolePermission;
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
        }

        $this.init = function () {
            initializeModalWithForm();
        };
    }

    $(function () {
        var self = new RolePermission();
        self.init();
    });

}(jQuery));