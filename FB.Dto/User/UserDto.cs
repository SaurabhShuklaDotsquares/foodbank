using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class UserDto
    {
        public int UserID { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [DisplayName("City")]
        public string CityName { get; set; }


        [DisplayName("Postcode")]
        public string Postcode { get; set; }

        public string Address { get; set; }
        public byte? Gender { get; set; }

        [DisplayName("Primary Mobile")]
        public string PrimaryMobile { get; set; }

        [DisplayName("Alternate Mobile")]
        public string AlternateMobile { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }

        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        public string Password { get; set; }


        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string PasswordSalt { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        public int RoleID { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        public int? StatusID { get; set; }
        public int? CentralOfficeID { get; set; }
        public string CentralOffice { get; set; }
        public int? CharityID { get; set; }
        public int? BranchID { get; set; }
        public int? PersonID { get; set; }
        public string Comment { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public string IP { get; set; }

        [DisplayName("Change Password")]
        public bool IsPasswordChange { get; set; }

        public RoleDto Role { get; set; }
        public int AudtiUserId { get; set; }

        public int CustomRoleID { get; set; }

        [DisplayName("MyGiving.Online")]
        public bool IsMGO { get; set; }
        [DisplayName("MyMembership")]
        public bool IsMMO { get; set; }
        [DisplayName("Foodbank")]
        public bool IsFoodbank { get; set; }
        public bool IsMMOSystemCreated { get; set; }
        public int? FoodbankId { get; set; }
        public int? CreatedBy { get; set; }

        public int? AuditUserId { get; set; }

        //public bool IsCentralOfficeOrCharityEnableForMSAPI { get; set; }
        public bool IsMS365EnableForCentralOfficeOrCharity { get; set; }
        [DisplayName("Is Team Manager")]
        public bool IsTeamManager { get; set; }
        public string HouseName { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public int? CountryId { get; set; }
        public int? FbAddressId { get; set; }
        public string OtherAddressLine { get; set; }
    }

    public class UserLoginDetailDto
    {
        public int UserId { get; set; }
        public int PersonID { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string PasswordSalt { get; set; }

    }

    public class MSUserDto
    {
        public int UserId { get; set; }
        public string MSObjectID { get; set; }
        public string MSUserPrincipalName { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Domain { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public bool IsPasswordChange { get; set; }
    }
}
