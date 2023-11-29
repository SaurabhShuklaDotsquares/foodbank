
(function ($) {

    function myDonation() {
        var $this = this, declarationGrid;

        function InitlizePage() {
            $(".datepicker").datepicker({ format: "dd/mm/yyyy" }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $('.datepicker').datepicker("setDate", new Date());

            $("#btnArchiveDeclaration").off("click").on("click", function () {
                var DeclarationDate = $("#DeclarationDate").val();
                var ValidForm = $("#ValidForm").val();
                var ValidTo = $("#ValidTo").val();
                var personID = $("#hdnPersonID").val();
                var flag = true;

                if (DeclarationDate.trim() != "" && ValidForm.trim() != "" && ValidTo.trim() != "") {
                    if (Global.ToDate(ValidTo.trim(), "/") < Global.ToDate(ValidForm.trim(), "/"))
                        flag = false;

                    if (flag) {
                        var dic = {}
                        dic["DeclarationDate"] = DeclarationDate;
                        dic["ValidForm"] = ValidForm;
                        dic["ValidTo"] = ValidTo;
                        dic["PersonID"] = personID;

                        $.get(Global.DomainName + 'Donor/IsDeclarationNotExist', { declarationDates: dic }, function (result) {
                            if (result.isSuccess != undefined) {
                                if (result.isSuccess) {
                                    Global.Confirm("Declaration History", "The declaration will be archived and used to validate tax claimable donations in future claims. Archived declarations are available to view below under the 'Declaration History' tab.", function () {
                                        $.post(Global.DomainName + 'Donor/SaveDeclarationHistory', { declarationDates: dic }, function (result) {
                                            if (result.isSuccess) {
                                                initializeDonorDeclarationHistoryTable(personID);
                                                Global.ShowMessage(result.data, Global.MessageType.Success);
                                                EmptyDeclarationDates();
                                            }
                                            else {
                                                Global.ShowMessage(result.data, Global.MessageType.Error);
                                            }
                                        });
                                    }, function () { return false; });
                                }
                                else if (result.data == "") {
                                    Global.ShowMessage("Entered declaration dates are already exist in declaration history", Global.MessageType.Error);
                                }
                                else {
                                    Global.ShowMessage(result.data, Global.MessageType.Error);
                                }
                            }
                        });
                    }
                    else
                        Global.ShowMessage("Valid to date should be greater than valid from date", Global.MessageType.Error);
                }
                else
                    Global.ShowMessage("Declaration dates should not be blank", Global.MessageType.Error);
            });

            $(document).off('click', '#btn-savedeclaration').on('click', '#btn-savedeclaration', function () {

                var obj = {
                    DeclarationDate: $("#DeclarationDate").val(),
                    ValidForm: $("#ValidForm").val(),
                    ValidTo: $("#ValidTo").val(),
                    PersonId: $("#hdnPersonID").val(),
                    Reference: $("#Reference").val(),
                    UserName: $("#UserName").val()
                };

                if ($("#FormSaveDeclaration").valid()) {

                    $.ajax({
                        url: "/donor/SaveDeclaration",
                        type: "POST",
                        data: obj,
                        dataType: 'json',
                        success: function (data) {
                            if (data.isSuccess) {
                                alertify.dismissAll();
                                alertify.success(data.data);
                                initializeGrid();
                            }
                            else {
                                alertify.dismissAll();
                                alertify.error(data.data);
                            }
                        },
                        error: function (data) {

                        },
                        complete: function () {

                        }
                    });
                }


            });

        }

        function initializeGrid() {
            if ($.fn.DataTable.isDataTable($this.declarationGrid)) {
                $($this.declarationGrid).DataTable().destroy();
            }
            $this.declarationGrid = new Global.GridAjaxHelper('#grid-declaration', {
                "aoColumns": [
                    { "sName": "PersonId" },
                    { "sName": "sno" },
                    { "sName": "DateDeclarationSigned" },
                    { "sName": "DateDeclarationValidFrom" },
                    { "sName": "DateDeclarationValidTo" }
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1] }, { 'visible': false, 'aTargets': [0] }],
            }, "Donor/GetDeclarationList?personId=" + $("#hdnPersonID").val(),
                Global.DeleteMasters);
            $("#grid-declaration").parent("div").parent("div").addClass("table-responsive");
            $this.declarationGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Donor/GetDeclarationList?personId=" + $("#hdnPersonID").val();
            });
            $this.declarationGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Donor/GetDeclarationList?personId=" + $("#hdnPersonID").val();
            });
        }

        function EmptyDeclarationDates() {
            $("#DeclarationDate").val('');
            $("#ValidForm").val('');
            $("#ValidTo").val('');
        }

        $this.init = function () {
            initializeGrid();
            InitlizePage();
        };
    }

    $(function () {
        var self = new myDonation();
        self.init();
    });
}(jQuery));