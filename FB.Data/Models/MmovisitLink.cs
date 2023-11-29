using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmovisitLink
    {
        public int VisitLinkId { get; set; }
        public int VisitId { get; set; }
        public int PersonId { get; set; }
        public bool? IsVisitor { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Mmovisit Visit { get; set; }
    }
}
