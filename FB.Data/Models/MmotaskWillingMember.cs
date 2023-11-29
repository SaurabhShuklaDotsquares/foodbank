using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmotaskWillingMember
    {
        public int TaskWillingId { get; set; }
        public int MasterTaskId { get; set; }
        public int PersonId { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public string Comments { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual MmomasterTask MasterTask { get; set; }
        public virtual Person Person { get; set; }
    }
}
