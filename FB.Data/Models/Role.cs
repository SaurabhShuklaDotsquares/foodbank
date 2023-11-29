using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            FoodbankRoleMenuPrivilege = new HashSet<FoodbankRoleMenuPrivilege>();
            MmoroleMenuPrivilege = new HashSet<MmoroleMenuPrivilege>();
            RoleMenuPrivilege = new HashSet<RoleMenuPrivilege>();
            UserRole = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int? ParentRoleId { get; set; }
        public int? CentralOfficeId { get; set; }
        public DateTime AddedDate { get; set; }
        public int? AuditUserId { get; set; }
        public string AuditIp { get; set; }
        public bool MyGivingOfflineAccess { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool IsFoodbankRole { get; set; }
        public int? FoodbankId { get; set; }

        public virtual Foodbank Foodbank { get; set; }
        public virtual ICollection<FoodbankRoleMenuPrivilege> FoodbankRoleMenuPrivilege { get; set; }
        public virtual ICollection<MmoroleMenuPrivilege> MmoroleMenuPrivilege { get; set; }
        public virtual ICollection<RoleMenuPrivilege> RoleMenuPrivilege { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
