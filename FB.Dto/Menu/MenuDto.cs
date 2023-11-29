using System;
using System.ComponentModel;

namespace FB.Dto
{
    public class MenuDto
    {
        public int MenuID { get; set; }

        [DisplayName("Menu Name")]
        public string MenuName { get; set; }

        [DisplayName("Menu Url")]
        public string MenuUrl { get; set; }
        public string MenuIcon { get; set; }

        [DisplayName("Parent Menu Name")]
        public int? ParentMenuID { get; set; }
        public string ParentMenuName { get; set; }

        [DisplayName("Show On Menu")]
        public bool ShowOnMenu { get; set; }

        [DisplayName("Show On Permission")]
        public bool ShowOnPermission { get; set; }

        [DisplayName("Permission to SuperAdmin")]
        public bool IsSuperAdminPermission { get; set; }

        [DisplayName("Permission to Internal")]
        public bool IsInternalPermission { get; set; }

        [DisplayName("Permission to Organisation")]
        public bool IsOrganizationPermission { get; set; }

        [DisplayName("Permission to Charity")]
        public bool IsCharityPermission { get; set; }

        [DisplayName("Permission to Branch")]
        public bool IsBranchPermission { get; set; }

        [DisplayName("Permission to Agent")]
        public bool IsAgentPermission { get; set; }

        [DisplayName("Permission to Tech. Support")]
        public bool IsTechnicalPermission { get; set; }

        [DisplayName("Permission to Donor")]
        public bool IsDonorPermission { get; set; }

        [DisplayName("Menu Sequence")]
        public int? Sequence { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string MenuIconFullPath { get; set; }

        [DisplayName("Permission to Input")]
        public bool IsInputPermission { get; set; }

        [DisplayName("Is Create?")]
        public bool IsCreate { get; set; }

        [DisplayName("Is Update?")]
        public bool IsUpdate { get; set; }

        [DisplayName("Is List?")]
        public bool IsList { get; set; }

        [DisplayName("Is Detail?")]
        public bool IsDetail { get; set; }

        [DisplayName("Is Delete?")]
        public bool IsDelete { get; set; }
    }
}
