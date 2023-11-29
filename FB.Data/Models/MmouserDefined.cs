using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmouserDefined
    {
        public int UserDefinedId { get; set; }
        public int FieldId { get; set; }
        public string FieldValue { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonId { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual MmouserDefinedField Field { get; set; }
        public virtual Household Household { get; set; }
        public virtual Person Person { get; set; }
    }
}
