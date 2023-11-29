using Microsoft.AspNetCore.Mvc;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.ViewComponents
{
    public class MenusViewComponent : ViewComponent
    {
        private readonly IUserService userService;
        public MenusViewComponent(IUserService _userService)
        {
            userService = _userService;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            var user = new CustomPrincipal(HttpContext.User);

            List<MenuDto> menus = new List<MenuDto>();
            if (user.IsAuthenticated)
            {
                List<FoodbankMenu> menuList = userService.GetMenus(user.UserID);
                foreach (var menu in menuList)
                {
                    MenuDto menuEntity = new MenuDto();
                    menuEntity.ParentMenuID = menu.ParentMenuId;
                    menuEntity.Sequence = menu.Sequence;
                    menuEntity.MenuID = menu.MenuId;
                    menuEntity.MenuName = menu.MenuName;
                    menuEntity.MenuUrl = menu.MenuUrl;
                    menus.Add(menuEntity);
                }
            }
            return System.Threading.Tasks.Task.FromResult<IViewComponentResult>(View("~/Views/Shared/_Menus.cshtml", menus));
        }
    }
}
