using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AdministrationLetter
    {
        public int LetterId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string LetterHeadImage { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public bool IsLetterHeadedPaper { get; set; }
        public int? LetterHeadSize { get; set; }
        public string OrgNames { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreationDate { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Greeting { get; set; }
        public string Names { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
    }
}
