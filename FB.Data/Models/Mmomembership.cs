using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmomembership
    {
        public Mmomembership()
        {
            MmomembershipMembersFee = new HashSet<MmomembershipMembersFee>();
        }

        public int MembershipMemberId { get; set; }
        public int MembershipCodeId { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime? RecentDate { get; set; }
        public decimal? Fee { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Household Household { get; set; }
        public virtual MmomembershipCode MembershipCode { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<MmomembershipMembersFee> MmomembershipMembersFee { get; set; }
    }
}
