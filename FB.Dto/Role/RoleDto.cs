using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class RoleDto
    {
        public int RoleID { get; set; }
        [DisplayName("Centraloffice")]
        public int? CentralOfficeID { get; set; }
        public int? ParentRoleID { get; set; }
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public int AuditUserId { get; set; }
        public string AuditIP { get; set; }
        public bool IsFoodbankRole { get; set; }
        public int FoodbankId { get; set; }
        public bool MyGivingOfflineAccess { get; set; }
        public List<RoleMenuPrivilegeDto> RoleMenuPrivileges { get; set; }
        public List<FoodbankRoleMenuPrivilegeDto> FoodbankRoleMenuPrivileges { get; set; }
    }
}
