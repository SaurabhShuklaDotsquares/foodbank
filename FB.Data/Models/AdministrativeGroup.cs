using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AdministrativeGroup
    {
        public AdministrativeGroup()
        {
            Person = new HashSet<Person>();
        }

        public int AdministrativeGroupId { get; set; }
        public string AdministativeGroupDescription { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
