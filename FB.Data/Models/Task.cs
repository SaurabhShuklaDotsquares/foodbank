using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Task
    {
        public Task()
        {
            PersonTask = new HashSet<PersonTask>();
            TaskSchedule = new HashSet<TaskSchedule>();
        }

        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public int? ScheduleId { get; set; }
        public int? TaskType { get; set; }
        public int? FrequencyId { get; set; }

        public virtual Frequency Frequency { get; set; }
        public virtual TaskType TaskTypeNavigation { get; set; }
        public virtual ICollection<PersonTask> PersonTask { get; set; }
        public virtual ICollection<TaskSchedule> TaskSchedule { get; set; }
    }
}
