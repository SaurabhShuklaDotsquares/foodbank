using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class InitialContact
    {
        public InitialContact()
        {
            Person = new HashSet<Person>();
        }

        public int InitialContactId { get; set; }
        public int CharityId { get; set; }
        public string InitialContactDescription { get; set; }

        public virtual Charity Charity { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
