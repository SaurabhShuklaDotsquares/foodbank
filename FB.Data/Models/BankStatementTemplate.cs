using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class BankStatementTemplate
    {
        public BankStatementTemplate()
        {
            BankStatementDonorMapped = new HashSet<BankStatementDonorMapped>();
        }

        public int Id { get; set; }
        public int BankTemplateMasterId { get; set; }
        public int UserId { get; set; }
        public string TempColumn { get; set; }
        public string MappedColumn { get; set; }
        public bool? IsShow { get; set; }

        public virtual BankTemplateMaster BankTemplateMaster { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BankStatementDonorMapped> BankStatementDonorMapped { get; set; }
    }
}
