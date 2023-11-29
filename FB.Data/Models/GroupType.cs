using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GroupType
    {
        public GroupType()
        {
            GroupGroupType = new HashSet<GroupGroupType>();
        }

        public int GroupTypeId { get; set; }
        public string GroupTypeDescription { get; set; }

        public virtual ICollection<GroupGroupType> GroupGroupType { get; set; }
    }
}
