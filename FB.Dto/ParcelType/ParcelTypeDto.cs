using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ParcelTypeDto
    {
        public string Title { get; set; }
        public int FoodBankId { get; set; }
        public int ParcelTypeId { get; set; }
        public int FoodCategoryId { get; set; }
        public int FoodItemId { get; set; }
        public DateTime AddedDate { get; set; }

        public string Quantity { get; set; }
        public string QuantityUnit { get; set; }
    }

    public class ParcelFoodItemDto
    {
        public int ParcelTypeId { get; set; }
        public int FoodItemId { get; set; }
    }
}
