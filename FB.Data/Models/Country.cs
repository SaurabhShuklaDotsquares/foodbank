using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            Address = new HashSet<Address>();
            Agent = new HashSet<Agent>();
            County = new HashSet<County>();
            Fbaddress = new HashSet<Fbaddress>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Abbreviation { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Agent> Agent { get; set; }
        public virtual ICollection<County> County { get; set; }
        public virtual ICollection<Fbaddress> Fbaddress { get; set; }
    }
}
