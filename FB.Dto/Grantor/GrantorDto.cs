using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class GrantorDto
    {
        public int GrantorId { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string GrantorToken { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int FoodBankId { get; set; }
        public string FoodBankName { get; set; }
        public int ContactId { get; set; }
        public string ContactNumber { get; set; }
        public string TotalAmount { get; set; }

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
        public bool IsGrantorQrCode { get; set; }
        public string GrantorQrCode { get; set; }
        //End
    }
}
