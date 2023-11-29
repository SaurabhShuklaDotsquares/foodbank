
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
                                            emptyDeclarationDates();
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
                        //contentType: 'application/json',
                        success: function (data) {
                            
                            if (data.isSuccess) {
                                alertify.dismissAll();
                                alertify.success(data.data);
                                DeclarationFilter();
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
            
            declarationGrid = new Global.GridAjaxHelper('#grid-declaration', {
                "aoColumns": [
                    { "sName": "sno" },
                    { "sName": "DeclarationDate" },
                    { "sName": "ValidForm" },
                    { "sName": "ValidTo" }
                ],
                "bStateSave": true,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [3] }],
            }, "Donor/GetDeclarationList",
                Global.DeleteMasters);
            $("#grid-declaration").parent("div").parent("div").addClass("table-responsive");
            declarationGrid.on('search.dt', function () {

                DeclarationFilter();
                Global.DataServer.dataURL = "Donor/GetDeclarationList";
            });
            declarationGrid.on('length.dt', function () {

                DeclarationFilter();
                Global.DataServer.dataURL = "Donor/GetDeclarationList";
            });
        }

        function DeclarationFilter() {
            Global.DataServer.multisearch = $.grep(Global.DataServer.multisearch, function (elem, index) {
                return elem.column != "PersonId";
            });

            if ($("#hdnPersonID").val() != "") {
                
                Global.DataServer.multisearch.push({ "column": "PersonId", "filter": Global.FilterType.Equals, "value": $("#hdnPersonID").val() });
            }
        }

        function reinitializeGrid() {

            Global.DataServer.dataURL = "";
            Global.DataServer.dataURL = "Donor/GetDeclarationList";
            declarationGrid.fnDraw(false);
            Global.DataServer.dataURL = "";
            Global.DataServer.multisearch = [];
        }

        function emptyDeclarationDates() {
            $("#DeclarationDate").val('');
            $("#ValidForm").val('');
            $("#ValidTo").val('');
        }

        $this.init = function () {
            initializeGrid();
            InitlizePage();
            DeclarationFilter();
            reinitializeGrid();
        };

    }
    

    $(function () {
        var self = new myDonation();
        self.init();
    });
}(jQuery));