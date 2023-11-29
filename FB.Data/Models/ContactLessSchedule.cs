using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class ContactLessSchedule
    {
        public ContactLessSchedule()
        {
            ContactLessScheduleDate = new HashSet<ContactLessScheduleDate>();
        }

        public int Id { get; set; }
        public int TerminalId { get; set; }
        public int BranchId { get; set; }
        public bool? IsRecurring { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? RecurringType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? RecentDate { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual ContactLessTerminal Terminal { get; set; }
        public virtual ICollection<ContactLessScheduleDate> ContactLessScheduleDate { get; set; }
    }
}
