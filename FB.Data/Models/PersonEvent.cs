using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PersonEvent
    {
        public int PersonEventId { get; set; }
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public string PersonSponsored { get; set; }
        public string Comment { get; set; }

        public virtual Event Event { get; set; }
        public virtual Person Person { get; set; }
    }
}
