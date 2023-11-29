using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Agencies
    {
        public Agencies()
        {
            FamilyAgency = new HashSet<FamilyAgency>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string BriefSummary { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int FoodBankId { get; set; }
        public int ContactId { get; set; }
        public int AddressId { get; set; }

        public virtual Fbaddress Address { get; set; }
        public virtual Fbcontact Contact { get; set; }
        public virtual Foodbank FoodBank { get; set; }
        public virtual ICollection<FamilyAgency> FamilyAgency { get; set; }
    }
}
