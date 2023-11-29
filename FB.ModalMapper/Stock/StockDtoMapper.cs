using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FB.ModalMapper
{
    public class StockDtoMapper
    {
        public static List<StockDto> MapList(List<Stock> availabilityItems)
        {
            var availabilityList = new List<StockDto>();
            foreach (var item in availabilityItems)
            {
                availabilityList.Add(new StockDto
                {
                    Id = item.Id,
                    FoodbankId = item.FoodbankId,
                    FoodId = item.FoodId,
                    TotalQuantity = Convert.ToString(item.TotalQuantity ?? 0),
                    IsItemLowInStock = item.IsItemLowInStock ?? 0,
                    AboutServicerOffered = item.AboutServicerOffered,
                    PricePerItem = Convert.ToString(item.PricePerItem ?? 0),
                    IsGrantorMoney = item.IsGrantorMoney ?? false,
                    AvailQuantity = item.TotalQuantity ?? 0,
                    GrantorId = item.GrantorId ?? 0,
                    Active = item.Active ?? false,
                    FoodName = item.Food.Name,
                    Unit = item.Unit,
                });
            }
            return availabilityList;
        }

        public static List<StockDto> MapListForDashboard(List<Stock> availabilityItems)
        {
            var availabilityList = new List<StockDto>();
            foreach (var item in availabilityItems)
            {
                availabilityList.Add(new StockDto
                {
                    Id = item.Id,
                    FoodbankId = item.FoodbankId,
                    FoodId = item.FoodId,
                    TotalQuantity = Convert.ToString(item.TotalQuantity ?? 0),
                    IsItemLowInStock = item.IsItemLowInStock ?? 0,
                    AboutServicerOffered = item.AboutServicerOffered,
                    PricePerItem = Convert.ToString(item.PricePerItem ?? 0),
                    IsGrantorMoney = item.IsGrantorMoney ?? false,
                    AvailQuantity = item.TotalQuantity ?? 0,
                    GrantorId = item.GrantorId ?? 0,
                    Active = item.Active ?? false,
                    FoodName = item.Food.Name,
                    Unit = item.Unit,
                });
            }
            return availabilityList;
        }
        public static StockDto Map(Stock item)
        {
            var availabilityList = new StockDto();
            availabilityList.Id = item.Id;
            availabilityList.FoodbankId = item.FoodbankId;
            availabilityList.FoodId = item.FoodId;
            availabilityList.TotalQuantity = Convert.ToString(item.TotalQuantity ?? 0);
            availabilityList.IsItemLowInStock = item.IsItemLowInStock ?? 0;
            availabilityList.AboutServicerOffered = item.AboutServicerOffered;
            availabilityList.PricePerItem = Convert.ToString(item.PricePerItem ?? 0);
            availabilityList.IsGrantorMoney = item.IsGrantorMoney ?? false;
            availabilityList.AvailQuantity = item.TotalQuantity ?? 0;
            availabilityList.GrantorId = item.GrantorId ?? 0;
            availabilityList.Active = item.Active ?? false;
            availabilityList.FoodName = item.Food.Name;
            availabilityList.Unit = item.Unit;
            availabilityList.FoodCategoryId = item.Food.CategoryApiId;
            availabilityList.hdnFoodCategoryId = item.Food.CategoryApiId;
            availabilityList.ProductApiId = item.Food.ProductIdApi;
            availabilityList.hdnFoodProductId = item.Food.ProductIdApi;
            availabilityList.GrantorName = item.Grantor != null ? (item.Grantor.Contact != null ? $"{item.Grantor.Contact.ForeName} {item.Grantor.Contact.Surname}" : "") : "";
            return availabilityList;
        }

        public static Stock MapSave(StockDto item)
        {
            var availabilityList = new Stock();

            availabilityList.Id = item.Id;
            availabilityList.FoodbankId = item.FoodbankId;
            availabilityList.FoodId = item.FoodId;
            availabilityList.TotalQuantity = Convert.ToInt32(item.TotalQuantity);
            availabilityList.IsItemLowInStock = item.IsItemLowInStock;
            availabilityList.AboutServicerOffered = item.AboutServicerOffered;
            availabilityList.PricePerItem = Convert.ToDecimal(item.PricePerItem);
            availabilityList.IsGrantorMoney = item.IsGrantorMoney;
            availabilityList.GrantorId = item.GrantorId;
            availabilityList.Active = true;
            availabilityList.Unit = item.Unit;

            return availabilityList;
        }
        public static Stock MapUpdate(StockDto item)
        {
            var availabilityList = new Stock();

            availabilityList.Id = item.Id;
            availabilityList.FoodbankId = item.FoodbankId;
            availabilityList.FoodId = item.FoodId;
            availabilityList.TotalQuantity = Convert.ToInt32(item.TotalQuantity);
            availabilityList.IsItemLowInStock = item.IsItemLowInStock;
            availabilityList.AboutServicerOffered = item.AboutServicerOffered;
            availabilityList.PricePerItem = Convert.ToDecimal(item.PricePerItem);
            availabilityList.IsGrantorMoney = item.IsGrantorMoney;
            availabilityList.GrantorId = item.GrantorId;
            availabilityList.Active = true;
            availabilityList.Unit = item.Unit;

            return availabilityList;
        }

       

        //public static Stock MapGrantorStockList(StockDto item)
        //{
        //    var foodItem = new FoodItem();

        //    foodItem.Id = item.Id;
        //    foodItem.FoodbankId = item.FoodbankId;
        //    foodItem.Foodid = item.FoodId;
        //    foodItem.Quntity = Convert.ToInt32(item.TotalQuantity);
        //    foodItem.AboutServicerOffered = item.AboutServicerOffered;
        //    foodItem.PricePerItem = Convert.ToDecimal(item.PricePerItem);
        //    foodItem.IsGrantorMoney = item.IsGrantorMoney;
        //    foodItem.GrantorId = item.GrantorId;
        //    foodItem.Active = true;
        //    foodItem.Unit = item.Unit;

        //    return availabilityList;
        //}
    }
}
