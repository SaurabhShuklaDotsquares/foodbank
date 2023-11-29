(function ($) {
    function AgencyIndex() {
        var $this = this;

        function initializeGridMyGrantor() {
            myReferralGrid = new Global.GridAjaxHelper('#grid-my-referral', {
                "aoColumns": [
                    { "sName": "FamilyId" },//0
                    { "sName": "SNo" },//1
                    { "sName": "Family.FamilyName" },//2
                    { "sName": "Family.Contactno" },//3
                    { "sName": "Family.Email" },//4
                    { "sName": "Family.TotalFamily" },//5
                    {
                        render: function (data, item, row, meta) {
                            return "<a href='/Foodbank/FoodbankReferrer/ViewMyReferrals/" + row[0] + "' class='view_btn'><img src='/Content/images/eye-icon.png' /></a>";
                        }
                    },
                ],
                "bStateSave": true,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 6] }, { 'visible': false, 'aTargets': [0] }],
            }, "Foodbank/Agencies/Createfamily?AgencyId=" + $('#AgencyId').val(),
            );
            $("#grid-my-referral").parent("div").parent("div").addClass("table-responsive");
            myReferralGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Foodbank/Agencies/Createfamily?AgencyId=" + $('#AgencyId').val();
            });
            myReferralGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "FoodBank/Agencies/Createfamily?AgencyId=" + $('#AgencyId').val();
            });
        }

        function InitializeOnLoadSection() {
            $("#drpPostcodeAddr").off("change").on("change", function () {
                SearchIdBegin(this);
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
                $('#CountryName').val($(this).text());
                if ($("#CountryID option:selected").text().toLowerCase().trim() == "united kingdom") {
                    $("#Overseas").prop("checked", false);
                } else {
                    $("#Overseas").prop("checked", true);
                }

                $("#Overseas").trigger("change");
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

        }

        $this.init = function () {
            InitializeOnLoadSection();
            initializeGridMyGrantor();
        }
    }

    $(
        function () {
            var self = new AgencyIndex();
            self.init();
        }
    )
})(jQuery)