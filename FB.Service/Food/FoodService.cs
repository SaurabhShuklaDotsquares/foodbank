using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class FoodService : IFoodService
    {
        private IRepository<Food> repoFood;
        private IRepository<FoodItem> repoFoodItem;
        private IRepository<Stock> repoStock;
        public FoodService(IRepository<Food> _repoFood, IRepository<FoodItem> _repoFoodItem, IRepository<Stock> _repoStock)
        {
            repoFood = _repoFood;
            repoFoodItem = _repoFoodItem;
            repoStock = _repoStock;
        }

        /// <summary>
        /// To get food item list
        /// </summary>     
        /// <param name="roleID"></param>
        /// <returns>List</returns>
        public List<Food> GetAllFoodList()
        {
            return repoFood
               .Query()
               .Get().ToList();
        }

        public List<Food> GetAllFoodListForPledge(int foodbankId)
        {
            return repoStock.Query().Filter(x => x.IsItemLowInStock.Value == 1 && x.FoodbankId == foodbankId).Include(x => x.Food).Get().Select(x => x.Food).ToList();
        }

        public List<Food> GetAllFoodListForParcel(int foodbankId)
        {
            return repoStock.Query().Filter(x => x.TotalQuantity > 0 && x.FoodbankId == foodbankId).Include(x => x.Food).Get().Select(x => x.Food).ToList();
        }

        public Stock GetFoodUnit(int foodId)
        {
            return repoStock.Query().Filter(x => x.FoodId == foodId).Get().FirstOrDefault();
        }


        /// <summary>
        /// To get food item list
        /// </summary>     
        /// <param name="roleID"></param>
        /// <returns>List</returns>
        public void SaveFoodDonation(FoodItem model)
        {
            repoFoodItem.Insert(model);
        }

        public List<FoodItem> GetRecordByStockId(int stockId, int foodbankId)
        {
            return repoFoodItem.Query().Filter(x => x.StockId == stockId && x.FoodbankId == foodbankId).Get().ToList();
        }

        public KeyValuePair<int, List<StockManageGrantorDto>> GetStockList(DataTableServerSide searchModel, int FoodbankId, int stockId)
        {
            var predicate = PredicateBuilder.True<FoodItem>();
            predicate = CustomPredicate.BuildPredicate<FoodItem>(searchModel, new Type[] { typeof(Grantor), typeof(Food), typeof(Person) });
            predicate = predicate.And(x => x.FoodbankId == FoodbankId);
            predicate = predicate.And(x => x.StockId == stockId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            var deliveryList = repoFoodItem
                .Query()
                .Include(x => x.Food).Include(x => x.Grantor).Include(x => x.Donor).Include(x => x.Stock)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Stock), typeof(Food) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = DonorDonationDtoMapper.MapGrantorStockList(deliveryList);

            KeyValuePair<int, List<StockManageGrantorDto>> resultResponse = new KeyValuePair<int, List<StockManageGrantorDto>>(totalCount, results);

            return resultResponse;

        }

        public Food SaveFood(Food model)
        {
            repoFood.Insert(model);
            return model;
        }

        public List<int> GetFoodList(int id)
        {
            return repoFoodItem.Query().Filter(x => x.GrantorId == id).Get().Select(x => x.Foodid.Value).Distinct().ToList();
        }
        public void Dispose()
        {
            if (repoFood != null)
            {
                repoFood.Dispose();
                repoFood = null;
            }
            if (repoFoodItem != null)
            {
                repoFoodItem.Dispose();
                repoFoodItem = null;
            }
        }
    }
}
