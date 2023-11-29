using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmorelationshipMember
    {
        public int RelationshipMemberId { get; set; }
        public int RelationshipTypeId { get; set; }
        public int OwnerId { get; set; }
        public int RelatedId { get; set; }
        public bool IsOwnerParent { get; set; }
        public bool IsRelatedParent { get; set; }
        public bool Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Person Owner { get; set; }
        public virtual Person Related { get; set; }
        public virtual MmorelationshipType RelationshipType { get; set; }
    }
}
