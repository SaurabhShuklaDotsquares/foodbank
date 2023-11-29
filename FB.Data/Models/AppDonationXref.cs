using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AppDonationXref
    {
        public int AppDonationId { get; set; }
        public int AppId { get; set; }
        public int DonationId { get; set; }

        public virtual App App { get; set; }
        public virtual Donation Donation { get; set; }
    }
}
