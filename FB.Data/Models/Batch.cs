using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Batch
    {
        public int BatchId { get; set; }
        public int? PersonId { get; set; }
        public int PurposeId { get; set; }
        public int? MethodId { get; set; }
        public int? EnvelopeId { get; set; }
        public int BranchId { get; set; }
        public string BatchReference { get; set; }
        public string Comment { get; set; }
        public DateTime DonationDate { get; set; }
        public bool IsClaimTax { get; set; }
        public bool IsProcess { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public bool Gasdseligible { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }
        public bool? IsAggregate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Envelope Envelope { get; set; }
        public virtual Method Method { get; set; }
        public virtual Person Person { get; set; }
        public virtual Purpose Purpose { get; set; }
    }
}
