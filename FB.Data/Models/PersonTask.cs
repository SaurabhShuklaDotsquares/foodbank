using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PersonTask
    {
        public int PersonTaskId { get; set; }
        public int PersonId { get; set; }
        public int TaskId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Task Task { get; set; }
    }
}
