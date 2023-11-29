using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IUserService : IDisposable
    {
        User GetUser(int id);
        List<User> GetUser(string userName, string Password);
        User GetUserEntityByUserName(string username);
        void Save(User user, int? CreatedBy, bool isNew = true);
        GraphAccessDto GetMSGraphApiAccess(int id);
        List<FoodbankMenu> GetMenus(int id);
        UserLiteDto GetUserByUserName(string username);
        UserLiteDto GetUserByUserNameOrEmail(string userName, string email);
        List<User> GetMultiUser(string userName, string EncryptPassword);
        bool IsUserNameExist(string userName, int userId = 0);
        KeyValuePair<int, List<UserDto>> GetUsersForFoodbank(DataTableServerSide searchModel, int CreatedBy, int roleId, int? FoodbankId = null);
        void Delete(int id);
    }
}
