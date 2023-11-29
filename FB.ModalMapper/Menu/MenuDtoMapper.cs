using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper.Menu
{
    public static class MenuDtoMapper
    {
        public static List<MenuDto> Map(List<FoodbankMenu> menus)
        {
            List<MenuDto> menuDto = new List<MenuDto>();

            foreach (var menu in menus)
            {
                menuDto.Add(new MenuDto
                {
                    MenuID = menu.MenuId,
                    MenuName = menu.MenuName,
                    MenuUrl = menu.MenuUrl,
                    MenuIcon = menu.MenuIcon,
                    ParentMenuID = menu.ParentMenuId,
                    ShowOnMenu = menu.ShowOnMenu,
                    ShowOnPermission = menu.ShowOnPermission,
                    IsSuperAdminPermission = menu.IsSuperAdminPermission ?? false,
                    IsInternalPermission = menu.IsInternalPermission ?? false,
                    IsOrganizationPermission = menu.IsOrganizationPermission ?? false,
                    IsCharityPermission = menu.IsCharityPermission ?? false,
                    IsBranchPermission = menu.IsBranchPermission ?? false,
                    IsAgentPermission = menu.IsAgentPermission ?? false,
                    IsTechnicalPermission = menu.IsTechnicalPermission ?? false,
                    IsDonorPermission = menu.IsDonorPermission ?? false,
                    Sequence = menu.Sequence,
                    IsActive = menu.IsActive,
                    IsInputPermission = menu.IsInputPermission ?? false,
                    IsCreate = menu.IsCreate ?? false,
                    IsUpdate = menu.IsUpdate ?? false,
                    IsList = menu.IsList ?? false,
                    IsDetail = menu.IsDetail ?? false,
                    IsDelete = menu.IsDelete ?? false
                });
            }

            return menuDto;
        }
    }
}
