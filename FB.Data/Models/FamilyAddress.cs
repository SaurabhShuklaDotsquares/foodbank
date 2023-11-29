using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FamilyAddress
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int FamilyId { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual Fbaddress Address { get; set; }
        public virtual Family Family { get; set; }
    }
}
