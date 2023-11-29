using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class OrganisationPreference
    {
        public int Id { get; set; }
        public int OrganisationId { get; set; }
        public int Years { get; set; }
        public bool UnclaimedDonations { get; set; }
        public bool ClaimedDonations { get; set; }
        public bool InactiveDonors { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
