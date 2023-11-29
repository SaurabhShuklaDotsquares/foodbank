

(function ($) {

    function UpdateReferral() {
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

        function BindHMRCAddress() {
            
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

        $this.init = function () {
            
            InitlizePage();
            ControlVisibilityChangePassword();
        };
    }

    $(function () {
        var self = new UpdateReferral();
        self.init();
    });
}(jQuery));