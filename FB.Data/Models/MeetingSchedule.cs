using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MeetingSchedule
    {
        public int MeetingScheduleId { get; set; }
        public int MeetingId { get; set; }
        public int ScheduleId { get; set; }

        public virtual Meeting Meeting { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
