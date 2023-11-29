using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Fbcontact
    {
        public Fbcontact()
        {
            Agencies = new HashSet<Agencies>();
            Fblocation = new HashSet<Fblocation>();
            Granter = new HashSet<Granter>();
            Grantor = new HashSet<Grantor>();
            Referrers = new HashSet<Referrers>();
            Volunteer = new HashSet<Volunteer>();
        }

        public int Id { get; set; }
        public string OrganisationName { get; set; }
        public string ForeName { get; set; }
        public string Surname { get; set; }
        public int? AddressId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Agencies> Agencies { get; set; }
        public virtual ICollection<Fblocation> Fblocation { get; set; }
        public virtual ICollection<Granter> Granter { get; set; }
        public virtual ICollection<Grantor> Grantor { get; set; }
        public virtual ICollection<Referrers> Referrers { get; set; }
        public virtual ICollection<Volunteer> Volunteer { get; set; }
    }
}
