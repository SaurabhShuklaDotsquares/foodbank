using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmoactivityLog
    {
        public int ActivityId { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }
        public string ActivityDetail { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string ActivityType { get; set; }
    }
}
