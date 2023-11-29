using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TaskSchedule
    {
        public int TaskScheduleId { get; set; }
        public int TaskId { get; set; }
        public int ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }
        public virtual Task Task { get; set; }
    }
}
