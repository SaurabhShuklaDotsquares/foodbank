using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class AgenciesDto
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int FoodBankId { get; set; }
        public string FoodBankName { get; set; }
        public int ContactId { get; set; }
        public string ContactNumber { get; set; }
        public string BriefSummary { get; set; }

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
    public class AgenciesFamilyDto
    {
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int FoodBankId { get; set; }
        public string FoodBankName { get; set; }
        public int ContactId { get; set; }
        public string ContactNumber { get; set; }
        public string BriefSummary { get; set; }

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
        public int Familyid { get; set; }
        public List<Agencies> Agencieslist { get; set; }
        //End
    }
}
