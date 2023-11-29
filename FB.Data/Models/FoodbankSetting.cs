using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodbankSetting
    {
        public int Id { get; set; }
        public int FoodBankId { get; set; }
        public string LogoImage { get; set; }
        public string DashboardImage { get; set; }
        public string TailoredNotes { get; set; }
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
        public string PhoneNumber { get; set; }
        public int? DailyReferralLimit { get; set; }
        public int? ParcelLimit { get; set; }

        public virtual Foodbank FoodBank { get; set; }
    }
}
