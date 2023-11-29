using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DataImportLog
    {
        public DataImportLog()
        {
            DataNotImport = new HashSet<DataNotImport>();
        }

        public int Id { get; set; }
        public int DataImportStatusId { get; set; }
        public int CharityId { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? ImportType { get; set; }
        public int? CentralOfficeId { get; set; }
        public string ImportedFile { get; set; }

        public virtual CentralOffice CentralOffice { get; set; }
        public virtual ICollection<DataNotImport> DataNotImport { get; set; }
    }
}
