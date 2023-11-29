using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class BankTemplateMaster
    {
        public BankTemplateMaster()
        {
            BankStatementTemplate = new HashSet<BankStatementTemplate>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string TemplateName { get; set; }
        public int ColumnNumber { get; set; }
        public int RowNumber { get; set; }
        public string CellAddress { get; set; }
        public int CentralOfficeId { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BankStatementTemplate> BankStatementTemplate { get; set; }
    }
}
