using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CharityGoCardLessPlan
    {
        public CharityGoCardLessPlan()
        {
            GoCardlessSubscription = new HashSet<GoCardlessSubscription>();
            TrueLayerStandingOrder = new HashSet<TrueLayerStandingOrder>();
        }

        public int PlanId { get; set; }
        public int CharityId { get; set; }
        public string PlanLink { get; set; }
        public int Frequency { get; set; }
        public byte FrequencyType { get; set; }
        public decimal Amount { get; set; }
        public bool? IsActive { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal AnnualIncrease { get; set; }
        public int? MethodId { get; set; }
        public decimal? TransactionFeeAmount { get; set; }
        public int? AnnualIncreaseShown { get; set; }

        public virtual Charity Charity { get; set; }
        public virtual Method Method { get; set; }
        public virtual ICollection<GoCardlessSubscription> GoCardlessSubscription { get; set; }
        public virtual ICollection<TrueLayerStandingOrder> TrueLayerStandingOrder { get; set; }
    }
}
