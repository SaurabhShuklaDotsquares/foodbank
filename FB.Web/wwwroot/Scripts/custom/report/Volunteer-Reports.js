(function ($) {
    'use strict';
    function ParcelsReportIndex() {
        var $this = this;
        var exportReport = new ExportReport();

        function initializeWizard() {
            $('#rootwizard').bootstrapWizard({ 'tabClass': 'bwizard-steps' });
            VolunteerChangeEvents();
            WizardValidate();
        }


        function WizardValidate() {

            $(".dateSelect").on("change", function () {
                $("#volunteer").html("");
                $("#volunteer").multipleSelect("refresh");
                var fromDate = $("#DateFrom").val();
                var toDate = $("#DateTo").val();
                if (fromDate || toDate) {
                    $.get(Global.DomainName + "VolunteerReports/GetVoluneteers", { fromDate: fromDate, toDate: toDate }, function (data) {
                        if (data != null) {
                            $.each(data.data, function (data, value) {
                                $("<option />").val(value.value).text(value.text).appendTo("#volunteer");
                            });
                            $("#volunteer").multipleSelect("refresh");
                            if ($('#volunteer option').length == 0) {
                                $("#volunteer").multipleselect("checkall");
                                $("#volunterrsids").val($("#volunteer").val());
                            }
                        }
                    });
                }
            });
            //$("#DateTo").on("change", function () {
            //    var fromDate = $(this).val();
            //    if (fromDate && !($("#DateFrom").val())) {
            //        document.getElementById("DateFrom").max = fromDate;
            //    }
            //});
            //var fromDate = $("#DateFrom").val();
            //var toDate = $("#DateFrom").val();

            $(".bwizard-steps li a").on("click", function () {
                return false;
            });


            $(".next a").on("click", function () {
                var IsStepOneValid = true;
                $('span[data-valmsg-for="VolunterrsIds"]').text('');
                $('span[data-valmsg-for="ShiftTypeIds"]').text('');
                //$('span[data-valmsg-for="DateTo"]').text('');


                if ($("#VolunterrsIds").val() == "" || $("#VolunterrsIds").val() == null ) {
                    IsStepOneValid = false
                    
                    $('.page-header').trigger('click');
                    $('span[data-valmsg-for="VolunterrsIds"]').text('*required');
                }
                if ($("#ShiftTypeIds").val() == "" || $("#ShiftTypeIds").val() == null) {
                    IsStepOneValid = false
                    $("#shiftType").dropdown("toggle");
                    $('span[data-valmsg-for="ShiftTypeIds"]').text('*required');
                }
                ScrollOrganisationDiv();

                if ($("#rootwizard li.active").find('a').attr('href') == "#tab1") {
                    if (IsStepOneValid) {
                        var frmRoleListOfPeople = new Global.FormHelper($("#frm-Role-list-of-people form"));
                        $(".img-loading-div").show();
                        $.post(Global.DomainName + "VolunteerReports/GetMembersByStepOneWizardValues", frmRoleListOfPeople.serialize(), function (result) {
                            if (result.isSuccess) {
                                if (result.data != "") {
                                    exportReport.SetRolePage(result.data);
                                    $(".rendertab2Data").show();
                                    $(".img-loading-div").hide();
                                }
                                else {
                                    $(".img-loading-div").hide();
                                    $(".rendertab2Data").hide();
                                    Global.Alert("Unable to Generate!", "Error Occurd");
                                }
                            }
                            else {
                                $(".img-loading-div").hide();
                                $(".rendertab2Data").hide();
                                Global.Alert("Error!", result.data);
                            }
                        });
                    }
                    else {

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

        function VolunteerChangeEvents() {

            $("#volunteer").multipleSelect({
                width: '100%',
                placeholder: "Select volunteer",
                onClose: function () {
                    $("#VolunterrsIds").val($("#volunteer").val());
                },
                onOpen: function () {
                    if ($('#volunteer option').length == 0) {
                        $("#volunteer").multipleSelect("checkAll");
                        $("#VolunterrsIds").val($("#volunteer").val());
                    }
                },
                //onCheckAll: function () {
                //    $("#Branches").multipleSelect("checkAll");
                //}
            });

            if ($('#volunteer option').length == 0) {
                $("#volunteer").multipleSelect("checkAll");
                $("#VolunterrsIds").val($("#volunteer").val());
            }

            $("#shiftType").multipleSelect({
                width: '100%',
                placeholder: "Select Shift Type",
                onClose: function () {
                    
                    $("#ShiftTypeIds").val($("#shiftType").val());
                },
                onOpen: function () {
                    if ($('#shiftType option').length == 1) {
                        $("#shiftType").multipleSelect("checkAll");
                        $("#ShiftTypeIds").val($("#shiftType").val());
                    }
                },
                //onCheckAll: function () {
                //    $("#Branches").multipleSelect("checkAll");
                //}
            });

            if ($('#shiftType option').length == 1) {
                $("#shiftType").multipleSelect("checkAll");
                $("#ShiftTypeIds").val($("#shiftType").val());
            }
        }
        $this.init = function () {
            initializeWizard();
        };
    }

    $(function () {
        var self = new ParcelsReportIndex();
        self.init();
    });
}(jQuery));