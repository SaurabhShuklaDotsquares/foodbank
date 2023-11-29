using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FB.Data.Models;
using FB.Dto;
using FB.Core;

namespace FB.Service
{
    public interface IDonorService : IDisposable
    {
        List<FoodItem> GetAllList(int donorid);
        int GetFoodDonationCount(int personId);
        int GetCardDonationCount(int personId);
        KeyValuePair<int, List<DonorDonationDto>> GetFoodDonations(DataTableServerSide searchModel, int personId);
        KeyValuePair<int, List<DonorDonationDto>> GetFoodDonationsByFoodBankId(DataTableServerSide searchModel, int foodBankId, int charitID, int BranchID);
        KeyValuePair<int, List<DonorDonationPaymentDto>> GetFoodDonationPaymentByFoodbankId(DataTableServerSide searchModel, int foodbankId, int charitID, int BranchID);


        int GetCashDonationCount(int personId);
        KeyValuePair<int, List<DonorDonationPaymentDto>> GetFoodDonationPayment(DataTableServerSide searchModel, int personId);
    }
}
