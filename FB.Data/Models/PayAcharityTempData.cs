using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PayAcharityTempData
    {
        public int PactempId { get; set; }
        public string PayReference { get; set; }
        public string PacReference { get; set; }
        public string PayCharityId { get; set; }
        public string TransactionReference { get; set; }
        public int BranchId { get; set; }
        public string UserHash { get; set; }
        public string SecretKey { get; set; }
        public decimal? Amount { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string OtherAddressLine { get; set; }
        public bool IsGiftAid { get; set; }
        public int? CountryId { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string District { get; set; }
        public int MethodId { get; set; }
        public int PurposeId { get; set; }
        public int? PersonId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Email { get; set; }
        public string ReturnUrlForLink { get; set; }
        public decimal? TransactionFeeAmount { get; set; }
    }
}
