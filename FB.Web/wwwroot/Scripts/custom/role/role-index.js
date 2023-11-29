
(function ($) {
    'use strict';
    function RoleIndex() {
        var $this = this, roleGrid, formAddEditRole, formRolePermission;
        var assignPermission = new AssignPermission();

        function reinitializeGrid() {
            Global.DataServer.dataURL = "";
            Global.DataServer.dataURL = "role/getroles?organisationId=" + $("div.filter-data #CentralOfficeID").val();
            Global.DataServer.multisearch = [];
            roleGrid.fnDraw(false);
        }

        function initializeGrid() {

            roleGrid = new Global.GridAjaxHelper('#grid-role', {
                "aoColumns": [{ "sName": "Id" },{ "sName": "RoleName" }, { "sName": "Description" }],
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1,2] }, { 'visible': false, 'aTargets': [3] }],
            }, "role/getroles");

            $('#grid-role').parent("div").parent("div").addClass("table-responsive");
        }

        function initializeControlWithEvents() {
            $("div.filter-data #CentralOfficeID").off("change").on("change", function () {
                if (roleGrid != null)
                    reinitializeGrid();
                else
                    initializeGrid();
            });
        }

        function initializeModalWithForm() {
            $("#modal-create-edit-role").on('loaded.bs.modal', function () {
                formAddEditRole = new Global.FormHelper($("#frm-create-edit-role form"), { updateTargetId: "validation-summary" }, function (result) {
                    if (result.isSuccess) {
                        $("#modal-create-edit-role").modal("hide");
                        Global.ModalClear($("#modal-create-edit-role"));
                        Global.ShowMessage(result.data, Global.MessageType.Success);
                        reinitializeGrid();
                    } else {
                        $('span[data-valmsg-for="CentralOfficeID"]').text('');
                        if ($("#modal-create-edit-role #CentralOfficeID").val() == "") {
                            $('span[data-valmsg-for="CentralOfficeID"]').text('*required');
                            $("#modal-create-edit-role #CentralOfficeID").focus();
                        } else {
                            Global.ShowMessage(result.data, Global.MessageType.Error);
                        }
                       
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-role").on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-assign-permission").on('loaded.bs.modal', function () {
                assignPermission.initializeRoleMenuWithEvent();
                formRolePermission = Global.FormHelper($("#frm-assign-permission form"), { updateTargetId: "validation-summary" }, function (result) {
                    if (result.isSuccess) {
                        $("#modal-assign-permission").modal("hide");
                        Global.ModalClear($("#modal-assign-permission"));
                        Global.ShowMessage(result.data, Global.MessageType.Success);
                        reinitializeGrid();
                    } else {
                        Global.ShowMessage(result.data, Global.MessageType.Error);
                    }
                }
                );
                return formRolePermission;
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });
        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
            initializeControlWithEvents();
        };
    }

    $(function () {
        var self = new RoleIndex();
        self.init();
    });

}(jQuery));