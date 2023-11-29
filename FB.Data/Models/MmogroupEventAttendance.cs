using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmogroupEventAttendance
    {
        public int GroupEventAttendanceId { get; set; }
        public int EventId { get; set; }
        public int GroupId { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonId { get; set; }
        public int? Reason { get; set; }
        public DateTime EventDate { get; set; }
        public string Comments { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual MmogroupEvent Event { get; set; }
        public virtual Mmogroup Group { get; set; }
        public virtual Household Household { get; set; }
        public virtual Person Person { get; set; }
        public virtual MmoattendanceCode ReasonNavigation { get; set; }
    }
}
