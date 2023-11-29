using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmouserDefinedField
    {
        public MmouserDefinedField()
        {
            MmomembershipEnrolmentFormUserDefinedFields = new HashSet<MmomembershipEnrolmentFormUserDefinedFields>();
            MmouserDefined = new HashSet<MmouserDefined>();
            MmouserDefinedFieldOption = new HashSet<MmouserDefinedFieldOption>();
        }

        public int FieldId { get; set; }
        public string FieldDescription { get; set; }
        public int FieldType { get; set; }
        public byte? IsHouseHold { get; set; }
        public string FieldDefaultValue { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkufielddef { get; set; }
        public bool? IsAutoAssignDefaultValue { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmomembershipEnrolmentFormUserDefinedFields> MmomembershipEnrolmentFormUserDefinedFields { get; set; }
        public virtual ICollection<MmouserDefined> MmouserDefined { get; set; }
        public virtual ICollection<MmouserDefinedFieldOption> MmouserDefinedFieldOption { get; set; }
    }
}
