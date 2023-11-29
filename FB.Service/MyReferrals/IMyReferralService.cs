using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IMyReferralService : IDisposable
    {
        List<ReferrerType> GetReferrerType();
        Referrers GetReferrerById(int id);
        Referrers GetReferrerByUserId(int id);
        bool Save(Referrers model);
        KeyValuePair<int, List<MyReferralsDto>> GetMyReferral(DataTableServerSide searchModel,int ReferrerID);
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferralYear(string year, int UserID);
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferralMonth(int month, int UserID);
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferralDate(DateTime sdate, DateTime edate, int UserID);
        List<MyReferralsDto> GetAllReferral();
        int CountPendingReferralByID(int id);
        KeyValuePair<int, List<FoodbankReferrerDto>> GetReferrer(DataTableServerSide searchModel,int FoodbankId, int CharityId);
        bool CheckToken(string token);
       
        void UpdateReferres(Referrers model);
        int GetMyReferralByReferrerId(int ReferrerId);
        List<Referrers> GetAllReferralForVoucher(int foodbankId);
        KeyValuePair<int, List<MyReferralsDto>> GetMyReferralByFoodbank(DataTableServerSide searchModel, int FoodbankId, int CharityId, int BranchId);
        List<Referrers> GetReferrasByFoodBankId(int foodBankId);
        List<ReferrerFamily> GetReferraFamilyDetails(int referraId);
    }
}
