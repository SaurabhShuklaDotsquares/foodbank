using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IParcelService : IDisposable
    {
        Parcels GetParcelById(int id);
        bool Save(Parcels parcel);
        bool CheckToken(string token);

        //Parcel Type Section
        ParcelType GetParcelTypeById(int id);
        List<ParcelType> GetAllParcelType(int foodbankId);
        KeyValuePair<int, List<ParcelType>> GetParcelTypeList(DataTableServerSide searchModel, int foodbankId);
        bool SaveParcelType(ParcelType parcelType);
        void DeleteParcelType(int id);

        //Parcel Food Items
        List<ParcelFoodItem> GetParcelFoodItemById(int parcelTypeId);
        bool SaveParcelFoodItem(ParcelFoodItem parcelFoodItem);
        KeyValuePair<int, List<ParcelFoodItem>> GetParcelFoodItemList(DataTableServerSide searchModel, int parcelTypeId);
        ParcelFoodItem GetParcelFoodItem(int id);
        void DeleteParcelFoodItem(int id);

        List<FamilyParcelFoodItem> GetFamilyParcelFoodItemById(int parcelId);
        void DeleteFamilyParcelFoodItem(List<FamilyParcelFoodItem> itemList);
        List<ReportParcelsDto> GetParcelDetailsForReport(ParcelsReportDto model);


        Parcels GetParcelByVoucherToken(int voucherId);
    }
}
