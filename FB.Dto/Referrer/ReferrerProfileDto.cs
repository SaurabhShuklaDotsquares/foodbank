using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class ReferrerProfileDto
    {
        public int ReferrerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReferrerName { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        
        [DisplayName("Email")]
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Profession { get; set; }
        public int ProfessionId { get; set; }
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
        //End

        //Change Password Section
        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }

        [DisplayName("Password")]
        public string EditPassword { get; set; }
        public string OldPostCode { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        [DisplayName("Change Password")]
        public bool IsChangePassword { get; set; }
        public bool ChangePassword { get; set; }
        //End



    }
}
