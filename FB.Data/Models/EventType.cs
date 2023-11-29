using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class EventType
    {
        public EventType()
        {
            Event = new HashSet<Event>();
        }

        public int EventTypeId { get; set; }
        public string EventTypeDescription { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
