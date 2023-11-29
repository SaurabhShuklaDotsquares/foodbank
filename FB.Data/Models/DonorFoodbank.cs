using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DonorFoodbank
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int FoodBankId { get; set; }

        public virtual Person Donor { get; set; }
        public virtual Foodbank FoodBank { get; set; }
    }
}
