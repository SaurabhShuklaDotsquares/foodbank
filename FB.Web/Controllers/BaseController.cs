using FB.Core;
using FB.Dto;
using FB.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FB.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICentralOfficeService centralOfficeService;
        private readonly ICharityService charityService;
        private readonly IBranchService branchService;
        private readonly IRoleService roleService;
        public BaseController(ICentralOfficeService _centralOfficeService = null,
            ICharityService _charityService = null,
            IBranchService _branchService = null, IRoleService _roleService=null)
        {
            this.centralOfficeService = _centralOfficeService;
            this.charityService = _charityService;
            this.branchService = _branchService;
            this.roleService = _roleService;
        }

        #region "Public Properties"

        public CustomPrincipal CurrentUser => new CustomPrincipal(HttpContext.User);

        #endregion

        #region "Notificatons"
        private void ShowMessages(string title, string message, MessageType messageType, bool isCurrentView)
        {
            Notification model = new Notification
            {
                Heading = title,
                Message = message,
                Type = messageType
            };

            if (isCurrentView)
                this.ViewData.AddOrReplace("NotificationModel", model);
            else
            {
                TempData["NotificationModel"] = JsonConvert.SerializeObject(model);
                TempData.Keep("NotificationModel");
            }
        }

        protected void ShowErrorMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Danger, isCurrentView);
        }

        protected void ShowSuccessMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Success, isCurrentView);
        }

        protected void ShowWarningMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Warning, isCurrentView);
        }

        protected void ShowInfoMessage(string title, string message, bool isCurrentView = true)
        {
            ShowMessages(title, message, MessageType.Info, isCurrentView);
        }
        #endregion

        #region "Authentication"

        public async Task CreateAuthenticationTicket(UserSessionDto user)
        {
            if (user != null)
            {
                var claims = new List<Claim>{
                        new Claim(ClaimTypes.PrimarySid, Convert.ToString(user.UserId)),
                        new Claim(ClaimTypes.Email, !string.IsNullOrEmpty(user.Email)?user.Email : string.Empty),
                        new Claim(ClaimTypes.GivenName, user.FirstName),
                        new Claim(nameof(user.LastName), !string.IsNullOrEmpty(user.LastName)?user.LastName : string.Empty),
                        new Claim(nameof(user.UserName), user.UserName),
                        new Claim(nameof(user.RoleID), Convert.ToString(user.RoleID)),
                        new Claim(nameof(user.OrganisationID), Convert.ToString(user.OrganisationID)),
                        new Claim(nameof(user.CharityID), user.CharityID.HasValue?Convert.ToString(user.CharityID.Value) :string.Empty),
                        new Claim(nameof(user.BranchID),  user.BranchID.HasValue?Convert.ToString(user.BranchID.Value) :string.Empty),
                        new Claim(nameof(user.MSApiUserId), user.MSApiUserId),
                        new Claim(nameof(user.FoodbankId), Convert.ToString(user.FoodbankId)),
                        new Claim(nameof(user.IsTeamManager), Convert.ToString(user.IsTeamManager))
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
        }

        public async Task RemoveAuthentication()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #endregion

        #region "Exception Handling"

        public PartialViewResult CreateModelStateErrors()
        {
            return PartialView("_ValidationSummary", ModelState.Values.SelectMany(x => x.Errors));
        }

        #endregion "Exception Handling"

        public IActionResult NewtonSoftJsonResult(object data)
        {
            return Json(data);            
        }

        public int? GetOrgLevelID(int? id, UserRoles role)
        {
            switch (role)
            {
                case UserRoles.Branch: return CurrentUser.BranchID.HasValue ? CurrentUser.BranchID : id;
                case UserRoles.Charity: return CurrentUser.CharityID.HasValue ? CurrentUser.CharityID : id;
                case UserRoles.Organisation: return CurrentUser.OrganisationID > 0 ? CurrentUser.OrganisationID : id;
                case UserRoles.Donor: return CurrentUser.PersonID > 0 ? CurrentUser.PersonID : id;
                
                case UserRoles.Foodbank: return CurrentUser.FoodbankId > 0 ? CurrentUser.FoodbankId : id;
                case UserRoles.FoodbankStaff: return CurrentUser.FoodbankId > 0 ? CurrentUser.FoodbankId : id;
                default: return id;
            }
        }
        protected IActionResult RedirectAccessDenied()
        {
            if (ContextProvider.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return RedirectToRoute(new RouteValueDictionary(new
                {
                    controller = "error",
                    action = "accessDeniedAjax"
                }));
            }
            else
            {
                return RedirectToRoute(new RouteValueDictionary(new
                {
                    controller = "error",
                    action = "accessDenied"
                }));
            }
        }
        /// <summary>
        /// Get Random Password
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRandomPassword()
        {
            try
            {
                string password = Common.CreateRandomPassword(10);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = password, IsSuccess = true });
            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.Message, IsSuccess = false });
            }
        }

        public void BindStaticViewBags(params BindViewBag[] bindViewBags)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            foreach (BindViewBag item in bindViewBags)
            {
                switch (item)
                {
                    case BindViewBag.Organisation:
                        if (CurrentUser.OrganisationID == 0)
                            ViewBag.Organisations = centralOfficeService.GetCentralOffices().Select(c => new SelectListItem { Text = c.OrganisationName, Value = c.CentralOfficeId.ToString() }).ToList();
                        else
                            ViewBag.Organisations = Enumerable.Empty<SelectListItem>().ToList();
                        break;
                    //case BindViewBag.Charity:
                    //    if (CurrentUser.RoleID == (int)UserRoles.Agent && CurrentUser.AgentID > 0 && controllerName == "claim")
                    //        ViewBag.Charities = charityService.GetCharitiesForClaimByAgent(CurrentUser.AgentID).Select(m => new SelectListItem { Text = m.CharityName, Value = m.CharityId.ToString() }).ToList();
                    //    else if (CurrentUser.OrganisationID > 0 && CurrentUser.CharityID == null && CurrentUser.BranchID == null)
                    //        ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.OrganisationID, CurrentUser.UserID).Select(c => new SelectListItem { Text = c.CharityName.AddCharityPrefix(c.Prefix), Value = c.CharityId.ToString() }).ToList();
                    //    else
                    //        ViewBag.Charities = Enumerable.Empty<SelectListItem>().ToList();
                    //    break;
                    //case BindViewBag.Branch:
                    //    if (CurrentUser.CharityID > 0 && CurrentUser.BranchID == null)
                    //        ViewBag.Branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.CharityID.Value, userID: CurrentUser.UserID).Select(m => new SelectListItem { Text = m.BranchDescription.AddBranchPrefix(m.BranchReference, m.Charity?.Prefix), Value = m.BranchId.ToString() }).ToList();
                    //    else
                    //        ViewBag.Branches = Enumerable.Empty<SelectListItem>().ToList();
                    //    break;
                }
            }

        }

        /// <summary>
        /// Check Authorised Data
        /// </summary>
        /// <param name="dictDataIds"></param>
        /// <returns></returns>
        public bool CheckAuthorisedData(Dictionary<DataEnityNames, object> dictDataIds)
        {
            var isAuthorised = true;

            if (CurrentUser.RoleID == (int)UserRoles.SuperAdmin || CurrentUser.RoleID == (int)UserRoles.Internal)
                return true;

            if (dictDataIds == null)
                return true;

            object dataCheckId = null;

            AuthorisedData authoriseData = new AuthorisedData(
                 centralOfficeService,
                 charityService,
                 branchService
                 );

            foreach (var dataId in dictDataIds)
            {
                dataCheckId = dataId.Value;

                switch (dataId.Key)
                {
                    case DataEnityNames.CentralOffice:
                        isAuthorised = authoriseData.CheckAuthorisedCentralOffice(dataCheckId);
                        break;

                    case DataEnityNames.Charity:
                        isAuthorised = authoriseData.CheckAuthorisedCharity(dataCheckId);
                        break;

                    case DataEnityNames.Branch:
                        isAuthorised = authoriseData.CheckAuthorisedBranch(dataCheckId);
                        break;

                    case DataEnityNames.Role:
                        isAuthorised = authoriseData.CheckAuthorisedRole(dataCheckId);
                        break;
                }

                if (!isAuthorised)
                    return isAuthorised;
            }
            return isAuthorised;
        }
        public IActionResult GetCustomRoles(int? orgID, int? charityID, int? branchID)
        {
            try
            {
                var roles = roleService.GetRolesByOrganization(GetOrgLevelID(orgID, UserRoles.Foodbank)).Where(e => e.ParentRoleID > 0);
                if (roles.IsNotNullAndNotEmpty())
                {
                    return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>>
                    {
                        IsSuccess = true,
                        Data = roles.Select(c => new SelectListItem
                        {
                            Text = c.RoleName,
                            Value = c.RoleID.ToString()
                        }).ToList()
                    });
                }
            }
            catch
            {
            }
            return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = string.Empty });
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
