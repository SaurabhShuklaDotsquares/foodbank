using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DataDevelopmentProcessFeeDetails
    {
        public int DataDevelopmentProcessFeeSummaryId { get; set; }
        public decimal? Amount { get; set; }
        public int? NoOfDonations { get; set; }
        public int? PaymentType { get; set; }
        public DateTime? MonthDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CentralOfficeId { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
    }
}
