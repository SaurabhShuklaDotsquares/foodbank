using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Fbaddress
    {
        public Fbaddress()
        {
            Agencies = new HashSet<Agencies>();
            FamilyAddress = new HashSet<FamilyAddress>();
            Fblocation = new HashSet<Fblocation>();
            Foodbank = new HashSet<Foodbank>();
            Grantor = new HashSet<Grantor>();
            Referrers = new HashSet<Referrers>();
        }

        public int Id { get; set; }
        public string HouseName { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public string Postcode { get; set; }
        public int? CountryId { get; set; }
        public string OtherAddressLine { get; set; }
        public int? UserId { get; set; }

        public virtual Country Country { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Agencies> Agencies { get; set; }
        public virtual ICollection<FamilyAddress> FamilyAddress { get; set; }
        public virtual ICollection<Fblocation> Fblocation { get; set; }
        public virtual ICollection<Foodbank> Foodbank { get; set; }
        public virtual ICollection<Grantor> Grantor { get; set; }
        public virtual ICollection<Referrers> Referrers { get; set; }
    }
}
