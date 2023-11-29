using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FB.Data.Models;
using FB.Dto;
using FB.Core;

namespace FB.Service
{
    public interface IFamilyService : IDisposable
    {
        Family  Save(Family family);
        FamilyMember Savesubfamily(FamilyMember family);
        ReferrerFamily SaveFamilyreferral(ReferrerFamily family);
        FamilyAddress SaveFamilyAddress(FamilyAddress family);
        void SaveFamilyreferralFoodbank(FoodbankFamily Foodbankfamily);
        Family GetFamilyDetails(int familyid);
        Family GetFamilyMoreDetails(int familyid);
        Fbaddress GetFamilyAddessDetails(int familyaddressid);
        List<FamilyMemberAllergy> GetFamilyMemberAllergyDetails(int familyid);
        List<ReferrerFamily> GetReferrerFamily(int referrerId);
        Family UpdateFamily(Family family);
        Fbaddress SaveFbAddress(Fbaddress family);
        FamilyMember GetFamilyMember(int familymemberid);
        FamilyMemberAllergy GetFamilyMemberAllergy(int familyalleryid);
        void DeleteFamilyMemberAllergy(int familyalleryid);
        void DeleteFamilyMember(int familymemberid);
        KeyValuePair<int, List<MyReferralsDto>> GetMyFamilyByFoodbank(DataTableServerSide searchModel, List<UserDataAccessDto> userDataAccess, int FoodbankId, int roleID, int? organisationId = null, int? charityId = null, int? branchId = null, int? userId = null, bool isUserPrefrence = true);
        int CountDailyReferrelLimitByFoodbankId(int foodbankid);
        List<Family> GetAllFamily(int foodbankId);
        List<ReportReferrerDto> GetFamilyforRefferreport(ReferrerReportDto model);
        List<Family> GetFamilysForFamilyReport(FamilyReportDto model);
        List<ReportFamilyDto> GetFamilysDetailsforFamilyReport(FamilyReportDto model);

        KeyValuePair<int, List<FamilyAgency>> GetFeedbackListByFoodbank(DataTableServerSide searchModel, int FoodbankId, int agenciesID);
    }
}
