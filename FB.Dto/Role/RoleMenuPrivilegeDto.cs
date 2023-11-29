using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
   public class RoleMenuPrivilegeDto
    {
        public int MenuID { get; set; }
        public int RoleID { get; set; }
        public bool IsCreate { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsList { get; set; }
        public bool IsDetail { get; set; }
        public bool IsDelete { get; set; }
    }
}
