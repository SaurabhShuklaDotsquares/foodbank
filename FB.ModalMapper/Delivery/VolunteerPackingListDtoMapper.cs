using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FB.ModalMapper
{
    public static class VolunteerPackingListDtoMapper
    {
        public static List<VolunteerPackingListDto> PackingMap(List<Parcels> packingItems)
        {
            var packingList = new List<VolunteerPackingListDto>();
            foreach (var item in packingItems)
            {
                packingList.Add(new VolunteerPackingListDto
                {
                    Id = item.Id,
                    AssignedDate = item.AddedDate,
                    PacelType = ((ParcelTypes)item.ParcelTypeId).GetDescription(),
                    DueDateDelivery = item.PackOnDate,
                    Status = item.Status

                });
            }
            return packingList;
        }
        public static List<VolunteerPackingAdminListDto> PackingAdminMap(List<Volunteer> Volunteertems)
        {

            var foodDonationList = new List<VolunteerPackingAdminListDto>();
            foreach (var Volunteertem in Volunteertems)
            {
                foodDonationList.Add(new VolunteerPackingAdminListDto
                {
                    VolunteerId = Volunteertem.Id,
                    LocationId = Volunteertem.LocationId,
                    PartnerId = Volunteertem.PartnerId,
                    ConfirmedBy = Volunteertem.ConfirmedBy,
                    CanDrive = Volunteertem.CanDrive,
                    DeliveryDriver = Volunteertem.DeliveryDriver,
                    DeliveryLimitPerShift = Volunteertem.DeliveryLimitPerShift,
                    ContactId = Volunteertem.ContactId,
                    OrganisationName = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.OrganisationName),
                    ForeName = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.ForeName),
                    Surname = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.Surname),
                    AddressId = (Volunteertem.Contact == null ? 0 : (Volunteertem.Contact.AddressId == null ? 0 : (int)Volunteertem.Contact.AddressId)),

                    Mobile = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.Mobile),
                    Email = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.Email),
                    VolunteerName = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.ForeName + " " + Volunteertem.Contact.Surname),
                    IndividualCouple = Volunteertem.IndividualCouple,
                    Packingordelivery = Volunteertem.Packingordelivery,
                    Howelsecanyouhelp = Volunteertem.Howelsecanyouhelp,
                    IndividualCouplename = ((IndividualCouple)(int)Volunteertem.IndividualCouple).GetDescription(),
                    Packingordeliveryname = ((packingordelivery)(int)Volunteertem.Packingordelivery).GetDescription(),
                    PendingDeliveries = Volunteertem.ParcelsDeliverer.Where(x=>x.DeliveredDate == null && x.DeliveryDate != null).ToList().Count
                }); 

            }
            return foodDonationList;
            
        }
    }
}
