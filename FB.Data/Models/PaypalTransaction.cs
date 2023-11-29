using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PaypalTransaction
    {
        public int Id { get; set; }
        public int DonationId { get; set; }
        public string TransactionId { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
    }
}
