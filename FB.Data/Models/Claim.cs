using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Claim
    {
        public Claim()
        {
            Donation = new HashSet<Donation>();
        }

        public int ClaimId { get; set; }
        public int AuditId { get; set; }
        public int CharityId { get; set; }
        public string Comment { get; set; }
        public DateTime? ClaimStart { get; set; }
        public DateTime? ClaimEnd { get; set; }
        public DateTime? DateProcessed { get; set; }
        public bool? ClaimPaid { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? ClaimAmount { get; set; }
        public decimal? FeeAmount { get; set; }
        public decimal? NetValue { get; set; }
        public string RefundClaimId { get; set; }
        public bool? Gasdsclaim { get; set; }
        public int? GasdsclaimTaxYear { get; set; }
        public int? CommBuildBranchId { get; set; }
        public bool? OtherIncomeClaim { get; set; }
        public decimal? AdjustmentValue { get; set; }
        public decimal? OverpaymentAmount { get; set; }
        public string AdjustmentSent { get; set; }
        public string AdjustmentId { get; set; }
        public bool IsDeleted { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? GasdsDonationId { get; set; }

        public virtual Audit Audit { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Branch CommBuildBranch { get; set; }
        public virtual ICollection<Donation> Donation { get; set; }
    }
}
