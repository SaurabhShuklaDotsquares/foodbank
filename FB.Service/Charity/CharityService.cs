using FB.Data.Models;
using FB.Repo;
using FB.Service;
using FB.Core;
using System.Linq;
using FB.Dto;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using FB.ModelMapper;

namespace FB.Service
{
    public class CharityService : ICharityService
    {
        private readonly IRepository<Charity> repoCharity;
        private readonly IRepository<AppErrorLog> repoAppErrorLog;
        private readonly IUserPreferenceService userPreferenceService;

        public CharityService(IRepository<Charity> _repoCharity, IUserPreferenceService _userPreferenceService)
        {
            repoCharity = _repoCharity;
            userPreferenceService = _userPreferenceService;
        }

        public Charity GetCharity(int id)
        {
            return repoCharity.Query().Filter(x => x.CharityId == id)
                .Include(x => x.CentralOffice)
                .Include(x => x.Branch).Include(x => x.CharityGoCardLessPlan).Get().FirstOrDefault();
        }
        public Charity GetCharityByToken(string CharityToken)
        {
            return repoCharity.Query().Filter(x => x.CharityToken == CharityToken)
                .Include(x => x.CentralOffice)
                .Include(x => x.CentralOffice)
                .Include(x => x.Branch).Get().FirstOrDefault();
        }
        public List<Charity> GetCharitiesByDataAccessibility(List<UserDataAccessDto> userDataAccess, int roleID, int organisationId, int userID, bool isUpdate = true, bool isUserPreference = true,int charityId=0)
        {
            //var CharityList = new List<Charity>();
            var predicate = PredicateBuilder.True<Charity>();
            predicate = predicate.And(CharityPredicate.GetDataAccessPredicate(predicate, userDataAccess, roleID));
            predicate = predicate.And(e => e.CentralOfficeId == organisationId);

            predicate = predicate.And(e => e.IsMmo);

            if (isUserPreference)
            {
                predicate = SetCharityActiveUserPreference(predicate, userID);
            }

            var orderBy = OrderCharityByUserPreference(userID);
            return repoCharity.Query().Filter(predicate).OrderBy(o => o.OrderBy(orderBy)).Get().ToList();
         //   return repoCharity.Query().Filter(predicate)/*.OrderBy(o => o.OrderBy(orderBy))*/.Get().ToList();
        }
        public List<CharityLiteDto> GetCharityByOrganisationID(int organisationID, int userID, bool IsUserPreference = true)
        {
            var predicate = PredicateBuilder.True<Charity>();
            predicate = predicate.And(e => e.CentralOfficeId == organisationID);
            if (IsUserPreference)
            {
                predicate = SetCharityActiveUserPreference(predicate, userID);
            }
            var orderBy = OrderCharityByUserPreference(userID);
            var charityList = repoCharity.Query().Filter(predicate).Include(c => c.CentralOffice).OrderBy(o => o.OrderBy(orderBy)).Get().ToList();

            return CharityDtoMapper.Map(charityList);
        }
        private Expression<Func<Charity, bool>> SetCharityActiveUserPreference(Expression<Func<Charity, bool>> predicate, int? userId)
        {
            if (userId > 0)
            {
                var userPreference = userPreferenceService.GetUserPreferencebyUserId(userId.Value);
                if (userPreference != null && userPreference.ShowInactiveCharityBranch == true)
                    return predicate;
            }
            return predicate.And(e => e.IsActive);
        }

        private Expression<Func<Charity, string>> OrderCharityByUserPreference(int? userId)
        {
            if (userId > 0)
            {
                var userPreference = userPreferenceService.GetUserPreferencebyUserId(userId.Value);
                if (userPreference != null)
                {
                    if (userPreference.OrderByCharityBranch == (byte)UserPreferenceOrderByCharityBranch.ByName)
                        return oo => oo.CharityName;
                    else if (userPreference.OrderByCharityBranch == (byte)UserPreferenceOrderByCharityBranch.ByRef)
                        return oo => oo.CharityReference;
                }
            }
            return oo => oo.CharityName;
        }

        /// <summary>
        /// Get Accessible Charity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="centralOfficeId"></param>
        /// <returns></returns>
        public bool IsAccessibleCharity(object id, bool isAccessibilityCheck, List<UserDataAccessDto> userDataAccess, int roleID, int centralOfficeId, int userID)
        {
            int checkCharityId = Convert.ToInt32(id);
            if (isAccessibilityCheck)
            {
                return GetCharitiesByDataAccessibility(userDataAccess, roleID, centralOfficeId, userID, false, isUserPreference: false).FirstOrDefault(m => m.CharityId == checkCharityId) != null;
            }
            else
            {
                return repoCharity
                    .Query()
                    .Filter(m => m.CharityId == checkCharityId && m.CentralOfficeId == centralOfficeId /*&& m.IsMmo*/)
                    .Get()
                    .FirstOrDefault() != null;
            }
        }

        public void Dispose()
        {
            if (repoCharity != null)
                repoCharity.Dispose();
        }
    }
}
