using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PersonCheck
    {
        public int PersonCheckId { get; set; }
        public int PersonId { get; set; }
        public int CheckedId { get; set; }

        public virtual Checked Checked { get; set; }
        public virtual Person Person { get; set; }
    }
}
