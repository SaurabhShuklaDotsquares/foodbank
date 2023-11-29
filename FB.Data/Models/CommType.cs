using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CommType
    {
        public CommType()
        {
            Communication = new HashSet<Communication>();
        }

        public int CommTypeId { get; set; }
        public string CommTypeDescription { get; set; }

        public virtual ICollection<Communication> Communication { get; set; }
    }
}
