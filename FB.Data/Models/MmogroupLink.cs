using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmogroupLink
    {
        public int GroupLinkId { get; set; }
        public int? GroupId { get; set; }
        public int? GroupTypeId { get; set; }

        public virtual Mmogroup Group { get; set; }
        public virtual MmogroupType GroupType { get; set; }
    }
}
