using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Gasds
    {
        public int Gasdsid { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public int? TaxYear { get; set; }
        public decimal? Allocation { get; set; }
        public bool? IsAddedfromTaxYear { get; set; }
        public bool? IsNew { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Charity Charity { get; set; }
    }
}
