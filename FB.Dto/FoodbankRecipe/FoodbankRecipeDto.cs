using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class FoodbankRecipeDto
    {
        public int Id { get; set; }
        public int FoodbankId { get; set; }
        public string RecipeTitle { get; set; }
        public string Ingredients { get; set; }
        public string Ingredientsids { get; set; }
        public int? Quntity { get; set; }
        public bool IsRecipeNeedToCook { get; set; }
        public string CookingInstructions { get; set; }
        public string RecipeNumber { get; set; }
        public bool IsDelete { get; set; }
        public int FoodItemId { get; set; }
        public int? Quantity { get; set; }
        public string QuantityUnit { get; set; }
        

    }
}
