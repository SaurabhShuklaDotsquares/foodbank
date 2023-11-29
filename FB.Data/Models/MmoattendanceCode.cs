using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmoattendanceCode
    {
        public MmoattendanceCode()
        {
            MmogroupEventAttendance = new HashSet<MmogroupEventAttendance>();
            MmotaskShift = new HashSet<MmotaskShift>();
        }

        public int AttendanceCodeId { get; set; }
        public string AttendanceCodeDescription { get; set; }
        public int AttendanceCodeFor { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string Mccpkreason { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual ICollection<MmogroupEventAttendance> MmogroupEventAttendance { get; set; }
        public virtual ICollection<MmotaskShift> MmotaskShift { get; set; }
    }
}
