using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Allergies
    {
        public Allergies()
        {
            FamilyMemberAllergy = new HashSet<FamilyMemberAllergy>();
            FoodItemAllergy = new HashSet<FoodItemAllergy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FamilyMemberAllergy> FamilyMemberAllergy { get; set; }
        public virtual ICollection<FoodItemAllergy> FoodItemAllergy { get; set; }
    }
}
