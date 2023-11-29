using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmoimportLogDetail
    {
        public long LogId { get; set; }
        public long? StatusId { get; set; }
        public string ErrorLog { get; set; }

        public virtual MmoimportLog Status { get; set; }
    }
}
