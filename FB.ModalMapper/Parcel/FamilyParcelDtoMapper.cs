using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System.Collections.Generic;

namespace FB.ModalMapper
{
    public class FamilyParcelDtoMapper
    {
        public static List<FamilyParcelDto> FamilyParcelMapper(List<Parcels> parcels)
        {
            var familyParcelDtoList = new List<FamilyParcelDto>();
            foreach (var parcel in parcels)
            {
                familyParcelDtoList.Add(new FamilyParcelDto
                {
                    ParcelId = parcel.Id,
                    ParcelType = ((ParcelTypes)parcel.ParcelTypeId).GetDescription(),
                    FamilyName = parcel.Family != null ? parcel.Family.FamilyName : "-",
                    DeliveredDate = parcel.DeliveredDate != null ? Extensions.ToFormatCustomString(parcel.DeliveredDate.Value) : "-",
                    PackOnDate = parcel.PackOnDate != null ? Extensions.ToFormatCustomString(parcel.PackOnDate.Value) : "-",
                    DueDate = parcel.DeliveryDate.Value,
                    Status = parcel.Status.Value,
                });
            }
            return familyParcelDtoList;
        }
    }
}
