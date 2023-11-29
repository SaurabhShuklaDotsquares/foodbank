using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class EnvelopeSetting
    {
        public int EnvelopeSettingId { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string Description { get; set; }
        public bool IsTitle { get; set; }
        public string Names { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
    }
}
