using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class BranchApp
    {
        public int Id { get; set; }
        public int? AppId { get; set; }
        public int? BranchId { get; set; }
        public Guid? ExternalId { get; set; }

        public virtual App App { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
