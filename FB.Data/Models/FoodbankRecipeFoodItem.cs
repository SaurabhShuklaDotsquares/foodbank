using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodbankRecipeFoodItem
    {
        public int Id { get; set; }
        public int? FoodbankRecipeId { get; set; }
        public int? FoodId { get; set; }
        public int? Quantity { get; set; }

        public virtual Food Food { get; set; }
        public virtual FoodbankRecipe FoodbankRecipe { get; set; }
    }
}
