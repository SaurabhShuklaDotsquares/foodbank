using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PendingPayment
    {
        public int PendingPaymentId { get; set; }
        public int? RegularGiftId { get; set; }
        public decimal? Amount { get; set; }
        public string Comment { get; set; }
        public DateTime? DonationDate { get; set; }
        public bool? ClaimTax { get; set; }
        public bool? Transfer { get; set; }
        public bool? Gasdseligible { get; set; }
        public int MethodId { get; set; }
        public int PurposeId { get; set; }
        public int? EnvelopeId { get; set; }

        public virtual Envelope Envelope { get; set; }
        public virtual Method Method { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual RegularGift RegularGift { get; set; }
    }
}
