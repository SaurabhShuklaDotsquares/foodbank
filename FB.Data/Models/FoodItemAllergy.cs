using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodItemAllergy
    {
        public int Id { get; set; }
        public int AllergyId { get; set; }
        public int FoodItemId { get; set; }

        public virtual Allergies Allergy { get; set; }
        public virtual FoodItem FoodItem { get; set; }
    }
}
