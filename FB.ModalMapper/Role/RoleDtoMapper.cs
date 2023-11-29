using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FB.Data.Models;
namespace FB.ModalMapper
{
    public static class RoleDtoMapper
    {
        public static RoleDto Map(Role role)
        {
            List<FoodbankRoleMenuPrivilegeDto> FoobankRoleMenuPrivileges = new List<FoodbankRoleMenuPrivilegeDto>();
            if (role.FoodbankRoleMenuPrivilege != null && role.FoodbankRoleMenuPrivilege.Count > 0)
            {
                foreach (var item in role.FoodbankRoleMenuPrivilege)
                {
                    FoobankRoleMenuPrivileges.Add(new FoodbankRoleMenuPrivilegeDto
                    {
                        MenuID = item.MenuId,
                        RoleID = item.RoleId,
                        IsCreate = item.IsCreate,
                        IsUpdate = item.IsUpdate,
                        IsList = item.IsList,
                        IsDetail = item.IsDetail,
                        IsDelete = item.IsDelete
                    });
                }
            }

            return new RoleDto
            {
                RoleID = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                ParentRoleID = role.ParentRoleId,
                CentralOfficeID = role.CentralOfficeId,
                AddedDate = role.AddedDate,
                MyGivingOfflineAccess = role.MyGivingOfflineAccess,
                FoodbankRoleMenuPrivileges = FoobankRoleMenuPrivileges
            };
        }

        public static List<RoleDto> Map(List<Role> roles)
        {
            List<RoleDto> roleDto = new List<RoleDto>();

            foreach (var role in roles)
            {
                roleDto.Add(new RoleDto
                {
                    RoleID = role.RoleId,
                    RoleName = role.RoleName,
                    Description = role.Description,
                    ParentRoleID = role.ParentRoleId,
                    CentralOfficeID = role.CentralOfficeId,
                    AddedDate = role.AddedDate,
                    //AuditUserId = role.AuditUserId,
                    //AuditIP = role.AuditIp,
                    MyGivingOfflineAccess = role.MyGivingOfflineAccess,
                });
            }

            return roleDto;
        }

        public static Role Map(RoleDto roleDto, Role role)
        {
            role.RoleId = roleDto.RoleID;
            role.RoleName = roleDto.RoleName;
            role.Description = roleDto.Description;
            role.ParentRoleId = roleDto.ParentRoleID;
            role.CentralOfficeId = roleDto.CentralOfficeID;
            role.AddedDate = roleDto.AddedDate;
            role.AuditIp = roleDto.AuditIP;
            role.AuditUserId = roleDto.AuditUserId;
            role.MyGivingOfflineAccess = roleDto.MyGivingOfflineAccess;
            role.IsFoodbankRole = roleDto.IsFoodbankRole;
            role.FoodbankId = roleDto.FoodbankId;
            return role;
        }

    }
}
