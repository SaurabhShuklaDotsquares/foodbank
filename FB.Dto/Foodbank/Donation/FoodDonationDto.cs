using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class FoodDonationDto
    {
        public int FoodBankId { get; set; }
        public int FoodItemId { get; set; }
        public int DonorId { get; set; }
        public int GrantorId { get; set; }
        public string DonationDate { get; set; }
        public string FoodItemName { get; set; }
        public int FoodIId { get; set; }
        public int MyProperty { get; set; }
        public int Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public int Status { get; set; }
        public string CauseOfDonation { get; set; }
        public string FoodCategoryId { get; set; }
        public string ProductApiId { get; set; }
        public string hdnFoodCategoryId { get; set; }
        public string hdnFoodProductId { get; set; }
    }

    public class StockFoodDonationDto
    {
        public int FoodItemId { get; set; }
        public int StockId { get; set; }
        public int? DonorId { get; set; }
        public int? GrantorId { get; set; }
        public string DonationDate { get; set; }
        public string FoodItemName { get; set; }
        public string Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public string CauseOfDonation { get; set; }
        public string hdnFoodCategoryId { get; set; }
        public string hdnFoodProductId { get; set; }
    }
}
