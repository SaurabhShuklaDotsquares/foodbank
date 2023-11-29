using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomembershipType
    {
        public MmomembershipType()
        {
            MmopersonAdditonalDetails = new HashSet<MmopersonAdditonalDetails>();
        }

        public int MembershipTypeId { get; set; }
        public string MembershipTypeDescription { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkgrouptype { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmopersonAdditonalDetails> MmopersonAdditonalDetails { get; set; }
    }
}
