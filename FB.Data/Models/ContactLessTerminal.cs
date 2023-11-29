using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ContactLessTerminal
    {
        public ContactLessTerminal()
        {
            ContactLessSchedule = new HashSet<ContactLessSchedule>();
        }

        public int Id { get; set; }
        public string TerminalId { get; set; }
        public int? MerchantId { get; set; }
        public int? BranchId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CharityId { get; set; }
        public bool? AutoImportDonation { get; set; }
        public DateTime? LastImportDate { get; set; }
        public DateTime? StartedImportDate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<ContactLessSchedule> ContactLessSchedule { get; set; }
    }
}
