using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ContactLessScheduleDate
    {
        public int ScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }

        public virtual ContactLessSchedule Schedule { get; set; }
    }
}
