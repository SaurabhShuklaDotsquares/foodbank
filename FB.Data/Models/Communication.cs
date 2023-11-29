using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Communication
    {
        public int CommunicationId { get; set; }
        public int? CommunicationType { get; set; }
        public string CommReference { get; set; }
        public bool? Private { get; set; }
        public bool? Active { get; set; }
        public int? CharityId { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? PersonId { get; set; }
        public int? BranchId { get; set; }
        public bool? Preferred { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual CommType CommunicationTypeNavigation { get; set; }
        public virtual Person Person { get; set; }
    }
}
