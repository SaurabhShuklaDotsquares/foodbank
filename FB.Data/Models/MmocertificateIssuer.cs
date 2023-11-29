using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmocertificateIssuer
    {
        public MmocertificateIssuer()
        {
            MmomemberCertificate = new HashSet<MmomemberCertificate>();
        }

        public int CertificateIssuerId { get; set; }
        public string IssuerCompanyName { get; set; }
        public string IssuerCompanyAddress { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpksecuritycheckcode { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public int? CountryId { get; set; }
        public string Postcode { get; set; }
        public string OtherAddressLine { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmomemberCertificate> MmomemberCertificate { get; set; }
    }
}
