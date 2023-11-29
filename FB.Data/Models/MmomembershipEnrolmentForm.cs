using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomembershipEnrolmentForm
    {
        public MmomembershipEnrolmentForm()
        {
            MmobraintreeUserDefinedField = new HashSet<MmobraintreeUserDefinedField>();
            MmomembershipEnrolmentFormUserDefinedFields = new HashSet<MmomembershipEnrolmentFormUserDefinedFields>();
            MmowebsiteButton = new HashSet<MmowebsiteButton>();
        }

        public int FormId { get; set; }
        public string Name { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public int? MembershipCodeId { get; set; }
        public byte FeePayable { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual MmomembershipCode MembershipCode { get; set; }
        public virtual ICollection<MmobraintreeUserDefinedField> MmobraintreeUserDefinedField { get; set; }
        public virtual ICollection<MmomembershipEnrolmentFormUserDefinedFields> MmomembershipEnrolmentFormUserDefinedFields { get; set; }
        public virtual ICollection<MmowebsiteButton> MmowebsiteButton { get; set; }
    }
}
