using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Envelope
    {
        public Envelope()
        {
            Batch = new HashSet<Batch>();
            Donation = new HashSet<Donation>();
            PendingPayment = new HashSet<PendingPayment>();
            RegularGift = new HashSet<RegularGift>();
            StandingGift = new HashSet<StandingGift>();
        }

        public int EnvelopeId { get; set; }
        public int? PersonId { get; set; }
        public string EnvelopeNumber { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public string OldEnvelopeNumber { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Batch> Batch { get; set; }
        public virtual ICollection<Donation> Donation { get; set; }
        public virtual ICollection<PendingPayment> PendingPayment { get; set; }
        public virtual ICollection<RegularGift> RegularGift { get; set; }
        public virtual ICollection<StandingGift> StandingGift { get; set; }
    }
}
