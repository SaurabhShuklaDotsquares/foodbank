using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class PacerrorLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public int? PersonId { get; set; }
    }
}
