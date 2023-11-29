using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class StandingGift
    {
        public int StandingGiftId { get; set; }
        public int RegularGiftId { get; set; }
        public int PurposeId { get; set; }
        public int? EnvelopeId { get; set; }
        public int? MethodId { get; set; }
        public string Comment { get; set; }
        public string BatchReference { get; set; }
        public DateTime DonationDate { get; set; }
        public bool IsClaimTax { get; set; }
        public bool Gasdseligible { get; set; }
        public decimal Amount { get; set; }
        public bool IsChecked { get; set; }
        public int BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public decimal? TransactionFeeAmount { get; set; }
        public bool? IsTrueLayer { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Envelope Envelope { get; set; }
        public virtual Method Method { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual RegularGift RegularGift { get; set; }
    }
}
