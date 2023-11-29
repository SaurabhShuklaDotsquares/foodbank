using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmogroupMembersFee
    {
        public int GroupMemberFeeId { get; set; }
        public int GroupMemberId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? Fee { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual MmogroupMembers GroupMember { get; set; }
    }
}
