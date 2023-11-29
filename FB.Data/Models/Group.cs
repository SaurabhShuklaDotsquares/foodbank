using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupGroupType = new HashSet<GroupGroupType>();
            Meeting = new HashSet<Meeting>();
        }

        public int GroupId { get; set; }
        public string GroupDescription { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateEnded { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<GroupGroupType> GroupGroupType { get; set; }
        public virtual ICollection<Meeting> Meeting { get; set; }
    }
}
