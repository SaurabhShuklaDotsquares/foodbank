using FB.Core;
using System;
using System.ComponentModel;

namespace FB.Dto
{
    public class EditPersonDto
    {
        public OrganistionType branchType { get; set; }

        [DisplayName("MyGiving.Online")]
        public bool EditIsMGO { get; set; }
        [DisplayName("MyMembership")]
        public bool EditIsMMO { get; set; }
        public int UserId { get; set; }
        public int PersonID { get; set; }

        [DisplayName("Donor type")]
        public int? PersonTypeID { get; set; }

        [DisplayName("Name")]
        public string FullName { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string EditPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        [DisplayName("Address")]
        public string FullAddress { get; set; }

        [DisplayName("HMRC Address")]
        public string FullHMRCAddress { get; set; }

        [DisplayName("Organisation")]
        public string CentralOfficeName { get; set; }

        [DisplayName("Charity")]
        public string CharityName { get; set; }

        [DisplayName("Branch")]
        public string BranchName { get; set; }

        [DisplayName("Home Phone")]
        public string HomePhone { get; set; }

        [DisplayName("Ext Dir.")]
        public bool IsHomePhoneExt { get; set; }

        [DisplayName("Mobile Phone")]
        public string MobilePhone { get; set; }

        [DisplayName("Ext Dir.")]
        public bool IsMobilePhoneExt { get; set; }

        [DisplayName("Work Phone")]
        public string OfficePhone { get; set; }

        [DisplayName("Ext.")]
        public string OfficePhoneExt { get; set; }

        [DisplayName("Fax")]
        public string FAX { get; set; }

        public string Email { get; set; }

        [DisplayName("Tag")]
        public bool IsTagged { get; set; }

        [DisplayName("Method")]
        public int? MethodID { get; set; }

        [DisplayName("Purpose")]
        public int? PurposeID { get; set; }

        [DisplayName("Envelope #")]
        public int? EnvelopeID { get; set; }

        public string Reference { get; set; }

        public string Comment { get; set; }

        public Month? Anniversary { get; set; }

        [DisplayName("Claim tax default")]
        public bool IsDefaultClaimTax { get; set; }

        public DateTime? DateAdded { get; set; }

        [DisplayName("Added")]
        public string DateAddedString { get; set; }

        public bool Active { get; set; }
        public bool Deceased { get; set; }

        public DateTime DateModified { get; set; }

        [DisplayName("Modified")]
        public string DateModifiedString { get; set; }

        public string Suffix { get; set; }
        public string EditTitle { get; set; }
        public string PreferredGreeting { get; set; }
        public string Initials { get; set; }
        [DisplayName("First Name")]
        public string ForeName { get; set; }
        [DisplayName("Last Name")]
        public string Surname { get; set; }
        public string FamilyName { get; set; }
        public int charityId { get; set; }
        public int AuditUserId { get; set; }
        public string AuditIP { get; set; }
        public int CountryId { get; set; }

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

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }
        public string OldPostCode { get; set; }

        [DisplayName("Address")]
        public string HMRCAddress { get; set; }

        [DisplayName("Overseas")]
        public bool Overseas { get; set; }
        [DisplayName("Change Password")]
        public bool IsChangePassword { get; set; }
        public bool ChangePassword { get; set; }
    }

    public class PersonAddressViewModel
    {
        public int PersonID { get; set; }

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

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Postcode")]
        public string PostCode { get; set; }

        [DisplayName("Address")]
        public string HMRCAddress { get; set; }

        [DisplayName("Overseas")]
        public bool Overseas { get; set; }

        [DisplayName("Over-ride Address")]
        public bool HMRCAddressOverride { get; set; }

        [DisplayName("Organisation")]
        public int? CentralOfficeID { get; set; }

        public int AuditUserId { get; set; }
        public string AuditIP { get; set; }

        public Nullable<byte> MMOAddressType { get; set; }
        public string MMODescripton { get; set; }
    }
}
