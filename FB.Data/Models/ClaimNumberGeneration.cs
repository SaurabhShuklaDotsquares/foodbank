using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ClaimNumberGeneration
    {
        public ClaimNumberGeneration()
        {
            Audit = new HashSet<Audit>();
        }

        public int Id { get; set; }
        public string ClaimNo { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual ICollection<Audit> Audit { get; set; }
    }
}
