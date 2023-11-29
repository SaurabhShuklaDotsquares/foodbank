using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public static class VolunteerDeliveryDtoMapper
    {
        public static List<VolunteerDeliveryListDto> DeliveryMap(List<Parcels> deliveryItems)
        {
            var deliveryList = new List<VolunteerDeliveryListDto>();
            foreach (var item in deliveryItems)
            {
                deliveryList.Add(new VolunteerDeliveryListDto
                {
                    Id = item.Id,
                    DateOfDelivery = item.DeliveryDate.Value,
                    DeliveryAddress = (item.Location == null ? "" : item.Location.Name),
                    ParcelType = ((ParcelTypes)item.ParcelTypeId).GetDescription(),
                    Status = item.Status.Value,
                    FamilyName = item.Family != null ? item.Family.FamilyName : ""
                }); ; 
            }
            return deliveryList;
        }
    }
}
