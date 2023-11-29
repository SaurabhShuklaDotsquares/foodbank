using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Audit
    {
        public Audit()
        {
            Claim = new HashSet<Claim>();
        }

        public int AuditId { get; set; }
        public DateTime AuditDate { get; set; }
        public string CorrelationId { get; set; }
        public string TaxYear { get; set; }
        public string SubmissionErrors { get; set; }
        public string Xmlsent { get; set; }
        public string Xmlreceived { get; set; }
        public string Xmlreturned { get; set; }
        public int? HmrcerrorCode { get; set; }
        public string SubmissionType { get; set; }
        public string EndPointUrl { get; set; }
        public string PollInterval { get; set; }
        public bool? AgencySubmission { get; set; }
        public string Hmrcreference { get; set; }
        public string OtherInfo { get; set; }
        public bool? LostCorrelationId { get; set; }
        public DateTime? PollDate { get; set; }
        public string Status { get; set; }
        public int? ClaimNumberId { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsDummyClaim { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? CentralOfficeId { get; set; }
        public string Cpkfilling { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual ClaimNumberGeneration ClaimNumber { get; set; }
        public virtual ICollection<Claim> Claim { get; set; }
    }
}
