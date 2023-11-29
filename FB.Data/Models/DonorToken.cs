using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DonorToken
    {
        public int TokenId { get; set; }
        public string Token { get; set; }
        public string DonationRequest { get; set; }
        public bool IsExpire { get; set; }
        public DateTime TokenDate { get; set; }
    }
}
