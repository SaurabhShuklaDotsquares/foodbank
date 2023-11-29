using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ConnectedCharity
    {
        public ConnectedCharity()
        {
            Charity = new HashSet<Charity>();
            CharityConnectedCharity = new HashSet<CharityConnectedCharity>();
        }

        public int ConnectedCharityId { get; set; }
        public string Description { get; set; }
        public string Hmrcreference { get; set; }
        public int? CentralOfficeId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity CharityNavigation { get; set; }
        public virtual ICollection<Charity> Charity { get; set; }
        public virtual ICollection<CharityConnectedCharity> CharityConnectedCharity { get; set; }
    }
}
