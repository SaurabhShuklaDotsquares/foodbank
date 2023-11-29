using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomasterTask
    {
        public MmomasterTask()
        {
            MmomemberRule = new HashSet<MmomemberRule>();
            MmotaskShift = new HashSet<MmotaskShift>();
            MmotaskSkill = new HashSet<MmotaskSkill>();
            MmotaskWillingMember = new HashSet<MmotaskWillingMember>();
        }

        public int MasterTaskId { get; set; }
        public string Name { get; set; }
        public int? NumberofPeoplePerShift { get; set; }
        public string ExtraInformation { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpktask { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmomemberRule> MmomemberRule { get; set; }
        public virtual ICollection<MmotaskShift> MmotaskShift { get; set; }
        public virtual ICollection<MmotaskSkill> MmotaskSkill { get; set; }
        public virtual ICollection<MmotaskWillingMember> MmotaskWillingMember { get; set; }
    }
}
