using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Available
    {
        public int AvailableId { get; set; }
        public int? PersonId { get; set; }
        public int? Availability { get; set; }

        public virtual Availability AvailabilityNavigation { get; set; }
        public virtual Person Person { get; set; }
    }
}
