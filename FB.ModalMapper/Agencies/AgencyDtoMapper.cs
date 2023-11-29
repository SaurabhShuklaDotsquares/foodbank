using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public class AgencyDtoMapper
    {
        public static List<AgenciesDto> AgencyMapper(List<Agencies> grantors)
        {
            var GrantorDtolist = new List<AgenciesDto>();
            foreach (var grantor in grantors)
            {
                GrantorDtolist.Add(new AgenciesDto
                {
                    AgencyId = grantor.Id,
                    AgencyName = grantor.Name,
                    FoodBankName = grantor.FoodBank.Name,
                    Email = grantor.Contact.Email,
                    ContactNumber = grantor.Contact.Mobile,
                });
            }
            return GrantorDtolist;
        }
        public static List<AgenciesDto> AgencyMapper(List<FamilyAgency> grantors)
        {
            var GrantorDtolist = new List<AgenciesDto>();
            foreach (var grantor in grantors)
            {
                GrantorDtolist.Add(new AgenciesDto
                {
                    AgencyId = grantor.Id,
                    AgencyName = grantor.Agency.Name,
                    Email = grantor.Agency.Contact.Email,
                    ContactNumber = grantor.Agency.Contact.Mobile,
                });
            }
            return GrantorDtolist;
        }
    }
}
