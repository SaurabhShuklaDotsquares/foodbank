using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FB.Service
{
    public class FeedbackService : IFeedbackService
    {

        private IRepository<FeedbackMaster> repoFeedbackMaster;
        private IRepository<FeedbackMasterFieldOption> repoFeedbackMasterFieldOption;
        private IRepository<Feedback> repoFeedback;
        private IRepository<Parcels> repoParcels;
        private IRepository<Foodbank> repoFoodbank;
        private IRepository<Family> repoFamily;
        private IRepository<FeedbackFormDetails> repoFeedbackFormDetails;
        public FeedbackService(IRepository<FeedbackMaster> _repoFeedbackMaster, IRepository<FeedbackMasterFieldOption> _repoFeedbackMasterFieldOption, IRepository<Feedback> _repoFeedback, IRepository<Parcels> _repoParcels, IRepository<Foodbank> _repoFoodbank, IRepository<Family> _repoFamily, IRepository<FeedbackFormDetails> _repoFeedbackFormDetails)
        {

            this.repoFeedback = _repoFeedback;
            this.repoFeedbackMaster = _repoFeedbackMaster;
            this.repoFeedbackMasterFieldOption = _repoFeedbackMasterFieldOption;
            this.repoParcels = _repoParcels;
            this.repoFoodbank = _repoFoodbank;
            this.repoFamily = _repoFamily;
            this.repoFeedbackFormDetails = _repoFeedbackFormDetails;
        }
        #region Feedback Master
        public KeyValuePair<int, List<FeedbackMasterDTO>> GetUserDefinedFields(DataTableServerSide searchModel, List<UserDataAccessDto> userDataAccess, int FoodbankId, int? userID = null, int? organisationID = null, int? charityID = null, int? branchID = null)
        {
           var predicate = PredicateBuilder.True<FeedbackMaster>();
            predicate = CustomPredicate.BuildPredicate<FeedbackMaster>(searchModel, new Type[] { typeof(FeedbackMaster) });
            predicate = predicate.And(m => m.FoodbankId == FoodbankId);


            int totalCount = 0;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            var res = repoFeedbackMaster
                .Query()
                .Include(u => u.FeedbackMasterFieldOption)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.FieldId))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(FeedbackMaster) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();


            var results = FeedbackDtoMapper.Map(res);
            return new KeyValuePair<int, List<FeedbackMasterDTO>>(totalCount > 0 ? totalCount : results.Count, results);

        }
        public FeedbackMaster GetUserDefinedField(int id)
        {
            return repoFeedbackMaster.Query().Filter(u => u.FieldId == id).Include(x => x.FeedbackMasterFieldOption).Get().FirstOrDefault();
        }
        public void Delete(int id)
        {
            repoFeedbackMaster.Delete(id);
        }
        public void UserDefinedFDelete(int id)
        {
            repoFeedbackMasterFieldOption.Delete(id);
        }
        public List<FeedbackMasterFieldOption> GetUserDefinedFieldOption(int id)
        {
            return repoFeedbackMasterFieldOption.Query().Filter(u => u.UserDefinedFieldId == id).Get().ToList();

        }

        public bool IsExist(int userDefinedFieldID, string description, int fieldType, int? orgId = null, int? charityId = null, int? branchId = null)
        {
            if (!orgId.HasValue && !charityId.HasValue && !branchId.HasValue)
            {
                return repoFeedbackMaster.Query().Filter(m => m.FieldId != userDefinedFieldID && m.FieldDescription.Trim() == description.Trim() && m.FieldType == fieldType).Get().Count() > 0;
            }
            else
            {
                return repoFeedbackMaster.Query().Filter(m => m.FieldId != userDefinedFieldID && m.FieldDescription.Trim() == description.Trim() && m.FieldType == fieldType).Get().Count() > 0;
            }
        }

        public void Save(FeedbackMaster userDefinedField, bool isNew = true)
        {
            if (isNew)
            {
                repoFeedbackMaster.Insert(userDefinedField);
            }
            else
            {
                repoFeedbackMaster.Update(userDefinedField);
            }
        }
        #endregion
        #region Feedback Form
        public void SaveFeedbackForm(Feedback feedback, bool isNew = true)
        {
            if (isNew)
            {
                repoFeedback.Insert(feedback);
            }
            else
            {
                repoFeedback.Update(feedback);
            }
        }
        public FeedbackDto GetFamilyandParcelDetails(int ParcelId, int FamilyID)
        {
            var res = repoParcels.Query().Include(x => x.StandardParcelType).Include(x => x.Family).Include(x=>x.Deliverer.User).Include(x => x.Packer.User).Filter(x => x.Id == ParcelId && x.Family.Id == FamilyID).Get().FirstOrDefault();
            return FeedbackDtoMapper.FeedbackManage(res);
        }
        public FeedbackDto GetParcelDetailsByToken(string TokenNumber)
        {
            var res = repoParcels.Query().Include(x => x.StandardParcelType).Include(x => x.Family).Include(x => x.Deliverer.Contact).Include(x => x.Packer.Contact).Include(x => x.Foodbank).Filter(x => x.ParcelToken == TokenNumber).Get().FirstOrDefault();
            return FeedbackDtoMapper.FeedbackManage(res);
        }
        public List<FeedbackMasterDTO> GetQuestionFormByFoodbank(int FoodBankId)
        {
            var res = repoFeedbackMaster.Query().Include(x => x.FeedbackFormDetails).Include(x => x.FeedbackMasterFieldOption).Filter(x => x.FoodbankId == FoodBankId).Get().ToList();
            return FeedbackDtoMapper.MapList(res);
        }
        public KeyValuePair<int, List<FeedbackDto>> GetFeedbackListByFoodbank(DataTableServerSide searchModel, int FoodbankId,int Familyid)
        {
            var predicate = PredicateBuilder.True<Feedback>();
            predicate = CustomPredicate.BuildPredicate<Feedback>(searchModel, new Type[] { typeof(Feedback), typeof(FeedbackMaster), typeof(FeedbackFormDetails), typeof(ParcelType) });
            predicate = predicate.And(x => x.FoodbankId == FoodbankId);
            if (Familyid > 0)
            {
                predicate = predicate.And(x => x.FamilyId == Familyid);
            }
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);


            var repoFeedbacklist = repoFeedback.Query().Include(x => x.Parcel).Include(x => x.Family).Include(x => x.FeedbackFormDetails).Include(x => x.Parcel.StandardParcelType)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).OrderByDescending(x => x.Id).ToList();

            var results = FeedbackDtoMapper.MapListFeedbackToFeedbackDto(repoFeedbacklist);
            KeyValuePair<int, List<FeedbackDto>> resultResponse = new KeyValuePair<int, List<FeedbackDto>>(totalCount, results);
            return resultResponse;
        }
        public List<FeedbackFormDetailsFormDto> GetFeedbackDetailsByFeedbackID(int FeedbackID)
        {
            var res = repoFeedbackFormDetails.Query().Include(x => x.FeedbackMaster).Include(x => x.Feedback).Include(x => x.Feedback.Parcel).Include(x => x.Feedback.Parcel.StandardParcelType).Include(x => x.Feedback.Family).Include(x => x.FeedbackMasterFieldOption).Filter(x => x.FeedbackId == FeedbackID).Get().ToList();
            return FeedbackDtoMapper.MapFeedbackFormDetailsFormDto(res);
        }

        public void DeleteFeedbackFormDetails(int id)
        {
            repoFeedbackFormDetails.Delete(id);
        }
        #endregion
        public void Dispose()
        {

            if (repoFeedbackMaster != null)
            {
                repoFeedbackMaster.Dispose();
                repoFeedbackMaster = null;
            }
            if (repoFeedbackMasterFieldOption != null)
            {
                repoFeedbackMasterFieldOption.Dispose();
                repoFeedbackMasterFieldOption = null;
            }
            if (repoFeedback != null)
            {
                repoFeedback.Dispose();
                repoFeedback = null;
            }
            if (repoParcels != null)
            {
                repoParcels.Dispose();
                repoParcels = null;
            }
            if (repoFoodbank != null)
            {
                repoFoodbank.Dispose();
                repoFoodbank = null;

            }
            if (repoFeedbackFormDetails != null)
            {
                repoFeedbackFormDetails.Dispose();
                repoFeedbackFormDetails = null;
            }
        }



    }
}
