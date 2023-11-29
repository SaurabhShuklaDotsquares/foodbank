using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Fblocation
    {
        public Fblocation()
        {
            Parcels = new HashSet<Parcels>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public int? ContactId { get; set; }
        public string SocialMedia { get; set; }
        public int FamilyId { get; set; }

        public virtual Fbaddress Address { get; set; }
        public virtual Fbcontact Contact { get; set; }
        public virtual Family Family { get; set; }
        public virtual ICollection<Parcels> Parcels { get; set; }
    }
}
