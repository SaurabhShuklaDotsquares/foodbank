using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class CentralOfficeApp
    {
        public int Id { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? AppId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ExternalId { get; set; }

        public virtual App App { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
    }
}
