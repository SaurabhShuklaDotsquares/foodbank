using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            MeetingSchedule = new HashSet<MeetingSchedule>();
            TaskSchedule = new HashSet<TaskSchedule>();
        }

        public int ScheduleId { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public TimeSpan? ScheduleTime { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<MeetingSchedule> MeetingSchedule { get; set; }
        public virtual ICollection<TaskSchedule> TaskSchedule { get; set; }
    }
}
