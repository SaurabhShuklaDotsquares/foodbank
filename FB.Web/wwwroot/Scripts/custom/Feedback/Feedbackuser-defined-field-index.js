
(function ($) {
    'use strict';
    function UserDefinedFieldIndex() {
        var $this = this, userDefinedFieldGrid, formAddEditUserDefinedField, formDeleteUserDefinedField;


        function initializeGrid() {
            if ($.fn.DataTable.isDataTable($this.userDefinedFieldGrid)) {
                $($this.userDefinedFieldGrid).DataTable().destroy();
            }
            $this.userDefinedFieldGrid = new Global.GridAjaxHelper('#grid-user-defined-field', {
                "aoColumns": [
                    { "sName": "FieldIddd" },
                    { "sName": "S.NO" },
                    { "sName": "FieldDescription" },
                    { "sName": "FieldType" },
                    { "sName": "FieldDefaultValue" },
                    { "sName": "IsAutoAssignDefaultValue_old" },
                    { "sName": " " }
                ],
                "bStateSave": true,
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets': [1, 6, 5] },
                    { 'visible': false, 'aTargets': [0,1] }
                ],
            }, "FoodBank/Feedback/getuserdefinedfields",
                Global.DeleteMasters);
            $("#grid-user-defined-field").parent("div").parent("div").addClass("table-responsive");

            $this.userDefinedFieldGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "FoodBank/Feedback/getuserdefinedfields";
            });
            $this.userDefinedFieldGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "FoodBank/Feedback/getuserdefinedfields";
            });
        }

        function initializeModalWithForm() {
            $("#modal-create-edit-user-defined-field").on('loaded.bs.modal', function () {
                initializeModalControlWithEvents();
                formAddEditUserDefinedField = new Global.FormHelper($("#frm-create-edit-user-defined-field form"), { updateTargetId: "validation-summary" }, function (result) {

                    if (result.isSuccess) {
                        Global.ModalClear($(this));
                        $("#modal-create-edit-user-defined-field").modal("hide");
                        Global.ShowMessage(result.data, Global.MessageType.Success);
                        initializeGrid();
                    }
                    else {
                        Global.ShowMessage(result.data, Global.MessageType.Error);
                    }

                });
            }).on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });

            $("#modal-delete-user-defined-field").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    $("#modal-delete-user-defined-field").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        $('#grid-user-defined-field').find("tr.selected").remove();
                        Global.ShowMessage("Question deleted successfully.", Global.MessageType.Success);
                    }
                    else {
                        Global.ShowMessage("You can't delete this Question  because it has families attached to it.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-membership-type").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }

        function initializeModalControlWithEvents() {

            $('#UserDefinedFieldOptionList').tokenfield().on('tokenfield:removedtoken', function (e) {
                bindUserDefinedFieldOptionList();
            });

            checkUserDefinedFieldType();
            bindUserDefinedFieldOptionList();
            $("#FieldDescription").on("blur", function () {
                Global.capitalize("FieldDescription", $("#FieldDescription").val());
            });

            $("#FieldType").off("change").on("change", function () {
                checkUserDefinedFieldType();
            });

            $('#UserDefinedFieldOptionList-tokenfield').keyup(function () {
                bindUserDefinedFieldOptionList();
            });
            $('#UserDefinedFieldOptionList-tokenfield').keydown(function () {
                bindUserDefinedFieldOptionList();
            });
            $('#UserDefinedFieldOptionList-tokenfield').keypress(function () {
                bindUserDefinedFieldOptionList();
            });
            $("#btn-submit").click(function (e) {
                $('form').valid();
                if ($("#FieldType").val() == "4" || $("#FieldType").val() == "5") { }
                else {
                    if ($("#IsAutoAssignDefaultValue")[0].checked == true) {
                        if ($("#FieldDefaultValue").val() == "") {
                            Global.Alert("Warning", "Please enter default value");
                            return false;
                        }
                    }
                    else {
                        $('form').submit();
                    }
                }

            })
        }

        function bindUserDefinedFieldOptionList() {
            if ($("#FieldType").val() == "4") {
                var str = $("#UserDefinedFieldOptionList").val();
                $('#ListFieldDefaultValue').html('');

                $.each(str.split(','), function (key, value) {
                    $('#ListFieldDefaultValue').append('<option value="' + value + '">' + (value == '-1' ? 'select' : value) + '</option>');
                });
                if ($("#hdnListFieldDefaultValue").val() == '') { }
                else { $("#ListFieldDefaultValue").val($("#hdnListFieldDefaultValue").val()); }

            } else {
                $('#ListFieldDefaultValue').html('');
            }
        }


        function checkUserDefinedFieldType() {
            if ($("#FieldType").val() == "4") {
                $(".user-defined-field-type-list").show()
                $(".user-defined-field-type-without-list").hide();
                $("#FieldDefaultValue").val('');
                $("#FieldDefaultValue").datepicker('remove').inputmask('remove');
                $(".user-defined-field-logical").hide();
            } else {
                $(".user-defined-field-type-without-list").show();
                $(".user-defined-field-type-list").hide()
                $('.tokenfield .token').remove();
                $("#UserDefinedFieldOptionList").val('');
                $("#FieldDefaultValue").datepicker('remove').inputmask('remove');
                $(".user-defined-field-logical").hide();
                bindUserDefinedFieldOptionList();
            }

            if ($("#FieldType").val() == "3") {
                $("#FieldDefaultValue").datepicker({ format: "dd/mm/yyyy" }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            } else {
                $("#FieldDefaultValue").datepicker('remove').inputmask('remove');
            }

            if ($("#FieldType").val() == "5") {
                $(".user-defined-field-logical").show()
                $(".user-defined-field-type-without-list").hide();
            } else {
                $(".user-defined-field-logical").hide()
            }
        }


        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }

    $(function () {
        var self = new UserDefinedFieldIndex();
        self.init();
    });
}(jQuery));