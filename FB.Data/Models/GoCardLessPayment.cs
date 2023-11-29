using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GoCardLessPayment
    {
        public int GoCardLessPaymentId { get; set; }
        public int DonationId { get; set; }
        public string BillId { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }

        public virtual Donation Donation { get; set; }
    }
}
