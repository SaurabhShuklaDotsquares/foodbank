using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class UserService : IUserService
    {
        private IRepository<User> repoUser;
        private IRepository<FoodbankMenu> repoMenu;
        public UserService(IRepository<User> _user, IRepository<FoodbankMenu> _repoMenu)
        {
            repoUser = _user;
            repoMenu = _repoMenu;
        }
        public User GetUser(int id)
        {
            return repoUser.Query().Filter(u => u.UserId == id).Include(ur => ur.UserRole).Include(ur => ur.UserRole.Role).Include(ur => ur.Person).Include(ur => ur.Person.DonorFoodbank).Include(x=>x.Fbaddress).Get().FirstOrDefault();
        }
        public List<User> GetUser(string userName, string Password)
        {
            return repoUser.Query().Include(r => r.UserRole).Filter(u => u.UserName == userName && u.Active).Include(ud => ud.MmouserDataAccessibility).Get().Where(x => x.Password == EncryptionUtils.HashPassword(Password, x.PasswordSalt, x.LastLoginDate)).ToList();
        }
        public User DemoGetUser(string userName)
        {
            return repoUser.Query().Include(r => r.UserRole).Filter(u => u.UserName == userName && u.Active && u.IsMmo).Include(ud => ud.MmouserDataAccessibility).Get().FirstOrDefault();
        }
        public User GetUserEntityByUserName(string username)
        {
            return repoUser
             .Query()
             .Filter(u => u.UserName == username && u.Active)
             .Get()
             .FirstOrDefault();
        }
        public void Save(User user, int? CreatedBy, bool isNew = true)
        {
            if (isNew)
            {
                user.CreatedBy = CreatedBy;
                repoUser.InsertGraph(user);
            }
            else
            {
                repoUser.Update(user);
            }
        }

        public GraphAccessDto GetMSGraphApiAccess(int id)
        {
            GraphAccessDto model = new GraphAccessDto();
            var user = repoUser.Query().Filter(u => u.UserId == id).
                Include(ur => ur.Charity).
                Include(ur => ur.CentralOfficeNavigation)
                .Get().FirstOrDefault();

            var charity = user.Charity;
            if (charity != null && charity.TenantId != null)
            {
                model.TenantId = charity.TenantId;
            }
            else
            {
                var centraloffice = user.CentralOfficeNavigation;
                if (centraloffice != null && centraloffice.TenantId != null)
                {
                    model.TenantId = centraloffice.TenantId;
                }
            }

            return model;
        }

        public List<FoodbankMenu> GetMenus(int id)
        {
            var menuIds = repoUser.Query().Filter(u => u.UserId == id && u.Active)
                .Include(u => u.UserRole)
                .Include(ur => ur.UserRole.Role)
                .Include(ur => ur.UserRole.Role.FoodbankRoleMenuPrivilege)
                .Get().SelectMany(u => u.UserRole.Role.FoodbankRoleMenuPrivilege).Select(s => s.MenuId).ToList();

            return repoMenu.Query().Filter(m => menuIds.Contains(m.MenuId) && m.ShowOnMenu).Get().ToList();
        }

        public UserLiteDto GetUserByUserName(string username)
        {
            UserLiteDto UserEntity = new UserLiteDto();
            var userModel = repoUser.Query().Filter(u => u.UserName == username).Include(inc => inc.UserRole).Get().FirstOrDefault();
            if (userModel != null)
            {
                UserEntity.UserID = userModel.UserId;
                UserEntity.UserName = userModel.UserName;
                UserEntity.Email = userModel.Email;
                UserEntity.RoleID = userModel.UserRole == null ? 0 : userModel.UserRole.RoleId;
                UserEntity.Password = userModel.Password;
                UserEntity.PasswordSalt = userModel.PasswordSalt;
                UserEntity.FirstName = userModel.FirstName;
                UserEntity.PasswordQuestion = userModel.PasswordQuestion;
                UserEntity.PasswordAnswer = userModel.PasswordAnswer;
            }
            return UserEntity;
        }
        public UserLiteDto GetUserByUserNameOrEmail(string userName, string email)
        {
            UserLiteDto UserEntity = new UserLiteDto();
            var userModel = repoUser.Query().Filter(u => u.UserName == userName || u.Email == email).Include(inc => inc.UserRole).Get().FirstOrDefault();
            if (userModel != null)
            {
                UserEntity.UserID = userModel.UserId;
                UserEntity.UserName = userModel.UserName;
                UserEntity.Email = userModel.Email;
                UserEntity.RoleID = userModel.UserRole == null ? 0 : userModel.UserRole.RoleId;
                UserEntity.Password = userModel.Password;
                UserEntity.PasswordSalt = userModel.PasswordSalt;
                UserEntity.FirstName = userModel.FirstName;
                UserEntity.PasswordQuestion = userModel.PasswordQuestion;
                UserEntity.PasswordAnswer = userModel.PasswordAnswer;
            }
            return UserEntity;
        }
        
        public List<User> GetMultiUser(string userName, string EncryptPassword)
        {
            return repoUser
          .Query()
          .Filter(u => u.UserName == userName && u.Password == EncryptPassword)
          .Get()
          .ToList();
        }
        /// <summary>
        /// To check username exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsUserNameExist(string userName, int userId = 0)
        {
            return repoUser.Query().Filter(u => u.UserName == userName && u.UserId != userId).Get().Count() > 0 ? true : false;
        }
        public KeyValuePair<int, List<UserDto>> GetUsersForFoodbank(DataTableServerSide searchModel, int CreatedBy, int roleId, int? FoodbankId = null)
        {
            var predicate = CustomPredicate.BuildPredicate<User>(searchModel);
            predicate = predicate.And(u => u.IsFoodbank == true && u.PersonId == null);
            predicate = predicate.And(u => u.FoodbankId == FoodbankId && u.UserRole.RoleId!= (int)UserRoles.Foodbank);
           
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);
            var userlist = repoUser
                 .Query()
                 .Filter(predicate)
                 .Include(ur => ur.UserRole)
                 .Include(o => o.CentralOfficeNavigation)
                 .Include(c => c.Charity)
                 .Include(c => c.Foodbank)
                 .CustomOrderBy(u => u.OrderBy(searchModel))
                 .GetPage(page, searchModel.length, out int totalCount).ToList();
            return new KeyValuePair<int, List<UserDto>>(totalCount, UserDtoMapper.Map(userlist));
        }
        public void Delete(int id)
        {
            repoUser.Delete(id);
        }

        public void Dispose()
        {
            if (repoUser != null)
            {
                repoUser.Dispose();
                repoUser = null;
            }
        }
    }
}
