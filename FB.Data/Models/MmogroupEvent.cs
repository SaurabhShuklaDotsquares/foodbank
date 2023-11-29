using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmogroupEvent
    {
        public MmogroupEvent()
        {
            MmogroupEventAttendance = new HashSet<MmogroupEventAttendance>();
        }

        public int EventId { get; set; }
        public int GroupId { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? RescheduledDate { get; set; }
        public string Comments { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string MseventId { get; set; }
        public string TenantId { get; set; }
        public bool? IsTeamsMeeting { get; set; }
        public string Mccpkgroupschedule { get; set; }

        public virtual Mmogroup Group { get; set; }
        public virtual ICollection<MmogroupEventAttendance> MmogroupEventAttendance { get; set; }
    }
}
