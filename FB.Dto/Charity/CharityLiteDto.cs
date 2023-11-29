using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class CharityLiteDto
    {
        public int CharityID { get; set; }
        public string CharityName { get; set; }
        public string Prefix { get; set; }
        public string OrganisationName { get; set; }
        public bool IsActive { get; set; }
        public byte? ClaimType { get; set; }
        public bool? IsMmosystemCreated { get; set; }
    }

    public class CharityMSAPIDto
    {
        public int CharityID { get; set; }
        [DisplayName("Charity")]
        public string CharityName { get; set; }
        public string Prefix { get; set; }
        public string OrganisationName { get; set; }
        public bool IsActive { get; set; }
        [DisplayName("Client Id")]
        public string ClientId { get; set; }
        [DisplayName("Tenant Id")]
        public string TenantId { get; set; }
        [DisplayName("Secret Code")]
        public string SecretCode { get; set; }
        public byte? ClaimType { get; set; }

        public bool IsCharityIndividually { get; set; }

        public string WarningMessage { get; set; }
        [DisplayName("Password")]
        public string ConfirmationPassword { get; set; }
    }

}
