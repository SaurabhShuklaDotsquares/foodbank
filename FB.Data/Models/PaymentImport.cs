using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PaymentImport
    {
        public int Id { get; set; }
        public int DonationId { get; set; }
        public string CustomerId { get; set; }
        public string PaymentId { get; set; }
        public string CreatedDateString { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PaymentGateway { get; set; }
        public string CustomerName { get; set; }
        public string Amount { get; set; }

        public virtual Donation Donation { get; set; }
    }
}
