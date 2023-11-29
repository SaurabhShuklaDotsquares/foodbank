using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ReferrerRegisterDto
    {
        public int Id { get; set; }
        public bool IsChangePassword { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public int ProfessionId { get; set; }
        public int UserId { get; set; }
        public string OrganisationName { get; set; }
        public int ContactId { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string EditPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string ContactNumber { get; set; }
        public string PostCode { get; set; }
        public bool Overseas { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public int? CountryID { get; set; }
        public string StreetName { get; set; }
        public string OtherAddressLine { get; set; }
        public string City { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public string District { get; set; }
        public int CharityID { get; set; }
        public string BranchName { get; set; }
        public string FoodbankToken { get; set; }

    }
}
