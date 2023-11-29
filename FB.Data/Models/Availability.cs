using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Availability
    {
        public Availability()
        {
            Available = new HashSet<Available>();
        }

        public int AvailabilityId { get; set; }
        public DateTime? DateAvailableFrom { get; set; }
        public DateTime? DateAvalableTo { get; set; }
        public bool? Active { get; set; }
        public bool? IsExcluded { get; set; }

        public virtual ICollection<Available> Available { get; set; }
    }
}
