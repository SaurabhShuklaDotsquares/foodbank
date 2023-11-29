using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DonationType
    {
        public DonationType()
        {
            Donation = new HashSet<Donation>();
        }

        public int DonationTypeId { get; set; }
        public string DonationTypeDescription { get; set; }

        public virtual ICollection<Donation> Donation { get; set; }
    }
}
