
(function ($) {

    function FoodbankReferrerIndex() {
        var $this = this, myReferrerGrid, myReferreralGrid;

        function InitalizeReferrerGrid() {
            if ($.fn.DataTable.isDataTable($this.myReferrerGrid)) {
                $($this.myReferrerGrid).DataTable().destroy();
            }
            Global.DataServer.dataURL = '';
            $this.myReferrerGrid = new Global.GridAjaxHelper('#foodbank-referrer-grid', {
                "aoColumns": [
                    { "sName": "#" },
                    { "sName": "Id" },
                    { "sName": "User.UserName" },
                    { "sName": "ServiceDescription" },
                    { "sName": "Contact.Mobile" },
                    { "sName": "IsStatus" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1,6] }, { 'visible': false, 'aTargets': [0] }],

            }, "Foodbank/FoodbankReferrer/ReferrerList?CharityId=" + $("#CharityIDRef").val(),
            );
            $("#foodbank-referrer-grid").parent("div").parent("div").addClass("table-responsive");
            $this.myReferrerGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/FoodbankReferrer/ReferrerList?CharityId=" + $("#CharityIDRef").val();
                Global.DataServer.dataURL = '';
            });
        }
        function InitalizeFoodbankReferreral() {
            if ($.fn.DataTable.isDataTable($this.myReferreralGrid)) {
                $($this.myReferreralGrid).DataTable().destroy();
            }
            Global.DataServer.dataURL = '';
            $this.myReferreralGrid = new Global.GridAjaxHelper('#foodbank-referral-grid', {
                "aoColumns": [
                    { "sName": "#" },
                    { "sName": "Id" },
                    { "sName": "AddedDate" },
                    { "sName": "Family.FamilyName" },
                    { "sName": "Family.Contactno" },
                    { "sName": "Family.Confirmed" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 6] }, { 'visible': false, 'aTargets': [0] }],

            }, "Foodbank/FoodbankReferrer/ReferralList?CharityId=" + $("#CharityID").val() + "&BranchId=" + $("#BranchID").val(),
            );
            $("#foodbank-referral-grid").parent("div").parent("div").addClass("table-responsive");
            $this.myReferreralGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/FoodbankReferrer/ReferralList?CharityId=" + $("#CharityID").val() + "&BranchId=" + $("#BranchID").val();
                Global.DataServer.dataURL = '';
            });
        }
        function initializeModalWithForm() {

            $('#CharityID').off("change").on('change', function () {
                
                $('#BranchID').html("<option value=''>Select Branch...</option>");

                if ($(this).val() != "") {
                    $("#BranchID").html('');
                    var html = "<option value=''>Select Branch...</option>";
                    var param = $('select#CharityID option:selected').val();

                    $.get('/foodbank/foodbankreferrer/BindBranches', { charityID: param }, function (data) {

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
                InitalizeFoodbankReferreral();
            });
            $('#BranchID').off("change").on('change', function () {
                InitalizeFoodbankReferreral();
            });
            $('#CharityIDRef').off("change").on('change', function () {
                InitalizeReferrerGrid();
            });
            $("#modal-delete-Referrer").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-delete-Referrer").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Referrer deleted successfully.", Global.MessageType.Success);
                        InitalizeReferrerGrid();
                    }
                    else {
                        Global.ShowMessage("You can't delete this Referrer  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-Referrer").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });

            $("#modal-accept-Referrer").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-accept-Referrer").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Referrer Accept successfully.", Global.MessageType.Success);
                        InitalizeReferrerGrid();
                    }
                    else {
                        Global.ShowMessage("You can't Accept this Referrer  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-accept-Referrer").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });

            $("#modal-reject-Referrer").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-reject-Referrer").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Referrer Reject successfully.", Global.MessageType.Success);
                        InitalizeReferrerGrid();
                    }
                    else {
                        Global.ShowMessage("You can't Reject this Referrer  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-reject-Referrer").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });

            $("#modal-postpone-Referrer").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-postpone-Referrer").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Referrer Postpone successfully.", Global.MessageType.Success);
                        InitalizeReferrerGrid();
                    }
                    else {
                        Global.ShowMessage("You can't Postpone this Referrer  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-postpone-Referrer").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
            $("#modal-accept-Referral").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-accept-Referral").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Family Accept successfully.", Global.MessageType.Success);
                        InitalizeFoodbankReferreral();
                    }
                    else {
                        Global.ShowMessage(result, Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-accept-Referral").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
            $("#modal-reject-Referral").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-reject-Referral").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Family Reject successfully.", Global.MessageType.Success);
                        InitalizeFoodbankReferreral();
                    }
                    else {
                        Global.ShowMessage("You can't Reject this Family  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-reject-Referral").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
            $("#modal-delete-Referral").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-delete-Referral").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Family Delete successfully.", Global.MessageType.Success);
                        InitalizeFoodbankReferreral();
                    }
                    else {
                        Global.ShowMessage("You can't Delete this Family  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-Referral").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
            $("#modal-postpone-Referral").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-postpone-Referral").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Family Postpone successfully.", Global.MessageType.Success);
                        InitalizeReferrerGrid();
                    }
                    else {
                        Global.ShowMessage("You can't Postpone this Referrer  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-postpone-Referral").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }
        $this.init = function () {
            InitalizeReferrerGrid();
            initializeModalWithForm();
            InitalizeFoodbankReferreral();
        };
    }

    $(function () {
        var self = new FoodbankReferrerIndex();
  
        self.init();
    });
}(jQuery));
