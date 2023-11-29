using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmoskill
    {
        public Mmoskill()
        {
            MmomemberSkill = new HashSet<MmomemberSkill>();
            MmotaskSkill = new HashSet<MmotaskSkill>();
        }

        public int SkillId { get; set; }
        public int? SkillGroupId { get; set; }
        public string SkillName { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkgroup { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual MmoskillGroup SkillGroup { get; set; }
        public virtual ICollection<MmomemberSkill> MmomemberSkill { get; set; }
        public virtual ICollection<MmotaskSkill> MmotaskSkill { get; set; }
    }
}
