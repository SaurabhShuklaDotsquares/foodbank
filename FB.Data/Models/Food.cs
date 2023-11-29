using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Food
    {
        public Food()
        {
            FamilyParcelFoodItem = new HashSet<FamilyParcelFoodItem>();
            FoodItem = new HashSet<FoodItem>();
            FoodbankRecipeFoodItem = new HashSet<FoodbankRecipeFoodItem>();
            ParcelFoodItem = new HashSet<ParcelFoodItem>();
            Stock = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Image { get; set; }
        public string Barcode { get; set; }
        public string ProductIdApi { get; set; }
        public string CategoryApiId { get; set; }

        public virtual ICollection<FamilyParcelFoodItem> FamilyParcelFoodItem { get; set; }
        public virtual ICollection<FoodItem> FoodItem { get; set; }
        public virtual ICollection<FoodbankRecipeFoodItem> FoodbankRecipeFoodItem { get; set; }
        public virtual ICollection<ParcelFoodItem> ParcelFoodItem { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
