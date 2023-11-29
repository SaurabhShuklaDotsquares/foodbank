using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ReferralReason
    {
        public ReferralReason()
        {
            ReferrerFamily = new HashSet<ReferrerFamily>();
        }

        public int Id { get; set; }
        public string Reason { get; set; }
        public int? FoodBankId { get; set; }

        public virtual ICollection<ReferrerFamily> ReferrerFamily { get; set; }
    }
}
