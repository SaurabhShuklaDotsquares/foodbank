
(function ($) {
    function VolunteerRegisterProfile() {
        var $this = this, volunteerUnavailabilityGrid, PackingGrid, formUpdateProfile, volunteerAvailabilityGrid, deleteVolunteerUnavailabilityGrid;

        function initializeModalWithForm() {
            $("#modal-add-availability").on('loaded.bs.modal', function (e) {
                HideLoader();

                $(document).off('change', '#Availability_FrequencyType').on('change', '#Availability_FrequencyType', function () {

                    AvailabilityFrequencyEvent();

                });

                $("#Availability_FromDate").on("change", function () {
                    var fromDate = $(this).val();
                    if (fromDate != "") {
                        $('#Availability_ToDate').datepicker('setStartDate', fromDate);
                    }
                });

                $("#Availability_ToDate").on("change", function () {
                    var toDate = $(this).val();
                    if (toDate != "") {
                        $('#Availability_FromDate').datepicker('setEndDate', toDate);
                    }
                });

                $("#Unavailability_FromDate").on("change", function () {
                    var fromDate = $(this).val();
                    if (fromDate != "") {
                        $('#Unavailability_ToDate').datepicker('setStartDate', fromDate);
                    }
                });

                $("#Unavailability_ToDate").on("change", function () {
                    var toDate = $(this).val();
                    if (toDate != "") {
                        $('#Unavailability_FromDate').datepicker('setEndDate', toDate);
                    }
                });

                initializeDatePicker();
                AvailabilityFrequencyEvent();
                AvailabilityfrequencyTypeChange();
                AvailabilityfrequencyweeklyTypeChange();
                AvailabilityfrequencyMonthlyTypeChange();
                AvailabilityfrequencyAnnualTypeChange();
                AvailabilityTimeEvent();


                volunteerAvailabilityGrid = new Global.FormHelper($(this).find("form"),
                    {
                        updateTargetId: "validation-summary", beforeSubmit: function () {
                            if ($('form').valid()) {
                                return true;
                            }
                        }
                    }, function (data) {
                        if (data.isSuccess) {

                            $(".close").trigger('click');
                            alertify.dismissAll();
                            alertify.success(data.data);
                            InitalizeAvailability();
                        }
                        else {
                            alertify.dismissAll();
                            alertify.error(data.data);
                        }
                    });

            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-availability").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });

            $("#modal-add-unavailability").on('loaded.bs.modal', function (e) {
                HideLoader();
                MainFunctionForUnavailability();

                volunteerUnavailabilityGrid = new Global.FormHelper($(this).find("form"),
                    {
                        updateTargetId: "validation-summary", beforeSubmit: function () {
                            if ($('form').valid()) {
                                return true;
                            }
                        }
                    }, function (data) {
                        if (data.isSuccess) {
                            $(".close").trigger('click');
                            alertify.dismissAll();
                            alertify.success(data.data);
                            InitalizeUnavailability();
                        }
                        else {
                            alertify.dismissAll();
                            alertify.error(data.data);
                        }
                    });

            }).on('hidden.bs.modal', function (e) {
                $("#modal-add-unavailability").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });

            $("#modal-delete-volunteer-availability").on('loaded.bs.modal', function (e) {

                deleteVolunteerUnavailabilityGrid = new Global.FormHelper($(this).find("form"),
                    {
                        updateTargetId: "validation-summary", beforeSubmit: function () {
                            return true;
                        }
                    }, function (data) {
                        if (data.isSuccess) {
                            $(".close").trigger('click');
                            alertify.dismissAll();
                            alertify.success(data.data);
                            InitalizeUnavailability();
                        }
                        else {
                            alertify.dismissAll();
                            alertify.error(data.data);
                        }
                    });

            }).on('hidden.bs.modal', function (e) {
                $("#modal-delete-volunteer-availability").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });

            $("#modal-delete-volunteer-unavailability").on('loaded.bs.modal', function (e) {

                deleteVolunteerUnavailabilityGrid = new Global.FormHelper($(this).find("form"),
                    {
                        updateTargetId: "validation-summary", beforeSubmit: function () {
                            return true;
                        }
                    }, function (data) {
                        if (data.isSuccess) {
                            $(".close").trigger('click');
                            alertify.dismissAll();
                            alertify.success(data.data);
                            InitalizeUnavailability();
                        }
                        else {
                            alertify.dismissAll();
                            alertify.error(data.data);
                        }
                    });
            }).on('hidden.bs.modal', function (e) {
                $("#modal-delete-volunteer-unavailability").find(".modal-content").html("");
                $(this).removeData('bs.modal');
            });

        }

        /////////////// Avalibility Section //////////////////////////////////////////

        function MainFunctionForUnavailability() {
            $("#Unavailability_FrequencyType").change(function () {
                unavailabilityFrequencyEvent();
            });
            initializeDatePicker();
            unavailabilityUntilEvent();
            unavailabilityTimeEvent();
            unavailabilityFrequencyEvent();
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
                $("#volunteerupdateprofileform #daily-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #weekly-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #monthly-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #annual-basis-avalibility").removeClass("hidden");
            } else if ($('[id=Availability_FrequencyType]').val() == "3") {
                $("#volunteerupdateprofileform #daily-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #weekly-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #monthly-basis-avalibility").removeClass("hidden");
                $("#volunteerupdateprofileform #annual-basis-avalibility").addClass("hidden");
            } else if ($('[id=Availability_FrequencyType]').val() == "2") {
                $("#volunteerupdateprofileform #daily-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #weekly-basis-avalibility").removeClass("hidden");
                $("#volunteerupdateprofileform #monthly-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #annual-basis-avalibility").addClass("hidden");
            } else {
                $("#volunteerupdateprofileform #daily-basis-avalibility").removeClass("hidden");
                $("#volunteerupdateprofileform #weekly-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #monthly-basis-avalibility").addClass("hidden");
                $("#volunteerupdateprofileform #annual-basis-avalibility").addClass("hidden");
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

        function InitalizeAvailability() {

            if ($.fn.DataTable.isDataTable($('#grid-volunteer-availability-list'))) {
                $($('#grid-volunteer-availability-list')).DataTable().destroy();
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
            //$("#grid-volunteer-availability-list").parent("div").parent("div").addClass("table-responsive");
            $this.volunteerAvailabilityGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "account/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.volunteerAvailabilityGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "account/GetAvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
        }

        /////////// Unavailbility Section ////////////////////////////////////////////
        function unavailabilityFrequencyEvent() {
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
            initializeTimePicker();
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


        function InitalizeUnavailability() {
            if ($.fn.DataTable.isDataTable($('#grid-volunteer-unavailability-list'))) {
                $($('#grid-volunteer-unavailability-list')).DataTable().destroy();
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
            //$("#grid-volunteer-unavailability-list").parent("div").parent("div").addClass("table-responsive");
            $this.volunteerUnavailabilityGrid.on('search.dt', function () {
                Global.DataServer.dataURL = "account/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
            $this.volunteerUnavailabilityGrid.on('length.dt', function () {
                Global.DataServer.dataURL = "account/GetUnvailabilityList?volunteerId=" + $("#hdnVolunteerId").val();
            });
        }

        $this.init = function () {
            initializeModalWithForm();
            //InitalizeUnavailability();
        };
    }

    $(
        function () {
            var self = new VolunteerRegisterProfile();
            self.init();
        }
    )
})(jQuery)