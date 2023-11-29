using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Licence
    {
        public int LicenceId { get; set; }
        public int CentralOfficeId { get; set; }
        public string LicenceNumber { get; set; }
        public bool IsAssigned { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
    }
}
