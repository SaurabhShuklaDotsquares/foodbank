using FB.Core;
using FB.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Code
{
    public class CustomActionFilterAdminAttribute : ActionFilterAttribute
    {
        //private UserRoles[] UserRoless;
        public CustomActionFilterAdminAttribute()
        {
        
        }
        protected virtual CustomPrincipal CurrentUser
        {
            get { return new CustomPrincipal(ContextProvider.HttpContext.User); }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (CurrentUser != null && CurrentUser.IsAuthenticated && CurrentUser.RoleID > 0)
            {
                string url = $"{ContextProvider.AbsoluteUri}";
                string urlToCheck = url.ToLower().Replace(SiteKeys.DomainName.ToLower(), "").ToLower();

                //if (UserRoless.Length > 0)
                //{
                //    bool obj = UserRoless.Any(r => CurrentUser.RoleID == (byte)r);
                //    if (!obj)
                //    {
                //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                //        {
                //            area = "",
                //            controller = "error",
                //            action = "accessDenied"
                //        }));
                //    }
                //}


                if (!string.IsNullOrWhiteSpace(urlToCheck) && !urlToCheck.Contains("accessdenied") && !urlToCheck.Contains("home") && !urlToCheck.Contains("dashboard"))
                {
                    bool isValid = false;
                    var menuService = (IMenuService)filterContext.HttpContext.RequestServices.GetService(typeof(IMenuService));
                    if (urlToCheck.Contains("menu"))
                    {
                        isValid = CurrentUser.RoleID == (int)UserRoles.SuperAdmin;
                    }
                    else if (urlToCheck.Contains("content") || urlToCheck.Contains("fonts"))
                    {
                        isValid = true;
                    }
                    else
                    {

                        isValid = menuService.CheckCurrentMenu(urlToCheck, CurrentUser.RoleID);
                    }

                    if (!isValid)
                    {
                        string controllerName = filterContext.RouteData.Values["controller"].ToString();
                        string actionName = filterContext.RouteData.Values["action"].ToString();

                        if (controllerName.ToLower() == "dashboard" && actionName.ToLower() == "index")
                        {
                            filterContext.Result = new RedirectResult("~/home");
                        }
                        else
                        {
                            ReturnAccessDenied(filterContext);
                        }
                    }
                }
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "error",
                        action = "SessionOut"
                    }));
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/home");
                }
            }
        }

        private void ReturnAccessDenied(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "error",
                    action = "accessDeniedAjax"
                }));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "error",
                    action = "accessDenied"
                }));
            }
        }
    }
}
