using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Mmomenu
    {
        public Mmomenu()
        {
            InverseParentMenu = new HashSet<Mmomenu>();
            MmoroleMenuPrivilege = new HashSet<MmoroleMenuPrivilege>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuIcon { get; set; }
        public int? ParentMenuId { get; set; }
        public bool ShowOnMenu { get; set; }
        public bool ShowOnPermission { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? IsSuperAdminPermission { get; set; }
        public bool? IsOrganizationPermission { get; set; }
        public bool? IsCharityPermission { get; set; }
        public bool? IsDonorPermission { get; set; }
        public bool? IsTechnicalPermission { get; set; }
        public bool? IsBranchPermission { get; set; }
        public bool? IsAgentPermission { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }
        public bool? IsInputPermission { get; set; }
        public bool? IsCreate { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsList { get; set; }
        public bool? IsDetail { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsInternalPermission { get; set; }

        public virtual Mmomenu ParentMenu { get; set; }
        public virtual ICollection<Mmomenu> InverseParentMenu { get; set; }
        public virtual ICollection<MmoroleMenuPrivilege> MmoroleMenuPrivilege { get; set; }
    }
}
