using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FilterCriteria
    {
        public FilterCriteria()
        {
            InverseParentCriteria = new HashSet<FilterCriteria>();
        }

        public int FilterCriteriaId { get; set; }
        public int SmartFilterId { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
        public int? CriteriaOrder { get; set; }
        public int? ParentCriteriaId { get; set; }
        public int? JoinCriteria { get; set; }
        public bool IsNot { get; set; }
        public int? AggType { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }

        public virtual FilterCriteria ParentCriteria { get; set; }
        public virtual SmartFilter SmartFilter { get; set; }
        public virtual ICollection<FilterCriteria> InverseParentCriteria { get; set; }
    }
}
