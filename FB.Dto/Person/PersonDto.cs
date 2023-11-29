using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB.Core;
using FB.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace FB.Dto
{
    public class PersonDto
    {
        public PersonDto()
        {
            Addresses = new List<PersonAddressDto>();
        }

        [DisplayName("MyGiving.Online")]
        public bool IsMGO { get; set; }
        [DisplayName("MyMembership")]
        public bool IsMMO { get; set; }

        public int PersonID { get; set; }

        public int? HouseholdID { get; set; }

        [DisplayName("Donor Type")]
        public int? PersonTypeID { get; set; }

        [DisplayName("Organisation")]
        public int? CentralOfficeID { get; set; }

        [DisplayName("Charity")]
        public int? CharityID { get; set; }

        [DisplayName("Branch")]
        public int? BranchID { get; set; }

        public string BranchIDs { get; set; }
        public string BranchName { get; set; }
        public string FoodbankToken { get; set; }
        public int FoodbankId { get; set; }
        public string Title { get; set; }

        [DisplayName("First Name(s)")]
        public string Forenames { get; set; }
        [DisplayName("Last name")]
        public string Surname { get; set; }

        public string Initials { get; set; }

        public string Suffix { get; set; }

        public Gender? Gender { get; set; }

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

        [DisplayName("Greetings")]
        public string PreferredGreeting { get; set; }

        [DisplayName("Method")]
        public int? DefaultMethodID { get; set; }

        [DisplayName("Purpose")]
        public int? DefaultPurposeID { get; set; }

        [DisplayName("Envelope #")]
        public int? DefaultEnvelopeID { get; set; }


        public decimal? DefaultAmount { get; set; }

        public decimal? TotalRecieved { get; set; }

        public string Comment { get; set; }
        public Month? Anniversary { get; set; }

        [DisplayName("Declaration Date")]
        public DateTime? DateDeclarationMade { get; set; }

        [DisplayName("Valid From")]
        public DateTime? DeclarationValidFrom { get; set; }

        [DisplayName("Valid To")]
        public DateTime? DeclarationValidTo { get; set; }

        [DisplayName("Claim tax default")]
        public bool IsDefaultClaimTax { get; set; }

        [DisplayName("Calculate From")]
        public DateTime? CalculateDate { get; set; }

        [DisplayName("Added")]
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }
        public bool Deceased { get; set; }

        [DisplayName("Modified")]
        public DateTime DateModified { get; set; }

        public string Reference { get; set; }

        public string DonorReference { get; set; }

        public int? InitialContactSource { get; set; }


        [DisplayName("HMRC Address")]
        public string HMRCAddress { get; set; }

        [DisplayName("Address is Overseas")]
        public bool Overseas { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        public string ReferenceType { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}",
                    !string.IsNullOrEmpty(Title) ? Title : "",
                    !string.IsNullOrEmpty(Forenames) ? Forenames : "",
                    Surname, !string.IsNullOrWhiteSpace(Suffix) ? Suffix : ""
                    );
            }
        }

        [DisplayName("Church or Charity")]
        public string CentralOfficeName { get; set; }

        public int AuditUserId { get; set; }
        public string AuditIP { get; set; }
        public bool? IsNew { get; set; }

        [NotMapped]
        [DisplayName("Lock Charity")]
        public bool IsCharityLocked { get; set; }

        [SkipProperty]
        [ExcelProperty]
        [NotMapped]
        [DisplayName("BranchName")]
        public string Branch { get; set; }

        public List<PersonAddressDto> Addresses { get; set; }
}
}
