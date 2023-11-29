using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MyGivingTasks
    {
        public MyGivingTasks()
        {
            MyGivingTaskUserSeen = new HashSet<MyGivingTaskUserSeen>();
        }

        public int Id { get; set; }
        public int TaskNo { get; set; }
        public string TaskName { get; set; }
        public string TaskDetail { get; set; }
        public byte TaskType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsReaded { get; set; }

        public virtual ICollection<MyGivingTaskUserSeen> MyGivingTaskUserSeen { get; set; }
    }
}
