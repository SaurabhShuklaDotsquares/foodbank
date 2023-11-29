using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class GroupGroupType
    {
        public int GroupGroupTypeId { get; set; }
        public int GroupTypeId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual GroupType GroupType { get; set; }
    }
}
