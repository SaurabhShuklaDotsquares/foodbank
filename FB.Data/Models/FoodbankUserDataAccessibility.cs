using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodbankUserDataAccessibility
    {
        public int UserAccessId { get; set; }
        public int UserId { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }

        public virtual User User { get; set; }
    }
}
