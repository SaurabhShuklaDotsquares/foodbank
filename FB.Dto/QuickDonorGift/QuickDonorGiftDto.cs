using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace FB.Dto
{
    public class QuickDonorGiftDto
    {
        public int QuickAddID { get; set; }

        [DisplayName("Organisation")]
        public int CentralOfficeID { get; set; }

        [DisplayName("Charity")]
        public int? CharityID { get; set; }

        [DisplayName("Branch")]
        public int? BranchID { get; set; }

        [DisplayName("Purpose")]
        public int PurposeID { get; set; }

        [DisplayName("Lock Purpose")]
        public bool IsPurposeLocked { get; set; }

        [DisplayName("Method")]
        public int? MethodID { get; set; }

        [DisplayName("Lock Charity")]
        public bool IsCharityLocked { get; set; }

        [DisplayName("Lock Branch")]
        public bool IsBranchLocked { get; set; }

        [DisplayName("Lock Method")]
        public bool IsMethodLocked { get; set; }

        public string Title { get; set; }

        [DisplayName("First Name(s)")]
        public string Forenames { get; set; }
        [DisplayName("Last name")]
        public string Surname { get; set; }

        [DisplayName("House Number")]
        public string HouseNumber { get; set; }


        [DisplayName("House Name")]
        public string HouseName { get; set; }


        [DisplayName("Street Name")]
        public string StreetName { get; set; }

        public string District { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

        [DisplayName("Donor Type")]
        public int? PersonTypeID { get; set; }

        [DisplayName("Lock Donor Type")]
        public bool IsPersonTypeLocked { get; set; }

        public string Reference { get; set; }

        public string Comment { get; set; }

        [DisplayName("Lock Comment")]
        public bool IsCommentLocked { get; set; }

        [DisplayName("Donation Date")]
        public string DonationDateString { get; set; }

        [DisplayName("Lock Date")]
        public bool IsDonationDateLocked { get; set; }

        public DateTime? DonationDate { get; set; }

        [DisplayName("Claim Tax")]
        public bool ClaimTax { get; set; }

        [DisplayName("Lock Claim Tax")]
        public bool IsClaimTaxLocked { get; set; }


        [DisplayName("Limit Declaration")]
        public bool LimitDeclaration { get; set; }

        [DisplayName("Lock Limit")]
        public bool IsLimitDeclarationLocked { get; set; }


        public decimal? Amount { get; set; }

        [DisplayName("Lock Amount")]
        public bool IsAmountLocked { get; set; }

        public bool? Transfer { get; set; }

        [DisplayName("Batch Reference")]
        public string BatchReference { get; set; }

        [DisplayName("Lock Batch")]
        public bool IsBatchReferenceLocked { get; set; }

        public int UserID { get; set; }

        public string Email { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }
        public string Password { get; set; }

        public string PasswordHidden { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        public Gender Gender { get; set; }


        public bool IsPasswordChange { get; set; }
        
        public int? PersonID { get; set; }

        [DisplayName("Address is Overseas")]
        public bool Overseas { get; set; }

        [DisplayName("Lock Overseas")]
        public bool IsOverseasLocked { get; set; }
    }
}
