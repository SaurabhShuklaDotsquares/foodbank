using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class BankStatementDonorMappedElement
    {
        public int Id { get; set; }
        public int DonorMappedId { get; set; }
        public int PurposeId { get; set; }
        public decimal Amount { get; set; }

        public virtual BankStatementDonorMapped DonorMapped { get; set; }
    }
}
