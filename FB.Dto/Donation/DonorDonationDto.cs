using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class DonorDonationDto
    {
        public int DonorId { get; set; }
        public string Refrence { get; set; }
        public string DonationTypeName { get; set; }
        public int DonationType { get; set; }
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public string Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public DateTime DonationDate { get; set; }
        public String CauseofDonation { get; set; }
        public int Status { get; set; }
        public string DonorName { get; set; }
        public string FoodCategoryId { get; set; }
        public string ProductApiId { get; set; }
        public string hdnFoodCategoryId { get; set; }
        public string hdnFoodProductId { get; set; }
    }
    public class DonorDonationPaymentDto
    {
        public int DonorId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PaymentGateway { get; set; }
        public string CustomerName { get; set; }
        public string Amount { get; set; }
        public bool IsGADecleared { get; set; }
    }

    public class PledgeList
    {
        public int FoodItemCount { get; set; }
    }
}
