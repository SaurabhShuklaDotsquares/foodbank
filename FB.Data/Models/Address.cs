using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            Mmovisit = new HashSet<Mmovisit>();
        }

        public int AddressId { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public int? CountryId { get; set; }
        public string Postcode { get; set; }
        public bool? Active { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public int? AddressType { get; set; }
        public int? HouseholdId { get; set; }
        public int? PersonId { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }
        public string OtherAddressLine { get; set; }
        public string Mmodescripton { get; set; }
        public byte? MmoaddressType { get; set; }
        public bool MmoisMailing { get; set; }
        public bool MmoisExDirectory { get; set; }
        public bool IsMmo { get; set; }
        public string Mccpkaddress { get; set; }

        public virtual AddressType AddressTypeNavigation { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Country Country { get; set; }
        public virtual Household Household { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Mmovisit> Mmovisit { get; set; }
    }
}
