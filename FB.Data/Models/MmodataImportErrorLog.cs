using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmodataImportErrorLog
    {
        public int LogId { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string TableName { get; set; }
        public string ErrorMsg { get; set; }
        public string InnerException { get; set; }
        public DateTime? LogDate { get; set; }
    }
}
