using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PgsdonorContact
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int ContactId { get; set; }
        public int CentralOfficeId { get; set; }
        public int CharityId { get; set; }
        public int BranchId { get; set; }
        public int PurposeId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual Person Donor { get; set; }
        public virtual Purpose Purpose { get; set; }
    }
}
