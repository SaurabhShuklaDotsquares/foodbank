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
    public class StockService : IStockService
    {
        private readonly IRepository<Stock> repoStock;
        private readonly IRepository<StockHistory> repoStockHistory;

        public StockService(IRepository<Stock> _repoStock, IRepository<StockHistory> _repoStockHistory)
        {
            repoStock = _repoStock;
            repoStockHistory = _repoStockHistory;
        }
        /// <summary>
        /// To save the volunteer
        /// </summary>
        /// <param name="country"></param>
        /// <param name="isNew"></param>
        public void Save(Stock stock, bool isNew = true)
        {
            if (isNew)
            {
                repoStock.Insert(stock);
            }
            else
            {
                repoStock.Update(stock);
            }
        }

        public KeyValuePair<int, List<StockDto>> GetStockList(DataTableServerSide searchModel, int FoodbankId)
        {
            var predicate = PredicateBuilder.True<Stock>();
            predicate = CustomPredicate.BuildPredicate<Stock>(searchModel, new Type[] { typeof(Stock), typeof(Food) });
            predicate = predicate.And(x => x.FoodbankId == FoodbankId);
            predicate = predicate.And(x => x.Active.Value == true);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            var deliveryList = repoStock
                .Query()
                .Include(x => x.Food).Include(x => x.Grantor)
                .Filter(predicate)
                .OrderBy(x => x.OrderByDescending(oo => oo.Id))
                .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Stock), typeof(Food) }))
                .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = StockDtoMapper.MapList(deliveryList);

            KeyValuePair<int, List<StockDto>> resultResponse = new KeyValuePair<int, List<StockDto>>(totalCount, results);

            return resultResponse;

        }

        public KeyValuePair<int, List<StockDto>> GetStockListForDashboard(int FoodbankId)
        {
            var predicate = PredicateBuilder.True<Stock>();

            predicate = predicate.And(x => x.Foodbank.Id == FoodbankId);
            predicate = predicate.And(x => x.IsItemLowInStock == 1 && x.Active == true);

            int totalCount;

            var deliveryList = repoStock
                .Query()
                .Include(x => x.Food)
                .Include(x => x.Grantor)
                .Filter(predicate)
                .OrderBy(x => x.OrderBy(oo => oo.TotalQuantity))
                .Get().Take(10).ToList();

            var results = StockDtoMapper.MapListForDashboard(deliveryList);

            KeyValuePair<int, List<StockDto>> resultResponse = new KeyValuePair<int, List<StockDto>>(0, results);

            return resultResponse;

        }

        public Stock GeStockById(int Id)
        {
            return repoStock.Query()
                .Include(x => x.Foodbank).Include(x => x.Grantor).Include(x => x.Food)
                .Filter(x => x.Id == Id).Get().FirstOrDefault();
        }
        public StockDto GeStockDetailsById(int Id)
        {
            return StockDtoMapper.Map(repoStock.Query()
                .Include(x => x.Foodbank).Include(x => x.Grantor).Include(x => x.Food).Include(x => x.Grantor.Contact)
                .Filter(x => x.Id == Id).Get().FirstOrDefault());
        }
        public int? GetStockAvialability(string FoodID, int FoodbankId)
        {
            var totalstock = repoStock.Query()
                  .Include(x => x.Foodbank).Include(x => x.Food)
                  .Filter(x => x.Food.ProductIdApi == FoodID && x.FoodbankId == FoodbankId).Get().FirstOrDefault();
            return (totalstock != null ? totalstock.TotalQuantity : 0);
        }

        public Stock GeStockByFoodId(int foodId, int foodbankId)
        {
            return repoStock.Query()
                .Include(x => x.Foodbank).Include(x => x.Food)
                .Filter(x => x.FoodId == foodId && x.FoodbankId == foodbankId).Get().FirstOrDefault();
        }

        public void SaveStockHistory(StockHistory stockHistory)
        {
            repoStockHistory.Insert(stockHistory);
        }

        public List<StockHistory> GetStockHistoryByStockId(int stockId)
        {
            return repoStockHistory.Query().Filter(x => x.StockId == stockId).Get().ToList();
        }

        public List<StockHistory> GetStockHistory(int parcelId)
        {
            return repoStockHistory.Query().Filter(x => x.ParcelId == parcelId).Include(x => x.Stock).Get().ToList();
        }

        public void DeleteStockHistory(int id)
        {
            repoStockHistory.Delete(id);
        }
        public void Dispose()
        {
            if (repoStock != null)
            {
                repoStock.Dispose();
            }

        }


    }
}
