using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            ContactHistory = new HashSet<ContactHistory>();
        }

        public int ContactTypeId { get; set; }
        public string ContactTypeDescription { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<ContactHistory> ContactHistory { get; set; }
    }
}
