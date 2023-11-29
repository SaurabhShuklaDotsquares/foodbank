using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class JustGivingPaymentRef
    {
        public int JustGivingPayRefId { get; set; }
        public int? CharityId { get; set; }
        public string PayRefId { get; set; }
        public DateTime LastImportDate { get; set; }

        public virtual Charity Charity { get; set; }
    }
}
