using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper.Menu;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FB.Service
{
    public class MenuService : IMenuService
    {
        private IRepository<FoodbankMenu> repoMenu;
        public MenuService(IRepository<FoodbankMenu> _repoMenu)
        {
            this.repoMenu = _repoMenu;
        }

        /// <summary>
        /// To check current menu url for role
        /// </summary>
        /// <param name="menuUrl"></param>
        /// <param name="roleID"></param>
        /// <returns>bool</returns>
        public bool CheckCurrentMenu(string menuUrl, int roleID)
        {
            List<FoodbankMenu> regexMenus = repoMenu
                 .Query()
                 .Filter(m => m.FoodbankRoleMenuPrivilege.FirstOrDefault(mp => mp.RoleId == roleID) != null && m.MenuUrl.Contains("{"))
                 .Get()
                 .ToList();

            List<FoodbankMenu> simpleMenus = repoMenu
                .Query()
                .Filter(m => m.FoodbankRoleMenuPrivilege.FirstOrDefault(mp => mp.RoleId == roleID) != null && !m.MenuUrl.Contains("{"))
                .Get()
                .ToList();

            bool isValid = false;

            isValid = simpleMenus.FirstOrDefault(m => m.MenuUrl.ToLower() == menuUrl.ToLower()) != null;

            if (!isValid)
            {
                isValid = true; // regexMenus.FirstOrDefault(m => Regex.IsMatch(menuUrl.Replace("?", "/").ToLower(), m.MenuUrl.Replace("?", "/").ToLower())) != null;
            }

            return isValid;
        }


        /// <summary>
        /// To get menu url for role
        /// </summary>     
        /// <param name="roleID"></param>
        /// <returns>List</returns>
        public List<string> GetActionMethodAccessbility(int roleID)
        {
            return repoMenu
               .Query()
               .Filter(m => m.FoodbankRoleMenuPrivilege.FirstOrDefault(mp => mp.RoleId == roleID) != null && m.MenuUrl.Contains("/"))
               .Get().Select(e => e.MenuUrl.ToLower())
               .ToList();
        }

        /// <summary>
        /// To get menus to show on page based on role
        /// </summary>
        /// <returns></returns>
        //public List<MenuDto> GetShowMenuBasedOnRole(bool isPermission, int RoleId)
        //{
        //   List<Mmomenu> menu = new List<Mmomenu>();
        //    if (RoleId == ((int)UserRoles.SuperAdmin) && isPermission)
        //    {
        //         menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsSuperAdminPermission == true)
        //            .Get().ToList();
        //    }
        //    else if (RoleId == ((int)UserRoles.Organisation) && isPermission)
        //    {
        //        menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true)
        //            .Get().ToList();
        //    }     
        //    else if (RoleId == ((int)UserRoles.Charity) && isPermission)
        //    {
        //        menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsCharityPermission == true)
        //            .Get().ToList();
        //    }
        //    else if (RoleId == ((int)UserRoles.Branch) && isPermission)
        //    {
        //        menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsBranchPermission == true)
        //            .Get().ToList();
        //    }
        //    else
        //    {
        //        menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true)
        //            .Get().ToList();
        //    }

        //    return MenuDtoMapper.Map(menu);
        //}

        /// <summary>
        /// To get menus to show on page based on role
        /// </summary>
        /// <returns></returns>
        public List<MenuDto> GetShowMenuBasedOnRole(bool isPermission, int RoleId)
        {
            List<FoodbankMenu> menu = new List<FoodbankMenu>();
            if (RoleId == ((int)UserRoles.SuperAdmin) && isPermission)
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsSuperAdminPermission == true)
                   .Get().ToList();
            }
            else if (RoleId == ((int)UserRoles.Foodbank) && isPermission)
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true)
                    .Get().ToList();
            }
            else if (RoleId == ((int)UserRoles.FoodbankStaff) && isPermission)
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true)
                    .Get().ToList();
            }
            //else if (RoleId == ((int)UserRoles.Branch) && isPermission)
            //{
            //    menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsBranchPermission == true)
            //        .Get().ToList();
            //}
            else
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                    .Get().ToList();
            }

            return MenuDtoMapper.Map(menu);
        }
        public List<MenuDto> GetShowMenuBasedOnRoleCustom(bool isPermission, int RoleId)
        {
            List<FoodbankMenu> menu = new List<FoodbankMenu>();
            if (RoleId == ((int)UserRoles.SuperAdmin) && isPermission)
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsSuperAdminPermission == true)
                   .Get().ToList();
            }
            else if (RoleId == ((int)UserRoles.Foodbank) && isPermission)
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true)
                    .Get().ToList();
            }
            else if (RoleId == ((int)UserRoles.FoodbankStaff) && isPermission)
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                    .Get().ToList();
            }
            //else if (RoleId == ((int)UserRoles.Branch) && isPermission)
            //{
            //    menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsBranchPermission == true)
            //        .Get().ToList();
            //}
            else
            {
                menu = repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                    .Get().ToList();
            }

            return MenuDtoMapper.Map(menu);
        }

        public List<MenuDto> GetPermittedMenus(int RoleId)
        {
            if (RoleId == (int)UserRoles.SuperAdmin)
            {
                return MenuDtoMapper.Map(repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsSuperAdminPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                    .Get().ToList());
            }
            else if (RoleId == (int)UserRoles.Organisation)
            {
               
                return MenuDtoMapper.Map(repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                    .Get().ToList());
            }
            else if (RoleId == (int)UserRoles.Foodbank)
            {
              
                return MenuDtoMapper.Map(repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                   .Get().ToList());
            }
            else if (RoleId == (int)UserRoles.FoodbankStaff)
            {
                return MenuDtoMapper.Map(repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                    .Get().ToList());
            }
            else
            {
                return MenuDtoMapper.Map(repoMenu.Query().Filter(m => (m.ShowOnMenu || m.ShowOnPermission) && m.IsOrganizationPermission == true && m.FoodbankRoleMenuPrivilege.Any(r => r.RoleId == RoleId))
                    .Get().ToList());
            }
        }
        public void Dispose()
        {
            if (repoMenu != null)
            {
                repoMenu.Dispose();
                repoMenu = null;
            }
        }
    }
}
