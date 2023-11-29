using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomemberSkill
    {
        public int MemberSkillId { get; set; }
        public int SkillId { get; set; }
        public int PersonId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Mmoskill Skill { get; set; }
    }
}
