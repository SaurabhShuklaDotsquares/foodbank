(function ($) {
    'use strict';
    function FamilyReportIndex() {
        var $this = this;
        var exportReport = new ExportReport();

        function initializeWizard() {
            $('#rootwizard').bootstrapWizard({ 'tabClass': 'bwizard-steps' });
            WizardValidate();
        }

        function WizardValidate() {

            $(".bwizard-steps li a").on("click", function () {
                return false;
            });


            $(".next a").on("click", function () {
                var IsStepOneValid = true;
                ScrollOrganisationDiv();

                if ($("#rootwizard li.active").find('a').attr('href') == "#tab1") {
                    $("#grid-family-list-step-two tbody").html('');
                    if (IsStepOneValid) {
                        var option = {};
                        option.StatusId = $('#StatusId').val();
                        option.DateAdded = $('#DateAdded').val();
                        option.IncludeFamailyMemberDetails = $('#IncludeFamailyMemberDetails').is(":checked");
                        option.IncludeParcelDetails = $('#IncludeParcelDetails').is(":checked");
                        $(".img-loading-div").show();
                        $.post(Global.DomainName + "FamilyReport/GetMembersByStepOneWizardValues", option, function (data) {
                            var html = "";
                            $("#grid-family-list-step-two tbody").html('');
                            if (!data.isSuccess) {
                                html = "<tr><td colspan='3'>No Records!!</td></tr>";
                                $("#grid-family-list-step-two tbody").append(html);
                                $(".img-loading-div").hide();
                             
                                Global.ShowMessage(data.data, Global.MessageType.Error);
                            } else {
                                $.each(data.data, function (index, item) {
                                    html = html + "<tr>";
                                    html = html + "<td class='hidden'></td>";
                                    html = html + "<td class='hidden'>" + item.value + "</td>";
                                    html = html + "<td class='align-center'><input type='checkbox' class='childChk' checked='checked' data-id='" + item.value + "' /></td>";
                                    html = html + "<td>" + item.text + "</td>";
                                    html = html + "</tr>";
                                });
                                if ($.fn.DataTable.isDataTable('#grid-family-list-step-two')) {
                                    $('#grid-family-list-step-two').DataTable().destroy();
                                    $("#grid-family-list-step-two").parent("div").parent("div").addClass("table-responsive");
                                }
                                $("#grid-family-list-step-two").parent("div").parent("div").addClass("table-responsive");
                                $('#grid-family-list-step-two tbody').empty();
                                $("#grid-family-list-step-two tbody").append(html);
                                $("#grid-family-list-step-two").DataTable({
                                    "aoColumnDefs": [
                                        {
                                            bSortable: false,
                                            aTargets: [0]
                                        },
                                        {
                                            bSortable: false,
                                            aTargets: [1]
                                        },
                                        {
                                            bSortable: false,
                                            aTargets: [2]
                                        },
                                        {
                                            bSortable: false,
                                            aTargets: [3]
                                        }

                                    ],

                                    "aLengthMenu": [
                                        [25, 50, 100, 200, -1],
                                        [25, 50, 100, 200, "All"]
                                    ],
                                    iDisplayLength: -1
                                });
                                $("#grid-family-list-step-two").parent("div").parent("div").addClass("table-responsive");
                                $(".img-loading-div").hide();
                                GridCheckboxAllEvent();
                            }
                        });
                    } else {
                        return false;
                    }
                }

                if ($("#rootwizard li.active").find('a').attr('href') == "#tab2") {
                    $(".img-loading-div").show();
                    var familyIds = [];
                    $("#grid-family-list-step-two tbody tr").each(function () {
                        var row = $(this);
                        if (row.find("td").eq(2).find("input[type=checkbox]").is(":checked")) {
                            familyIds.push(row.find("td").eq(1).html());
                        }
                    });
                    if (!familyIds.length > 0) {
                        Global.ShowMessage("Please select at least one family.", Global.MessageType.Error);
                        $(".img-loading-div").hide();
                        return false;
                    }
                    else {
                        $(".img-loading-div").show();
                        GenerateReport();
                    }
                    $(".img-loading-div").hide();
                }

            });

        }


        function ScrollOrganisationDiv() {
            $('html, body').animate({
                scrollTop: $(".organisationDiv").offset().top
            });
        }



        function GridCheckboxAllEvent() {

            $('#grid-family-list-step-two > tbody').on('click', 'td:nth-child(4)', function () {
                var trchk = $(this).parent('tr').find('input[type="checkbox"]');

                if (trchk.is(":checked")) {
                    trchk.prop('checked', false);
                } else {
                    trchk.prop('checked', true);
                }
            });

            $('#chkAll').click(function (e) {
                var table = $(e.target).closest('table');
                $('td input:checkbox', table).prop('checked', this.checked);
            });
        }


        function GenerateReport() {
            var familyIds = [];
            $('#grid-family-list-step-two > tbody').find('input[type="checkbox"]:checked').each(function () {
                familyIds.push($(this).attr('data-id'));
            });
            if (familyIds.length > 0) {
                $("#FamailyIds").val(familyIds);
                var frmRoleListOfPeople = new Global.FormHelper($("#frm-Role-list-of-people form"));
                if (frmRoleListOfPeople.valid()) {
                    $.ajax({
                        url: Global.DomainName + 'FamilyReport/Index',
                        data: frmRoleListOfPeople.serialize(),
                        type: 'POST',
                        success: function (result) {
                            if (result.isSuccess == true) {
                                if (result.data != "") {
                                    exportReport.SetRolePage(result.data);
                                    $(".rendertab3Data").show();
                                }
                                else {
                                    Global.Alert("Unable to Generate!", "There is no data to be reported.");
                                }
                            }
                            else if (result.isSuccess == false) {
                                Global.Alert("Error!", result.data);
                            }
                        },
                        complete: function () {
                            /*Global.EndLoader($("#btn-report-simplelistofpeople"));*/
                        }
                    });
                }


            } else {
                Global.Alert("Error", "You must select at least one member.");
                return false;
            }
        }

        $this.init = function () {
            initializeWizard();
        };
    }

    $(function () {
        var self = new FamilyReportIndex();
        self.init();
    });
}(jQuery));