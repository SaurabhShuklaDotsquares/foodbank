using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TaskType
    {
        public TaskType()
        {
            Task = new HashSet<Task>();
        }

        public int TaskTypeId { get; set; }
        public string TaskTypeDescription { get; set; }

        public virtual ICollection<Task> Task { get; set; }
    }
}
