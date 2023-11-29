using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Meeting
    {
        public Meeting()
        {
            Attendance = new HashSet<Attendance>();
            MeetingSchedule = new HashSet<MeetingSchedule>();
        }

        public int MeetingId { get; set; }
        public DateTime? MeetingDate { get; set; }
        public TimeSpan? MeetingTime { get; set; }
        public int? ScheduleId { get; set; }
        public int? GroupId { get; set; }
        public string Notes { get; set; }
        public int? FrequencyId { get; set; }

        public virtual Frequency Frequency { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<MeetingSchedule> MeetingSchedule { get; set; }
    }
}
