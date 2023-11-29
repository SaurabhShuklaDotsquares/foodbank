using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public int PersonId { get; set; }
        public int MeetingId { get; set; }

        public virtual Meeting Meeting { get; set; }
        public virtual Person Person { get; set; }
    }
}
