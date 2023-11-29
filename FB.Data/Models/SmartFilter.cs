using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class SmartFilter
    {
        public SmartFilter()
        {
            FilterCriteria = new HashSet<FilterCriteria>();
        }

        public int SmartFilterId { get; set; }
        public string Query { get; set; }
        public string FilterName { get; set; }
        public string Description { get; set; }
        public int? CentralOfficeId { get; set; }
        public bool IsSimple { get; set; }
        public bool ShowNavigation { get; set; }
        public bool IsImport { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual ICollection<FilterCriteria> FilterCriteria { get; set; }
    }
}
