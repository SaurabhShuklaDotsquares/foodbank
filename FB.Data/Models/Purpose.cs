using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Purpose
    {
        public Purpose()
        {
            BankStatementDonorMapped = new HashSet<BankStatementDonorMapped>();
            Batch = new HashSet<Batch>();
            Charity = new HashSet<Charity>();
            Donation = new HashSet<Donation>();
            GoCardlessSubscription = new HashSet<GoCardlessSubscription>();
            PendingPayment = new HashSet<PendingPayment>();
            PgsdonorContact = new HashSet<PgsdonorContact>();
            PledgeDetail = new HashSet<PledgeDetail>();
            PurposeAccess = new HashSet<PurposeAccess>();
            QuickDonorGift = new HashSet<QuickDonorGift>();
            RegularGift = new HashSet<RegularGift>();
            StandingGift = new HashSet<StandingGift>();
            TrueLayerStandingOrder = new HashSet<TrueLayerStandingOrder>();
            WebsiteButton = new HashSet<WebsiteButton>();
        }

        public int PurposeId { get; set; }
        public string PurposeTitle { get; set; }
        public string PurposeDescription { get; set; }
        public bool? IsDefault { get; set; }
        public bool Active { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? BranchId { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }
        public int? CharityId { get; set; }
        public bool InternalUseOnly { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual ICollection<BankStatementDonorMapped> BankStatementDonorMapped { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }
        public virtual ICollection<Charity> Charity { get; set; }
        public virtual ICollection<Donation> Donation { get; set; }
        public virtual ICollection<GoCardlessSubscription> GoCardlessSubscription { get; set; }
        public virtual ICollection<PendingPayment> PendingPayment { get; set; }
        public virtual ICollection<PgsdonorContact> PgsdonorContact { get; set; }
        public virtual ICollection<PledgeDetail> PledgeDetail { get; set; }
        public virtual ICollection<PurposeAccess> PurposeAccess { get; set; }
        public virtual ICollection<QuickDonorGift> QuickDonorGift { get; set; }
        public virtual ICollection<RegularGift> RegularGift { get; set; }
        public virtual ICollection<StandingGift> StandingGift { get; set; }
        public virtual ICollection<TrueLayerStandingOrder> TrueLayerStandingOrder { get; set; }
        public virtual ICollection<WebsiteButton> WebsiteButton { get; set; }
    }
}
