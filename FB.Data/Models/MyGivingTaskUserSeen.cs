using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MyGivingTaskUserSeen
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MyGivingTaskId { get; set; }

        public virtual MyGivingTasks MyGivingTask { get; set; }
        public virtual User User { get; set; }
    }
}
