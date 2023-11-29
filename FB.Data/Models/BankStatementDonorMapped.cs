using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class BankStatementDonorMapped
    {
        public BankStatementDonorMapped()
        {
            BankStatementDonorMappedElement = new HashSet<BankStatementDonorMappedElement>();
        }

        public int Id { get; set; }
        public int BankStatementTemplateId { get; set; }
        public string MappedText { get; set; }
        public int MappedDonor { get; set; }
        public int MappedPurpose { get; set; }
        public int? MappedMethod { get; set; }
        public bool MappedClaimTax { get; set; }
        public int UserId { get; set; }

        public virtual BankStatementTemplate BankStatementTemplate { get; set; }
        public virtual Person MappedDonorNavigation { get; set; }
        public virtual Method MappedMethodNavigation { get; set; }
        public virtual Purpose MappedPurposeNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BankStatementDonorMappedElement> BankStatementDonorMappedElement { get; set; }
    }
}
