using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ExceptionLog
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string PageName { get; set; }
        public int? OrganisationId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public int? UserId { get; set; }
        public string Exception { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual CentralOffice Organisation { get; set; }
        public virtual User User { get; set; }
    }
}
