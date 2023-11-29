using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class AgentUser
    {
        public int UserId { get; set; }
        public int AgentId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual User User { get; set; }
    }
}
