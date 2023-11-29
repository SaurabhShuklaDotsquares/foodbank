using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ImportLogDetail
    {
        public long LogId { get; set; }
        public long? StatusId { get; set; }
        public string ErrorLog { get; set; }

        public virtual ImportLog Status { get; set; }
    }
}
