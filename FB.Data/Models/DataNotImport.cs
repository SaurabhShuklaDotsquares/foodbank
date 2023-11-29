using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DataNotImport
    {
        public int Id { get; set; }
        public int DataImportLogId { get; set; }
        public string TableName { get; set; }
        public string TableRowPk { get; set; }
        public string Reason { get; set; }
        public int CentralOfficeId { get; set; }
        public int CharityId { get; set; }
        public int BranchId { get; set; }

        public virtual DataImportLog DataImportLog { get; set; }
    }
}
