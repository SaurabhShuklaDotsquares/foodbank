using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto.Foodbank
{
    public class UpdateProfileDto
    {
        public int FoodBankId { get; set; }
        [DisplayName("Food Name")]
        public string FoodName { get; set; }
        public string ContactNumber { get; set; }
        public int AddressId { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        [DisplayName("Organisation")]
        public int? CentralOfficeID { get; set; }
        [DisplayName("Charity")]
        public int? CharityID { get; set; }
        [DisplayName("Branch")]
        public int? BranchID { get; set; }
        public string BranchIDs { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Change Password")]
        public bool IsChangePassword { get; set; }
        public bool ChangePassword { get; set; }

        [DisplayName("Password")]
        public string EditPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        //Adderess Section
        public int AddressID { get; set; }
        [DisplayName("House Name")]
        public string HouseName { get; set; }

        [DisplayName("House Number")]
        public string HouseNumber { get; set; }

        [DisplayName("Street Name")]
        public string StreetName { get; set; }

        [DisplayName("Other Address Line")]
        public string OtherAddressLine { get; set; }

        [DisplayName("District")]
        public string District { get; set; }

        [DisplayName("Country")]
        public int? CountryID { get; set; }

        [DisplayName("Overseas")]
        public bool Overseas { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }
        public string OldPostCode { get; set; }
        public string CountryName { get; set; }
        //End
    }
    public class DashboardDto
    {
        public int Id { get; set; }
        
        public DateTime? Date { get; set; }
       
        //End
    }

    public class FamilyPersonDto
    {
        public List<int> Age { get; set; }

        //End
    }
}
