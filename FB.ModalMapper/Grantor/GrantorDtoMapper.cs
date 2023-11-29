using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public class GrantorDtoMapper
    {
        public static List<GrantorDto> GrantorMapper(List<Grantor> grantors)
        {
            var GrantorDtolist = new List<GrantorDto>();
            foreach (var grantor in grantors)
            {
                GrantorDtolist.Add(new GrantorDto
                {
                    GrantorId = grantor.Id,
                    ForeName = grantor.ForeName,
                    SurName = grantor.SurName,
                    FoodBankName = grantor.FoodBank.Name,
                    TotalAmount = grantor.TotalAmount > 0 ? Convert.ToString(grantor.TotalAmount.Value) : null,
                    ContactNumber = grantor.Contact.Mobile,
                    GrantorToken = grantor.GrantorToken,
                });
            }
            return GrantorDtolist;
        }
    }
}
