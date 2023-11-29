using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB.Core;
using FB.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FB.Data.Models;

namespace FB.Dto
{
    public class FamilyDTo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string FamilyName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FamilyToken { get; set; }
        public int? LocalAuthCodeId { get; set; }
        public string DeliveryNote { get; set; }
        public bool? Confirmed { get; set; }
        public int? ConfirmedById { get; set; }
        [DisplayName("Address is Overseas")]
        public bool Overseas { get; set; }
        public int? SelfReffered { get; set; }
        public int BranchID { get; set; }
        public string Email { get; set; }
        public string Allergies { get; set; }
        public string Contactno { get; set; }
        public int? Addressid { get; set; }
        public string TotalFamily { get; set; }
        public string TotalAdults { get; set; }
        public string TotalChild { get; set; }

        public List<PersonAddressDto> Addresses { get; set; }
        public List<string> subfamilyname { get; set; }
        public List<string> subfamilynameIds { get; set; }
        public List<string> subfamilydob { get; set; }
        public List<string> subfamilyisadult { get; set; }
        public string SubFamilyAllergries { get; set; }
        public string subfamilyisadult2 { get; set; }
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

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }
        public string OldPostCode { get; set; }


    }
    public class AddFamilyDto
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FamilyToken { get; set; }
        public int LocalAuthCodeId { get; set; }
        public string DeliveryNote { get; set; }
        public bool? Confirmed { get; set; }
        public int? ConfirmedById { get; set; }
        [DisplayName("Address is Overseas")]
        public bool Overseas { get; set; }
        public int? SelfReffered { get; set; }
        public int? ReferralId { get; set; }
        public string Email { get; set; }
        public string Allergies { get; set; }
        public string Contactno { get; set; }
        public int? Addressid { get; set; }
        public string TotalFamily { get; set; }
        public string TotalAdults { get; set; }
        public string TotalChild { get; set; }
        public int? FoodbankId { get; set; }
        public List<PersonAddressDto> Addresses { get; set; }
        public List<string> subfamilyname { get; set; }
        public List<string> subfamilydob { get; set; }
        public List<string> subfamilyisadult { get; set; }
        public string subfamilyisadult2 { get; set; }
        public string SubFamilyAllergries { get; set; }
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

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }
        public string OldPostCode { get; set; }


        public DateTime ParcelDeliver { get; set; }
        public int OtherAgency { get; set; }
        public string GDPRPreferences { get; set; }
        public string VoucherNumber { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int CharityID { get; set; }
        public int CentralOfficeID { get; set; }
    }
    public class EditFamilyDto
    {
        public EditFamilyDto()
        {
            subfamilyname = new List<string>();
            subfamilydob = new List<string>();
            subfamilyisadult = new List<string>();
            subfamilynameIds = new List<string>();
            SubFamilyAllergryarry = new List<string>();
            SubFamilyAllergryarryId = new List<string>();
        }
        

        public List<Agencies> Agencieslist { get; set; }
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FamilyToken { get; set; }
        public string SelfRefferedName { get; set; }
        public int? LocalAuthCodeId { get; set; }
        public string DeliveryNote { get; set; }
        public bool? Confirmed { get; set; }
        public int? ConfirmedById { get; set; }
        [DisplayName("Address is Overseas")]
        public bool Overseas { get; set; }
        public int PersonID { get; set; }
        public int? SelfReffered { get; set; }
        public int? ReferralId { get; set; }
        public string Email { get; set; }
        public string Allergies { get; set; }
        public string Contactno { get; set; }
        public int? Addressid { get; set; }
        public string TotalFamily { get; set; }
        public string TotalAdults { get; set; }
        public string TotalChild { get; set; }
        public int? FoodbankId { get; set; }
        public string StatusName { get; set; }
        public List<PersonAddressDto> Addresses { get; set; }
        public List<string> subfamilyname { get; set; }
        public List<string> subfamilynameIds { get; set; }
        public List<string> subfamilydob { get; set; }
        public List<string> subfamilyisadult { get; set; }
        public string subfamilyisadult2 { get; set; }
        public string SubFamilyAllergries { get; set; }
        public List<string> SubFamilyAllergryarry { get; set; }
        public List<string> SubFamilyAllergryarryId { get; set; }
        public bool Active { get; set; }
        public string CentralOfficeName { get; set; }
        public string CharityName { get; set; }
        

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

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }
        public string OldPostCode { get; set; }


        public string ParcelDeliverDate { get; set; }
        public DateTime ParcelDeliver { get; set; }
        public int OtherAgency { get; set; }
        public string OtherAgencyName { get; set; }
        public string GDPRPreferences { get; set; }
        public string VoucherNumber { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int CharityID { get; set; }
        public int CentralOfficeID { get; set; }
    }
    public class AdminEditFamilyDto
    {
        public AdminEditFamilyDto()
        {
            subfamilyname = new List<string>();
            subfamilydob = new List<string>();
            subfamilyisadult = new List<string>();
            subfamilynameIds = new List<string>();
            SubFamilyAllergryarry = new List<string>();
            SubFamilyAllergryarryId = new List<string>();
        }
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FamilyToken { get; set; }
        public string LocalAuthCodeId { get; set; }
        public string DeliveryNote { get; set; }
        public bool? Confirmed { get; set; }
        public int? ConfirmedById { get; set; }
        [DisplayName("Address is Overseas")]
        public bool Overseas { get; set; }
        public string SelfRefferedName { get; set; }
        public int PersonID { get; set; }
        public string OtherAgencyName { get; set; }
        public int? SelfReffered { get; set; }
        public int? ReferralId { get; set; }
        public string Email { get; set; }
        public string Allergies { get; set; }
        public string Contactno { get; set; }
        public int? Addressid { get; set; }
        public string TotalFamily { get; set; }
        public string TotalAdults { get; set; }
        public string TotalChild { get; set; }
        public int? FoodbankId { get; set; }
        public List<PersonAddressDto> Addresses { get; set; }
        public List<string> subfamilyname { get; set; }
        public List<string> subfamilynameIds { get; set; }
        public List<string> subfamilydob { get; set; }
        public List<string> subfamilyisadult { get; set; }
        public string subfamilyisadult2 { get; set; }
        public string SubFamilyAllergries { get; set; }
        public List<string> SubFamilyAllergryarry { get; set; }
        public List<string> SubFamilyAllergryarryId { get; set; }
        public bool Active { get; set; }
        public string CentralOfficeName { get; set; }
        public string CharityName { get; set; }


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

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }
        public string OldPostCode { get; set; }


        public string ParcelDeliverDate { get; set; }
        public int OtherAgency { get; set; }
        public string GDPRPreferences { get; set; }
        public string VoucherNumber { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int CharityID { get; set; }
        public int CentralOfficeID { get; set; }
    }
    
}
