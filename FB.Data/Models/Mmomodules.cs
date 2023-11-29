using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmomodules
    {
        public int ModulesId { get; set; }
        public int CentralOfficeId { get; set; }
        public bool IsMembers { get; set; }
        public bool IsSkills { get; set; }
        public bool IsGroup { get; set; }
        public bool IsRotas { get; set; }
        public bool IsVisits { get; set; }
        public bool IsMapping { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
    }
}
