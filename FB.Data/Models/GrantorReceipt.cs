using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GrantorReceipt
    {
        public int Id { get; set; }
        public DateTime TransectionDate { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool CreatedInMfo { get; set; }
        public string ReceiptImage { get; set; }
        public int GrantorId { get; set; }

        public virtual Grantor Grantor { get; set; }
    }
}
