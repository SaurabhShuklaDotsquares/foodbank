
(function ($) {
    'use strict';

    function FoodbankProfileIndex() {
        var $this = this;

        function InitializeOnLoadSection() {
            $("#drpPostcodeAddr").off("change").on("change", function () {
                SearchIdBegin(this);
            });

            $("#CentralOfficeID").off("change").on("change", function () {
                $("#CharityID").html("<option value=''>Select Charity...</option>");
                $("#CharityGroup").html("");
                if ($(this).val() != "") {
                   
                    var html = "<option value=''>Select Charity...</option>";
                    $.get(Global.DomainName + "foodbank/dashboard/bindcharities", { organisationID: $(this).val() }, function (data) {
                        if (data.data.length > 1) {
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $(".charity-drp").html(html);
                            if ($('.charity-drp option').length > 0) {
                                $('.charity-drp').val($('#CharityID').val());
                                $(".charity-drp").trigger('change');
                            }
                        }
                        else {
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                $.get(Global.DomainName + "foodbank/dashboard/bindbranches", { charityID: item.value }, function (data) {
                                    var branchhtml;
                                    $.each(data.data, function (index, item) {
                                        branchhtml = branchhtml + "<option value='" + item.value + "'>" + item.text + "</option>";
                                    });
                                    $(".branch-drp").html(branchhtml);
                                    $(".branch-drp").val($("#BranchID").val());
                                    $(".branch-drp").trigger('change');
                                    $("#BranchIDs").val($("#BranchID").val());
                                });
                            });
                            $(".charity-drp").html(html);
                        }
                    });
                }
            });

            $(".charity-drp").off("change").on("change", function () {
                
                if ($(this).val() != "") {
                    $.get(Global.DomainName + "foodbank/dashboard/bindbranches", { charityID: $(this).val() }, function (data) {
                        var html;
                        $.each(data.data, function (index, item) {
                            html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $(".branch-drp").html(html);
                        $("#BranchIDs").val($("#BranchID").val());

                        if ($('.branch-drp option').length == 1) {
                            $('.branch-drp').val($('#BranchID').val());
                            $(".branch-drp").trigger('change');
                        }

                    });
                }

            });

            $('#IsChangePassword').off("click").on("click", function () {
                ControlVisibilityChangePassword();
            });

            $("#Overseas").off("change").on("change", function () {
                if ($(this).is(":checked")) {
                    $('#OldPostCode').val($("#PostCode").val());
                    $("#PostCode").attr("disabled", "disabled");
                }
                else {
                    $('#OldPostCode').val("");
                    $("#PostCode").removeAttr("disabled");
                }
            });

            $("#CountryID").off("change").on("change", function () {
              //  alert($(this).text());
                $('#CountryName').val($("#CountryID option:selected").text().toLowerCase().trim());
                //if ($("#CountryID option:selected").text().toLowerCase().trim() == "united kingdom") {
                //    $("#Overseas").prop("checked", false);
                //} else {
                //    $("#Overseas").prop("checked", true);
                //}

                //$("#Overseas").trigger("change");
            });

            $(document).on('click', '.toggle-password', function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var input = $(".random-password");
                if (input.attr("type") === "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }

            });

            $("#btn-submit").click(function (e) {
                $('form').valid();
                if ($('.postcode').val() !== "") {
                    if ($('.postcode').val().trim().indexOf(' ') < 0) {
                        Global.Alert("Invalid Postcode", "The postocde you have entered is invalid. Please ensure that you have entered space between the postcode.");
                        return false;
                    }
                    else {
                        $('form').submit();
                    }
                }
            })

            $("#CentralOfficeID").trigger('change');

        }

        function ControlVisibilityChangePassword() {
            if ($("#IsChangePassword").is(":checked")) {
                $('#ChangePassword').val(true);
                $(".change-password").show();
            }
            else {
                $('#ChangePassword').val(false);
                $(".change-password").hide();
            }
        }

        $this.init = function () {
            InitializeOnLoadSection();
            ControlVisibilityChangePassword();
        };
    }

    $(function () {
        var self = new FoodbankProfileIndex();
        self.init();
    });
}(jQuery));