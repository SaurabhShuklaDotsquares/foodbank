using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Paragraph
    {
        public int ParagraphId { get; set; }
        public short? SequenceNumber { get; set; }
        public int? LetterId { get; set; }
        public string Contents { get; set; }

        public virtual Letter Letter { get; set; }
    }
}
