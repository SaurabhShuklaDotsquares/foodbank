(function ($) {
    'use strict';
    function ParcelsReportIndex() {
        var $this = this;
        var exportReport = new ExportReport();

        function initializeWizard() {
            $('#rootwizard').bootstrapWizard({ 'tabClass': 'bwizard-steps' });
            WizardValidate();
        }

        function WizardValidate() {
            
            $("#DateFrom").on("change", function () {
                var fromDate = $(this).val();
                if (fromDate && !($("#DateTo").val())) {
                    document.getElementById("DateTo").min= fromDate;
                }
            });
            $("#DateTo").on("change", function () {
                var fromDate = $(this).val();
                if (fromDate && !($("#DateFrom").val())) {
                    document.getElementById("DateFrom").max = fromDate;
                }
            });
            //if ($("#DateFrom").val() || $("#DateTo").val()) {
            //    if ($("#DateFrom").val() && $("#DateTo").val()) {
            //        IsStepOneValid = true;
            //    }
            //}
            $(".bwizard-steps li a").on("click", function () {
                return false;
            });


            $(".next a").on("click", function () {
                var IsStepOneValid = true;
                //$('span[data-valmsg-for="DateFrom"]').text('');
                //$('span[data-valmsg-for="DateTo"]').text('');
                
                
                //if ($("#DateFrom").val() || $("#DateTo").val()) {
                //    IsStepOneValid = false
                //    if ($("#DateFrom").val() && !($("#DateTo").val())) {
                //        $('span[data-valmsg-for="DateTo"]').text('*required');
                //        //$("#DateTo").attr('required', '');
                //        //$("#DateFrom").removeAttr('required');
                //    }
                //    if ($("#DateTo").val() && !($("#DateFrom").val())) {
                //        $('span[data-valmsg-for="DateFrom"]').text('*required');
                //        //$("#DateFrom").attr('required', '');
                //        //$("#DateTo").removeAttr('required');
                //    }
                //    if (($("#DateFrom").val()) && ($("#DateTo").val())) {
                //        IsStepOneValid = true;
                //    }
                //}
                ScrollOrganisationDiv();

                if ($("#rootwizard li.active").find('a').attr('href') == "#tab1") {
                    if (IsStepOneValid) {
                        var frmRoleListOfPeople = new Global.FormHelper($("#frm-Role-list-of-people form"));
                        $(".img-loading-div").show();
                        $.post(Global.DomainName + "ParcelsReport/GetMembersByStepOneWizardValues", frmRoleListOfPeople.serialize(), function (result) {
                            
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
                    else
                    {
                        
                        return false;
                    }
                    $(".img-loading-div").hide();
                }

                //if ($("#rootwizard li.active").find('a').attr('href') == "#tab2") {
                //    $(".img-loading-div").show();
                //    var familyIds = [];
                //    $("#grid-family-list-step-two tbody tr").each(function () {
                //        var row = $(this);
                //        if (row.find("td").eq(2).find("input[type=checkbox]").is(":checked")) {
                //            familyIds.push(row.find("td").eq(1).html());
                //        }
                //    });
                //    if (!familyIds.length > 0) {
                //        Global.ShowMessage("Please select at least one family.", Global.MessageType.Error);
                //        $(".img-loading-div").hide();
                //        return false;
                //    }
                //    else {
                //        $(".img-loading-div").show();
                //        GenerateReport();
                //    }
                //    $(".img-loading-div").hide();
                //}

            });

        }


        function ScrollOrganisationDiv() {
            $('html, body').animate({
                scrollTop: $(".organisationDiv").offset().top
            });
        }



        //function GridCheckboxAllEvent() {

        //    $('#grid-family-list-step-two > tbody').on('click', 'td:nth-child(4)', function () {
        //        var trchk = $(this).parent('tr').find('input[type="checkbox"]');

        //        if (trchk.is(":checked")) {
        //            trchk.prop('checked', false);
        //        } else {
        //            trchk.prop('checked', true);
        //        }
        //    });

        //    $('#chkAll').click(function (e) {
        //        var table = $(e.target).closest('table');
        //        $('td input:checkbox', table).prop('checked', this.checked);
        //    });
        //}


        //function GenerateReport() {
        //    var familyIds = [];
        //    $('#grid-family-list-step-two > tbody').find('input[type="checkbox"]:checked').each(function () {
        //        familyIds.push($(this).attr('data-id'));
        //    });
        //    if (familyIds.length > 0) {
        //        $("#FamailyIds").val(familyIds);
        //        var frmRoleListOfPeople = new Global.FormHelper($("#frm-Role-list-of-people form"));
        //        if (frmRoleListOfPeople.valid()) {
        //            $.ajax({
        //                url: Global.DomainName + 'FamilyReport/Index',
        //                data: frmRoleListOfPeople.serialize(),
        //                type: 'POST',
        //                success: function (result) {
        //                    if (result.isSuccess == true) {
        //                        if (result.data != "") {
        //                            exportReport.SetRolePage(result.data);
        //                            $(".rendertab3Data").show();
        //                        }
        //                        else {
        //                            Global.Alert("Unable to Generate!", "There is no data to be reported.");
        //                        }
        //                    }
        //                    else if (result.isSuccess == false) {
        //                        Global.Alert("Error!", result.data);
        //                    }
        //                },
        //                complete: function () {
        //                    /*Global.EndLoader($("#btn-report-simplelistofpeople"));*/
        //                }
        //            });
        //        }


        //    } else {
        //        Global.Alert("Error", "You must select at least one member.");
        //        return false;
        //    }
        //}

        $this.init = function () {
            initializeWizard();
        };
    }

    $(function () {
        var self = new ParcelsReportIndex();
        self.init();
    });
}(jQuery));