using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FamilyNotes
    {
        public int NoteId { get; set; }
        public int FamilyId { get; set; }
        public bool Privacy { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime? NoteDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Family Family { get; set; }
    }
}
