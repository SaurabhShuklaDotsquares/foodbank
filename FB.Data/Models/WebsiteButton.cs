using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class WebsiteButton
    {
        public Guid ButtonId { get; set; }
        public byte ButtonType { get; set; }
        public int CharityId { get; set; }
        public int UserId { get; set; }
        public DateTime AddedDate { get; set; }
        public int? PurposeId { get; set; }
        public int? BranchId { get; set; }
        public byte[] Qrcode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Purpose Purpose { get; set; }
        public virtual User User { get; set; }
    }
}
