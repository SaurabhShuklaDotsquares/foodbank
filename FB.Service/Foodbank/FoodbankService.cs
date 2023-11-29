using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto.Foodbank;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FB.Service
{
    public class FoodbankService : IFoodbankService
    {
        private IRepository<Foodbank> repoFoodbank;
        private IRepository<DonorFoodbank> repoDonorFoodbank;
        private IRepository<FoodItem> repoFoodItem;
        private IRepository<Parcels> repoParcels;
        private IRepository<FoodbankSetting> repoFoodbankSetting;
        private IRepository<FoodbankRecipe> repoFoodbankRecipe;
        private IRepository<User> repoUser;
        public FoodbankService(IRepository<Foodbank> _repoFoodbank, IRepository<DonorFoodbank> _repoDonorFoodbank, IRepository<FoodItem> _repoFoodItem,
            IRepository<Parcels> _repoParcels, IRepository<FoodbankSetting> _repoFoodbankSetting, IRepository<FoodbankRecipe> _repoFoodbankRecipe, IRepository<User> _repoUser)
        {
            repoFoodbank = _repoFoodbank;
            repoDonorFoodbank = _repoDonorFoodbank;
            repoFoodItem = _repoFoodItem;
            repoParcels = _repoParcels;
            repoFoodbankSetting = _repoFoodbankSetting;
            repoFoodbankRecipe = _repoFoodbankRecipe;
            repoUser = _repoUser;
        }

        public Foodbank GetFoodbankById(int id)
        {
            return repoFoodbank.Query().Filter(x => x.Id == id).Include(x => x.User).Include(x => x.Address)
                .Get().FirstOrDefault();
        }
        public Foodbank GetFoodbankByUserId(int id)
        {
            return repoFoodbank.Query().Filter(x => x.UserId == id).Include(x => x.User).Include(x => x.Address)
                .Get().FirstOrDefault();
        }
        public Foodbank GetFoodbankByCenterOfficerId(int CenterOfficerId)
        {
            return repoFoodbank.Query().Include(x => x.User).Include(x => x.Address).Filter(x => x.User.CentralOfficeId == CenterOfficerId)
                .Get().FirstOrDefault();
        }
        public List<DonorFoodbank> GetDonorList(int foodbankid, int charityID, int Branchid)
        {
            var predicate = PredicateBuilder.True<DonorFoodbank>();
            predicate = predicate.And(p => p.Donor.Active == true &&  p.FoodBankId == foodbankid);//&&
            if (charityID > 0)
            {
                predicate = predicate.And(p => p.Donor.CharityId == charityID);
            }
            if (Branchid > 0)
            {
                predicate = predicate.And(p => p.Donor.BranchId == Branchid);
            }
            return repoDonorFoodbank.Query().Include(x => x.Donor).Filter(predicate).Get().ToList();

        }
        public bool SaveDonorFoodbank(DonorFoodbank personfoodbank)
        {
            try
            {
                repoDonorFoodbank.Insert(personfoodbank);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public KeyValuePair<int, List<FeedbackDonorDto>> GetPersonsByFoodbank(DataTableServerSide searchModel, int FoodbankId, int charitID, int BranchID)
        {

            var predicate = PredicateBuilder.True<DonorFoodbank>();
            predicate = CustomPredicate.BuildPredicate<DonorFoodbank>(searchModel, new Type[] { typeof(Person), typeof(Branch), typeof(CentralOffice), typeof(User) });
            predicate = predicate.And(p => p.Donor.Active == true && p.FoodBank.User.Active == true && p.FoodBankId == FoodbankId);//&&
            if (charitID > 0)
            {
                predicate = predicate.And(p => p.Donor.CharityId == charitID);
            }
            if (BranchID > 0)
            {
                predicate = predicate.And(p => p.Donor.BranchId == BranchID);
            }
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var centralOfficeList = repoDonorFoodbank
                .Query().Include(x => x.Donor).Include(x => x.Donor.User).Include(x => x.Donor.CentralOffice).Include(x => x.Donor.Charity).Include(x => x.Donor.Branch)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                //.CustomOrderBy(u => u.OrderBy(searchModel))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Person), typeof(Branch), typeof(CentralOffice), typeof(User) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();


            List<FeedbackDonorDto> returnList = new List<FeedbackDonorDto>();

            foreach (var donorlist in centralOfficeList)
            {
                // var org = repoUser.Query().Include(x=>x.CentralOfficeNavigation).Include(x=>x.Charity).Include(x=>x.BranchNavigation).Filter(x=>x.UserId==donorlist.Donor.User.FirstOrDefault().UserId).Get().FirstOrDefault();

                FeedbackDonorDto fmt = new FeedbackDonorDto();
                fmt.PersonID = donorlist.DonorId;
                fmt.FoodbankId = donorlist.FoodBankId;
                fmt.Forenames = donorlist.Donor.Forenames;
                fmt.Surname = donorlist.Donor.Surname;
                fmt.Email = donorlist.Donor.Email;
                fmt.Suffix = donorlist.Donor.Suffix;
                fmt.Reference = donorlist.Donor.Reference;
                fmt.DonorReference = donorlist.Donor.DonorReference;
                fmt.CentralOfficeName = donorlist.Donor.CentralOffice.OrganisationName;
                fmt.CharityName = donorlist.Donor.Charity.CharityName;
                fmt.Branch = donorlist.Donor.Branch.BranchDescription; ;
                fmt.Title = donorlist.Donor.Title;
                returnList.Add(fmt);
            }

            KeyValuePair<int, List<FeedbackDonorDto>> resultResponse = new KeyValuePair<int, List<FeedbackDonorDto>>(totalCount, returnList);

            return resultResponse;
        }
        public bool Save(Foodbank foodbank)
        {
            try
            {
                if (foodbank.Id > 0)
                {
                    repoFoodbank.Update(foodbank);
                }
                else
                {
                    repoFoodbank.Insert(foodbank);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //Food Donation Section
        public FoodItem GetFoodDonationById(int id)
        {

            return repoFoodItem.Query().Filter(x => x.Id == id).Include(x => x.Food).Get().FirstOrDefault();
        }

        public bool SaveFoodDonation(FoodItem foodItem)
        {
            try
            {
                if (foodItem.Id > 0)
                {
                    repoFoodItem.Update(foodItem);
                    return true;
                }
                else
                {
                    repoFoodItem.Insert(foodItem);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DeleteFoodDonation(int id)
        {
            repoFoodItem.Delete(id);
        }
        //End


        public Foodbank GetFoodbankByToken(string token)
        {
            return repoFoodbank.Query().Filter(x => x.SelfReferralToken == token).Include(x => x.User).Include(x => x.User.Branch).Include(x => x.User.UserRole).Get().FirstOrDefault();
        }

        public int GetFoodParcelsCount(int foodbankId, DateTime startofweek, DateTime lastOfWeek)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = predicate.And(x => x.FoodbankId == foodbankId);
            predicate = predicate.And(x => x.DeliveryDate.Value.Date >= startofweek.Date && x.DeliveryDate.Value.Date <= lastOfWeek);
            return repoParcels.Query().Filter(predicate).Get().Count();
        }

        public int GetParcelsDeliveredCount(int foodbankId)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = predicate.And(x => x.FoodbankId == foodbankId && x.DelivererId > 0 && x.Status == (int)ParcelStatus.Delivered);

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            predicate = predicate.And(x => x.DeliveredDate.Value.Date >= startDate.Date && x.DeliveryDate.Value.Date <= endDate);

            return repoParcels.Query().Filter(predicate).Get().Count();
        }
        public KeyValuePair<int, List<DashboardDto>> GetMyParcelMonth(int year, int FoodbankId)
        {

            var centralOfficeList = repoParcels
              .Query()
              .Filter(x => x.DeliveredDate != null && x.DeliveredDate.Value.Year == year && x.FoodbankId == FoodbankId).Get().ToList();

            var results = FoodbankDashboard.MyParcelYearMap(centralOfficeList);

            KeyValuePair<int, List<DashboardDto>> resultResponse = new KeyValuePair<int, List<DashboardDto>>(1, results);

            return resultResponse;
        }

        public List<int> GetFamilyMemberMonth(int month, int FoodbankId)
        {

            var parcelList = repoParcels
              .Query().Include(x=>x.Family).Include(x => x.Family.FamilyMember)
              .Filter(x => x.DeliveredDate != null && x.DeliveredDate.Value.Month == month && x.FoodbankId == FoodbankId).Get().ToList();

            var results = FoodbankDashboard.MyParcelMonthMap(parcelList);
            return results;
        }

        public FoodbankSetting GetFoodbankSettingByFoodbankID(int FoodbankId)
        {

            var centralOfficeList = repoFoodbankSetting
              .Query()
              .Filter(x => x.FoodBankId == FoodbankId).Get().FirstOrDefault();
            return centralOfficeList;
        }


        public bool SaveFoodbankSetting(FoodbankSetting foodbankSetting)
        {
            try
            {
                if (foodbankSetting.Id > 0)
                {
                    repoFoodbankSetting.Update(foodbankSetting);
                    repoFoodbankSetting.SaveChanges();
                    return true;
                }
                else
                {
                    repoFoodbankSetting.Insert(foodbankSetting);
                    repoFoodbankSetting.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public FoodbankSetting GetFoodbankSetting(int id)
        {
            return repoFoodbankSetting.Query().Filter(x => x.FoodBankId == id).Get().FirstOrDefault();
        }
        public void Dispose()
        {
            if (repoFoodbank != null)
            {
                repoFoodbank.Dispose();
                repoFoodbank = null;
            }
            if (repoDonorFoodbank != null)
            {
                repoDonorFoodbank.Dispose();
                repoDonorFoodbank = null;
            }

        }
    }
}
