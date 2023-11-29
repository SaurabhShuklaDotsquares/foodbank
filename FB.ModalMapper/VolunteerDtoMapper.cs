using FB.Data.Models;
using FB.Dto;
using FB.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public static class VolunteerDtoMapper
    {
        public static List<VolunteerDto> FBMap(List<Volunteer> Volunteertems)
        {
            var foodDonationList = new List<VolunteerDto>();
            foreach (var Volunteertem in Volunteertems)
            {
                foodDonationList.Add(new VolunteerDto
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
                    //Phone = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.Phone),
                    Mobile = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.Mobile),
                    Email = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.Email),
                    VolunteerName = (Volunteertem.Contact == null ? "" : Volunteertem.Contact.ForeName+" "+ Volunteertem.Contact.Surname),
                    IndividualCouple = Volunteertem.IndividualCouple,
                    Packingordelivery = Volunteertem.Packingordelivery,
                    Howelsecanyouhelp = Volunteertem.Howelsecanyouhelp,
                    IndividualCouplename = ((IndividualCouple)(int)Volunteertem.IndividualCouple).GetDescription(),
                    Packingordeliveryname = ((packingordelivery)(int)Volunteertem.Packingordelivery).GetDescription(),
                    AddedDate= Volunteertem.AddedDate
                }); ;   
       
            }
            return foodDonationList;
        }
       
    }
}
