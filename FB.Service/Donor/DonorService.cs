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
    public class DonorService : IDonorService
    {
        private IRepository<FoodItem> repoFoodItem;
        private IRepository<PaymentImport> repoPaymentImport;
        private IRepository<Donation> repoDonation;
        public DonorService(IRepository<FoodItem> _repoFoodItem, IRepository<PaymentImport> _repoPaymentImport, IRepository<Donation> _repoDonation)
        {
            repoFoodItem = _repoFoodItem;
            repoPaymentImport = _repoPaymentImport;
            repoDonation = _repoDonation;
        }
        //private IRepository<Mmomenu> repoMenu;

        /// <summary>
        /// To check current menu url for role
        /// </summary>
        /// <param name="menuUrl"></param>
        /// <param name="roleID"></param>
        /// <returns>bool</returns>


        /// <summary>
        /// To get menu url for role
        /// </summary>     
        /// <param name="roleID"></param>
        /// <returns>List</returns>
        public List<FoodItem> GetAllList(int donorid)
        {
            return repoFoodItem
               .Query()
               .Filter(m => m.Donorid == donorid)
               .Get().ToList();
        }
        /// <summary>
        /// To get food donation count
        /// </summary>     
        /// <param name="roleID"></param>
        /// <returns>List</returns>
        public int GetFoodDonationCount(int personId)
        {
            return repoFoodItem.Query().Filter(x => x.Donorid == personId).Get().Count();
        }

        /// <summary>
        /// To get cash donation count
        /// </summary>     
        /// <param name="roleID"></param>
        /// <returns>List</returns>
        public int GetCashDonationCount(int personId)
        {
            return repoDonation.Query().Filter(x => x.DonorId == personId && x.Method.Cash == true).GetQueryable().Count();
        }

        /// <summary>
        /// To get card donation count
        /// </summary>     
        /// <param name="roleID"></param>
        /// <returns>List</returns>
        public int GetCardDonationCount(int personId)
        {
            return repoDonation.Query().Filter(x => x.DonorId == personId && x.Method.Cash == false).GetQueryable().Count();
        }
        public KeyValuePair<int, List<DonorDonationDto>> GetFoodDonations(DataTableServerSide searchModel, int personId)
        {
            var predicate = PredicateBuilder.True<FoodItem>();
            predicate = CustomPredicate.BuildPredicate<FoodItem>(searchModel, new Type[] { typeof(FoodItem), typeof(Food) });
            predicate = predicate.And(m => m.Donorid == personId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var centralOfficeList = repoFoodItem
            .Query()
            .Include(x => x.Food).Include(x => x.Donor)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(FoodItem), typeof(Food) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = DonorDonationDtoMapper.FBMap(centralOfficeList);

            KeyValuePair<int, List<DonorDonationDto>> resultResponse = new KeyValuePair<int, List<DonorDonationDto>>(totalCount, results);

            return resultResponse;

        }

        public KeyValuePair<int, List<DonorDonationDto>> GetFoodDonationsByFoodBankId(DataTableServerSide searchModel, int foodBankId, int charitID, int BranchID)
        {
            var predicate = PredicateBuilder.True<FoodItem>();
            predicate = CustomPredicate.BuildPredicate<FoodItem>(searchModel, new Type[] {  typeof(FoodItem), typeof(Food), typeof(Person), typeof(DonorFoodbank) });
            predicate = predicate.And(m => m.Donor.DonorFoodbank.Select(x => x.FoodBankId).Contains(foodBankId));
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

            var centralOfficeList = repoFoodItem
            .Query()
            .Include(x => x.Food)
            .Include(x => x.Donor)
            .Include(x => x.Donor.DonorFoodbank)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] {typeof(FoodItem), typeof(Food), typeof(Person), typeof(DonorFoodbank) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = DonorDonationDtoMapper.FBMap(centralOfficeList);

            KeyValuePair<int, List<DonorDonationDto>> resultResponse = new KeyValuePair<int, List<DonorDonationDto>>(totalCount, results);

            return resultResponse;

        }
        public KeyValuePair<int, List<DonorDonationPaymentDto>> GetFoodDonationPayment(DataTableServerSide searchModel, int personId)
        {
            var predicate = PredicateBuilder.True<Donation>();
            predicate = CustomPredicate.BuildPredicate<Donation>(searchModel, new Type[] { typeof(Donation), typeof(Method) });
            predicate = predicate.And(x => x.DonorId != null);
            predicate = predicate.And(x => x.DonorId == personId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var centralOfficeList = repoDonation
                .Query()
                .Include(x => x.Donor)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.CreatedDate))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Donation), typeof(Method) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = DonorDonationDtoMapper.PaymentMap(centralOfficeList);

            KeyValuePair<int, List<DonorDonationPaymentDto>> resultResponse = new KeyValuePair<int, List<DonorDonationPaymentDto>>(totalCount, results);

            return resultResponse;

        }

        public KeyValuePair<int, List<DonorDonationPaymentDto>> GetFoodDonationPaymentByFoodbankId(DataTableServerSide searchModel, int foodbankId, int charitID, int BranchID)
        {
            var predicate = PredicateBuilder.True<Donation>();
            predicate = CustomPredicate.BuildPredicate<Donation>(searchModel, new Type[] { typeof(Donation), typeof(Method) });
            predicate = predicate.And(x => x.DonorId != null);
            predicate = predicate.And(m => m.Donor.DonorFoodbank.Select(x => x.FoodBankId).Contains(foodbankId));
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

            var centralOfficeList = repoDonation
                .Query()
                .Include(x => x.Donor)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.CreatedDate))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Donation), typeof(Method) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = DonorDonationDtoMapper.PaymentMap(centralOfficeList);

            KeyValuePair<int, List<DonorDonationPaymentDto>> resultResponse = new KeyValuePair<int, List<DonorDonationPaymentDto>>(totalCount, results);

            return resultResponse;

        }

        public void Dispose()
        {
            if (repoPaymentImport != null)
            {
                repoPaymentImport.Dispose();
                repoPaymentImport = null;
            }
            if (repoFoodItem != null)
            {
                repoFoodItem.Dispose();
                repoFoodItem = null;
            }
        }
    }
}
