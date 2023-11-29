using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Method
    {
        public Method()
        {
            BankStatementDonorMapped = new HashSet<BankStatementDonorMapped>();
            Batch = new HashSet<Batch>();
            CharityGoCardLessPlan = new HashSet<CharityGoCardLessPlan>();
            Donation = new HashSet<Donation>();
            GoCardlessSubscription = new HashSet<GoCardlessSubscription>();
            MethodAccess = new HashSet<MethodAccess>();
            PendingPayment = new HashSet<PendingPayment>();
            QuickDonorGift = new HashSet<QuickDonorGift>();
            RegularGift = new HashSet<RegularGift>();
            StandingGift = new HashSet<StandingGift>();
            TrueLayerStandingOrder = new HashSet<TrueLayerStandingOrder>();
        }

        public int MethodId { get; set; }
        public string MethodDescription { get; set; }
        public bool? Cash { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public bool? Active { get; set; }
        public bool InternalUseOnly { get; set; }
        public bool? IsClaimedExternally { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<BankStatementDonorMapped> BankStatementDonorMapped { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }
        public virtual ICollection<CharityGoCardLessPlan> CharityGoCardLessPlan { get; set; }
        public virtual ICollection<Donation> Donation { get; set; }
        public virtual ICollection<GoCardlessSubscription> GoCardlessSubscription { get; set; }
        public virtual ICollection<MethodAccess> MethodAccess { get; set; }
        public virtual ICollection<PendingPayment> PendingPayment { get; set; }
        public virtual ICollection<QuickDonorGift> QuickDonorGift { get; set; }
        public virtual ICollection<RegularGift> RegularGift { get; set; }
        public virtual ICollection<StandingGift> StandingGift { get; set; }
        public virtual ICollection<TrueLayerStandingOrder> TrueLayerStandingOrder { get; set; }
    }
}
