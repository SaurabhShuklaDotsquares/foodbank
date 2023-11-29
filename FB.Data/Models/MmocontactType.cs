using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmocontactType
    {
        public MmocontactType()
        {
            Mmocontact = new HashSet<Mmocontact>();
        }

        public int ContactTypeId { get; set; }
        public string ContactTypeDescription { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkphonetype { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<Mmocontact> Mmocontact { get; set; }
    }
}
