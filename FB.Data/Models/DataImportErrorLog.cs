using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DataImportErrorLog
    {
        public int LogId { get; set; }
        public int? CharityId { get; set; }
        public string TableName { get; set; }
        public string ErrorMsg { get; set; }
        public string InnerException { get; set; }
        public DateTime? LogDate { get; set; }
    }
}
