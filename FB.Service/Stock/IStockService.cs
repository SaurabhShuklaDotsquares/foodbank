using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IStockService : IDisposable
    {
        void Save(Stock volunteer, bool isNew = true);
        KeyValuePair<int, List<StockDto>> GetStockList(DataTableServerSide searchModel, int FoodbankId);
        Stock GeStockById(int Id);
        StockDto GeStockDetailsById(int Id);
        int? GetStockAvialability(string FoodID, int FoodbankId);
        KeyValuePair<int, List<StockDto>> GetStockListForDashboard(int FoodbankId);

        //Manage Stock
        void SaveStockHistory(StockHistory stockHistory);
        Stock GeStockByFoodId(int foodId, int foodbankId);
        List<StockHistory> GetStockHistoryByStockId(int stockId);
        void DeleteStockHistory(int id);
        List<StockHistory> GetStockHistory(int parcelId);
    }
}
