
(function ($) {
    function VolunteerRegister() {
        var $this = this, volunteerDeliveryGrid, PackingGrid, formUpdateProfile, volunteerAvailabilityGrid, volunteerUnavailabilityGrid;

        /////////////// Avalibility Section //////////////////////////////////////////
        //$("#Availability_FrequencyType").change(function () {
        //    
        //    AvailabilityFrequencyEvent();
        //});
        //$("#Unavailability_FrequencyType").change(function () {
        //    
        //    unavailabilityFrequencyEvent();
        //});

        //$.get(Global.DomainName + "account/GetPartialView", { volunteerId: 75 }, function (result) {
        //    if (result != '') {

        //        $('.volunteer-first-page').hide();
        //        $('.AvailabilityAndUnavailability').show();
        //        $('.AvailabilityAndUnavailability').html(result);
        //        InitalizeAvailability();
        //        $('.first-footer').hide();
        //        $('.second-footer').show();
        //    }
        //    HideLoader();
        //});



        $("#btn-submit").click(function (e) {
            if (!$('form').valid()) {
                return false;
            }
            $.ajax({
                method: 'post',
                data: $('form').serialize(),
                success: function (data) {
                    if (data.isSuccess) {
                        $.get(Global.DomainName + "account/GetPartialView", { volunteerId: data.data }, function (result) {
                            if (result != '') {

                                $('.volunteer-first-page').hide();
                                $('.AvailabilityAndUnavailability').show();
                                $('.AvailabilityAndUnavailability').html(result);
                                InitalizeAvailability();
                            }
                            HideLoader();
                        });

                    } else {
                        alert("Error");
                    }
                }
            });
        })


        function InitalizeAvailability() {

            if ($.fn.DataTable.isDataTable($this.volunteerAvailabilityGrid)) {
                $($this.volunteerAvailabilityGrid).DataTable().destroy();
            }
            $this.volunteerAvailabilityGrid = new Global.GridAjaxHelper('#grid-volunteer-availability-list', {
                "aoColumns": [{ "sName": "AvailabilityId" },
                { "sName": "FormDate" },
                { "sName": "MapPattern" },
                { "sName": "ToDate" },
                { "sName": "AllDay" },
                { "sName": "" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0, 2, 4, 5] }, { 'visible': false, 'aTargets': [0] }],
            }, "account/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val(),
                Global.DeleteMasters);
            $this.volunteerAvailabilityGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "account/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.volunteerAvailabilityGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "account/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });


            //UnAvailibility Grid
            if ($.fn.DataTable.isDataTable($this.volunteerUnavailabilityGrid)) {
                $($this.volunteerUnavailabilityGrid).DataTable().destroy();
            }
            $this.volunteerUnavailabilityGrid = new Global.GridAjaxHelper('#grid-volunteer-unavailability-list', {
                "aoColumns": [{ "sName": "UnAvailabilityId" },
                { "sName": "FormDate" },
                { "sName": "MapPattern" },
                { "sName": "ToDate" },
                { "sName": "AllDay" },
                { "sName": "" },
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0, 2, 4, 5] }, { 'visible': false, 'aTargets': [0] }],
            }, "account/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val(),
                Global.DeleteMasters);
            $this.volunteerUnavailabilityGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "account/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.volunteerUnavailabilityGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "account/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
        }


        function AvailabilityFrequencyEvent() {


            AvailabilityfrequencyTypeChange();
            if ($('[id=Availability_FrequencyType]').val() == "1") {
                AvailabilityfrequencyTypeChange();
            }

            AvailabilityfrequencyweeklyTypeChange();

            if ($('[id=Availability_FrequencyType]').val() == "2") {
                AvailabilityfrequencyweeklyTypeChange();
            }
            AvailabilityfrequencyMonthlyTypeChange();
            if ($('[id=Availability_FrequencyType]').val() == "3") {
                AvailabilityfrequencyMonthlyTypeChange();
            }

            AvailabilityfrequencyAnnualTypeChange();
            if ($('[id=Availability_FrequencyType]').val() == "4") {
                AvailabilityfrequencyAnnualTypeChange();
            }
        }
        function AvailabilityfrequencyTypeChange() {

            if ($('[id=Availability_FrequencyType]').val() == "4") {
                $("#daily-basis-avalibility").addClass("hidden");
                $("#weekly-basis-avalibility").addClass("hidden");
                $("#monthly-basis-avalibility").addClass("hidden");
                $("#annual-basis-avalibility").removeClass("hidden");
            } else if ($('[id=Availability_FrequencyType]').val() == "3") {
                $("#daily-basis-avalibility").addClass("hidden");
                $("#weekly-basis-avalibility").addClass("hidden");
                $("#monthly-basis-avalibility").removeClass("hidden");
                $("#annual-basis-avalibility").addClass("hidden");
            } else if ($('[id=Availability_FrequencyType]').val() == "2") {
                $("#daily-basis-avalibility").addClass("hidden");
                $("#weekly-basis-avalibility").removeClass("hidden");
                $("#monthly-basis-avalibility").addClass("hidden");
                $("#annual-basis-avalibility").addClass("hidden");
            } else {
                $("#daily-basis-avalibility").removeClass("hidden");
                $("#weekly-basis-avalibility").addClass("hidden");
                $("#monthly-basis-avalibility").addClass("hidden");
                $("#annual-basis-avalibility").addClass("hidden");
            }
        }
        function AvailabilityfrequencyweeklyTypeChange() {

            if ($('[id=Availability_FrequencyType]').val() == "2") {
                $("#Availability_DailyDays").attr("readonly", true);
            } else {
                $("#Availability_DailyDays").removeAttr("readonly");
            }
        }
        function AvailabilityfrequencyMonthlyTypeChange() {

            if ($('[id=Availability_FrequencyType]').val() == "2") {
                $("#Availability_MonthlyDays").attr("readonly", true);
                $("#Availability_MonthlyMonths").attr("readonly", true);

                $("#Availability_MonthlyWeek").removeAttr("disabled");
                $("#Availability_MonthlyWeekDay").removeAttr("disabled");
                $("#Availability_MonthlyWeekMonth").removeAttr("readonly");
            } else {
                $("#Availability_MonthlyDays").removeAttr("readonly");
                $("#Availability_MonthlyMonths").removeAttr("readonly");

                $("#Availability_MonthlyWeekMonth").attr("readonly", true);
                $("#Availability_MonthlyWeek").attr("disabled", "disabled");
                $("#Availability_MonthlyWeekDay").attr("disabled", "disabled");
            }
        }
        function AvailabilityfrequencyAnnualTypeChange() {
            if ($('[id=Availability_FrequencyType]').val() == "2") {
                $("#Availability_AnnualMonthDay").attr("readonly", true);
                $("#Availability_AnnualMonth").attr("disabled", "disabled");

                $("#Availability_AnnualWeek").removeAttr("disabled");
                $("#Availability_AnnualWeekDay").removeAttr("disabled");
                $("#Availability_AnnualMonthWeek").removeAttr("disabled");
            } else {
                $("#Availability_AnnualMonthDay").removeAttr("readonly");
                $("#Availability_AnnualMonth").removeAttr("disabled");

                $("#Availability_AnnualWeek").attr("disabled", "disabled");
                $("#Availability_AnnualWeekDay").attr("disabled", "disabled");
                $("#Availability_AnnualMonthWeek").attr("disabled", "disabled");
            }
        }

        function AvailabilityTimeEvent() {
            AvailabilityTimeTypeChange();
            $('input[type=radio][id=Availability_UnavailabilityTimeType]').change(function () {

                AvailabilityTimeTypeChange();
            });
        }

        function AvailabilityTimeTypeChange() {

            if ($('input[type=radio][id=Availability_UnavailabilityTimeType]:checked').val() == "1") {

                $("#Availability_TimeForm").attr("disabled", "disabled");
                $("#Availability_TimeTo").attr("disabled", "disabled");
            } else {
                $("#Availability_TimeForm").removeAttr("disabled");
                $("#Availability_TimeTo").removeAttr("disabled");
            }
        }




        ///////////// Unavailbility Section ////////////////////////////////////////////
        function unavailabilityFrequencyEvent() {
            //unavailabilityfrequencyTypeChange();
            //$('input[type=radio][id=Unavailability_FrequencyType]').change(function () {
            //    unavailabilityfrequencyTypeChange();
            //});

            //unavailabilityfrequencyDailyTypeChange();
            //$('input[type=radio][id=Unavailability_DailyType]').change(function () {
            //    unavailabilityfrequencyDailyTypeChange();
            //});

            //unavailabilityfrequencyMonthlyTypeChange();
            //$('input[type=radio][id=Unavailability_MonthlyType]').change(function () {
            //    unavailabilityfrequencyMonthlyTypeChange();
            //});

            //unavailabilityfrequencyAnnualTypeChange();
            //$('input[type=radio][id=Unavailability_AnnualType]').change(function () {
            //    unavailabilityfrequencyAnnualTypeChange();
            //});

            unavailabilityfrequencyTypeChange();
            if ($('[id=Unavailability_FrequencyType]').val() == "1") {
                unavailabilityfrequencyTypeChange();
            }

            unavailabilityfrequencyweeklyTypeChange();
            if ($('[id=Unavailability_FrequencyType]').val() == "2") {
                unavailabilityfrequencyweeklyTypeChange();
            }
            unavailabilityfrequencyMonthlyTypeChange();
            if ($('[id=Unavailability_FrequencyType]').val() == "3") {
                unavailabilityfrequencyMonthlyTypeChange();
            }

            unavailabilityfrequencyAnnualTypeChange();
            if ($('[id=Unavailability_FrequencyType]').val() == "4") {
                unavailabilityfrequencyAnnualTypeChange();
            }
            //unavailabilityValidate();
        }

        function unavailabilityfrequencyTypeChange() {
            if ($('[id=Unavailability_FrequencyType]').val() == "4") {
                $("#daily-basis").addClass("hidden");
                $("#weekly-basis").addClass("hidden");
                $("#monthly-basis").addClass("hidden");
                $("#annual-basis").removeClass("hidden");
            } else if ($('[id=Unavailability_FrequencyType]').val() == "3") {
                $("#daily-basis").addClass("hidden");
                $("#weekly-basis").addClass("hidden");
                $("#monthly-basis").removeClass("hidden");
                $("#annual-basis").addClass("hidden");
            } else if ($('[id=Unavailability_FrequencyType]').val() == "2") {
                $("#daily-basis").addClass("hidden");
                $("#weekly-basis").removeClass("hidden");
                $("#monthly-basis").addClass("hidden");
                $("#annual-basis").addClass("hidden");
            } else {
                $("#daily-basis").removeClass("hidden");
                $("#weekly-basis").addClass("hidden");
                $("#monthly-basis").addClass("hidden");
                $("#annual-basis").addClass("hidden");
            }
        }

        function unavailabilityfrequencyweeklyTypeChange() {
            if ($('[id=Unavailability_FrequencyType]').val() == "2") {
                $("#Unavailability_DailyDays").attr("readonly", true);
            } else {
                $("#Unavailability_DailyDays").removeAttr("readonly");
            }
        }

        function unavailabilityfrequencyMonthlyTypeChange() {
            if ($('[id=Unavailability_FrequencyType]').val() == "2") {
                $("#Unavailability_MonthlyDays").attr("readonly", true);
                $("#Unavailability_MonthlyMonths").attr("readonly", true);

                $("#Unavailability_MonthlyWeek").removeAttr("disabled");
                $("#Unavailability_MonthlyWeekDay").removeAttr("disabled");
                $("#Unavailability_MonthlyWeekMonth").removeAttr("readonly");
            } else {
                $("#Unavailability_MonthlyDays").removeAttr("readonly");
                $("#Unavailability_MonthlyMonths").removeAttr("readonly");

                $("#Unavailability_MonthlyWeekMonth").attr("readonly", true);
                $("#Unavailability_MonthlyWeek").attr("disabled", "disabled");
                $("#Unavailability_MonthlyWeekDay").attr("disabled", "disabled");
            }
        }

        function unavailabilityfrequencyAnnualTypeChange() {
            if ($('[id=Unavailability_FrequencyType]').val() == "2") {
                $("#Unavailability_AnnualMonthDay").attr("readonly", true);
                $("#Unavailability_AnnualMonth").attr("disabled", "disabled");

                $("#Unavailability_AnnualWeek").removeAttr("disabled");
                $("#Unavailability_AnnualWeekDay").removeAttr("disabled");
                $("#Unavailability_AnnualMonthWeek").removeAttr("disabled");
            } else {
                $("#Unavailability_AnnualMonthDay").removeAttr("readonly");
                $("#Unavailability_AnnualMonth").removeAttr("disabled");

                $("#Unavailability_AnnualWeek").attr("disabled", "disabled");
                $("#Unavailability_AnnualWeekDay").attr("disabled", "disabled");
                $("#Unavailability_AnnualMonthWeek").attr("disabled", "disabled");
            }
        }

        function initializeDatePicker() {
            $(".datepicker").datepicker({
                format: "dd/mm/yyyy"
                , startDate: '-0m'
            }).on('hide', function (e) {
                // set focus outss
                $(".datepicker").blur();
            });

            $(".datepicker").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });

            $(".date-today-availibilty").on("click", function () {
                var date = new Date();
                var currentDate = new Date(date.getYear(), date.getMonth(), date.getDate());
                var todaydate = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
                $(".datepicker-availibilty").datepicker('setDate', todaydate);
                $(".datepicker-availibilty").datepicker('update');
            });

            $(".date-today-unavailibilty").on("click", function () {
                var date = new Date();
                var currentDate = new Date(date.getYear(), date.getMonth(), date.getDate());
                var todaydate = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
                $(".datepicker-unavailibilty").datepicker('setDate', todaydate);
                $(".datepicker-unavailibilty").datepicker('update');
            });
            initializeTimePicker();
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
        }

        function unavailabilityUntilEvent() {
            unavailabilityUntilTypeChange();
            $('input[type=radio][id=Unavailability_UnavailabilityUntilType]').change(function () {
                unavailabilityUntilTypeChange();
            });
        }

        function unavailabilityTimeEvent() {
            unavailabilityTimeTypeChange();
            $('input[type=radio][id=Unavailability_UnavailabilityTimeType]').change(function () {
                unavailabilityTimeTypeChange();
            });
        }

        function unavailabilityTimeTypeChange() {
            if ($('input[type=radio][id=Unavailability_UnavailabilityTimeType]:checked').val() == "1") {
                $("#Unavailability_TimeForm").attr("disabled", "disabled");
                $("#Unavailability_TimeTo").attr("disabled", "disabled");
            } else {
                $("#Unavailability_TimeForm").removeAttr("disabled");
                $("#Unavailability_TimeTo").removeAttr("disabled");
            }
        }

        function unavailabilityUntilTypeChange() {
            if ($('input[type=radio][id=Unavailability_UnavailabilityUntilType]:checked').val() == "1") {
                $("#FinishDate").attr("disabled", "disabled");
            } else {
                $("#FinishDate").removeAttr("disabled");
            }
        }

        function initializeTimePicker() {
            $('.timepicker').inputmask("hh:mm", {
                placeholder: "HH:MM",
                insertMode: false,
                showMaskOnHover: false,
                hourFormat: "24"
            });
        }

        ///// hide myprofile section on load
        function hideUpdateProfileSection() {
            $("#regularavailability-container").hide();
            $("#unavailability-container").hide();
        }

        ///// change password functionalty
        $("#IsChangePassword").val(true)
        //$(document).off('click', '#IsChangePassword').on('click', '#IsChangePassword', function () {

        //    if ($("#IsChangePassword").is(":checked")) {
        //        $("#changepassword-container").show();
        //        $("#IsChangePassword").val(true)
        //    }
        //    else {
        //        $("#changepassword-container").show();
        //        $("#IsChangePassword").val(true)
        //    }
        //});

        ///// regular availability functionalty
        $(document).off('click', '#IsRegularAvailability').on('click', '#IsRegularAvailability', function () {

            if ($("#IsRegularAvailability").is(":checked")) {
                $("#regularavailability-container").show();
                $("#IsRegularAvailability").val(true)
            }
            else {
                $("#regularavailability-container").hide();
                $("#IsRegularAvailability").val(false)
            }

        });

        ///// unavailability functionalty
        $(document).off('click', '#IsUnavailability').on('click', '#IsUnavailability', function () {

            if ($("#IsUnavailability").is(":checked")) {
                $("#unavailability-container").show();
                $("#IsUnavailability").val(true)
            }
            else {
                $("#unavailability-container").hide();
                $("#IsUnavailability").val(false)
            }

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
        /////



        $this.init = function () {
            initializeDatePicker();
            unavailabilityFrequencyEvent();
            unavailabilityUntilEvent();
            unavailabilityTimeEvent();
            AvailabilityFrequencyEvent();
            AvailabilityfrequencyTypeChange();
            AvailabilityfrequencyweeklyTypeChange();
            AvailabilityfrequencyMonthlyTypeChange();
            AvailabilityfrequencyAnnualTypeChange();
            AvailabilityTimeEvent();
            hideUpdateProfileSection();
        };

    }

    $(
        function () {
            var self = new VolunteerRegister();
            self.init();
        }
    )
})(jQuery)