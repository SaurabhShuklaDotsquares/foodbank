using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomembershipCode
    {
        public MmomembershipCode()
        {
            Mmomembership = new HashSet<Mmomembership>();
            MmomembershipEnrolmentForm = new HashSet<MmomembershipEnrolmentForm>();
        }

        public int MembershipCodeId { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public decimal? Fee { get; set; }
        public int? Frequency { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public bool? Active { get; set; }
        public string Mccpkmembercode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<Mmomembership> Mmomembership { get; set; }
        public virtual ICollection<MmomembershipEnrolmentForm> MmomembershipEnrolmentForm { get; set; }
    }
}
