using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class AvailabilityDto
    {
        public int AvailabilityId { get; set; }
        public int FrequencyType { get; set; }
        public int UnavailabilityUntilType { get; set; }
        public string Comment { get; set; }
        public int UnavailabilityTimeType { get; set; }
        [DisplayName("From Date*")]
        public string FromDate { get; set; }
        [DisplayName("To Date*")]
        public string ToDate { get; set; }
        [DisplayName("All Day")]
        public bool AllDay { get; set; }
        [DisplayName("Time Form")]
        public string TimeForm { get; set; }
        [DisplayName("Time To")]
        public string TimeTo { get; set; }
        public int VolunteerId { get; set; }
        public int DailyType { get; set; }
        public int DailyDays { get; set; }
        #region Weekly Basis
        public int WeeklyDays { get; set; }
        [DisplayName("Monday")]
        public bool IsWeeklyMonday { get; set; }
        [DisplayName("Tuesday")]
        public bool IsWeeklyTuesday { get; set; }
        [DisplayName("Wednesday")]
        public bool IsWeeklyWednesday { get; set; }
        [DisplayName("Thursday")]
        public bool IsWeeklyThursday { get; set; }
        [DisplayName("Friday")]
        public bool IsWeeklyFriday { get; set; }
        [DisplayName("Saturday")]
        public bool IsWeeklySaturday { get; set; }
        [DisplayName("Sunday")]
        public bool IsWeeklySunday { get; set; }
        #endregion
        #region Monthly Basis
        public int MonthlyType { get; set; }
        public int MonthlyDays { get; set; }
        public int MonthlyMonths { get; set; }
        public int MonthlyWeek { get; set; }
        public int MonthlyWeekDay { get; set; }
        public int MonthlyWeekMonth { get; set; }
        #endregion
        #region Annual Basis
        public int AnnualType { get; set; }
        public int AnnualYears { get; set; }
        public int AnnualMonth { get; set; }
        public int AnnualMonthDay { get; set; }
        public int AnnualWeek { get; set; }
        public int AnnualWeekDay { get; set; }
        public int AnnualMonthWeek { get; set; }
        #endregion

        public string AuditIp { get; set; }
        public int AuditUserId { get; set; }

        public string Pattern { get; set; }
        public string PatternName { get; set; }
    }
}
