using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmocontact
    {
        public Mmocontact()
        {
            MmopersonAdditonalDetailsPrimaryEmailNavigation = new HashSet<MmopersonAdditonalDetails>();
            MmopersonAdditonalDetailsPrimaryPhoneNumberNavigation = new HashSet<MmopersonAdditonalDetails>();
        }

        public int ContactId { get; set; }
        public int? ContactTypeId { get; set; }
        public int? DefaultContactTypeId { get; set; }
        public string Detail { get; set; }
        public bool IsExDirectory { get; set; }
        public string Comment { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonId { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkphone { get; set; }

        public virtual MmocontactType ContactType { get; set; }
        public virtual Household Household { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<MmopersonAdditonalDetails> MmopersonAdditonalDetailsPrimaryEmailNavigation { get; set; }
        public virtual ICollection<MmopersonAdditonalDetails> MmopersonAdditonalDetailsPrimaryPhoneNumberNavigation { get; set; }
    }
}
