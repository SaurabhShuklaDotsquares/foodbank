using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FB.Data.Models;
using FB.Dto;
using FB.Core;

namespace FB.Service
{
    public interface IFeedbackService : IDisposable
    {
        #region Feedback Master
        public KeyValuePair<int, List<FeedbackMasterDTO>> GetUserDefinedFields(DataTableServerSide searchModel, List<UserDataAccessDto> userDataAccess , int roleID, int? userID = null, int? organisationID = null, int? charityID = null, int? branchID = null);
        FeedbackMaster GetUserDefinedField(int id);
        void Delete(int id);
        void UserDefinedFDelete(int id);
        public List<FeedbackMasterFieldOption> GetUserDefinedFieldOption(int id);
        void Save(FeedbackMaster userDefinedField, bool isNew = true);
        bool IsExist(int userDefinedFieldID, string description, int fieldType, int? orgId = null, int? charityId = null, int? branchId = null);
        #endregion
        #region Feedback Form 
        void SaveFeedbackForm(Feedback feedback, bool isNew = true);
        FeedbackDto GetFamilyandParcelDetails(int ParcelId, int FamilyID);
        List<FeedbackMasterDTO> GetQuestionFormByFoodbank(int FoodBankId);
        KeyValuePair<int, List<FeedbackDto>> GetFeedbackListByFoodbank(DataTableServerSide searchModel, int FoodbankId, int Familyid);
        List<FeedbackFormDetailsFormDto> GetFeedbackDetailsByFeedbackID(int FeedbackID);
        FeedbackDto GetParcelDetailsByToken(string TokenNumber);
        #endregion

        void DeleteFeedbackFormDetails(int id);
    }
}
