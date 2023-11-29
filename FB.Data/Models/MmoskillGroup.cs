using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmoskillGroup
    {
        public MmoskillGroup()
        {
            Mmoskill = new HashSet<Mmoskill>();
        }

        public int SkillGroupId { get; set; }
        public string GroupName { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkgrouptype { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<Mmoskill> Mmoskill { get; set; }
    }
}
