using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Granter
    {
        public Granter()
        {
            GranterReceipt = new HashSet<GranterReceipt>();
        }

        public int Id { get; set; }
        public string GranterName { get; set; }
        public string GranterToken { get; set; }
        public int ContactId { get; set; }

        public virtual Fbcontact Contact { get; set; }
        public virtual ICollection<GranterReceipt> GranterReceipt { get; set; }
    }
}
