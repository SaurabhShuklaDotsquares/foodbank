using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class StockDto
    {
        public int Id { get; set; }
        public int FoodbankId { get; set; }
        public int FoodId { get; set; }
        public string TotalQuantity { get; set; }
        public int IsItemLowInStock { get; set; }
        public string AboutServicerOffered { get; set; }
        public string PricePerItem { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool IsGrantorMoney { get; set; }
        public int? AvailQuantity { get; set; }
        public int? GrantorId { get; set; }
        public bool Active { get; set; }
        public string Weight { get; set; }
        public string FoodName { get; set; }
        public string Unit { get; set; }
        public string GrantorName { get; set; }
        public int StockManageId { get; set; }
        public string FoodCategoryId { get; set; }
        public string ProductApiId { get; set; }
        public string hdnFoodCategoryId { get; set; }
        public string hdnFoodProductId { get; set; }

    }
    public class StockManageDto
    {
        public int Id { get; set; }
        public int FoodbankId { get; set; }
        public int FoodId { get; set; }
        public int? TotalQuantity { get; set; }
        public int? IsItemLowInStock { get; set; }
        public string AboutServicerOffered { get; set; }
        public decimal? PricePerItem { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool? IsGrantorMoney { get; set; }
        public int? GrantorId { get; set; }
        public bool? Active { get; set; }
        public int StockId { get; set; }
        public int? Quantity { get; set; }

    }

    public class StockManageGrantorDto
    {
        public int Id { get; set; }
        public int FoodbankId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? AddedDate { get; set; }
        public bool IsGrantorDonation { get; set; }
        public bool IsDonorDonation { get; set; }
        public int? GrantorId { get; set; }
        public string GrantorName{ get; set; }
        public int? DonorId { get; set; }
        public string DonorName { get; set; }
        public int StockId { get; set; }
    }
}
