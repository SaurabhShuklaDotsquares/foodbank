using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomembershipMembersFee
    {
        public int MembershipMemberFeeId { get; set; }
        public int MembershipMemberId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? Fee { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string SubcriptionId { get; set; }
        public string PaymentId { get; set; }
        public string TransectionId { get; set; }
        public decimal? ProcessingFee { get; set; }

        public virtual Mmomembership MembershipMember { get; set; }
    }
}
