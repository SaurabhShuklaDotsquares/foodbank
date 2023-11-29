using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmocertificate
    {
        public Mmocertificate()
        {
            MmomemberCertificate = new HashSet<MmomemberCertificate>();
        }

        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public int? RenewalFrequency { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? Renewal { get; set; }
        public string Mccpksecuritycheckcode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmomemberCertificate> MmomemberCertificate { get; set; }
    }
}
