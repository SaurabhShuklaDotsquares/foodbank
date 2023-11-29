using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IFoodbankDataAccessibilityService : IDisposable
    {
        /// <summary>
        /// To save the User Data Accessibility 
        /// </summary>
        /// <param name="userdataaccess"></param>
        /// <param name="isNew"></param>
        void Save(FoodbankUserDataAccessibility method, bool isNew = true);

        /// <summary>
        /// To delete the user data accessibility
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        void DeleteByUserId(int userId);

        /// <summary>
        /// To get sigle user data accessibility
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<FoodbankUserDataAccessibility> GetUserDataAccessibility(int userID);
    }
}
