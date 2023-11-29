using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ParcelFoodItem
    {
        public int Id { get; set; }
        public int ParcelTypeId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }

        public virtual Food Food { get; set; }
        public virtual ParcelType ParcelType { get; set; }
    }
}
