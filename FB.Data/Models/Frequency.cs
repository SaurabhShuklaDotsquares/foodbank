using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Frequency
    {
        public Frequency()
        {
            Donation = new HashSet<Donation>();
            Meeting = new HashSet<Meeting>();
            Task = new HashSet<Task>();
        }

        public int FrequencyId { get; set; }
        public string FrequencyDescription { get; set; }
        public short? OccursEvery { get; set; }
        public bool? Weekend { get; set; }

        public virtual ICollection<Donation> Donation { get; set; }
        public virtual ICollection<Meeting> Meeting { get; set; }
        public virtual ICollection<Task> Task { get; set; }
    }
}
