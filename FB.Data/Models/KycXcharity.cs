using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class KycXcharity
    {
        public int KycId { get; set; }
        public int CharityId { get; set; }

        public virtual Charity Charity { get; set; }
    }
}
