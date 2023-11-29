using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IMenuService : IDisposable
    {
        bool CheckCurrentMenu(string menuUrl, int roles);

        List<string> GetActionMethodAccessbility(int roleID);
        List<MenuDto> GetPermittedMenus(int RoleId);
        List<MenuDto> GetShowMenuBasedOnRole(bool isPermission, int RoleId);
        List<MenuDto> GetShowMenuBasedOnRoleCustom(bool isPermission, int RoleId);
    }
}
