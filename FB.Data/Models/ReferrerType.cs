using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ReferrerType
    {
        public ReferrerType()
        {
            Referrers = new HashSet<Referrers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Referrers> Referrers { get; set; }
    }
}
