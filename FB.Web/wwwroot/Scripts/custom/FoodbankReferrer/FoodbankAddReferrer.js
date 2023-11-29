
(function ($) {

    function FoodbankReferrerIndex() {
        var $this = this, myReferrerGrid;

        function initializeModalWithForm() {
            $(".datepicker").datepicker({ format: "dd/mm/yyyy", endDate: '-0m' }).inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $('.datepicker').datepicker("setDate", new Date());
        }

        $("#drpPostcodeAddr").off("change").on("change", function () {
            SearchIdBegin(this);
            //setTimeout(function () { BindHMRCAddress(); }, 1000);
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
        $('#IsChangePassword').off("click").on("click", function () {
            ControlVisibilityChangePassword();
        });
        $("#btn-submit").click(function (e) {
            $('form').valid();
            $('#Profession').val($("#ProfessionId option:selected").text().trim());
            if ($('.postcode').val() == "") {
              
                    Global.Alert("Invalid Postcode", "Please enter postcode.");
              
            }
            if ($('.postcode').val() !== "") {
                if ($('.postcode').val().trim().indexOf(' ') < 0) {
                    Global.Alert("Invalid Postcode", "The postocde you have entered is invalid. Please ensure that you have entered space between the postcode.");
                    return false;
                }
                else {
                    $('form').submit();
                }
            }
            $('form').submit();
        });

       

        $this.init = function () {
            initializeModalWithForm(); ControlVisibilityChangePassword();
        };
    }

    $(function () {
        var self = new FoodbankReferrerIndex();
        self.init();
    });
}(jQuery));
