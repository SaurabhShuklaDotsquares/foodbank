using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PersonType
    {
        public PersonType()
        {
            Person = new HashSet<Person>();
            QuickDonorGift = new HashSet<QuickDonorGift>();
        }

        public int PersonTypeId { get; set; }
        public string PersonTypeDescription { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<QuickDonorGift> QuickDonorGift { get; set; }
    }
}
