using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmotaskShift
    {
        public int TaskShiftId { get; set; }
        public int MasterTaskId { get; set; }
        public int? PersonId { get; set; }
        public int? IsComplete { get; set; }
        public DateTime? ShiftDate { get; set; }
        public string ShiftTimeFrom { get; set; }
        public string ShiftTimeTo { get; set; }
        public bool? IsAuto { get; set; }
        public bool? Active { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        public string MstaskEventId { get; set; }

        public virtual MmoattendanceCode IsCompleteNavigation { get; set; }
        public virtual MmomasterTask MasterTask { get; set; }
        public virtual Person Person { get; set; }
    }
}
