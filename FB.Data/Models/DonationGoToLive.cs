using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DonationGoToLive
    {
        public int DonationId { get; set; }
        public int BranchId { get; set; }
        public int? DonationTypeId { get; set; }
        public int PurposeId { get; set; }
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
    }
}
