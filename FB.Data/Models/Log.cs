using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? LogDate { get; set; }
        public string LogEntry { get; set; }
        public decimal? ErrorNumber { get; set; }
        public string ErrorDescription { get; set; }
        public string CurrentPage { get; set; }

        public virtual User User { get; set; }
    }
}
