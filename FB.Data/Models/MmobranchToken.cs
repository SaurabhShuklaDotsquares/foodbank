using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmobranchToken
    {
        public int BranchId { get; set; }
        public string Mmotoken { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
