using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.ModalMapper
{
    public static class DonorDonationDtoMapper
    {
        public static List<DonorDonationDto> FBMap(List<FoodItem> foodItems)
        {
            var foodDonationList = new List<DonorDonationDto>();
            foreach (var foodItem in foodItems)
            {
                foodDonationList.Add(new DonorDonationDto
                {
                    FoodItemId = foodItem.Id,
                    FoodItemName = foodItem.Food.Name,
                    Quantity = Convert.ToString(foodItem.Quntity),
                    QuantityUnit = Convert.ToString(foodItem.QuantityUnit),
                    DonationDate = (foodItem.ExpiryDate.Value),
                    CauseofDonation = foodItem.CauseofDonation,
                    Status = foodItem.Status.Value,
                    DonorName = $"{foodItem.Donor.Forenames} {foodItem.Donor.Surname}"
                });
            }

            return foodDonationList;
        }
        public static List<DonorDonationPaymentDto> PaymentMap(List<Donation> paymentItems)
        {
            var paymentDonationList = new List<DonorDonationPaymentDto>();
            foreach (var paymentItem in paymentItems)
            {
                if (paymentItem.DonorId != null)
                {
                    paymentDonationList.Add(new DonorDonationPaymentDto
                    {
                        CreatedDate = paymentItem.DonationDate,
                        CustomerName = $"{paymentItem.Donor.Forenames} {paymentItem.Donor.Surname}",
                        PaymentGateway = paymentItem.Method != null ? (paymentItem.Method.Cash == true ? "Cash" : "Card") : "Card",
                        Amount = Convert.ToString(paymentItem.Amount),
                        IsGADecleared = paymentItem.Gasdseligible != null ? paymentItem.Gasdseligible.Value : false
                    });
                }
            }
            return paymentDonationList;
        }

        public static List<StockManageGrantorDto> MapGrantorStockList(List<FoodItem> foodItems)
        {
            var availabilityList = new List<StockManageGrantorDto>();
            foreach (var item in foodItems)
            {
                availabilityList.Add(new StockManageGrantorDto
                {
                    Id = item.Id,
                    FoodbankId = item.FoodbankId.Value,
                    FoodId = item.Foodid.Value,
                    AddedDate = item.AddedDate,
                    Quantity = item.Quntity ?? 0,
                    TotalPrice = item.Stock.PricePerItem * item.Quntity,
                    IsGrantorDonation = item.GrantorId != null ? true : false,
                    IsDonorDonation = item.Donorid != null ? true : false,
                    GrantorId = item.GrantorId ?? null,
                    DonorId = item.Donorid ?? null,
                    DonorName = item.Donor != null ? $"{item.Donor.Forenames} {item.Donor.Surname}" : string.Empty,
                    GrantorName = item.Grantor != null ? $"{item.Grantor.ForeName} {item.Grantor.SurName}" : string.Empty,
                });
            }
            return availabilityList;
        }
    }
}
