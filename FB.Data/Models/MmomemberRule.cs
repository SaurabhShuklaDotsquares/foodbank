using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmomemberRule
    {
        public int MemberRuleId { get; set; }
        public int MasterTaskId { get; set; }
        public int PersonId { get; set; }
        public int PreferenceType { get; set; }
        public int OppositePersonId { get; set; }
        public DateTime? UnAvailableDate { get; set; }
        public string UnAvailableTime { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual MmomasterTask MasterTask { get; set; }
        public virtual Person OppositePerson { get; set; }
        public virtual Person Person { get; set; }
    }
}
