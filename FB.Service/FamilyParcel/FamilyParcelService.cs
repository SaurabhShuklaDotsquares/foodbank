using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.Service
{
    public class FamilyParcelService : IFamilyParcelService
    {
        private IRepository<Parcels> repoParcel;
        private IRepository<ParcelType> repoParcelType;
        private IRepository<ParcelFoodItem> repoParcelFoodItem;
        private IRepository<FamilyParcelFoodItem> repoFamilyParcelFoodItem;
        private readonly IFoodService foodService;

        public FamilyParcelService(IRepository<Parcels> _repoParcel, IRepository<ParcelType> _repoParcelType, IRepository<ParcelFoodItem> _repoParcelFoodItem,
            IRepository<FamilyParcelFoodItem> _repoFamilyParcelFoodItem, IFoodService _foodService)
        {
            repoParcel = _repoParcel;
            repoParcelType = _repoParcelType;
            repoParcelFoodItem = _repoParcelFoodItem;
            repoFamilyParcelFoodItem = _repoFamilyParcelFoodItem;
            foodService = _foodService;
        }

        public Parcels GetParcelById(int id)
        {
            return repoParcel.Query().Filter(x => x.Id == id).Include(x => x.Feedback).Include(x=>x.FamilyParcelFoodItem).Get().FirstOrDefault();
        }
        public KeyValuePair<int, List<FamilyParcelDto>> GetFamilyParcelList(DataTableServerSide searchModel, int foodbankId)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = CustomPredicate.BuildPredicate<Parcels>(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) });
            predicate = predicate.And(m => m.FoodbankId == foodbankId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var parcelList = repoParcel
           .Query()
           .Include(x => x.Deliverer)
           .Include(x => x.StandardParcelType)
           .Include(x => x.Family)
           .Filter(predicate)
           .OrderBy(x => x.OrderByDescending(oo => oo.Id))
           .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) }))
           .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = FamilyParcelDtoMapper.FamilyParcelMapper(parcelList);

            KeyValuePair<int, List<FamilyParcelDto>> resultResponse = new KeyValuePair<int, List<FamilyParcelDto>>(totalCount, results);

            return resultResponse;
        }
        public KeyValuePair<int, List<FamilyParcelDto>> GetFamilyParcelListByFamilyID(DataTableServerSide searchModel, int familyid)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = CustomPredicate.BuildPredicate<Parcels>(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) });
            predicate = predicate.And(m => m.FamilyId == familyid);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var parcelList = repoParcel
            .Query()
            .Include(x => x.Deliverer)
            .Include(x => x.StandardParcelType)
            .Include(x => x.Family)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = FamilyParcelDtoMapper.FamilyParcelMapper(parcelList);

            KeyValuePair<int, List<FamilyParcelDto>> resultResponse = new KeyValuePair<int, List<FamilyParcelDto>>(totalCount, results);

            return resultResponse;
        }
        public void DeleteParcel(int id)
        {
            repoParcel.Delete(id);
        }

        public List<FamilyParcelFoodItem> GetParcelFoodItemById(int parcelId)
        {
            return repoFamilyParcelFoodItem.Query().Filter(x => x.ParcelId == parcelId).Get().ToList();
        }
        public void DeleteFamilyParcelFoodItem(int id)
        {
            repoFamilyParcelFoodItem.Delete(id);
        }


        public KeyValuePair<int, List<FamilyParcelDto>> GetVolunteerDeliveryDetailsID(DataTableServerSide searchModel, int familyid, int DelivererId)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = CustomPredicate.BuildPredicate<Parcels>(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) });
            predicate = predicate.And(m => m.DelivererId == DelivererId && m.DeliveredDate !=null);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var parcelList = repoParcel
            .Query()
            .Include(x => x.Deliverer)
            .Include(x => x.StandardParcelType)
            .Include(x => x.Family)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = FamilyParcelDtoMapper.FamilyParcelMapper(parcelList);

            KeyValuePair<int, List<FamilyParcelDto>> resultResponse = new KeyValuePair<int, List<FamilyParcelDto>>(totalCount, results);

            return resultResponse;
        }
        public KeyValuePair<int, List<FamilyParcelDto>> GetVolunteerParcelDetailsID(DataTableServerSide searchModel, int familyid, int PackerId)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = CustomPredicate.BuildPredicate<Parcels>(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) });
            predicate = predicate.And(m => m.PackerId == PackerId && m.PackOnDate != null);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var parcelList = repoParcel
            .Query()
            .Include(x => x.Deliverer)
            .Include(x => x.StandardParcelType)
            .Include(x => x.Family)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = FamilyParcelDtoMapper.FamilyParcelMapper(parcelList);

            KeyValuePair<int, List<FamilyParcelDto>> resultResponse = new KeyValuePair<int, List<FamilyParcelDto>>(totalCount, results);

            return resultResponse;
        }

        public KeyValuePair<int, List<Parcels>> GetParcelfoodlists(DataTableServerSide searchModel, int FoodbankId, int GrantorId)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            predicate = CustomPredicate.BuildPredicate<Parcels>(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) });
            predicate = predicate.And(m => m.FoodbankId == FoodbankId && m.GranterId == GrantorId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var parcelList = repoParcel
            .Query()
            .Include(x => x.Deliverer)
            .Include(x => x.StandardParcelType)
            .Include(x => x.Family)
            .Include(x => x.FamilyParcelFoodItem)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Parcels), typeof(ParcelType), typeof(Family), typeof(Volunteer) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var FoodIdList1 = foodService.GetFoodList(GrantorId).Distinct();


            List<Parcels> parcels = new List<Parcels>();
            foreach (var item in parcelList)
            {

                var list = item.FamilyParcelFoodItem.Select(x => x.FoodId).ToList();
                var containlList = list.Where(x => FoodIdList1.Contains(x)).ToList();

                if (containlList != null)
                {
                    parcels.Add(item);
                }
            }
            totalCount = parcels.Count;
            KeyValuePair<int, List<Parcels>> resultResponse = new KeyValuePair<int, List<Parcels>>(totalCount, parcels);

            return resultResponse;
        }
        public List<Parcels> GetParcelsFoodLists(int id)
        {
            return repoParcel.Query().Filter(x => x.FoodbankId == id).Include(x => x.FamilyParcelFoodItem).Get().ToList();
        }
        public List<Parcels> GetParcelsByRecipeid(int Recipeid)
        {
            return repoParcel.Query().Filter(x => x.RecipeId == Recipeid).Include(x => x.FamilyParcelFoodItem).Get().ToList();
        }
        public void Dispose()
        {
            if (repoParcelFoodItem != null)
            {
                repoParcelFoodItem.Dispose();
                repoParcelFoodItem = null;
            }
        }
    }
}
