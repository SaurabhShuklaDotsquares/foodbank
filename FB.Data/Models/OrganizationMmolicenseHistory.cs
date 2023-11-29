using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class OrganizationMmolicenseHistory
    {
        public int UpdateLicenseHistoryId { get; set; }
        public int? CentralOfficeId { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
    }
}
