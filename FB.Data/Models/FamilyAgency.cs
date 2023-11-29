using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FamilyAgency
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int AgencyId { get; set; }

        public virtual Agencies Agency { get; set; }
        public virtual Family Family { get; set; }
    }
}
