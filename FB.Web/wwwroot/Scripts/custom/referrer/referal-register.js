

(function ($) {

    function RegisterReferral() {
        var $this = this, declarationGrid;

        function InitlizePage() {
            $(".datepicker").datepicker({ format: "dd/mm/yyyy" }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $('.datepicker').datepicker("setDate", new Date());

            $("#drpPostcodeAddr").off("change").on("change", function () {
                SearchIdBegin(this);
                //setTimeout(function () { BindHMRCAddress(); }, 1000);
            });

            $('#IsChangePassword').off("click").on("click", function () {
                ControlVisibilityChangePassword();
            });

            $(document).off('click', '.toggle-password').on('click', '.toggle-password', function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var input = $(".random-password");
                if (input.attr("type") === "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }

            });

            $('select option:contains("United Kingdom")').prop('selected', true);
            $("#UserName").blur(function () {
                
                var usertext = $(this).val();
                if (usertext != "") {
                    $.get(Global.DomainName + 'Account/UserAvailability', { userName: usertext }, function (data) {
                        if (data.length > 0) {
                            $("#divError").html(data);
                            $("#divError").parent("div").css({ "display": "block", "color": "red" })
                        }
                        else
                            $("#divError").parent("div").css("display", "none")
                    });
                }
                else {
                    $("#divError").parent("div").css("display", "none")
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

            $("#btn-submit").click(function (e) {
                $('form').valid();
                $('#Profession').val($("#ProfessionId option:selected").text().trim());
                
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
        }

        $this.init = function () {
            
            InitlizePage();
        };
    }

    $(function () {
        var self = new RegisterReferral();
        self.init();
    });
}(jQuery));