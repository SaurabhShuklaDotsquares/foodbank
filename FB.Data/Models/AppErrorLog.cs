using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AppErrorLog
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string LoginToken { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
