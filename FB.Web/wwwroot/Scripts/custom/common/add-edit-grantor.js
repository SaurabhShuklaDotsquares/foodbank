(function ($) {
    function GrantorIndex() {

        var $this = this;

        function initializeFamilyParcelGridgrantor() {
            if ($.fn.DataTable.isDataTable($this.familyparcelGrid)) {
                $($this.familyparcelGrid).DataTable().destroy();
            }
            $this.familyparcelGrid = new Global.GridAjaxHelper('#grid-family-parcel-grantor', {
                "aoColumns": [
                    { "sName": "ParcelTypeId" },
                    { "sName": "S.NO" },
                    { "sName": "ParcelType.Name" },
                    { "sName": "Family.FamilyName" },
                    { "sName": "DeliveredDate" },
                    { "sName": "DeliveryDate" },
                    { "sName": "Status" },
                    {
                        "sName": ""
                    },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [1, 7] }, { 'visible': false, 'aTargets': [0] }],
            }, "Common/BindGrantorTableView?GrantorId=" + $("#GrantorId").val(),
            );
            $("#grid-family-parcel-grantor").parent("div").parent("div").addClass("table-responsive");
            $this.familyparcelGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "Common/BindGrantorTableView?GrantorId=" + $("#GrantorId").val();
            });
            $this.familyparcelGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "Common/BindGrantorTableView?GrantorId=" + $("#GrantorId").val();
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
                $('#CountryName').val($("#CountryID option:selected").text().toLowerCase().trim() );
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
            initializeFamilyParcelGridgrantor();
        }
    }

    $(
        function () {
            var self = new GrantorIndex();
            self.init();
        }
    )
})(jQuery)