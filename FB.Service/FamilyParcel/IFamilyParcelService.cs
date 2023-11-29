using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IFamilyParcelService : IDisposable
    {
        Parcels GetParcelById(int id);
        KeyValuePair<int, List<FamilyParcelDto>> GetFamilyParcelList(DataTableServerSide searchModel, int parcelTypeId);
        void DeleteParcel(int id);
        void DeleteFamilyParcelFoodItem(int id);
        List<FamilyParcelFoodItem> GetParcelFoodItemById(int parcelId);
        KeyValuePair<int, List<FamilyParcelDto>> GetFamilyParcelListByFamilyID(DataTableServerSide searchModel, int familyid);

        KeyValuePair<int, List<FamilyParcelDto>> GetVolunteerDeliveryDetailsID(DataTableServerSide searchModel, int familyid, int DelivererId);
        KeyValuePair<int, List<FamilyParcelDto>> GetVolunteerParcelDetailsID(DataTableServerSide searchModel, int familyid, int PackerId);
        public KeyValuePair<int, List<Parcels>> GetParcelfoodlists(DataTableServerSide searchModel, int FoodbankId, int GrantorId);
        List<Parcels> GetParcelsByRecipeid(int Recipeid);
    }
}
