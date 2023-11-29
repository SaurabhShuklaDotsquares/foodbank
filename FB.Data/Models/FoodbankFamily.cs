using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodbankFamily
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int? FoodbankId { get; set; }
        public bool Inward { get; set; }
        public int? ReasonId { get; set; }

        public virtual Family Family { get; set; }
        public virtual Foodbank Foodbank { get; set; }
    }
}
