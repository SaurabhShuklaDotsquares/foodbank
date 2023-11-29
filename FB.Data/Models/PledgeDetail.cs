using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PledgeDetail
    {
        public int PledgeId { get; set; }
        public int PersonId { get; set; }
        public decimal? PledgeAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte PledgeGivingType { get; set; }
        public int? PurposeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Purpose Purpose { get; set; }
    }
}
