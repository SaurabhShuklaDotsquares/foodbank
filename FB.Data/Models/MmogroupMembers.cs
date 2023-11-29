using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmogroupMembers
    {
        public MmogroupMembers()
        {
            MmogroupMembersFee = new HashSet<MmogroupMembersFee>();
        }

        public int GroupMemberId { get; set; }
        public int GroupId { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? RecentDate { get; set; }
        public string Comments { get; set; }
        public bool IsMember { get; set; }
        public bool IsLeader { get; set; }
        public bool? AllowEmailNotifications { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public bool? IsTeamMember { get; set; }

        public virtual Mmogroup Group { get; set; }
        public virtual Household Household { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<MmogroupMembersFee> MmogroupMembersFee { get; set; }
    }
}
