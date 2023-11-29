using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class FamilyParcelDto
    {
        public FamilyParcelDto()
        {
            this.FamilyParcelList = new List<FamilyParcel>();
        }
        public int FoodBankId { get; set; }
        public int ParcelId { get; set; }
        public int? FamilyId { get; set; }
        public string FamilyName { get; set; }
        public int DeliverrerId { get; set; }
        public string DeliverrerName { get; set; }
        public int PackerId { get; set; }
        public string PackerName { get; set; }
        public int? ParcelTypeId { get; set; }
        public int? StandardParcelId { get; set; }
        public bool StandardParcel { get; set; }
        public bool BespokeParcel { get; set; }
        public string ParcelType { get; set; }
        public int Status { get; set; }
        public string SpecialNote { get; set; }
        public int RecipeId { get; set; }
        public int GrantorId { get; set; }
        public int VoucherId { get; set; }
        public bool IncludeRecipe { get; set; }
        public string ParcelQrcode { get; set; }
        public DateTime DueDate { get; set; }
        public string DeliveredDate { get; set; }
        public string PackOnDate { get; set; }
        public string DeliveryDate { get; set; }
        public DateTime AddedDate { get; set; }

        public int? FoodItemId { get; set; }
        public string Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public string AvailableQuantity { get; set; }
        public List<FamilyParcel> FamilyParcelList { get; set; }

        public List<string> hdnparcelId { get; set; }
        public List<string> hdnfoodItemId { get; set; }
        public List<string> itemquantity { get; set; }
        

    }

    public class FamilyParcel
    {
        public int ParcelId { get; set; }
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
