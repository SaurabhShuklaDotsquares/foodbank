using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ClaimLog
    {
        public int Id { get; set; }
        public int CharityId { get; set; }
        public string CharityName { get; set; }
        public int UserId { get; set; }
        public string BranchIds { get; set; }
        public string Status { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorLog { get; set; }
        public int? AuditId { get; set; }
        public string SentXml { get; set; }
        public string CorrelationId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ClaimNo { get; set; }
        public string CommonReference { get; set; }
        public bool? IsHmrcsubmit { get; set; }
        public bool? IsDemoClaim { get; set; }
        public string ClaimSteps { get; set; }
    }
}
