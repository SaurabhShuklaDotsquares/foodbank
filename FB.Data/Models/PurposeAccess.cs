using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PurposeAccess
    {
        public int Id { get; set; }
        public int PurposeId { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Purpose Purpose { get; set; }
    }
}
