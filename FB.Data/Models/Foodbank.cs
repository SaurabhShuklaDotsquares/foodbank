using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Foodbank
    {
        public Foodbank()
        {
            Agencies = new HashSet<Agencies>();
            DonorFoodbank = new HashSet<DonorFoodbank>();
            Feedback = new HashSet<Feedback>();
            FeedbackMaster = new HashSet<FeedbackMaster>();
            FoodItem = new HashSet<FoodItem>();
            FoodbankFamily = new HashSet<FoodbankFamily>();
            FoodbankRecipe = new HashSet<FoodbankRecipe>();
            FoodbankSetting = new HashSet<FoodbankSetting>();
            Grantor = new HashSet<Grantor>();
            ParcelType = new HashSet<ParcelType>();
            Parcels = new HashSet<Parcels>();
            Profession = new HashSet<Profession>();
            Referrers = new HashSet<Referrers>();
            Role = new HashSet<Role>();
            Stock = new HashSet<Stock>();
            UserNavigation = new HashSet<User>();
            Volunteer = new HashSet<Volunteer>();
            Voucher = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public string SelfReferralToken { get; set; }
        public int? ReferralSuccessResponse { get; set; }
        public int? ReferralRejectionResponse { get; set; }
        public int? ParcelLimitReachedResponse { get; set; }
        public string FeedbackResponse { get; set; }
        public string FeedbackReturnUrl { get; set; }
        public int? InsertBy { get; set; }
        public int? UserId { get; set; }

        public virtual Fbaddress Address { get; set; }
        public virtual User InsertByNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Agencies> Agencies { get; set; }
        public virtual ICollection<DonorFoodbank> DonorFoodbank { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<FeedbackMaster> FeedbackMaster { get; set; }
        public virtual ICollection<FoodItem> FoodItem { get; set; }
        public virtual ICollection<FoodbankFamily> FoodbankFamily { get; set; }
        public virtual ICollection<FoodbankRecipe> FoodbankRecipe { get; set; }
        public virtual ICollection<FoodbankSetting> FoodbankSetting { get; set; }
        public virtual ICollection<Grantor> Grantor { get; set; }
        public virtual ICollection<ParcelType> ParcelType { get; set; }
        public virtual ICollection<Parcels> Parcels { get; set; }
        public virtual ICollection<Profession> Profession { get; set; }
        public virtual ICollection<Referrers> Referrers { get; set; }
        public virtual ICollection<Role> Role { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
        public virtual ICollection<User> UserNavigation { get; set; }
        public virtual ICollection<Volunteer> Volunteer { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }
}
