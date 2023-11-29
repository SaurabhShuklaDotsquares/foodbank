using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class StripePayment
    {
        public int StripePaymentId { get; set; }
        public int DonationId { get; set; }
        public string CardId { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
        public string FailureMessage { get; set; }
        public string TransactionId { get; set; }

        public virtual Donation Donation { get; set; }
    }
}
