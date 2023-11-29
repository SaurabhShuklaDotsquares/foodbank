using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodbankRecipe
    {
        public FoodbankRecipe()
        {
            FoodbankRecipeFoodItem = new HashSet<FoodbankRecipeFoodItem>();
            Parcels = new HashSet<Parcels>();
        }

        public int Id { get; set; }
        public int FoodbankId { get; set; }
        public string RecipeTitle { get; set; }
        public string Ingredients { get; set; }
        public int? Quntity { get; set; }
        public bool? IsRecipeNeedToCook { get; set; }
        public string CookingInstructions { get; set; }
        public string RecipeNumber { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDelete { get; set; }

        public virtual Foodbank Foodbank { get; set; }
        public virtual ICollection<FoodbankRecipeFoodItem> FoodbankRecipeFoodItem { get; set; }
        public virtual ICollection<Parcels> Parcels { get; set; }
    }
}
