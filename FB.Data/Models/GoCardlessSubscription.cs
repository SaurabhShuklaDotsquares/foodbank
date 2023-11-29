using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GoCardlessSubscription
    {
        public GoCardlessSubscription()
        {
            RegularGift = new HashSet<RegularGift>();
        }

        public int GoCardlessSubscriptionId { get; set; }
        public string SubscriptionId { get; set; }
        public string GocardlessCustomerId { get; set; }
        public int? PlanId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Frequency { get; set; }
        public byte? IntervalUnit { get; set; }
        public string Mandate { get; set; }
        public bool IsPlanModified { get; set; }
        public int PersonId { get; set; }
        public int CharityId { get; set; }
        public byte SubscriptionStatus { get; set; }
        public decimal? AnnualIncrease { get; set; }
        public bool IsAnnualIncrease { get; set; }
        public bool? IsGiftAidDonation { get; set; }
        public int? PurposeId { get; set; }
        public int? MethodId { get; set; }
        public decimal? TransactionFeeAmount { get; set; }

        public virtual Charity Charity { get; set; }
        public virtual Method Method { get; set; }
        public virtual Person Person { get; set; }
        public virtual CharityGoCardLessPlan Plan { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual ICollection<RegularGift> RegularGift { get; set; }
    }
}
