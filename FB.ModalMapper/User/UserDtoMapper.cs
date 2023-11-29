using System;
using System.Collections.Generic;
using System.Text;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB.Data.Models;

namespace FB.ModalMapper
{
    public static class UserDtoMapper
    {
        public static UserDto Map(User user)
        {
            return new UserDto
            {
                UserID = user.UserId,
                UserName = user.UserName,
                IsActive = user.Active,
                AddedDate = user.AddedDate,
                Address = user.Address,
                AlternateMobile = user.AlternateMobile,
                AudtiUserId = user.AuditUserId ?? 0,
                CityName = user.CityName,
                Comment = user.Comment,
                Email = user.Email,
                FirstName = user.FirstName,
                Gender = user.Gender,
                IP = user.Ip,
                Landline = user.Landline,
                LastLoginDate = user.LastLoginDate,
                LastName = user.LastName,
                LastPasswordChange = user.LastPasswordChange,
                ModifiedDate = user.ModifiedDate,
                CentralOfficeID = user.CentralOfficeId,
                CharityID = user.CharityId,
                BranchID = user.BranchId,
                //CentralOffice = user.CentralOfficeId.HasValue?user..OrganisationName ?? "N/A",
                //Password = user.Password,
                //PasswordAnswer = user.PasswordAnswer,
                //PasswordQuestion = user.PasswordQuestion,
                //PasswordSalt = user.PasswordSalt,
                Postcode = user.Postcode,
                PrimaryMobile = user.PrimaryMobile,
                StatusID = user.StatusId,
                CreatedBy = user.CreatedBy,
                IsMGO = user.IsMgo ?? false,
                IsMMO = user.IsMmo,
                IsFoodbank = user.IsFoodbank,
                RoleID = user.UserRole.RoleId,
                FbAddressId = (user.Fbaddress.Count >0? user.Fbaddress.FirstOrDefault().Id:0),

            };
        }

        public static List<UserDto> Map(IEnumerable<User> users)
        {
            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                bool MSAPI = false;
                var charity = user.Charity;
                if (charity != null && charity.TenantId != null)
                {
                    MSAPI = true;
                }
                else
                {
                    var centraloffice = user.CentralOfficeNavigation;
                    if (centraloffice != null && centraloffice.TenantId != null)
                    {
                        MSAPI = true;
                    }
                }


                userDtos.Add(new UserDto
                {
                    UserID = user.UserId,
                    UserName = user.UserName,
                    IsActive = user.Active,
                    AddedDate = user.AddedDate,
                    Address = user.Address,
                    AlternateMobile = user.AlternateMobile,
                    AudtiUserId = user.AuditUserId ?? 0,
                    CityName = user.CityName,
                    Comment = user.Comment,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Gender = user.Gender,
                    IP = user.Ip,
                    Landline = user.Landline,
                    LastLoginDate = user.LastLoginDate,
                    LastName = user.LastName,
                    LastPasswordChange = user.LastPasswordChange,
                    ModifiedDate = user.ModifiedDate,
                    CentralOfficeID = user.CentralOfficeId,
                    CharityID = user.CharityId,
                    BranchID = user.BranchId,
                    //CentralOffice = user.CentralOfficeId.HasValue?user..OrganisationName ?? "N/A",
                    Password = user.Password,
                    PasswordAnswer = user.PasswordAnswer,
                    PasswordQuestion = user.PasswordQuestion,
                    PasswordSalt = user.PasswordSalt,
                    Postcode = user.Postcode,
                    PrimaryMobile = user.PrimaryMobile,
                    StatusID = user.StatusId,
                    RoleID = user.UserRole.RoleId,
                    CreatedBy = user.CreatedBy,
                    IsMGO = user.IsMgo ?? false,
                    IsMMO = user.IsMmo,
                    IsMMOSystemCreated = user.IsMmosystemCreated ?? false,
                    IsMS365EnableForCentralOfficeOrCharity = MSAPI,
                    IsFoodbank=user.IsFoodbank,
                    FoodbankId=user.FoodbankId
                });
            }

            return userDtos;
        }
        public static User Map(UserDto model, User user)
        {
            user.UserId = model.UserID;
            user.UserName = model.UserName;
            user.Active = model.IsActive;
            user.AddedDate = model.AddedDate ?? DateTime.Now;
            user.Address = model.Address;
            user.AlternateMobile = model.AlternateMobile;
            user.AuditUserId = model.AuditUserId;
            user.CityName = model.CityName;
            user.Comment = model.Comment;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.Gender = model.Gender ?? 1;
            user.Ip = model.IP;
            user.Landline = model.Landline;
            user.LastLoginDate = model.LastLoginDate ?? DateTime.Now;
            user.LastName = model.LastName;
            user.LastPasswordChange = model.LastPasswordChange ?? DateTime.Now;
            user.ModifiedDate = model.ModifiedDate ?? DateTime.Now;
            user.CentralOfficeId = model.CentralOfficeID;
            user.BranchId = model.BranchID;
            user.CharityId = model.CharityID;
            user.Password = model.Password;
            user.PasswordAnswer = model.PasswordAnswer;
            user.PasswordQuestion = model.PasswordQuestion;
            user.PasswordSalt = model.PasswordSalt;
            user.Postcode = model.Postcode;
            user.PrimaryMobile = model.PrimaryMobile;
            user.StatusId = model.StatusID;
            user.CreatedBy = model.CreatedBy;
            user.IsMgo = model.IsMGO;
            user.IsMmo = model.IsMMO;
            user.IsFoodbank = model.IsFoodbank;
            user.FoodbankId = model.FoodbankId;
            user.IsMmosystemCreated = model.IsMMOSystemCreated;
            user.IsTeamManager = model.IsTeamManager;
            
            return user;
        }

    }
}
