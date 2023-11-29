using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FailedBtdonationEmailDetail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int? BranchId { get; set; }
        public int? CharityId { get; set; }
        public DateTime? FiledDate { get; set; }
        public int? FailedAttempt { get; set; }
        public string Ip { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Charity Charity { get; set; }
    }
}
