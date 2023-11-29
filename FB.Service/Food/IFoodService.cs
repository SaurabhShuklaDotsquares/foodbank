using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IFoodService : IDisposable
    {
        List<Food> GetAllFoodList();
        void SaveFoodDonation(FoodItem model);
        List<FoodItem> GetRecordByStockId(int stockId, int foodbankId);
        Food SaveFood(Food model);
        List<Food> GetAllFoodListForPledge(int foodbankId);
        List<Food> GetAllFoodListForParcel(int foodbankId);
        Stock GetFoodUnit(int foodId);

        KeyValuePair<int, List<StockManageGrantorDto>> GetStockList(DataTableServerSide searchModel, int FoodbankId, int stockId);
        List<int> GetFoodList(int id);
    }
}
