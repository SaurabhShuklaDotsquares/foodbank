using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TaxYear
    {
        public int TaxYearId { get; set; }
        public DateTime TaxYearStart { get; set; }
        public DateTime TaxYearEnd { get; set; }
        public decimal? TaxRate { get; set; }
        public bool Keeper { get; set; }
        public decimal? Gasdslimit { get; set; }
        public decimal? GasdsmaxGiftAmount { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
    }
}
