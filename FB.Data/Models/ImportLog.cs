using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ImportLog
    {
        public ImportLog()
        {
            ImportLogDetail = new HashSet<ImportLogDetail>();
        }

        public long StatusId { get; set; }
        public int? UserId { get; set; }
        public bool ErrorStatus { get; set; }
        public DateTime? AddedDate { get; set; }
        public string Service { get; set; }
        public int? CharityId { get; set; }
        public byte? ImportType { get; set; }
        public int? CentralOfficeId { get; set; }
        public string ImportedFile { get; set; }

        public virtual ICollection<ImportLogDetail> ImportLogDetail { get; set; }
    }
}
