using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Parcels
    {
        public Parcels()
        {
            FamilyParcelFoodItem = new HashSet<FamilyParcelFoodItem>();
            Feedback = new HashSet<Feedback>();
            StockHistory = new HashSet<StockHistory>();
        }

        public int Id { get; set; }
        public int? LocationId { get; set; }
        public int? ParcelTypeId { get; set; }
        public int? RegularId { get; set; }
        public int? PackerId { get; set; }
        public int? DelivererId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? PackOnDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public int? Status { get; set; }
        public int? ReferrerId { get; set; }
        public int? LimitOverrideBy { get; set; }
        public DateTime? PackedDate { get; set; }
        public int? FamilyId { get; set; }
        public int? FoodbankId { get; set; }
        public int? RecipeId { get; set; }
        public int? StandardParcelTypeId { get; set; }
        public string SpecialNote { get; set; }
        public int? GranterId { get; set; }
        public int? VoucherId { get; set; }
        public string ParcelToken { get; set; }
        public byte[] ParcelQrcode { get; set; }
        public byte[] ParcelFeedbackQrcode { get; set; }

        public virtual Volunteer Deliverer { get; set; }
        public virtual Family Family { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual Grantor Granter { get; set; }
        public virtual Fblocation Location { get; set; }
        public virtual Volunteer Packer { get; set; }
        public virtual FoodbankRecipe Recipe { get; set; }
        public virtual ParcelType StandardParcelType { get; set; }
        public virtual Voucher Voucher { get; set; }
        public virtual ICollection<FamilyParcelFoodItem> FamilyParcelFoodItem { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<StockHistory> StockHistory { get; set; }
    }
}
