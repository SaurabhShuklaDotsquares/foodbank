using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System;
//using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace FB.Service
{
    public class MyReferralService : IMyReferralService
    {
        private IRepository<Referrers> repoMyReferral;
        private IRepository<ReferrerFamily> repoReferrerFamily;
        private IRepository<ReferrerType> repoReferrerType;
        public MyReferralService(IRepository<Referrers> _repoMyReferral, IRepository<ReferrerFamily> _repoReferrerFamily, IRepository<ReferrerType> _repoReferrerType)
        {
            this.repoMyReferral = _repoMyReferral;
            this.repoReferrerFamily = _repoReferrerFamily;
            this.repoReferrerType = _repoReferrerType;
        }
        public List<ReferrerType> GetReferrerType()
        {
            var res = repoReferrerType.Query().Get().ToList();
            return res;
        }
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferral(DataTableServerSide searchModel, int ReferrerID)
        {
            var predicate = PredicateBuilder.True<ReferrerFamily>();
            predicate = CustomPredicate.BuildPredicate<ReferrerFamily>(searchModel, new Type[] { typeof(Referrers), typeof(Fbcontact), typeof(Family), typeof(FamilyAddress), typeof(Fbaddress) });
            predicate = predicate.And(x => x.ReferrerId == ReferrerID);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);


            var referrerFamilies = repoReferrerFamily.Query().Include(x => x.Family).Include(x => x.Family.FamilyAddress).Include(x=>x.Family.Voucher)
                .Filter(predicate)
                .CustomOrderBy(u => u.OrderBy(searchModel))
                .GetPage(page, searchModel.length, out totalCount).ToList();


            var results = MyReferralDtoMapper.MyReferralMap(referrerFamilies);

            KeyValuePair<int, List<MyReferralsDto>> resultResponse = new KeyValuePair<int, List<MyReferralsDto>>(totalCount, results);

            return resultResponse;

        }
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferralYear(string year, int UserID)
        {
            var centralOfficeList = repoReferrerFamily
               .Query().Include(x => x.Referrer)
               .Filter(x => x.ReferralDate.Year == Convert.ToInt32(year) && x.Referrer.UserId == UserID).Get().ToList();

            var results = MyReferralDtoMapper.MyReferralYearMap(centralOfficeList);

            KeyValuePair<int, List<MyReferralsDto>> resultResponse = new KeyValuePair<int, List<MyReferralsDto>>(1, results);

            return resultResponse;

        }
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferralMonth(int month, int UserID)
        {

            var centralOfficeList = repoReferrerFamily
              .Query().Include(x => x.Referrer)
                .Filter(x => x.ReferralDate.Month == month && x.Referrer.UserId == UserID).Get().ToList();

            var results = MyReferralDtoMapper.MyReferralYearMap(centralOfficeList);

            KeyValuePair<int, List<MyReferralsDto>> resultResponse = new KeyValuePair<int, List<MyReferralsDto>>(1, results);

            return resultResponse;
        }
        public List<MyReferralsDto> GetAllReferral()
        {
            var res = repoMyReferral.Query().Include(x => x.Contact)
                 .Get().ToList();
            return MyReferralDtoMapper.GetAllList(res);
        }
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferralDate(DateTime sdate, DateTime edate, int Userid)
        {
            var centralOfficeList = repoReferrerFamily
                .Query().Include(x => x.Referrer)
                .Filter(x => x.Referrer.UserId == Userid && x.ReferralDate.Date >= sdate && x.ReferralDate.Date <= edate).Get().ToList();

            var results = MyReferralDtoMapper.MyReferralYearMap(centralOfficeList);

            KeyValuePair<int, List<MyReferralsDto>> resultResponse = new KeyValuePair<int, List<MyReferralsDto>>(1, results);

            return resultResponse;

        }
        public Referrers GetReferrerById(int id)
        {
            return repoMyReferral.Query().Filter(x => x.Id == id).Include(x => x.User).Include(x => x.RefType).Include(x => x.Address).Include(x => x.Contact)
                .Include(x => x.Contact).Get().FirstOrDefault();
        }

        public Referrers GetReferrerByUserId(int id)
        {
            return repoMyReferral.Query().Filter(x => x.UserId == id)
                .Include(x => x.User)
                .Include(x => x.Address)
                .Include(x => x.Contact).Get().FirstOrDefault();
        }
        public bool Save(Referrers model)
        {
            try
            {
                if (model.Id > 0)
                {
                    repoMyReferral.Update(model);
                    return true;
                }
                else
                {
                    repoMyReferral.Insert(model);
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int CountPendingReferralByID(int id)
        {
            return repoReferrerFamily.Query().Include(x=>x.Family).Filter(x => x.Family.ConfirmedById == null).Include(x=>x.Referrer)
            .Get().Where(x=>x.Referrer.UserId == id).Count();
        }
       
        public KeyValuePair<int, List<FoodbankReferrerDto>> GetReferrer(DataTableServerSide searchModel,int FoodbankId,int CharityId)
        {
            var predicate = PredicateBuilder.True<Referrers>();
            predicate = CustomPredicate.BuildPredicate<Referrers>(searchModel, new Type[] { typeof(Referrers), typeof(Fbcontact), typeof(Fbaddress), typeof(User) ,typeof(ReferrerType) });
            predicate = predicate.And(x => x.FoodbankId == FoodbankId && x.Active == true); //
            if (CharityId > 0)
            {
                predicate = predicate.And(x => x.User.CharityId == CharityId); //

            }
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var referrerList = repoMyReferral.Query()
                .Include(x => x.User)
                .Include(x => x.Contact)
                .Include(x=> x.Address)
                .Include(x => x.RefType)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Referrers), typeof(Fbcontact), typeof(Fbaddress), typeof(User), typeof(ReferrerType) } ))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = MyReferralDtoMapper.ReferrerMap(referrerList);

            KeyValuePair<int, List<FoodbankReferrerDto>> resultResponse = new KeyValuePair<int, List<FoodbankReferrerDto>>(totalCount, results);

            return resultResponse;

        }

        public List<Referrers> GetAllReferralForVoucher(int foodbankId)
        {
            var res = repoMyReferral.Query().Filter(x => x.IsVoucher == true && x.FoodbankId== foodbankId && x.Active==true && x.IsStatus==(int)ReferrersStatus.Accept).Get().ToList();
            return res;
        }
        public void UpdateReferres(Referrers model)
        {
            repoMyReferral.Update(model);
        }
        public int GetMyReferralByReferrerId(int ReferrerId)
        {
            return repoReferrerFamily.Query().Filter(x => x.ReferrerId == ReferrerId).Get().Count();
        }
        public bool CheckToken(string token)
        {
            var result = repoMyReferral.Query().Filter(x => x.ReffToken == token).Get().FirstOrDefault();
            if (result != null)
                return true;
            else
                return false;
        }
        public KeyValuePair<int, List<MyReferralsDto>> GetMyReferralByFoodbank(DataTableServerSide searchModel, int FoodbankId, int CharityId, int BranchId)
        {
            var predicate = PredicateBuilder.True<ReferrerFamily>();
            predicate = CustomPredicate.BuildPredicate<ReferrerFamily>(searchModel, new Type[] { typeof(Referrers), typeof(Fbcontact), typeof(Family), typeof(FamilyAddress), typeof(Fbaddress) });
            predicate = predicate.And(x => x.Referrer.FoodbankId == FoodbankId && x.Family.Active==true);
            if (CharityId > 0)
            {
                predicate = predicate.And(x => x.Family.CharityId == CharityId); //

            }
            if (BranchId > 0)
            {
                predicate = predicate.And(x => x.Family.BranchId == BranchId); //

            }
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);


            var referrerFamilies = repoReferrerFamily.Query().Include(x => x.Family).Include(x => x.Family.FamilyAddress)
                .Filter(predicate)
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Referrers), typeof(Fbcontact), typeof(Family), typeof(FamilyAddress), typeof(Fbaddress) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();


            var results = MyReferralDtoMapper.MyReferralMap(referrerFamilies);

            KeyValuePair<int, List<MyReferralsDto>> resultResponse = new KeyValuePair<int, List<MyReferralsDto>>(totalCount, results);

            return resultResponse;

        }
        public List<Referrers> GetReferrasByFoodBankId(int foodBankId)
        {
            return repoMyReferral.Query().Filter(x => x.FoodbankId == foodBankId).Get().ToList();
        }

        public List<ReferrerFamily> GetReferraFamilyDetails(int referraId)
        {
            return repoReferrerFamily.Query().Filter(x => x.ReferrerId == referraId).Include(p => p.Family).Get().ToList();
        }
        public void Dispose()
        {
            if (repoMyReferral != null)
            {
                repoMyReferral.Dispose();
                repoMyReferral = null;
            }
            if (repoReferrerFamily != null)
            {
                repoReferrerFamily.Dispose();
                repoReferrerFamily = null;
            }
        }


    }
}
