﻿

(function ($) {
    'use strict';
    function VolunteerProfile() {
        var $this = this, volunteerDeliveryGrid, PackingGrid, formUpdateProfile, volunteerAvailabilityGrid, volunteerUnavailabilityGrid;

        /////////////// Avalibility Section //////////////////////////////////////////
        function AvailabilityFrequencyEvent() {
            //AvailabilityfrequencyTypeChange();
            //$('input[type=radio][id=Availability_FrequencyType]').change(function () {
            //    AvailabilityfrequencyTypeChange();
            //});

            //AvailabilityfrequencyDailyTypeChange();
            //$('input[type=radio][id=Availability_DailyType]').change(function () {
            //    AvailabilityfrequencyDailyTypeChange();
            //});

            //AvailabilityfrequencyMonthlyTypeChange();
            //$('input[type=radio][id=Availability_MonthlyType]').change(function () {
            //    AvailabilityfrequencyMonthlyTypeChange();
            //});

            //AvailabilityfrequencyAnnualTypeChange();
            //$('input[type=radio][id=Availability_AnnualType]').change(function () {
            //    AvailabilityfrequencyAnnualTypeChange();
            //});

            //unavailabilityValidate();
        }
        function AvailabilityfrequencyTypeChange() {
            //if ($('input[type=radio][id=Availability_FrequencyType]:checked').val() == "4") {
            //    $("#daily-basis-avalibility").addClass("hidden");
            //    $("#weekly-basis-avalibility").addClass("hidden");
            //    $("#monthly-basis-avalibility").addClass("hidden");
            //    $("#annual-basis-avalibility").removeClass("hidden");
            //} else if ($('input[type=radio][id=Availability_FrequencyType]:checked').val() == "3") {
            //    $("#daily-basis-avalibility").addClass("hidden");
            //    $("#weekly-basis-avalibility").addClass("hidden");
            //    $("#monthly-basis-avalibility").removeClass("hidden");
            //    $("#annual-basis-avalibility").addClass("hidden");
            //} else if ($('input[type=radio][id=Availability_FrequencyType]:checked').val() == "2") {
            //    $("#daily-basis-avalibility").addClass("hidden");
            //    $("#weekly-basis-avalibility").removeClass("hidden");
            //    $("#monthly-basis-avalibility").addClass("hidden");
            //    $("#annual-basis-avalibility").addClass("hidden");
            //} else {
            //    $("#daily-basis-avalibility").removeClass("hidden");
            //    $("#weekly-basis-avalibility").addClass("hidden");
            //    $("#monthly-basis-avalibility").addClass("hidden");
            //    $("#annual-basis-avalibility").addClass("hidden");
            //}
        }
        function AvailabilityfrequencyDailyTypeChange() {
            //if ($('input[type=radio][id=Availability_DailyType]:checked').val() == "2") {
            //    $("#Availability_DailyDays").attr("readonly", true);
            //} else {
            //    $("#Availability_DailyDays").removeAttr("readonly");
            //}
        }
        function AvailabilityfrequencyMonthlyTypeChange() {
            //if ($('input[type=radio][id=Availability_MonthlyType]:checked').val() == "2") {
            //    $("#Availability_MonthlyDays").attr("readonly", true);
            //    $("#Availability_MonthlyMonths").attr("readonly", true);

            //    $("#Availability_MonthlyWeek").removeAttr("disabled");
            //    $("#Availability_MonthlyWeekDay").removeAttr("disabled");
            //    $("#Availability_MonthlyWeekMonth").removeAttr("readonly");
            //} else {
            //    $("#Availability_MonthlyDays").removeAttr("readonly");
            //    $("#Availability_MonthlyMonths").removeAttr("readonly");

            //    $("#Availability_MonthlyWeekMonth").attr("readonly", true);
            //    $("#Availability_MonthlyWeek").attr("disabled", "disabled");
            //    $("#Availability_MonthlyWeekDay").attr("disabled", "disabled");
            //}
        }
        function AvailabilityfrequencyAnnualTypeChange() {
            //if ($('input[type=radio][id=Availability_AnnualType]:checked').val() == "2") {
            //    $("#Availability_AnnualMonthDay").attr("readonly", true);
            //    $("#Availability_AnnualMonth").attr("disabled", "disabled");

            //    $("#Availability_AnnualWeek").removeAttr("disabled");
            //    $("#Availability_AnnualWeekDay").removeAttr("disabled");
            //    $("#Availability_AnnualMonthWeek").removeAttr("disabled");
            //} else {
            //    $("#Availability_AnnualMonthDay").removeAttr("readonly");
            //    $("#Availability_AnnualMonth").removeAttr("disabled");

            //    $("#Availability_AnnualWeek").attr("disabled", "disabled");
            //    $("#Availability_AnnualWeekDay").attr("disabled", "disabled");
            //    $("#Availability_AnnualMonthWeek").attr("disabled", "disabled");
            //}
        }

        function AvailabilityTimeEvent() {
            //AvailabilityTimeTypeChange();
            //$('input[type=radio][id=Availability_UnavailabilityTimeType]').change(function () {

            //    AvailabilityTimeTypeChange();
            //});
        }

        function AvailabilityTimeTypeChange() {

            //if ($('input[type=radio][id=Availability_UnavailabilityTimeType]:checked').val() == "1") {

            //    $("#Availability_TimeForm").attr("disabled", "disabled");
            //    $("#Availability_TimeTo").attr("disabled", "disabled");
            //} else {
            //    $("#Availability_TimeForm").removeAttr("disabled");
            //    $("#Availability_TimeTo").removeAttr("disabled");
            //}
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

            //unavailabilityValidate();
        }

        function unavailabilityfrequencyTypeChange() {
            //if ($('input[type=radio][id=Unavailability_FrequencyType]:checked').val() == "4") {
            //    $("#daily-basis").addClass("hidden");
            //    $("#weekly-basis").addClass("hidden");
            //    $("#monthly-basis").addClass("hidden");
            //    $("#annual-basis").removeClass("hidden");
            //} else if ($('input[type=radio][id=Unavailability_FrequencyType]:checked').val() == "3") {
            //    $("#daily-basis").addClass("hidden");
            //    $("#weekly-basis").addClass("hidden");
            //    $("#monthly-basis").removeClass("hidden");
            //    $("#annual-basis").addClass("hidden");
            //} else if ($('input[type=radio][id=Unavailability_FrequencyType]:checked').val() == "2") {
            //    $("#daily-basis").addClass("hidden");
            //    $("#weekly-basis").removeClass("hidden");
            //    $("#monthly-basis").addClass("hidden");
            //    $("#annual-basis").addClass("hidden");
            //} else {
            //    $("#daily-basis").removeClass("hidden");
            //    $("#weekly-basis").addClass("hidden");
            //    $("#monthly-basis").addClass("hidden");
            //    $("#annual-basis").addClass("hidden");
            //}
        }

        function unavailabilityfrequencyDailyTypeChange() {
            //if ($('input[type=radio][id=Unavailability_DailyType]:checked').val() == "2") {
            //    $("#Unavailability_DailyDays").attr("readonly", true);
            //} else {
            //    $("#Unavailability_DailyDays").removeAttr("readonly");
            //}
        }

        function unavailabilityfrequencyMonthlyTypeChange() {
            //if ($('input[type=radio][id=Unavailability_MonthlyType]:checked').val() == "2") {
            //    $("#Unavailability_MonthlyDays").attr("readonly", true);
            //    $("#Unavailability_MonthlyMonths").attr("readonly", true);

            //    $("#Unavailability_MonthlyWeek").removeAttr("disabled");
            //    $("#Unavailability_MonthlyWeekDay").removeAttr("disabled");
            //    $("#Unavailability_MonthlyWeekMonth").removeAttr("readonly");
            //} else {
            //    $("#Unavailability_MonthlyDays").removeAttr("readonly");
            //    $("#Unavailability_MonthlyMonths").removeAttr("readonly");

            //    $("#Unavailability_MonthlyWeekMonth").attr("readonly", true);
            //    $("#Unavailability_MonthlyWeek").attr("disabled", "disabled");
            //    $("#Unavailability_MonthlyWeekDay").attr("disabled", "disabled");
            //}
        }

        function unavailabilityfrequencyAnnualTypeChange() {
            //if ($('input[type=radio][id=Unavailability_AnnualType]:checked').val() == "2") {
            //    $("#Unavailability_AnnualMonthDay").attr("readonly", true);
            //    $("#Unavailability_AnnualMonth").attr("disabled", "disabled");

            //    $("#Unavailability_AnnualWeek").removeAttr("disabled");
            //    $("#Unavailability_AnnualWeekDay").removeAttr("disabled");
            //    $("#Unavailability_AnnualMonthWeek").removeAttr("disabled");
            //} else {
            //    $("#Unavailability_AnnualMonthDay").removeAttr("readonly");
            //    $("#Unavailability_AnnualMonth").removeAttr("disabled");

            //    $("#Unavailability_AnnualWeek").attr("disabled", "disabled");
            //    $("#Unavailability_AnnualWeekDay").attr("disabled", "disabled");
            //    $("#Unavailability_AnnualMonthWeek").attr("disabled", "disabled");
            //}
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
            $(".volunteer-Skiils").select2({
                placeholder: "Select Skills"
            });
            //$(".date-today-availibilty").on("click", function () {
            //    var date = new Date();
            //    var currentDate = new Date(date.getYear(), date.getMonth(), date.getDate());
            //    var todaydate = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
            //    $(".datepicker-availibilty").datepicker('setDate', todaydate);
            //    $(".datepicker-availibilty").datepicker('update');
            //});

            //$(".date-today-unavailibilty").on("click", function () {
            //    var date = new Date();
            //    var currentDate = new Date(date.getYear(), date.getMonth(), date.getDate());
            //    var todaydate = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
            //    $(".datepicker-unavailibilty").datepicker('setDate', todaydate);
            //    $(".datepicker-unavailibilty").datepicker('update');
            //});
            initializeTimePicker();
            initializeTimePicker();
        }

        function unavailabilityUntilEvent() {
            //unavailabilityUntilTypeChange();
            //$('input[type=radio][id=Unavailability_UnavailabilityUntilType]').change(function () {
            //    unavailabilityUntilTypeChange();
            //});
        }

        function unavailabilityTimeEvent() {
            //unavailabilityTimeTypeChange();
            //$('input[type=radio][id=Unavailability_UnavailabilityTimeType]').change(function () {
            //    unavailabilityTimeTypeChange();
            //});
        }

        function unavailabilityTimeTypeChange() {
            //if ($('input[type=radio][id=Unavailability_UnavailabilityTimeType]:checked').val() == "1") {
            //    $("#Unavailability_TimeForm").attr("disabled", "disabled");
            //    $("#Unavailability_TimeTo").attr("disabled", "disabled");
            //} else {
            //    $("#Unavailability_TimeForm").removeAttr("disabled");
            //    $("#Unavailability_TimeTo").removeAttr("disabled");
            //}
        }

        function unavailabilityUntilTypeChange() {
            //if ($('input[type=radio][id=Unavailability_UnavailabilityUntilType]:checked').val() == "1") {
            //    $("#FinishDate").attr("disabled", "disabled");
            //} else {
            //    $("#FinishDate").removeAttr("disabled");
            //}
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
            $("#changepassword-container").hide();
            $("#regularavailability-container").hide();
            $("#unavailability-container").hide();
        }

        ///// change password functionalty
        $(document).off('click', '#IsChangePassword').on('click', '#IsChangePassword', function () {

            if ($("#IsChangePassword").is(":checked")) {
                $("#changepassword-container").show();
                $("#IsChangePassword").val(true)
            }
            else {
                $("#changepassword-container").hide();
                $("#IsChangePassword").val(false)
            }
        });

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

        /////
        $(document).off('change', '#availabilityunavailability').on('change', '#availabilityunavailability', function () {

            if ($(this).val() == 0) {
                InitalizeAvailability();
                $("#div-volunteer-availability-list").show();
                $("#grid-volunteer-availability-list").show();
                $("#div-volunteer-unavailability-list").hide();
                $("#grid-volunteer-unavailability-list").hide();
            }
            else if ($(this).val() == 1) {
                InitalizeUnavailability();
                $("#div-volunteer-unavailability-list").show();
                $("#grid-volunteer-unavailability-list").show();
                $("#div-volunteer-availability-list").hide();
                $("#grid-volunteer-availability-list").hide();
            }
            else if ($(this).val() == 'Select') {
                $("#div-volunteer-availability-list").hide();
                $("#grid-volunteer-availability-list").hide();
                $("#div-volunteer-unavailability-list").hide();
                $("#grid-volunteer-unavailability-list").hide();
            }

        });

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
              /*  { "sName": "" },*/
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0, 2] }, { 'visible': false, 'aTargets': [0] }],
            }, "FoodBank/AdminVolunteer/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val(),
                Global.DeleteMasters);
            $("#grid-volunteer-availability-list").parent("div").parent("div").addClass("table-responsive");
            $this.volunteerAvailabilityGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "FoodBank/AdminVolunteer/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.volunteerAvailabilityGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "FoodBank/AdminVolunteer/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
        }

        function InitalizeUnavailability() {
            if ($.fn.DataTable.isDataTable($this.volunteerUnavailabilityGrid)) {
                $($this.volunteerUnavailabilityGrid).DataTable().destroy();
            }
            $this.volunteerUnavailabilityGrid = new Global.GridAjaxHelper('#grid-volunteer-unavailability-list', {
                "aoColumns": [{ "sName": "UnAvailabilityId" },
                { "sName": "FormDate" },
                { "sName": "MapPattern" },
                { "sName": "ToDate" },
                { "sName": "AllDay" },
              /*  { "sName": "" },*/
                ],
                "bStateSave": false,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0, 2] }, { 'visible': false, 'aTargets': [0] }],
            }, "FoodBank/AdminVolunteer/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val(),
                Global.DeleteMasters);
            $("#grid-volunteer-unavailability-list").parent("div").parent("div").addClass("table-responsive");
            $this.volunteerUnavailabilityGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "FoodBank/AdminVolunteer/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.volunteerUnavailabilityGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "FoodBank/AdminVolunteer/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
        }

        $(document).off('click', '#btn-submit').on('click', '#btn-submit', function () {
            $('form').valid();
            
            if ($('#DocPath1').val() == "") {
                if (document.getElementById('file').value == "") {
                    alertify.dismissAll();
                    alertify.error("Please select files.");
                    return false;
                }
            }
            var Ingredients = $('Select[name="SkillsId"]').map(function () {
                return this
            }).get();
            var valueIngredients = '';
            for (var i = 0; i < Ingredients.length; i++) {
                var sub = '';
                if (Ingredients[i].selectedOptions.length == 0) {
                    alertify.dismissAll();
                    alertify.error("Please select skills.");
                    return false;
                }
                for (var j = 0; j < Ingredients[i].selectedOptions.length; j++) {
                    sub += Ingredients[i].selectedOptions[j].value + ',';
                }
                valueIngredients += sub;
            }
            $('#SkillsIds').val(valueIngredients);
            $('form').submit();
        });


        $this.init = function () {
            initializeDatePicker();
            unavailabilityFrequencyEvent();
            unavailabilityUntilEvent();
            unavailabilityTimeEvent();
            AvailabilityFrequencyEvent();
            AvailabilityfrequencyTypeChange();
            AvailabilityfrequencyDailyTypeChange();
            AvailabilityfrequencyMonthlyTypeChange();
            AvailabilityfrequencyAnnualTypeChange();
            AvailabilityTimeEvent();
            hideUpdateProfileSection();
            InitalizeAvailability();
            InitalizeUnavailability();
            $("#div-volunteer-availability-list").hide();
            $("#grid-volunteer-availability-list").hide();
            $("#div-volunteer-unavailability-list").hide();
            $("#grid-volunteer-unavailability-list").hide();
        };

    }

    $(
        function () {
            var self = new VolunteerProfile();
            self.init();
        }
    )
})(jQuery)