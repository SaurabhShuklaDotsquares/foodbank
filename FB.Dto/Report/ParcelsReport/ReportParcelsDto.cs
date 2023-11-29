using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ReportParcelsDto
    {
        public ReportParcelsDto()
        {
            FoodDetail = new List<FoodDetail>();
        }
        public string ParcelType { get; set; }
        public string PackerName { get; set; }
        public string PackedDate { get; set; }
        public string DeliveredDate { get; set; }
        public string DeliveryDriverName { get; set; }
        public string Status { get; set; }
        public bool IsListbyFamily { get; set; }
        public bool IsIncludeFoodDetails { get; set; }
        public string FamilyName { get; set; }
        public List<FoodDetail> FoodDetail { get; set; }
    }
    public class FoodDetail
    {
        public string FoodName { get; set; }
        public int Quantity { get; set; }
    }

}
