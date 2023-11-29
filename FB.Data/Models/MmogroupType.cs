using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmogroupType
    {
        public MmogroupType()
        {
            MmogroupLink = new HashSet<MmogroupLink>();
        }

        public int GroupTypeId { get; set; }
        public string GroupTypeDescription { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkgrouptype { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmogroupLink> MmogroupLink { get; set; }
    }
}
