using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Declaration
    {
        public int DeclarationId { get; set; }
        public int PersonId { get; set; }
        public DateTime? DateDeclarationSigned { get; set; }
        public DateTime? DateDeclarationValidFrom { get; set; }
        public DateTime? DateDeclarationValidTo { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Person Person { get; set; }
    }
}
