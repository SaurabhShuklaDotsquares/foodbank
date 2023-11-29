using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CharityContactPerson
    {
        public int Id { get; set; }
        public int CharityId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsDefault { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string LandLineNo { get; set; }
        public string MobileNo { get; set; }
        public byte? ContactPreference { get; set; }
        public string Title { get; set; }

        public virtual Charity Charity { get; set; }
    }
}
