using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class ParcelService : IParcelService
    {
        private IRepository<Parcels> repoParcel;
        private IRepository<ParcelType> repoParcelType;
        private IRepository<ParcelFoodItem> repoParcelFoodItem;
        private IRepository<FamilyParcelFoodItem> repoFamilyParcelFoodItem;
        private IRepository<Food> repoFood;

        public ParcelService(IRepository<Parcels> _repoParcel, IRepository<ParcelType> _repoParcelType, 
            IRepository<ParcelFoodItem> _repoParcelFoodItem,
            IRepository<FamilyParcelFoodItem> _repoFamilyParcelFoodItem,
            IRepository<Food>  _repoFood)
        {
            repoParcel = _repoParcel;
            repoParcelType = _repoParcelType;
            repoParcelFoodItem = _repoParcelFoodItem;
            repoFamilyParcelFoodItem = _repoFamilyParcelFoodItem;
            repoFood = _repoFood;
        }

        public Parcels GetParcelById(int id)
        {
            return repoParcel.Query().Filter(x => x.Id == id).Get().FirstOrDefault();
        }

        public Parcels GetParcelByVoucherToken(int voucherId)
        {
            return repoParcel.Query().Filter(x => x.VoucherId == voucherId).Get().FirstOrDefault();
        }

        public bool Save(Parcels parcel)
        {
            try
            {
                if (parcel.Id > 0)
                {
                    repoParcel.Update(parcel);
                    return true;
                }
                else
                {
                    repoParcel.Insert(parcel);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool CheckToken(string token)
        {
            var result = repoParcel.Query().Filter(x => x.ParcelToken == token).Get().FirstOrDefault();
            if (result != null)
                return true;
            else
                return false;
        }

        public ParcelType GetParcelTypeById(int id)
        {
            return repoParcelType.Query().Filter(x => x.Id == id).Get().FirstOrDefault();
        }

        public List<ParcelType> GetAllParcelType(int foodbankId)
        {
            return repoParcelType.Query().Filter(x => x.FoodbankId == foodbankId).Get().ToList();
        }

        public bool SaveParcelType(ParcelType parcelType)
        {
            try
            {
                if (parcelType.Id > 0)
                {
                    repoParcelType.Update(parcelType);
                    return true;
                }
                else
                {
                    parcelType.Adddate = System.DateTime.Now;
                    repoParcelType.Insert(parcelType);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public KeyValuePair<int, List<ParcelType>> GetParcelTypeList(DataTableServerSide searchModel, int foodbankId)
        {
            var predicate = PredicateBuilder.True<ParcelType>();
            predicate = CustomPredicate.BuildPredicate<ParcelType>(searchModel, new Type[] { typeof(ParcelType) });
            predicate = predicate.And(m => m.FoodbankId == foodbankId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var ParcelTypeList = repoParcelType
            .Query()
            .Include(x => x.ParcelFoodItem)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(ParcelType) ,typeof(ParcelFoodItem)}))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            KeyValuePair<int, List<ParcelType>> resultResponse = new KeyValuePair<int, List<ParcelType>>(totalCount, ParcelTypeList);

            return resultResponse;
        }

        public void DeleteParcelType(int id)
        {
            repoParcelType.Delete(id);
        }



        public KeyValuePair<int, List<ParcelFoodItem>> GetParcelFoodItemList(DataTableServerSide searchModel, int parcelTypeId)
        {
            var predicate = PredicateBuilder.True<ParcelFoodItem>();
            predicate = CustomPredicate.BuildPredicate<ParcelFoodItem>(searchModel, new Type[] { typeof(ParcelFoodItem) });
            predicate = predicate.And(m => m.ParcelTypeId == parcelTypeId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var foodItemList = repoParcelFoodItem
            .Query()
            .Include(x => x.Food)
            .Include(x => x.Food.Stock)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(ParcelFoodItem), typeof(Food) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            KeyValuePair<int, List<ParcelFoodItem>> resultResponse = new KeyValuePair<int, List<ParcelFoodItem>>(totalCount, foodItemList);

            return resultResponse;
        }

        public List<ParcelFoodItem> GetParcelFoodItemById(int parcelTypeId)
        {
            return repoParcelFoodItem.Query().Filter(x => x.ParcelTypeId == parcelTypeId).Include(x => x.Food).Get().ToList();
        }
        public bool SaveParcelFoodItem(ParcelFoodItem parcelFoodItem)
        {
            try
            {
                if (parcelFoodItem.Id > 0)
                {
                    repoParcelFoodItem.Update(parcelFoodItem);
                    return true;
                }
                else
                {
                    repoParcelFoodItem.Insert(parcelFoodItem);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public ParcelFoodItem GetParcelFoodItem(int id)
        {
            return repoParcelFoodItem.Query().Filter(x => x.Id == id).Get().FirstOrDefault();
        }

        public void DeleteParcelFoodItem(int id)
        {
            repoParcelFoodItem.Delete(id);
        }

        public List<FamilyParcelFoodItem> GetFamilyParcelFoodItemById(int parcelId)
        {
            return repoFamilyParcelFoodItem.Query().Filter(x => x.ParcelId == parcelId).Get().ToList();
        }

        public void DeleteFamilyParcelFoodItem(List<FamilyParcelFoodItem> itemList)
        {
            foreach (var item in itemList)
            {
                repoFamilyParcelFoodItem.Delete(item);
            }
        }

        public List<ReportParcelsDto> GetParcelDetailsForReport(ParcelsReportDto model)
        {
            var predicate = PredicateBuilder.True<Parcels>();
            if (model.DateFrom!=null && model.DateTo == null)
            {
                predicate = predicate.And(m => m.AddedDate >= model.DateFrom);
            }
            else if (model.DateTo != null && model.DateFrom == null)
            {
                predicate = predicate.And(m => m.AddedDate <= model.DateTo);
            }
            else if(model.DateFrom != null && model.DateTo != null)
            {
                predicate = predicate.And(m => m.AddedDate >=  model.DateFrom && m.AddedDate <= model.DateTo);
            }
            if (model.StatusId != null)
            {
                predicate = predicate.And(m => m.Status == model.StatusId);
            }
            if (model.ParcelTypeId != null)
            {
                predicate = predicate.And(m => m.StandardParcelTypeId == model.ParcelTypeId);
            }
            var parcelList = repoParcel
           .Query()
           .Include(x => x.FamilyParcelFoodItem)
           .Include(x => x.StandardParcelType)
           .Include(x => x.Family)
           .Include(x => x.Packer)
           .Include(x => x.Deliverer)
           .Include(x => x.Packer.Contact)
           .Include(x => x.Deliverer.Contact)
           .Filter(predicate).Get().ToList();

            List<ReportParcelsDto> reportParcelsDtos = new List<ReportParcelsDto>();
            if (parcelList!=null)
            {
                foreach (var item in parcelList)
                {
                    ReportParcelsDto reportParcelsDto = new ReportParcelsDto();

                    reportParcelsDto.ParcelType =item.StandardParcelType!=null? item.StandardParcelType.Name : "--";
                    reportParcelsDto.PackerName = item.Packer!=null? item.Packer.Contact.ForeName: "--";
                    reportParcelsDto.PackedDate = item.PackOnDate!=null?item.PackOnDate.ToFormatString() : "--";
                    reportParcelsDto.DeliveryDriverName = item.Deliverer!=null?item.Deliverer.Contact.ForeName :"--";
                    reportParcelsDto.DeliveredDate = item.DeliveryDate!=null?item.DeliveryDate.ToFormatString() : "--";
                    reportParcelsDto.Status = ((ParcelStatus)item.Status.Value).GetDescription();
                    if (model.IncludeFoodDetails)
                    {
                        FoodDetail foodDetail = new FoodDetail();
                        foreach (var data in item.FamilyParcelFoodItem)
                        {
                            var foodData = repoFood.Query().Filter(x=>x.Id==data.FoodId).Get().FirstOrDefault();
                            foodDetail.FoodName = foodData .Name!= null? foodData.Name : "";
                            foodDetail.Quantity = data.Quantity.ToString() != null ? data.Quantity : 0;
                            reportParcelsDto.FoodDetail.Add(foodDetail);
                        }
                        reportParcelsDto.IsIncludeFoodDetails = model.IncludeFoodDetails;

                    }
                    if (model.IsListByFamily)
                    {
                        reportParcelsDto.FamilyName = item.Family!=null?item.Family.FamilyName:"";
                        reportParcelsDto.IsListbyFamily = model.IsListByFamily;
                    }
                    reportParcelsDtos.Add(reportParcelsDto);
                }
                return reportParcelsDtos;
            }
            return new List<ReportParcelsDto>();

        }

        public void Dispose()
        {
            if (repoParcel != null)
            {
                repoParcel.Dispose();
                repoParcel = null;
            }
            if (repoParcelType != null)
            {
                repoParcelType.Dispose();
                repoParcelType = null;
            }
        }
    }
}
