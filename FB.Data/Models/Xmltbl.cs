using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Xmltbl
    {
        public int Id { get; set; }
        public string Ttl { get; set; }
        public string Fore { get; set; }
        public string Sur { get; set; }
        public string House { get; set; }
        public string Postcode { get; set; }
        public DateTime? DonDate { get; set; }
        public decimal? Total { get; set; }
        public int? ClaimId { get; set; }
        public int? CharityId { get; set; }
        public int? AuditId { get; set; }
    }
}
