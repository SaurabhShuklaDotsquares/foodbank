using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TrueLayerStandingOrder
    {
        public TrueLayerStandingOrder()
        {
            RegularGift = new HashSet<RegularGift>();
        }

        public int TrueLayerStandingOrderId { get; set; }
        public string Simpid { get; set; }
        public int? PlanId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Frequency { get; set; }
        public byte? IntervalUnit { get; set; }
        public bool IsPlanModified { get; set; }
        public int PersonId { get; set; }
        public int CharityId { get; set; }
        public bool? IsGiftAidDonation { get; set; }
        public int PurposeId { get; set; }
        public int? MethodId { get; set; }
        public decimal? TransactionFeeAmount { get; set; }
        public bool IsAnnualIncrease { get; set; }
        public decimal? AnnualIncrease { get; set; }

        public virtual Charity Charity { get; set; }
        public virtual Method Method { get; set; }
        public virtual Person Person { get; set; }
        public virtual CharityGoCardLessPlan Plan { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual ICollection<RegularGift> RegularGift { get; set; }
    }
}
