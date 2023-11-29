using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DataImportStatus
    {
        public int Id { get; set; }
        public int CharityId { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public byte? ImportType { get; set; }
        public int? CentralOfficeId { get; set; }
        public string ImportedFile { get; set; }
        public bool? IsErrorInFile { get; set; }
    }
}
