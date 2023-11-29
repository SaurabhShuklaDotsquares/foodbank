using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MethodAccess
    {
        public int Id { get; set; }
        public int MethodId { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Method Method { get; set; }
    }
}
