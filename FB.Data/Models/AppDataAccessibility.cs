using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AppDataAccessibility
    {
        public int Id { get; set; }
        public int? AppId { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }

        public virtual App App { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
    }
}
