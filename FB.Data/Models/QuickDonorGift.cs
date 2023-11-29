using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class QuickDonorGift
    {
        public int QuickAddId { get; set; }
        public int CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public int PurposeId { get; set; }
        public int? MethodId { get; set; }
        public string Title { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string Postcode { get; set; }
        public int? PersonTypeId { get; set; }
        public string Reference { get; set; }
        public string Comment { get; set; }
        public DateTime DonationDate { get; set; }
        public bool ClaimTax { get; set; }
        public bool LimitDeclaration { get; set; }
        public decimal Amount { get; set; }
        public bool? Transfer { get; set; }
        public string BatchReference { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public byte? Gender { get; set; }
        public string HouseName { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? PersonId { get; set; }
        public string City { get; set; }
        public bool? Overseas { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Method Method { get; set; }
        public virtual PersonType PersonType { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual User User { get; set; }
    }
}
