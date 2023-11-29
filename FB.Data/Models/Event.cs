using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Event
    {
        public Event()
        {
            Donation = new HashSet<Donation>();
            PersonEvent = new HashSet<PersonEvent>();
        }

        public int EventId { get; set; }
        public int? CharityId { get; set; }
        public int? EventTypeId { get; set; }
        public string EventDescription { get; set; }
        public string EventLocation { get; set; }
        public int? ContactPerson { get; set; }
        public bool? GiftAided { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventName { get; set; }
        public string EventReference { get; set; }

        public virtual Charity Charity { get; set; }
        public virtual EventType EventType { get; set; }
        public virtual ICollection<Donation> Donation { get; set; }
        public virtual ICollection<PersonEvent> PersonEvent { get; set; }
    }
}
