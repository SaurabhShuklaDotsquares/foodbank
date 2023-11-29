(function ($) {
    'use strict';
    function GrantorsReportIndex() {
        var $this = this;
        var exportReport = new ExportReport();

        function initializeWizard() {
            $('#rootwizard').bootstrapWizard({ 'tabClass': 'bwizard-steps' });
            grantorChangeEvents();
            WizardValidate();
        }
        function grantorChangeEvents() {
            
            $("#grantor").multipleSelect({
                width: '100%',
                placeholder: "Select Grantor",
                onClose: function () {
                    $("#GrantorId").val($("#grantor").val());
                },
                onOpen: function () {
                    if ($('#grantor option').length == 1) {
                        $("#grantor").multipleSelect("checkAll");
                        $("#GrantorId").val($("#grantor").val());
                    }
                },
                //onCheckAll: function () {
                //    $("#Branches").multipleSelect("checkAll");
                //}
            });

            if ($('#grantor option').length == 1) {
                $("#grantor").multipleSelect("checkAll");
                $("#GrantorId").val($("#grantor").val());
            }
        }


        function WizardValidate() {

            //$("#DateFrom").on("change", function () {
            //    var fromDate = $(this).val();
            //    if (fromDate && !($("#DateTo").val())) {
            //        document.getElementById("DateTo").min = fromDate;
            //    }
            //});
            //$("#DateTo").on("change", function () {
            //    var fromDate = $(this).val();
            //    if (fromDate && !($("#DateFrom").val())) {
            //        document.getElementById("DateFrom").max = fromDate;
            //    }
            //});
            $(".bwizard-steps li a").on("click", function () {
                return false;
            });


            $(".next a").on("click", function () {
                var IsStepOneValid = true;
                ScrollOrganisationDiv();
                if ($("#GrantorId").val() != "") {
                    if ($("#rootwizard li.active").find('a').attr('href') == "#tab1") {
                        if (IsStepOneValid) {
                            var frmRoleListOfPeople = new Global.FormHelper($("#frm-Role-list-of-people form"));
                            $(".img-loading-div").show();
                            $.post(Global.DomainName + "GrantorsReport/GetMembersByStepOneWizardValues", frmRoleListOfPeople.serialize(), function (result) {
                                
                                if (result.isSuccess) {
                                    if (result.data != "") {
                                        exportReport.SetRolePage(result.data);
                                        $(".rendertab2Data").show();
                                        $(".img-loading-div").hide();
                                    }
                                    else {
                                        $(".rendertab2Data").hide();
                                        $(".img-loading-div").hide();
                                        Global.Alert("Unable to Generate!", "Error Occurd");
                                    }
                                }
                                else {
                                    $(".rendertab2Data").hide();
                                    $(".img-loading-div").hide();
                                    Global.Alert("Error!", result.data);
                                }
                            });
                        }
                        else {

                            return false;
                        }
                        $(".img-loading-div").hide();
                    }
                }
                else {
                    Global.Alert("Error!", "Please select a grantor");
                    return false;
                }
            });

        }


        function ScrollOrganisationDiv() {
            $('html, body').animate({
                scrollTop: $(".organisationDiv").offset().top
            });
        }

        $this.init = function () {
            initializeWizard();
        };
    }

    $(function () {
        var self = new GrantorsReportIndex();
        self.init();
    });
}(jQuery));