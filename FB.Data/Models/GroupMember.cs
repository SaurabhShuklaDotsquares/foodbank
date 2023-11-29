using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GroupMember
    {
        public int GroupId { get; set; }
        public int PersonId { get; set; }
        public DateTime? DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public bool? Active { get; set; }
        public int GroupMemberId { get; set; }

        public virtual Person Group { get; set; }
        public virtual Person Person { get; set; }
    }
}
