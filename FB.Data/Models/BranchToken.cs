using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class BranchToken
    {
        public int BranchId { get; set; }
        public string Olgatoken { get; set; }

        public virtual Branch Branch { get; set; }
    }
}
