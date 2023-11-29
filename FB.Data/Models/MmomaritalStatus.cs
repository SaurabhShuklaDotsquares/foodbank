using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomaritalStatus
    {
        public MmomaritalStatus()
        {
            MmopersonAdditonalDetails = new HashSet<MmopersonAdditonalDetails>();
        }

        public int MaritalStatusId { get; set; }
        public string MaritalStatusDescription { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkmaritalcode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmopersonAdditonalDetails> MmopersonAdditonalDetails { get; set; }
    }
}
