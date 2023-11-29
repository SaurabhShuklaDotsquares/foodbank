using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class County
    {
        public County()
        {
            City = new HashSet<City>();
        }

        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int? CountryId { get; set; }
        public string Abbreviation { get; set; }
        public int? CentralOfficeId { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<City> City { get; set; }
    }
}
