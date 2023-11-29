using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CharityConnectedCharity
    {
        public int CharityId { get; set; }
        public int ConnectedCharityId { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Charity Charity { get; set; }
        public virtual ConnectedCharity ConnectedCharity { get; set; }
    }
}
