using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public int AddressTypeId { get; set; }
        public string AddressTypeDescription { get; set; }
        public string Abbreviation { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
