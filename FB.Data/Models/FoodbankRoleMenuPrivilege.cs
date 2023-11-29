using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class FoodbankRoleMenuPrivilege
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public bool IsCreate { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsList { get; set; }
        public bool IsDetail { get; set; }
        public bool IsDelete { get; set; }

        public virtual FoodbankMenu Menu { get; set; }
        public virtual Role Role { get; set; }
    }
}
