using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmotaskSkill
    {
        public int TaskSkillId { get; set; }
        public int MasterTaskId { get; set; }
        public int SkillId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual MmomasterTask MasterTask { get; set; }
        public virtual Mmoskill Skill { get; set; }
    }
}
