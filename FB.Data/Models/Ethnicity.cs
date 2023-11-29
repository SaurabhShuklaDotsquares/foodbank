using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Ethnicity
    {
        public Ethnicity()
        {
            FamilyMember = new HashSet<FamilyMember>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FamilyMember> FamilyMember { get; set; }
    }
}
