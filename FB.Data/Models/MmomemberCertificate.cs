using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomemberCertificate
    {
        public int MemberCertificateId { get; set; }
        public int CertificateId { get; set; }
        public int PersonId { get; set; }
        public int? CertificateIssuerId { get; set; }
        public DateTime? IssueDate { get; set; }
        public string FileName { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Mmocertificate Certificate { get; set; }
        public virtual MmocertificateIssuer CertificateIssuer { get; set; }
        public virtual Person Person { get; set; }
    }
}
