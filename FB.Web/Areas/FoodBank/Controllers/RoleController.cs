using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class RoleController : BaseController
    {
        private IRoleService roleService;
        private IMenuService menuService;
        private ICentralOfficeService centralOfficeService;
        public RoleController(
            IRoleService _roleService,
            IMenuService _menuService,
            ICentralOfficeService _centralOfficeService
            )
        {
            this.roleService = _roleService;
            this.menuService = _menuService;
            this.centralOfficeService = _centralOfficeService;
        }
        [HttpGet]
        public IActionResult RolePrivileges()
        {
            int? foodbankId = null;
            //if (CurrentUser.IsInRole((int)UserRoles.Foodbank))
            //{
            //    foodbankId = CurrentUser.FoodbankId;
            //}
            var roles = roleService.GetRolesByOrganization(foodbankId);
            return View(roles);
        }

        [HttpGet]
        //[DataActionFilter("roleId", DataEnityNames.Role)]
        public IActionResult MMORolePermission(int roleId)
        {
            //RoleDto role = roleService.GetRole(roleId);
            //ViewBag.Role = role;
            //

            //
            var IsSuperAdminUser = false;
            if (CurrentUser.FoodbankId > 0)
            {
                IsSuperAdminUser = true;
            }


            //roleId = (int)UserRoles.Foodbank;
            var role = roleService.GetRolePrivileges(roleId,0, IsSuperAdminUser);
            ViewBag.Role = role;
            var menu = menuService.GetShowMenuBasedOnRole(true, roleId);
            //if (!CurrentUser.IsSuperAdminUser() && !CurrentUser.IsOrganisationUser())
            //{
            //    var showMenu = GetMenus().Where(m => menu.Select(x => x.MenuID).Contains(m.MenuID)).ToList();
            //    ViewBag.Menus = showMenu;
            //}
            //else
                ViewBag.Menus = menu;
            return PartialView("_MMORoleMenu", role.FoodbankRoleMenuPrivileges);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MMORolePermission(List<FoodbankRoleMenuPrivilegeDto> model, string returnPage)
        {
            try
            {
                var data = model.Where(a => a.MenuID == 0);

                int roleId = model.FirstOrDefault().RoleID;
                model.RemoveAll(m => m.MenuID == 0);
                roleService.SaveFoodbankRolePrivileges(model, roleId);
                ShowSuccessMessage("Success!", "Role privileges saved successfully.", false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "Role privileges saved successfully.", RedirectUrl = SiteKeys.DomainName + (returnPage == null ? "foodbank/role/roleprivileges" : returnPage) });

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error!", ex.InnerException.Message);
                return CreateModelStateErrors();
            }
        }
        [HttpGet]
        //[DataActionFilter("roleId", DataEnityNames.Role)]
        public IActionResult MMORoleCustom(int roleId)
        {
            //RoleDto role = roleService.GetRole(roleId);
            //ViewBag.Role = role;
            //

            //
            var IsSuperAdminUser = false;
            if (CurrentUser.FoodbankId > 0)
            {
                IsSuperAdminUser = true;
            }


            
            var role = roleService.GetRole(roleId, CurrentUser.OrganisationID, IsSuperAdminUser);
            ViewBag.Role = role;
            if (roleId == (int)UserRoles.Foodbank)
            {
                roleId = (int)UserRoles.FoodbankStaff;
            }
            var menu = menuService.GetShowMenuBasedOnRoleCustom(true, (int)UserRoles.FoodbankStaff);
            if (!CurrentUser.IsSuperAdminUser() && !CurrentUser.IsOrganisationUser())
            {
                var showMenu = GetMenus().Where(m => menu.Select(x => x.MenuID).Contains(m.MenuID)).ToList();
                ViewBag.Menus = showMenu;
            }
            else
                ViewBag.Menus = menu;
            return PartialView("_MMORoleCustom", role.FoodbankRoleMenuPrivileges);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MMORoleCustom(List<FoodbankRoleMenuPrivilegeDto> model, string returnPage)
        {
            try
            {
                var data = model.Where(a => a.MenuID == 0);

                int roleId = model.FirstOrDefault().RoleID;
                model.RemoveAll(m => m.MenuID == 0);
                roleService.SaveFoodbankRolePrivileges(model, roleId);
                ShowSuccessMessage("Success!", "Role privileges saved successfully.", false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "Role privileges saved successfully.", RedirectUrl = SiteKeys.DomainName + (returnPage == null ? "foodbank/role" : returnPage) });

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error!", ex.InnerException.Message);
                return CreateModelStateErrors();
            }
        }

        /// <summary>
        /// To view page of role listing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            BindStaticViewBags(BindViewBag.Organisation);
            return View();
        }



        /// <summary>
        /// To get roles for server side table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[DataActionFilterAttribute("organisationId", DataEnityNames.Client)]
        public IActionResult GetRoles(DataTableServerSide model, int? organisationId)
        {
            if (CurrentUser.OrganisationID > 0)
            {
                organisationId = CurrentUser.OrganisationID;
            }
            KeyValuePair<int, List<RoleDto>> roles = roleService.GetRoles(model, organisationId ?? 0,CurrentUser.FoodbankId);

            var centraloffice = centralOfficeService.GetCentralOffice(organisationId ?? 0);

            return Json(new
            {
                draw = model.draw,
                recordsTotal = roles.Key,
                recordsFiltered = roles.Key,
                data = roles.Value.Select(m => new List<object> {
                    m.RoleName,
                    m.Description,
                    "<a data-toggle='modal' data-target='#modal-create-edit-role'  href='/Foodbank/role/createedit/"+m.RoleID+"'" 
                    + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"
                    + "<a data-toggle='modal' data-target='#modal-delete-role' href='/Foodbank/role/delete/"+m.RoleID+"'" 
                    + "' class='btn btn-primary grid-btn btn-sm'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"
                    + "<a data-toggle='modal' data-target='#modal-assign-permission' href='/Foodbank/role/MMORoleCustom/?roleId="+m.RoleID+"&returnPage=role' class='btn btn-primary grid-btn btn-sm'>Edit Foodbank Permission <i class='fa fa-edit'></i></a>&nbsp;",
                    m.RoleID
                })
            });
        }

        /// <summary>
        /// To open pop to create/edit the role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        //[DataActionFilterAttribute("id", DataEnityNames.Role)]
        public IActionResult CreateEdit(int? id = null)
        {
            BindStaticViewBags(BindViewBag.Organisation);
            if (id.HasValue)
            {
                var role = roleService.GetRoleByFoodbank(id.Value,CurrentUser.FoodbankId);
                return PartialView("_CreateEdit", role);
            }

            return PartialView("_CreateEdit", new RoleDto { AddedDate = DateTime.Now, CentralOfficeID = CurrentUser.OrganisationID, ParentRoleID = 5 });
        }

        /// <summary>
        /// To save the role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(RoleDto model)
        {
            try
            {
                if (!model.CentralOfficeID.HasValue)
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = "Centraloffice required." });
                }

                if (ModelState.IsValid)
                {
                    Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>();
                    dictDataIds.Add(DataEnityNames.CentralOffice, model.CentralOfficeID);
                    dictDataIds.Add(DataEnityNames.Role, model.RoleID);
                    if (CheckAuthorisedData(dictDataIds))
                    {
                        model.AuditIP = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                        model.AuditUserId = CurrentUser.UserID;
                        model.IsFoodbankRole = true;
                        model.FoodbankId = CurrentUser.FoodbankId;
                        Role entity;
                        if (model.RoleID == 0)
                        {
                            entity = new Role();
                        }
                        else
                        {
                            entity = roleService.GetRoleByIdandFoodbank(model.RoleID,CurrentUser.FoodbankId);
                        }
                        entity = RoleDtoMapper.Map(model, entity);
                        roleService.Save(entity, model.RoleID == 0);
                        ShowSuccessMessage("Success!", "Role saved successfully");
                        return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = (model.RoleID == 0 ? "Role saved successfully" : "Role updated successfully") });
                    }
                    else
                        return RedirectAccessDenied();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error!", ex.Message);
            }

            return CreateModelStateErrors();
        }
        
        private List<MenuDto> GetMenus()
        {
            var menus = new List<MenuDto>();
            if (CurrentUser != null)
            {
                menus = menuService.GetPermittedMenus(CurrentUser.RoleID);
               
                //    var orgMenu = menus.FirstOrDefault(m => m.MenuName == SiteConstant.Organisations);
                //    if (orgMenu != null)
                //    {
                //        orgMenu.MenuName = CurrentUser.OrganisationLabel ?? SiteConstant.Organisations;
                //    }

                //    var groupMenu = menus.FirstOrDefault(m => m.MenuName == SiteConstant.Groups);
                //    if (groupMenu != null)
                //    {
                //        groupMenu.MenuName = CurrentUser.GroupLabel ?? SiteConstant.Groups;
                //    }

                //    var branchMenu = menus.FirstOrDefault(m => m.MenuName == SiteConstant.Branches);
                //    if (branchMenu != null)
                //    {
                //        branchMenu.MenuName = CurrentUser.BranchLabel ?? SiteConstant.Branches;
                //    }
                
                ////Hide hope churches from menu
                //var organisation = CurrentUser.OrganisationID>0 ? organisationService.GetOrganisation(UserOrganisationId.Value) : null;
                //if (organisation != null)
                //{
                //    var restricatedMenues = new List<string>();
                //    if (restricatedMenues.Any())
                //    {
                //        menus = menus.Where(m => !restricatedMenues.Any(rm => rm == m.MenuName.ToLower())).ToList();
                //    }
                //}
            }
            return menus;
        }
      

        /// <summary>
        /// To open the modal popup dialog of delete role 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        //[DataActionFilterAttribute("id", DataEnityNames.Role)]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure you want to delete this role?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Role" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        //[DataActionFilterAttribute("id", DataEnityNames.Role)]
        public IActionResult Delete(int id, IFormCollection formCollection)
        {
            try
            {
                roleService.RoleDelete(id);
                ShowSuccessMessage("Success!", "Role deleted successfully.", false);
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "You can't delete this role because it has records attached to it.";
                ShowErrorMessage("Error!", message, false);
            }
            return RedirectToAction("Index");
        }

        #region "Dispose"

        protected override void Dispose(bool disposing)
        {
            if (roleService != null)
            {
                roleService.Dispose();
                roleService = null;
            }

            if (menuService != null)
            {
                menuService.Dispose();
                menuService = null;
            }

            if (centralOfficeService != null)
            {
                centralOfficeService.Dispose();
                centralOfficeService = null;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
