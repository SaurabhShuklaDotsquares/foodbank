using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TransactionFee
    {
        public int TransactionFeeId { get; set; }
        public int TransactionFeeForTp { get; set; }
        public decimal? PercentFee { get; set; }
        public decimal? PenceFee { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public decimal? DataDevelopmentFee { get; set; }
        public decimal? DataDevPercentage { get; set; }
        public decimal? DataDevFixedAmount { get; set; }
    }
}
