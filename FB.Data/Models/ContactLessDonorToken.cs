using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ContactLessDonorToken
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public string Token { get; set; }
        public int BranchId { get; set; }
        public int? UserId { get; set; }
        public byte? TokenSource { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DonorReceiptEmail { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Person Donor { get; set; }
        public virtual User User { get; set; }
    }
}
