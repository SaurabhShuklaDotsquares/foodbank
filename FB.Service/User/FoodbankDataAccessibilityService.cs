using FB.Data.Models;
using FB.Repo;
using FB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class FoodbankDataAccessibilityService : IFoodbankDataAccessibilityService
    {
        private IRepository<FoodbankUserDataAccessibility> repoDataAccessibility;

        public FoodbankDataAccessibilityService(IRepository<FoodbankUserDataAccessibility> _repoDataAccessibility)
        {
            this.repoDataAccessibility = _repoDataAccessibility;
        }

        /// <summary>
        /// To save the User Data Accessibility
        /// </summary>
        /// <param name="UserDataAccessibility"></param>
        /// <param name="isNew"></param>
        public void Save(FoodbankUserDataAccessibility userDataAccessibility, bool isNew = true)
        {
            if (isNew)
            {
                repoDataAccessibility.Insert(userDataAccessibility);
            }
            else
            {
                repoDataAccessibility.Update(userDataAccessibility);
            }
        }

        /// <summary>
        /// To delete the User Data Accessibility
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            repoDataAccessibility.Delete(id);
        }

        public void DeleteByUserId(int userId)
        {
            var ids = GetUserDataAccessibility(userId).Select(e => e.UserAccessId).ToList();
            foreach (var id in ids)
            {
                repoDataAccessibility.Delete(id);
            }
        }

        /// <summary>
        /// To get sigle Get User Data Accessibility
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FoodbankUserDataAccessibility> GetUserDataAccessibility(int userID)
        {
            return repoDataAccessibility.Query().Filter(x => x.UserId == userID).Get().ToList();
        }

        public void Dispose()
        {
            if (repoDataAccessibility != null)
            {
                repoDataAccessibility.Dispose();
                repoDataAccessibility = null;
            }
        }
    }
}
