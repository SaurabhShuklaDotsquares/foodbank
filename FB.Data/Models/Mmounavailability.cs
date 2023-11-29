using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmounavailability
    {
        public int UnavailabilityId { get; set; }
        public int PersonId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool? Allday { get; set; }
        public bool? Checkbox { get; set; }
        public string Pattern { get; set; }
        public string Comment { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Person Person { get; set; }
    }
}
