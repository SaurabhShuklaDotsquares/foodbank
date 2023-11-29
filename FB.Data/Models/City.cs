using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class City
    {
        public City()
        {
            UserPreference = new HashSet<UserPreference>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }
        public int? CentralOfficeId { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual County County { get; set; }
        public virtual ICollection<UserPreference> UserPreference { get; set; }
    }
}
