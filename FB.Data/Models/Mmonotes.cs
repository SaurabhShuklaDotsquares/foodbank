using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmonotes
    {
        public int NoteId { get; set; }
        public int PersonId { get; set; }
        public bool Privacy { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime? NoteDate { get; set; }
        public int CreatedBy { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Person Person { get; set; }
    }
}
