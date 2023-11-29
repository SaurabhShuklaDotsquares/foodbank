using FB.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
   public class NoteDto
    {
        public int NoteId { get; set; }
        public int PersonId { get; set; }
        public bool Privacy { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        [DisplayName("Date")]
        public string NoteDate { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public int CreatedBy { get; set; }
    }
}
