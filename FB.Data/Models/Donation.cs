using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Donation
    {
        public Donation()
        {
            AppDonationXref = new HashSet<AppDonationXref>();
            GoCardLessPayment = new HashSet<GoCardLessPayment>();
            PaymentImport = new HashSet<PaymentImport>();
            StripePayment = new HashSet<StripePayment>();
        }

        public int DonationId { get; set; }
        public int BranchId { get; set; }
        public int? DonationTypeId { get; set; }
        public int? PurposeId { get; set; }
        public int? DonorId { get; set; }
        public int? Frequency { get; set; }
        public int? ClaimId { get; set; }
        public int? MethodId { get; set; }
        public int? EnvelopeId { get; set; }
        public string Comment { get; set; }
        public DateTime? DonationDate { get; set; }
        public bool? ClaimTax { get; set; }
        public bool? CapitalPaid { get; set; }
        public bool? Gasdseligible { get; set; }
        public bool? FromSponsoredEvent { get; set; }
        public int? Sponsor { get; set; }
        public decimal? Amount { get; set; }
        public int? RegularGiftId { get; set; }
        public int? EventId { get; set; }
        public string BatchReference { get; set; }
        public bool IsAggregate { get; set; }
        public int? RefundType { get; set; }
        public bool? IsAnonymous { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public bool? IsTransferedToFc { get; set; }
        public bool? MarkedAsRead { get; set; }
        public byte? DonationSource { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsTransferedToFcweb { get; set; }
        public int? CpkGift { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ContactLessTerminalId { get; set; }
        public bool? ClaimedExternally { get; set; }
        public bool? IsPartialClaimed { get; set; }
        public decimal? TransactionFeeAmount { get; set; }
        public string ExternalDonId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Claim Claim { get; set; }
        public virtual DonationType DonationType { get; set; }
        public virtual Person Donor { get; set; }
        public virtual Envelope Envelope { get; set; }
        public virtual Event Event { get; set; }
        public virtual Frequency FrequencyNavigation { get; set; }
        public virtual Method Method { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual ICollection<AppDonationXref> AppDonationXref { get; set; }
        public virtual ICollection<GoCardLessPayment> GoCardLessPayment { get; set; }
        public virtual ICollection<PaymentImport> PaymentImport { get; set; }
        public virtual ICollection<StripePayment> StripePayment { get; set; }
    }
}
