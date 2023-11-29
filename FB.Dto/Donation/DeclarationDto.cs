using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class DeclarationDto
    {
        public string UserName { get; set; }
        public string Reference { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public DateTime? ValidForm { get; set; }
        public DateTime? ValidTo { get; set; }
        public int PersonId { get; set; }
    }
}
