using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomembershipEnrolmentFormUserDefinedFields
    {
        public int FormUserDefinedFieldId { get; set; }
        public int FormId { get; set; }
        public int FieldId { get; set; }

        public virtual MmouserDefinedField Field { get; set; }
        public virtual MmomembershipEnrolmentForm Form { get; set; }
    }
}
