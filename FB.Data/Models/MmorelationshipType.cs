using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmorelationshipType
    {
        public MmorelationshipType()
        {
            InverseParentRelationshipType = new HashSet<MmorelationshipType>();
            MmorelationshipMember = new HashSet<MmorelationshipMember>();
        }

        public int RelationshipTypeId { get; set; }
        public string OwnerDescription { get; set; }
        public string RelatedDescription { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int? ParentRelationshipTypeId { get; set; }
        public string Mccpkrelcode { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual MmorelationshipType ParentRelationshipType { get; set; }
        public virtual ICollection<MmorelationshipType> InverseParentRelationshipType { get; set; }
        public virtual ICollection<MmorelationshipMember> MmorelationshipMember { get; set; }
    }
}
