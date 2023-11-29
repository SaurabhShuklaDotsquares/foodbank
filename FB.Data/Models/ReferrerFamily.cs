using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ReferrerFamily
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int? ReferrerId { get; set; }
        public bool Inward { get; set; }
        public int? ReasonId { get; set; }
        public DateTime ReferralDate { get; set; }

        public virtual Family Family { get; set; }
        public virtual ReferralReason Reason { get; set; }
        public virtual Referrers Referrer { get; set; }
    }
}
