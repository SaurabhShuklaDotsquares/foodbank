using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Profession
    {
        public int ProfessionId { get; set; }
        public string Title { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? FoodbankId { get; set; }

        public virtual Foodbank Foodbank { get; set; }
    }
}
