using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmobraintreeUserDefinedField
    {
        public int MmobraintreeUserDefinedFieldId { get; set; }
        public Guid ButtonId { get; set; }
        public int CharityId { get; set; }
        public int EnrolementFormId { get; set; }
        public int FieldId { get; set; }
        public string FieldValue { get; set; }
        public string BraintreeCustomerId { get; set; }

        public virtual MmowebsiteButton Button { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual MmomembershipEnrolmentForm EnrolementForm { get; set; }
    }
}
