using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmocorrespondence
    {
        public int CorrespondenceId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonId { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Household Household { get; set; }
        public virtual Person Person { get; set; }
    }
}
