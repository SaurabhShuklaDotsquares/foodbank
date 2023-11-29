using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Stock
    {
        public Stock()
        {
            FoodItem = new HashSet<FoodItem>();
            StockHistory = new HashSet<StockHistory>();
        }

        public int Id { get; set; }
        public int FoodbankId { get; set; }
        public int FoodId { get; set; }
        public int? TotalQuantity { get; set; }
        public int? IsItemLowInStock { get; set; }
        public string AboutServicerOffered { get; set; }
        public decimal? PricePerItem { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool? IsGrantorMoney { get; set; }
        public int? GrantorId { get; set; }
        public bool? Active { get; set; }
        public string Unit { get; set; }

        public virtual Food Food { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual Grantor Grantor { get; set; }
        public virtual ICollection<FoodItem> FoodItem { get; set; }
        public virtual ICollection<StockHistory> StockHistory { get; set; }
    }
}
