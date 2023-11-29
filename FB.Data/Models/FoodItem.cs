using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodItem
    {
        public FoodItem()
        {
            FoodItemAllergy = new HashSet<FoodItemAllergy>();
        }

        public int Id { get; set; }
        public int? Foodid { get; set; }
        public int? Donorid { get; set; }
        public int? Quntity { get; set; }
        public int? LocationId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? Status { get; set; }
        public int? Batchid { get; set; }
        public bool? OwnPurchase { get; set; }
        public string CauseofDonation { get; set; }
        public string QuantityUnit { get; set; }
        public int? GrantorId { get; set; }
        public int? StockId { get; set; }
        public int? FoodbankId { get; set; }

        public virtual Person Donor { get; set; }
        public virtual Food Food { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual Grantor Grantor { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual ICollection<FoodItemAllergy> FoodItemAllergy { get; set; }
    }
}
