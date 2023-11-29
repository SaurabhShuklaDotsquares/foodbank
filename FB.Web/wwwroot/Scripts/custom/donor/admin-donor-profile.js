
(function ($) {
    'use strict';

    function PersonIndex() {
        var $this = this;

        function InitializeOnLoadSection() {
            $("#drpPostcodeAddr").off("change").on("change", function () {
                SearchIdBegin(this);
                setTimeout(function () { BindHMRCAddress(); }, 1000);
            });

            $("#LastReference").off("click").on("click", function () {
                var branchvalue = $("#BranchIDs").val();

                if (branchvalue == "") {
                    Global.Alert("Warning !", "Please select a branch.");
                }
                else {
                    var $thisBtn = $(this);
                    $thisBtn.find('i').removeClass("fa fa-arrow-circle-right");
                    $thisBtn.find('i').addClass("fa fa-spinner fa-spin");
                    $thisBtn.find('i').prop('disabled', true);

                    $.ajax({
                        url: Global.DomainName + "Foodbank/Donor/GetLastReference?BranchIds=" + branchvalue,
                        type: "Get",
                        success: function (result) {
                            if (result.isSuccess) {
                                if (!result.data.isNextReference) {
                                    var html = '';
                                    $(".textReference").removeAttr("readonly");
                                    $(".textReference").val('');
                                    $.each(result.data.references, function (key, item) {
                                        html += "Donor Last Reference : " + item.donorReference + "  (Branch : " + item.branchName + ")"
                                            + "<a id='donarLastReference' data-ref='" + item.donorReference + "' class='btn-link' style='margin-left:10px; color:#3593D2 !important;' href='javascript:void(0);'>Copy</a></br>";
                                    });

                                    Global.Alert("Last References", html, Global.MessageType.Success);
                                }
                                else {
                                    if (isNaN(result.data.donorReference)) {
                                        Global.Alert("Last Reference", "Last Donor Reference : " + result.data.donorReference, Global.MessageType.Success);
                                        $(".textReference").removeAttr("readonly");
                                    }
                                    else
                                        $(".textReference").val(result.data.donorReference);
                                }
                            }
                            else {
                                Global.Alert("Error !", result.data);
                            }
                        },
                        complete: function () {
                            $thisBtn.find('i').removeClass("fa fa-spinner fa-spin");
                            $thisBtn.find('i').addClass("fa fa-arrow-circle-right");
                            $thisBtn.find('i').prop('disabled', false);
                        }
                    });
                }
            });

            $(document).off('click', '#donarLastReference').on('click', '#donarLastReference', function () {
                var referenceNumber = $(this).data("ref");
                $('#copyURL').val(referenceNumber);
                CopyToClipboard(referenceNumber);
            });

            function CopyToClipboard(data) {
                
                var copyText = document.getElementById("copyURL");
                copyText.select();
                copyText.setSelectionRange(0, 99999);
                navigator.clipboard.writeText(copyText.value);
                $('.ajs-close').trigger('click');
            }

            $("#donor-list-anchor").off("click").on("click", function () {
                $("#donor-list-anchor1").prop("href", $("#DonorUrl").val() + "?text=" + encodeURI($("#Surname").val()) + "&orgID=" + $("#frm-create-donor #CentralOfficeID").val() + "&charityID=" + $("#frm-create-donor #CharityID").val() + "&branchesIDs=" + $("#frm-create-donor #BranchIDs").val());
                $("#donor-list-anchor1").click();
            });

            $("#CentralOfficeID").off("change").on("change", function () {
                $("#CharityID").html("<option value=''>Select Charity...</option>");
                //$("#CharityID").select2("destroy");
                //$("#CharityID").select2();
                $("#CharityGroup").html("");
                if ($(this).val() != "") {
                    var html = "<option value=''>Select Charity...</option>";
                    $.get(Global.DomainName + "Foodbank/Donor/bindcharities", { organisationID: $(this).val() }, function (data) {
                        if (data.data.length > 1) {
                            $.each(data.data, function (index, item) {
                                html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                            });
                            $("#CharityID").html(html);
                        }
                        else {
                            $.each(data.data, function (index, item) {
                                html = "<option value='" + item.value + "'>" + item.text + "</option>";
                                $.get(Global.DomainName + "Foodbank/Donor/bindbranches", { charityID: item.value }, function (data) {
                                    var branchhtml;
                                    $.each(data.data, function (index, item) {
                                        branchhtml = branchhtml + "<option value='" + item.value + "'>" + item.text + "</option>";
                                    });
                                    
                                    $("#BranchID").html(branchhtml);
                                    $("#BranchIDs").val($("#BranchID").val());
                                });
                            });
                            $("#CharityID").html(html);
                            //$("#CharityID").select2();
                        }
                    });
                }
            });

            $("#CharityID").off("change").on("change", function () {
                $("#BranchID").html("<option value=''>Select Branch...</option>");
                if ($(this).val() != "") {
                    $.get(Global.DomainName + "Foodbank/Donor/bindbranches", { charityID: $(this).val() }, function (data) {
                        var html;
                        $.each(data.data, function (index, item) {
                            html = html + "<option value='" + item.value + "'>" + item.text + "</option>";
                        });
                        $("#BranchID").html(html);
                        $("#BranchIDs").val($("#BranchID").val());

                        if ($('#BranchID option').length == 1) {
                            $('#BranchID').val($('#BranchID option:last').val());
                            $("#BranchID").trigger('change');
                        }

                    });
                }

            });

            $('#BranchID').off("change").on("change", function () {
                $("#BranchIDs").val($('#BranchID').val());
                var branchvalue = $("#BranchIDs").val();
                $('.textReference').val('');
                if (branchvalue != "") {
                    $('.textReference').removeAttr("readonly");
                    if (branchvalue.indexOf(",") < 0) {
                        ReferenceType = GetReferenceType(branchvalue);
                    }
                    else {
                        $('#lastrefmsg').text("Last Reference");
                    }
                }
                else {
                    $('#lastrefmsg').text("Last Reference");
                    $('.textReference').attr("readonly", "readonly");
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
                if ($("#CountryID option:selected").text().toLowerCase().trim() == "united kingdom") {
                    $("#Overseas").prop("checked", false);
                } else {
                    $("#Overseas").prop("checked", true);
                }

                $("#Overseas").trigger("change");
            });

            $('#btnGeneratePassword').off("click").on("click", function () {
                GeneratePassword();
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
            $("#UserName").blur(function () {
                
                var usertext = $(this).val();
                if (usertext != "") {
                    $.get(Global.DomainName + 'Foodbank/donor/UserAvailability', { userName: usertext }, function (data) {
                        if (data.length > 0) {
                            $("#divError").html(data);
                            $("#divError").parent("div").css({ "display": "block", "color": "red" })
                        }
                        else
                            $("#divError").parent("div").css("display", "none")
                    });
                }
                else
                {
                    $("#divError").parent("div").css("display", "none")
                }
            });
        }

        function GeneratePassword() {
            $.get(Global.DomainName + "base/GetRandomPassword", function (result) {
                if (result.isSuccess == true) {
                    var bodyText = "Password : <b>" + result.data + "</b>";
                    bodyText += "<br/><br/>Please make a note of this password as you will not be able to view it later.";
                    Global.Confirm("Random Password", bodyText, function () {
                        $('.random-password').val(result.data);
                    }, function () { return false; }, "Continue", "Cancel");
                }
                else {
                    Global.ShowMessage(result.data, Global.MessageType.Error);
                }
            });
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

        function GetReferenceType(branchvalue) {
            if (branchvalue.indexOf(",") < 0) {
                $.get(Global.DomainName + "Foodbank/Donor/GetReferenceType", { BranchId: branchvalue }, function (result) {
                    if (result.isSuccess) {
                        if (result.data == 1) {
                            $('#lastrefmsg').text('Next Reference');
                            ReferenceType = "Numeric";
                        }
                        else {
                            $('#lastrefmsg').text('Last Reference');
                            ReferenceType = "Character";
                        }
                        return ReferenceType;
                    }
                    else {
                        Global.Alert("Error !", result.data);
                        $('#lastrefmsg').text('Last Reference');
                        ReferenceType = "Character";
                        return ReferenceType;
                    }
                });
            }
            else {
                $('#lastrefmsg').text('Last Reference');
                ReferenceType = "Character";
                return ReferenceType;
            }
        }

        function BindHMRCAddress() {
            if (!$("#HMRCAddressOverride").is(":checked")) {

                var housename = $(".house-name").val();
                var housenumber = $(".house-number").val();

                var streetname = $(".street-name").val();
                var postcode = $(".postcode").val();
                var fulladdr = "";

                if (housename.trim() != "") {
                    fulladdr = housename;
                }

                if (housenumber.trim() != "" || streetname.trim() != "") {
                    fulladdr = fulladdr != "" ? fulladdr + ", " : "";
                    fulladdr = (fulladdr + housenumber + " " + streetname).trim();
                }

                if (postcode.trim() != "") {
                    fulladdr = fulladdr != "" ? fulladdr + ", " : "";
                    fulladdr = fulladdr + postcode;
                }

                $("#HMRCAddress").val(fulladdr.trim());
            }
        }

        $this.init = function () {
            InitializeOnLoadSection();
            ControlVisibilityChangePassword();
        };
    }

    $(function () {
        var self = new PersonIndex();
        self.init();
    });
}(jQuery));