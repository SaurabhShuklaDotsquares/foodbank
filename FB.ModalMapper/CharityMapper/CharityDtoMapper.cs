using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModelMapper
{
    public static class CharityDtoMapper
    {
        public static List<CharityLiteDto> Map(List<Charity> charities)
        {
            var charityLiteDto = new List<CharityLiteDto>();
            foreach (var charity in charities)
            {
                charityLiteDto.Add(new CharityLiteDto
                {
                    CharityID = charity.CharityId,
                    CharityName = charity.CharityName,
                    Prefix = charity.Prefix,
                    OrganisationName = charity.CentralOffice.OrganisationName,
                    IsActive = charity.IsActive,
                    IsMmosystemCreated = charity.IsMmosystemCreated
                });
            }
            return charityLiteDto;
        }
    }


}
