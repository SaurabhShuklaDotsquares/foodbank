using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ContactHistory
    {
        public int ContactHistoryId { get; set; }
        public int PersonId { get; set; }
        public int ContactType { get; set; }
        public DateTime? ContactDate { get; set; }
        public string Notes { get; set; }
        public string FileName { get; set; }
        public string Comment { get; set; }
        public byte[] Contents { get; set; }
        public string InboundOutbound { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }

        public virtual ContactType ContactTypeNavigation { get; set; }
        public virtual Person Person { get; set; }
    }
}
