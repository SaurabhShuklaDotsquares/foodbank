using System;
using System.Collections.Generic;
using FB.Core;
using FB.Data.Models;
using FB.Dto;

namespace FB.Service
{
    public interface IRoleService : IDisposable
    {
        Role GetRoleByName(string name);
        Role GetRoleById(int id);
        List<RoleDto> GetRoles();
        void Save(UserRole userRole, bool isNew = true);
        Role GetRoleByIdandFoodbank(int id, int FoodbankId);
        void Save(Role role, bool isNew = true);
        List<RoleDto> GetRolesByFoodbank(int? FoodbankId);
        List<RoleDto> GetRolesByOrganization(int? organizationId);
        RoleDto GetRole(int id);
        RoleDto GetRoleByFoodbank(int id, int FoodbankId);
        void SaveFoodbankRolePrivileges(List<FoodbankRoleMenuPrivilegeDto> mmoRoleMenuPrivilege, int roleID);
        KeyValuePair<int, List<RoleDto>> GetRoles(DataTableServerSide searchModel, int? organisationId,int FoodbankId);

        void RoleDelete(int id);
        RoleDto GetRole(int id, int? organisationId = null, bool isAdmin = false);
        RoleDto GetRolePrivileges(int id, int? organisationId = null, bool isAdmin = false);
        void SaveRolePrivileges(List<FoodbankRoleMenuPrivilegeDto> roleMenuPrivilege, int roleID);
    }

}
