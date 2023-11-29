using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmoimportLog
    {
        public MmoimportLog()
        {
            MmoimportLogDetail = new HashSet<MmoimportLogDetail>();
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
        public int? BranchId { get; set; }
        public bool? IsOverwriteMgo { get; set; }
        public bool? IsRemoveDataFromMgo { get; set; }

        public virtual ICollection<MmoimportLogDetail> MmoimportLogDetail { get; set; }
    }
}
