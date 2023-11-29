using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GranterReceipt
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime AddedDate { get; set; }
        public decimal? Total { get; set; }
        public bool? CreatedInMfo { get; set; }
        public string ReceiptImage { get; set; }
        public int GranterId { get; set; }

        public virtual Granter Granter { get; set; }
    }
}
