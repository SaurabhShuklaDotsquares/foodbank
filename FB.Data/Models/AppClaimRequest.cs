using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AppClaimRequest
    {
        public int AppClaimId { get; set; }
        public DateTime LastGiftDate { get; set; }
        public int AppId { get; set; }
        public int CharityId { get; set; }
        public int? BranchId { get; set; }
        public Guid ConfirmCode { get; set; }
        public DateTime Created { get; set; }
        public int? ClaimId { get; set; }
        public decimal ExpectedToClaim { get; set; }

        public virtual App App { get; set; }
    }
}
