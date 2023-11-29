using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace FB.Service
{
    public class BranchPredicate
    {
        /// <summary>
        /// To return the predicate to branch according to data accessibility 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="model"></param>
        /// <returns></returns>

        public static Expression<Func<Branch, bool>> GetDataAccessPredicate(Expression<Func<Branch, bool>> predicate, List<UserDataAccessDto> userDataAccess, int roleID)
        {
            if (roleID != (int)UserRoles.SuperAdmin && roleID != (int)UserRoles.Internal && roleID != (int)UserRoles.Branch && roleID != (int)UserRoles.Agent && roleID != (int)UserRoles.Donor && roleID != (int)UserRoles.TechnicalSupport)
            {
                if (userDataAccess.Count() > 0)
                {
                    var userAcess = userDataAccess.FirstOrDefault();
                    if (userAcess.CharityBranches != null && userAcess.CharityBranches.Count > 0)
                    {
                        List<int> charities = new List<int>();
                        List<int> branches = new List<int>();
                        foreach (var charity in userAcess.CharityBranches)
                        {
                            if (charity.Key > 0)
                                charities.Add(charity.Key);
                            if (charity.Value.Count > 0)
                                branches.AddRange(charity.Value);
                        }

                        predicate = predicate.And(b => (charities.Count > 0 ? charities.Contains(b.CharityId) : true) && (branches.Count > 0 ? branches.Contains(b.BranchId) : true));
                    }
                }
                else
                {
                    predicate = predicate.And(b => false);
                }
            }

            return predicate;
        }

    }
}
