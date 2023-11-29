using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PersonPaymentMethodInfo
    {
        public int PersonPaymentMethodInfoId { get; set; }
        public int? PersonId { get; set; }
        public string CharityApiKey { get; set; }
        public string RegistrationId { get; set; }
        public byte? PaymentType { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal? AccountBalance { get; set; }
        public DateTime? RegisteredOn { get; set; }

        public virtual Person Person { get; set; }
    }
}
