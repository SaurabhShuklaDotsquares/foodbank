(function ($) {
    function MyReferralIndex() {
        var $this = this, myReferralGrid, formDeleteUserDefinedField, mydeliveryGrid, formDeletedelivery;
        initializeModalWithForm();
        initializeModalWithFormdelivery();
        function initializeGridMyReferral() {
            if ($.fn.DataTable.isDataTable($this.myReferralGrid)) {
                $($this.myReferralGrid).DataTable().destroy();
            }
            Global.DataServer.dataURL = '';
            $this.myReferralGrid = new Global.GridAjaxHelper('#grid-my-volunteer', {
                "aoColumns": [
                    { "sName": "Event" },
                    { "sName": "Id" },
                    { "sName": "Contact.ForeName" },
                    { "sName": "Contact.Mobile" },
                    { "sName": "IndividualCouple" },
                    { "sName": "Packingordelivery" },
                    { "sName": "AddedDate" },
                    { "sName": "AddedDate" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 7] }, { 'visible': false, 'aTargets': [0] }],

            }, "Foodbank/AdminVolunteer/VolunteerList?CharityId=" + $("#CharityIDRef").val(),
            );
            $("#grid-my-volunteer").parent("div").parent("div").addClass("table-responsive");
            $this.myReferralGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/AdminVolunteer/VolunteerList?CharityId=" + $("#CharityIDRef").val();
            });
        }
        function initializeGridMyDelivery() {
            if ($.fn.DataTable.isDataTable($this.mydeliveryGrid)) {
                $($this.mydeliveryGrid).DataTable().destroy();
            } Global.DataServer.dataURL = '';
            $this.mydeliveryGrid = new Global.GridAjaxHelper('#grid-my-delivery', {
                "aoColumns": [
                    { "sName": "Event" },
                    { "sName": "Id" },
                    { "sName": "Contact.ForeName,Contact.Surname" },
                    { "sName": "Contact.Mobile" },
                    { "sName": "IndividualCouple" },
                    { "sName": "Packingordelivery" },
                    { "sName": "Event" },

                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 7] }, { 'visible': false, 'aTargets': [0, 1] }],

            }, "Foodbank/AdminVolunteer/VolunteerDeliveryList?CharityId=" + $("#CharityID").val(),
            );
            $("#grid-my-delivery").parent("div").parent("div").addClass("table-responsive");
            $this.mydeliveryGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/AdminVolunteer/VolunteerDeliveryList?CharityId=" + $("#CharityID").val();
            });
        }
        function initializeModalWithForm() {
            $('#CharityIDRef').off("change").on('change', function () {
                initializeGridMyReferral();
            });
            $('#CharityID').off("change").on('change', function () {
                initializeGridMyDelivery();
            });

            $("#modal-delete-volunteer").on('loaded.bs.modal', function () {
                formDeleteUserDefinedField = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-delete-volunteer").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Volunteer deleted successfully.", Global.MessageType.Success);
                        initializeGridMyReferral(); initializeGridMyDelivery();
                    }
                    else {
                        Global.ShowMessage("You can't delete this Volunteer  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-volunteer").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }
        function initializeModalWithFormdelivery() {


            $("#modal-delete-delivilery").on('loaded.bs.modal', function () {
                formDeletedelivery = new Global.FormDeleteHelper($("#frm-delete form"), {}, function (result) {
                    
                    $("#modal-delete-delivilery").modal("hide");

                    if (result.indexOf("Success") > -1) {
                        // $('#grid-my-volunteer').find("tr.selected").remove();

                        Global.ShowMessage("Volunteer deleted successfully.", Global.MessageType.Success);
                        initializeGridMyReferral(); initializeGridMyDelivery();
                    }
                    else {
                        Global.ShowMessage("You can't delete this Volunteer  because it something.", Global.MessageType.Error);
                    }
                });
            }).on('hidden.bs.modal', function () {
                Global.ModalClear($(this));
            });

            $("#modal-delete-volunteer").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }
        $this.init = function () {
            initializeGridMyReferral();
            initializeGridMyDelivery();
        }
    }

    $(
        function () {
            var self = new MyReferralIndex();
            self.init();

        }
    )
})(jQuery)