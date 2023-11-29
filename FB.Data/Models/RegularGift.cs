using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class RegularGift
    {
        public RegularGift()
        {
            PendingPayment = new HashSet<PendingPayment>();
            StandingGift = new HashSet<StandingGift>();
        }

        public int RegularGiftId { get; set; }
        public int? PersonId { get; set; }
        public string Description { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateFinish { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime? RecentDate { get; set; }
        public DateTime? NextDate { get; set; }
        public bool ClaimTax { get; set; }
        public decimal? Amount { get; set; }
        public int? Unposted { get; set; }
        public bool Gasdseligible { get; set; }
        public string Type { get; set; }
        public string RegularGiftReference { get; set; }
        public int MethodId { get; set; }
        public int PurposeId { get; set; }
        public int? EnvelopeId { get; set; }
        public string Comment { get; set; }
        public int BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? GoCardlessSubscriptionId { get; set; }
        public int? TrueLayerStandingOrderId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Envelope Envelope { get; set; }
        public virtual GoCardlessSubscription GoCardlessSubscription { get; set; }
        public virtual Method Method { get; set; }
        public virtual Person Person { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual TrueLayerStandingOrder TrueLayerStandingOrder { get; set; }
        public virtual ICollection<PendingPayment> PendingPayment { get; set; }
        public virtual ICollection<StandingGift> StandingGift { get; set; }
    }
}
