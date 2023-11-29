using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FB.Data.Models;
using FB.Dto;
using FB.Core;
using FB.Dto.Foodbank;

namespace FB.Service
{
    public interface IFoodbankService : IDisposable
    {
        Foodbank GetFoodbankById(int id);
        Foodbank GetFoodbankByUserId(int id);
        Foodbank GetFoodbankByCenterOfficerId(int CenterOfficerId);
        bool SaveDonorFoodbank(DonorFoodbank personfoodbank);
        KeyValuePair<int, List<FeedbackDonorDto>> GetPersonsByFoodbank(DataTableServerSide searchModel, int FoodbankId, int charitID, int BranchID);
        bool Save(Foodbank foodbank);
        public List<DonorFoodbank> GetDonorList(int foodbankid,int charityID, int Branchid);
        
        //Food Donation Section
        FoodItem GetFoodDonationById(int id);
        public bool SaveFoodDonation(FoodItem foodItem);
        void DeleteFoodDonation(int id);
        //End

        Foodbank GetFoodbankByToken(string token);
        int GetFoodParcelsCount(int foodbankId, DateTime startofweek, DateTime lastOfWeek);
        public int GetParcelsDeliveredCount(int foodbankId);
        KeyValuePair<int, List<DashboardDto>> GetMyParcelMonth(int year, int FoodbankId);
        List<int> GetFamilyMemberMonth(int month, int FoodbankId);
        public bool SaveFoodbankSetting(FoodbankSetting foodbankSetting);
        FoodbankSetting GetFoodbankSettingByFoodbankID(int FoodbankId);
        FoodbankSetting GetFoodbankSetting(int id);
    }
}
