using FB.Data.Models;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FB.Service
{
    public interface ICharityService : IDisposable
    {
        Charity GetCharity(int id);
        Charity GetCharityByToken(string CharityToken);
        List<Charity> GetCharitiesByDataAccessibility(List<UserDataAccessDto> userDataAccess, int roleID, int organisationId, int userID, bool isUpdate = true, bool isUserPreference = true, int charityId = 0);

        bool IsAccessibleCharity(object id, bool isAccessibilityCheck, List<UserDataAccessDto> userDataAccess, int roleID, int centralOfficeId, int userID);
        List<CharityLiteDto> GetCharityByOrganisationID(int organisationID, int userID, bool IsUserPreference = true);

    }
}
