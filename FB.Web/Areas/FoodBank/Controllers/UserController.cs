using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using FB.ModalMapper;
using FB.Web.Code;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
using System.Data;
using FB.Service;
using FB.Dto.Branch;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Globalization;
using System.Text.RegularExpressions;
using FB.Web.Code.Libs;

namespace FB.Web.Areas.FoodBank.Controllers
{

    [CustomActionFilterAdminAttribute]
    public class UserController : BaseController
    {
        private ICentralOfficeService centralOfficeService;
        private ICharityService charityService;
        private IBranchService branchService;
        private IUserService userService;
        private IRoleService roleService;
        private readonly ICountryService countryService;
        private IUserPreferenceService userPreferenceService;
        private IFoodbankService foodbankService;
        private IAddressService addressService;
        private IFoodbankDataAccessibilityService foodbankDataAccessibilityService;
        public UserController(
            ICentralOfficeService _centralOfficeService,
            ICharityService _charityService,
            IBranchService _branchService,
            IUserService _userService,
            IRoleService _roleService,
            ICountryService _countryService,
            IUserPreferenceService _userPreferenceService, IFoodbankService _foodbankService,IAddressService _addressService, IFoodbankDataAccessibilityService _foodbankDataAccessibilityService
            ) 
        {
            this.centralOfficeService = _centralOfficeService;
            this.charityService = _charityService;
            this.branchService = _branchService;
            this.userService = _userService;
            this.roleService = _roleService;
            this.countryService = _countryService;
            this.userPreferenceService = _userPreferenceService;
            this.foodbankService = _foodbankService;
            this.addressService = _addressService;
            this.foodbankDataAccessibilityService = _foodbankDataAccessibilityService;
        }

        /// <summary>
        /// To view page of user listing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[CustomActionFilterAttribute]
        public IActionResult Index()
        {
            BindStaticViewBags(BindViewBag.Organisation, BindViewBag.Charity, BindViewBag.Branch);
            return View();
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
                    case BindViewBag.Charity:
                        if (CurrentUser.OrganisationID > 0 && CurrentUser.CharityID == null && CurrentUser.BranchID == null)
                            ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.OrganisationID, CurrentUser.UserID).Select(c => new SelectListItem { Text = c.CharityName.AddCharityPrefix(c.Prefix), Value = c.CharityId.ToString() }).ToList();
                        else
                            ViewBag.Charities = Enumerable.Empty<SelectListItem>().ToList();
                        break;
                    case BindViewBag.Branch:
                        if (CurrentUser.CharityID > 0 && CurrentUser.BranchID == null)
                            ViewBag.Branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.CharityID.Value, userID: CurrentUser.UserID).Select(m => new SelectListItem { Text = m.BranchDescription.AddBranchPrefix(m.BranchReference, m.Charity?.Prefix), Value = m.BranchId.ToString() }).ToList();
                        else
                            ViewBag.Branches = Enumerable.Empty<SelectListItem>().ToList();
                        break;
                       
                }
            }
        }

        //public void MMORegBindStaticViewBags(params BindViewBag[] bindViewBags)
        //{
        //    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
        //    foreach (BindViewBag item in bindViewBags)
        //    {
        //        switch (item)
        //        {
        //            case BindViewBag.Organisation:
        //                if (CurrentUser.OrganisationID == 0)
        //                    ViewBag.Organisations = centralOfficeService.GetMMORegCentralOffices().Select(c => new SelectListItem { Text = c.OrganisationName, Value = c.CentralOfficeId.ToString() }).ToList();
        //                else
        //                    ViewBag.Organisations = Enumerable.Empty<SelectListItem>().ToList();
        //                break;
        //            case BindViewBag.Charity:
        //                if (CurrentUser.RoleID == (int)UserRoles.Agent && CurrentUser.AgentID > 0 && controllerName == "claim")
        //                    ViewBag.Charities = charityService.GetCharitiesForClaimByAgent(CurrentUser.AgentID).Select(m => new SelectListItem { Text = m.CharityName, Value = m.CharityId.ToString() }).ToList();
        //                else if (CurrentUser.OrganisationID > 0 && CurrentUser.CharityID == null && CurrentUser.BranchID == null)
        //                    ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.OrganisationID, CurrentUser.UserID).Select(c => new SelectListItem { Text = c.CharityName.AddCharityPrefix(c.Prefix), Value = c.CharityId.ToString() }).ToList();
        //                else
        //                    ViewBag.Charities = Enumerable.Empty<SelectListItem>().ToList();
        //                break;
        //            case BindViewBag.Branch:
        //                if (CurrentUser.CharityID > 0 && CurrentUser.BranchID == null)
        //                    ViewBag.Branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.CharityID.Value, userID: CurrentUser.UserID).Select(m => new SelectListItem { Text = m.BranchDescription.AddBranchPrefix(m.BranchReference, m.Charity?.Prefix), Value = m.BranchId.ToString() }).ToList();
        //                else
        //                    ViewBag.Branches = Enumerable.Empty<SelectListItem>().ToList();
        //                break;
        //        }
        //    }

        //}

        /// <summary>
        /// To get users for server side table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        
        public IActionResult GetUsers(DataTableServerSide model, int? orgID,int? charitID,int? BranchID)
        {

            KeyValuePair<int, List<UserDto>> users = userService.GetUsersForFoodbank(model, CurrentUser.UserID, CurrentUser.RoleID, CurrentUser.FoodbankId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = users.Key,
                recordsFiltered = users.Key,
                data = users.Value.Select(u => new List<object> {
                    u.UserName,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.PrimaryMobile,
                    u.IsActive ? "<input type='checkbox' value="+u.UserID+" name='Active' id='Active' checked='checked' class='switchBox' />" :"<input type='checkbox' value="+u.UserID+" name='Active' id='Active' class='switchBox' />" ,
                    "<a data-toggle='modal' data-target='#modal-create-edit-user'  href='/FoodBank/User/CreateEdit/"+ u.UserID +"' class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>" + "&nbsp;" +
                    "<a data-toggle='modal' data-target='#modal-delete-user' href='/FoodBank/User/delete/"+ u.UserID +"' class='btn btn-danger grid-btn btn-sm ps3'>Delete <i class='fa fa-trash-o'></i></a>" + "&nbsp;"+
                    //(u.IsMS365EnableForCentralOfficeOrCharity ? "<a data-toggle='modal' data-target='#modal-create-edit-ms-user'  href=" + Url.Action("CreateEditMSUser", "User", new { id = u.UserID }) + " class='btn btn-primary grid-btn btn-sm'>Add Microsoft 365 User <i class='fa fa-edit'></i></a>"  + "&nbsp;" : "" ) +
                  (u.IsFoodbank ? ((u.RoleID == (int)UserRoles.Foodbank || u.RoleID == (int)UserRoles.SuperAdmin ) ?    "<a data-toggle='modal' data-target='#modal-user-accessdata-user' href='/FoodBank/User/FoodbankUserDataAccess/?userID="+ u.UserID  + "' class='btn btn-primary grid-btn btn-sm ps3'>Edit Data Access <i class='fa fa-edit'></i></a>":"" ) :""),
                   //"<a data-toggle='modal' data-target='#modal-user-accessdata-user' href='" + Url.Action("FoodbankUserDataAccess", "user", new { userID = u.UserID }) + "' class='btn btn-primary grid-btn btn-sm ps3'>Edit Data Access <i class='fa fa-edit'></i></a>",

                    u.UserID
                })
            });
        }


        /// <summary>
        /// To get or open the partial view to edit/create user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
       // [DataActionFilter("id", DataEnityNames.User)]
        public IActionResult CreateEdit(int? id = null)
        {
            BindViewBags(isCreateEdit: true);
            if (id.HasValue)
            {
                var user = userService.GetUser(id.Value);
                var userEntity = UserDtoMapper.Map(user);
                BindDropDownsViewBags(userEntity);
                if (user.UserRole.Role.ParentRoleId > 0)
                {
                    userEntity.CustomRoleID = userEntity.RoleID;
                    userEntity.RoleID = -1;
                }
                var fbaddress = addressService.GetFBAddressByUserid(id??0);
                if (fbaddress != null)
                {
                    userEntity.HouseName= fbaddress.HouseName;
                    userEntity.Street = fbaddress.Street  ;
                    userEntity.HouseName = fbaddress.HouseNumber;
                    userEntity.City = fbaddress.City ;
                    userEntity.Postcode = fbaddress.Postcode ;
                    userEntity.CountryId = fbaddress.CountryId ;
                    userEntity.CountryName = fbaddress.CountryName ;
                    userEntity.OtherAddressLine = fbaddress.OtherAddressLine ;
                    userEntity.District = fbaddress.District ;
                   
                }
                return PartialView("_CreateEdit", userEntity ?? new UserDto());
            }
            BindDropDownsViewBags(null, false);
            return PartialView("_CreateEdit", new UserDto { IsActive = true,  IsFoodbank = true });
        }


        /// <summary>
        /// To bind view bags
        /// </summary>
        /// <param name="countryID"></param>
        [NonAction]
        public void BindViewBags(int? countryID = null, bool isCreateEdit = false)
        {
           
                ViewBag.Genders = EnumHelper.SelectListFor<Gender>();
                var role = roleService.GetRoleById(CurrentUser.RoleID);
                ViewBag.RoleName = role.RoleName;
                List<RoleDto> roles = new List<RoleDto>();
                if (role.RoleName == UserRoles.SuperAdmin.ToString())
                    roles = roleService.GetRoles().Where(c => c.ParentRoleID == null &&
                    (
                    c.RoleName.ToLower() == UserRoles.Foodbank.ToString().ToLower() ||
                    c.RoleName.ToLower() == UserRoles.FoodbankStaff.ToString().ToLower() 
                    )
                    ).ToList();
                else if (role.RoleName == UserRoles.Foodbank.ToString())
                    roles = roleService.GetRoles().Where(c => c.RoleName.ToLower() == UserRoles.FoodbankStaff.ToString().ToLower()).ToList();
                else if (role.RoleName == UserRoles.FoodbankStaff.ToString())
                    roles = roleService.GetRoles().Where(c => c.RoleName.ToLower() == UserRoles.FoodbankStaff.ToString().ToLower()).ToList();


                var mainRoles = roles.Where(e => !e.ParentRoleID.HasValue).Select(c => new SelectListItem
                {
                    Text = c.RoleName,
                    Value = c.RoleID.ToString()
                }).ToList();

                if (!CurrentUser.CharityID.HasValue)
                    mainRoles.Add(new SelectListItem { Text = "Custom", Value = "-1" });

                ViewBag.Roles = mainRoles;
                ViewBag.Organisations = new List<SelectListItem>();
                ViewBag.Charities = new List<SelectListItem>();
                ViewBag.Branches = new List<SelectListItem>();
            
                ViewBag.Countries = countryService.GetCountries().Select(c => new SelectListItem
                {
                    Text = c.CountryName,
                    Value = c.CountryId.ToString(),
                    Selected = (countryID == null) ? (c.CountryName == Constants.DefaultCountry) : countryID == c.CountryId
                }).ToList();
            

            ViewBag.CustomRoles = new List<SelectListItem>();
        }

        public string UserAvailability(string userName)
        {
            if (userService.IsUserNameExist(userName))
            {
                return "Username already exist";
            }
            return "";
        }
        /// <summary>
        /// for binding the view bags for dropdowns
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isEdit"></param>
        [NonAction]
        public void BindDropDownsViewBags(UserDto user, bool isEdit = true)
        {
            if (isEdit)
            {
                    var roles = roleService.GetRolesByFoodbank(CurrentUser.FoodbankId).Where(e => e.ParentRoleID > 0);
                    if (roles.IsNotNullAndNotEmpty())
                    {
                        ViewBag.CustomRoles = roles.Select(c => new SelectListItem
                        {
                            Text = c.RoleName,
                            Value = c.RoleID.ToString()
                        }).ToList();

                    }
            }
        }



        /// <summary>
        /// To change status of user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [DataActionFilter("id", DataEnityNames.User)]
        public IActionResult Active(int id)
        {
            try
            {
                var user = userService.GetUser(id);
                user.Active = !user.Active;
                userService.Save(user, user.CreatedBy, false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true });
            }
            catch
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false });
            }
        }

        /// <summary>
        /// for binding the organisation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BindOrganisations()
        {
            var result = centralOfficeService.GetCentralOffices().Select(c => new SelectListItem
            {
                Text = c.OrganisationName,
                Value = c.CentralOfficeId.ToString()
            }).ToList();

            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }

        /// <summary>
        /// for binding the Charities by OrganisationID
        /// </summary>
        /// <param name="organisationID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BindCharities(int organisationID)
        {
            var result = charityService.GetCharityByOrganisationID(organisationID, CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.CharityName,
                Value = c.CharityID.ToString()
            }).ToList();
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }

        /// <summary>
        /// for binding branches by CharityID
        /// </summary>
        /// <param name="charityID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BindBranches(int charityID)
        {
            charityID = charityID == 0 ? CurrentUser.CharityID ?? 0 : charityID;
            var result = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, charityID, userID: CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();

            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }


        /// <summary>
        /// To save the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(UserDto model)
        {
            var foodbank = foodbankService.GetFoodbankByUserId(CurrentUser.UserID);
            
                model.CentralOfficeID = foodbank.User.CentralOfficeId;
                model.CharityID = foodbank.User.CharityId;
                model.BranchID = foodbank.User.BranchId;

            if (model.RoleID == -1)
            {
                model.RoleID = model.CustomRoleID;
            }

            ModelState.Remove("CentralOfficeID");
            ModelState.Remove("CharityID");
            ModelState.Remove("BranchID");
            ModelState.Remove("CustomRoleID");

            ModelState.Remove("OldPassword");
            ModelState.Remove("Gender");
            if (model.UserID > 0 && model.IsPasswordChange == false)
            {
                ModelState.Remove("Password");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("PasswordQuestion");
                ModelState.Remove("PasswordAnswer");
                ModelState.Remove("PasswordSalt");
            }

            if (ModelState.IsValid && model.UserName.Contains("-"))
            {
                ModelState.AddModelError("UserName", "*space or hyphen not allowed in username");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool isAccessible = true;
                    if (CurrentUser.RoleID != (int)UserRoles.SuperAdmin && CurrentUser.RoleID != (int)UserRoles.Internal)
                    {
                        if (CurrentUser.RoleID == (int)UserRoles.Organisation)
                            isAccessible = new int[] { (int)UserRoles.SuperAdmin, (int)UserRoles.Organisation }.Any(e => e == model.RoleID) == false;
                        else if (CurrentUser.RoleID == (int)UserRoles.Charity)
                            isAccessible = new int[] { (int)UserRoles.SuperAdmin, (int)UserRoles.Organisation, (int)UserRoles.Charity }.Any(e => e == model.RoleID) == false;

                        if (isAccessible)
                        {
                            //Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>
                            //{
                            //    { DataEnityNames.User, model.UserID },
                            //    { DataEnityNames.CentralOffice, model.CentralOfficeID },
                            //    { DataEnityNames.Charity, model.CharityID },
                            //    { DataEnityNames.Branch, model.BranchID }
                            //};
                            isAccessible = true;//CheckAuthorisedData(dictDataIds);
                        }
                    }

                    if (isAccessible)
                    {
                        model.Postcode = model.Postcode.Trim();
                        bool IsEntryForDataAccess = false;
                        if (model.RoleID != ((int)UserRoles.Charity) && model.RoleID != ((int)UserRoles.Branch))
                        {
                            model.CharityID = null;
                            model.BranchID = null;
                        }

                        if (userService.IsUserNameExist(model.UserName, model.UserID))
                        {
                            ModelState.AddModelError("Error!", "Username already exist.");
                            return CreateModelStateErrors();
                        }

                        var user = userService.GetUser(model.UserID);
                        if (user == null)
                            user = new Data.Models.User();
                        else
                        {
                            model.AddedDate = user.AddedDate;
                            model.LastLoginDate = user.LastLoginDate;
                            if (user.UserRole.RoleId != model.RoleID || user.CentralOfficeId != model.CentralOfficeID || user.CharityId != model.CharityID)
                            {
                                var userdataaccesslist = foodbankDataAccessibilityService.GetUserDataAccessibility(model.UserID);
                                userdataaccesslist.ForEach(x => foodbankDataAccessibilityService.Delete(x.UserAccessId));
                                IsEntryForDataAccess = true;
                            }
                        }

                        if (model.UserID == 0 || model.IsPasswordChange)
                        {
                            string saltValue = Common.GetRandomPasswordSalt();
                            model.Password = EncryptionUtils.HashPassword(model.Password, saltValue, DateTime.Now);
                            model.PasswordSalt = saltValue;
                            model.PasswordAnswer = EncryptionUtils.Encrypt(model.PasswordAnswer, saltValue);
                            model.LastPasswordChange = DateTime.Now;
                            model.LastLoginDate = DateTime.Now;
                        }
                        else
                        {
                            model.Password = user.Password;
                            model.PasswordSalt = user.PasswordSalt;
                            model.PasswordQuestion = user.PasswordQuestion;
                            model.PasswordAnswer = user.PasswordAnswer;
                        }

                        model.AuditUserId = CurrentUser.UserID;
                        model.ModifiedDate = DateTime.Now;
                        model.IP = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                        model.Gender = (byte)Gender.Male;
                        model.CreatedBy = CurrentUser.UserID;
                        model.AuditUserId = CurrentUser.UserID;
                        model.FoodbankId = foodbank.Id;
                        user = UserDtoMapper.Map(model, user);
                        if (model.UserID > 0)
                        {
                            userService.Save(user, user.CreatedBy, false);
                            var userRole = user.UserRole;
                            userRole.RoleId = model.RoleID;
                            roleService.Save(userRole, false);

                           
                          
                            var fbaddress = addressService.GetFBAddressByUserid(model.UserID);
                            if (fbaddress != null)
                            {
                                fbaddress.HouseName = model.HouseName;
                                fbaddress.Street = model.Street;
                                fbaddress.HouseNumber = model.HouseNumber;
                                fbaddress.City = model.City;
                                fbaddress.Postcode = model.Postcode;
                                fbaddress.CountryId = model.CountryId;
                                fbaddress.CountryName = "";
                                fbaddress.OtherAddressLine = model.OtherAddressLine;
                                fbaddress.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                                addressService.Save(fbaddress,false);
                            }
                            if (IsEntryForDataAccess)
                            {
                                if (user.BranchId == null)
                                {
                                    FoodbankUserDataAccessibility userDataAccessibility = new FoodbankUserDataAccessibility
                                    {
                                        CentralOfficeId = user.CentralOfficeId,
                                        CharityId = user.CharityId,
                                        UserId = user.UserId
                                    };
                                    foodbankDataAccessibilityService.Save(userDataAccessibility, true);
                                }
                            }
                            ShowSuccessMessage("Success!", "User updated successfully.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("index") });
                        }
                        else
                        {
                            userService.Save(user, user.CreatedBy, true);
                            UserRole userRole = new UserRole
                            {
                                UserId = user.UserId,
                                RoleId = model.RoleID,
                            }; 
                            roleService.Save(userRole, true);
                            Fbaddress fbaddress = new Fbaddress();
                            fbaddress.HouseName = model.HouseName;
                            fbaddress.Street = model.Street;
                            fbaddress.HouseNumber = model.HouseNumber;
                            fbaddress.City = model.City;
                            fbaddress.Postcode = model.Postcode;
                            fbaddress.CountryId = model.CountryId;
                            fbaddress.CountryName = "";
                            fbaddress.OtherAddressLine = model.OtherAddressLine;
                            fbaddress.UserId = user.UserId;
                            fbaddress.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                            addressService.Save(fbaddress, fbaddress.Id == 0);
                            if (IsEntryForDataAccess)
                            {
                                if (user.BranchId == null)
                                {
                                    FoodbankUserDataAccessibility userDataAccessibility = new FoodbankUserDataAccessibility
                                    {
                                        CentralOfficeId = user.CentralOfficeId,
                                        CharityId = user.CharityId,
                                        UserId = user.UserId
                                    };
                                    foodbankDataAccessibilityService.Save(userDataAccessibility, true);
                                }
                            }
                            ShowSuccessMessage("Success!", "User saved successfully.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("index") });
                        }
                        
                    }
                    else
                    {
                        return RedirectAccessDenied();
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.GetBaseException().Message;
                    if (message.Contains("UNIQUE KEY"))
                    {
                        if (message.Contains("uc_branch_reference"))
                            message = "Donor with this reference number already exists within same branch";
                        else if (message.Contains("@"))
                            message = "Email already exists";
                        else
                            message = "This username is already exists";
                    }
                    ModelState.AddModelError("Error!", message);
                }
            }
            return CreateModelStateErrors();
        }



        /// <summary>
        /// for giving MMO user data accessibility.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet]
        //[CustomActionFilterAttribute]
        [DataActionFilter("userID", DataEnityNames.User)]
        public IActionResult FoodbankUserDataAccess(int userID)
        {
            FoodbankUserDataAccessibilityDto userDataAccessibilityDto = new FoodbankUserDataAccessibilityDto()
            {
                BranchesId = string.Empty,
                UserID = userID,
                UserFullName = "",
                CentralOfficeName = "",
                CharityName = "",
                IsFullAccess = false,
                IsPrivateNotesAccess = false
            };

            var userdataaccesslist = foodbankDataAccessibilityService.GetUserDataAccessibility(userID);
            if (userdataaccesslist.Count > 0)
            {
                userDataAccessibilityDto.BranchesId = string.Join(",", userdataaccesslist.Select(b => b.BranchId));
                userDataAccessibilityDto.IsFullAccess = string.IsNullOrEmpty(userDataAccessibilityDto.BranchesId) ? true : false;
            }

            List<SelectListItem> charities = new List<SelectListItem>();
            var userentity = userService.GetUser(userID);
            if (userentity != null && userentity.CentralOfficeId.HasValue)
            {
                if (userentity.CharityId.HasValue && userentity.CharityId > 0)
                {
                    ViewBag.CharitiesNotAccess = GetCharities(userDataAccessibilityDto.BranchesId, false, userentity.CharityId, null, false);
                    ViewBag.CharitiesAccess = GetCharities(userDataAccessibilityDto.BranchesId, true, userentity.CharityId, null, false);
                }
                else
                {
                    ViewBag.CharitiesNotAccess = GetCharities(userDataAccessibilityDto.BranchesId, false, null, userentity.CentralOfficeId, false);
                    ViewBag.CharitiesAccess = GetCharities(userDataAccessibilityDto.BranchesId, true, null, userentity.CentralOfficeId, false);
                }

                userDataAccessibilityDto.CentralOfficeID = userentity.CentralOfficeId.Value;
                userDataAccessibilityDto.UserFullName = userentity.FirstName + " " + userentity.LastName;

                if (userentity.CentralOfficeId.HasValue)
                {
                    var centraloffice = centralOfficeService.GetCentralOffice(userentity.CentralOfficeId.Value);
                    if (centraloffice != null)
                    {
                        userDataAccessibilityDto.CentralOfficeName = centraloffice.OrganisationName;
                    }

                }
                if (userentity.CharityId.HasValue)
                {
                    var charity = charityService.GetCharity(userentity.CharityId.Value);
                    if (charity != null)
                    {
                        userDataAccessibilityDto.CharityName = charity.CharityName;
                    }

                }

                userDataAccessibilityDto.CentralOfficeName = userentity.CentralOfficeNavigation.OrganisationName;
                userDataAccessibilityDto.CharityName = userentity.Charity != null ? userentity.Charity.CharityName : "";
            }
            else
            {
                ViewBag.CharitiesNotAccess = GetCharities(userDataAccessibilityDto.BranchesId, false, null, null, false);
                ViewBag.CharitiesAccess = GetCharities(userDataAccessibilityDto.BranchesId, true, null, null, false);
                userDataAccessibilityDto.UserFullName = userentity.FirstName + " " + userentity.LastName;
            }
            userDataAccessibilityDto.IsPrivateNotesAccess = userentity.IsPrivateNotesAccess;

            return PartialView("_FoodbankUserDataAccess", userDataAccessibilityDto);

        }


        [NonAction]
        public List<SelectListItem> GetCharities(string branchesId, bool isAccess, int? charityID = null, int? orgId = null, bool IsMGO = true)
        {
            string[] brancharr = branchesId.Split(',');
            List<int> branches = new List<int>();
            foreach (var item in brancharr)
            {
                int branchid;
                if (int.TryParse(item, out branchid))
                    branches.Add(branchid);
            }

            List<string> Groups = new List<string>();
            var branchlist = branchService.GetBranches(charityID, orgId, userID: CurrentUser.UserID)/*.Where(m => (IsMGO ? m.IsMgo == true : m.IsMmo == true))*/
                .Where(e => isAccess ? branches.Contains(e.BranchId) : !branches.Contains(e.BranchId)).ToList();
            Groups.AddRange(branchlist.Select(b => string.Format("{0} {1}", b.Charity.Prefix.AddBracket(), b.Charity.CharityName).Trim()));
            Groups = Groups.Distinct().ToList();
            List<SelectListGroup> SelectGroups = new List<SelectListGroup>();
            foreach (var groupname in Groups)
            {
                SelectGroups.Add(new SelectListGroup { Name = groupname });
            }

            List<SelectListItem> selectlist = new List<SelectListItem>();

            foreach (var selectgroup in SelectGroups)
            {
                foreach (var branch in branchlist)
                {
                    string groupdetail = string.Format("{0} {1}", branch.Charity.Prefix.AddBracket(), branch.Charity.CharityName).Trim();
                    if (groupdetail == selectgroup.Name)
                    {
                        SelectListItem listItem = new SelectListItem();
                        listItem.Group = selectgroup;
                        listItem.Text = string.Format("{0} {1}", branch.BranchReference.ISNULL(branch.Charity.Prefix).AddBracket(), branch.BranchDescription).Trim();
                        listItem.Value = branch.BranchId.ToString();
                        listItem.Selected = branches.Contains(branch.BranchId);
                        selectlist.Add(listItem);
                    }
                }
            }

            return selectlist.OrderBy(o => o.Group.Name).ThenBy(oo => oo.Text).ToList();

       
        }

        /// <summary>
        /// To save the MMO User Data Accessibility
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [DataActionFilterAttribute("UserID", DataEnityNames.User)]
        public IActionResult FoodbankUserDataAccess(FoodbankUserDataAccessibilityDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userentity = userService.GetUser(model.UserID);
                    if (userentity != null && userentity.PersonId == null && userentity.BranchId == null && userentity.UserRole != null ? userentity.UserRole.Role.RoleName.Trim().ToLower() != UserRoles.TechnicalSupport.ToString().Trim().ToLower() : true)
                    {
                        var userdataaccesslist = foodbankDataAccessibilityService.GetUserDataAccessibility(model.UserID);
                        userdataaccesslist.ForEach(x => foodbankDataAccessibilityService.Delete(x.UserAccessId));
                        if (model.IsFullAccess)
                            model.BranchesId = string.Empty;
                        if (!string.IsNullOrEmpty(model.BranchesId))
                        {
                            foreach (string id in model.BranchesId.Split(',').ToArray())
                            {
                                Branch branch = branchService.GetBranch(Convert.ToInt32(id));
                                if (branch != null)
                                {
                                    FoodbankUserDataAccessibility mmoUserDataAccessibility = new FoodbankUserDataAccessibility
                                    {
                                        BranchId = branch.BranchId,
                                        CentralOfficeId = branch.Charity.CentralOfficeId,
                                        CharityId = branch.CharityId,
                                        UserId = model.UserID
                                    };
                                    foodbankDataAccessibilityService.Save(mmoUserDataAccessibility, true);
                                }

                            }
                        }
                        else
                        {
                            if (model.IsFullAccess)
                            {
                                FoodbankUserDataAccessibility mmoUserDataAccessibility = new FoodbankUserDataAccessibility
                                {
                                    CharityId = userentity.CharityId,
                                    CentralOfficeId = userentity.CentralOfficeId,
                                    UserId = model.UserID
                                };
                                foodbankDataAccessibilityService.Save(mmoUserDataAccessibility, true);
                            }
                        }

                        if (userentity != null)
                        {
                            //userentity.IsPrivateNotesAccess = model.IsPrivateNotesAccess;
                            //userService.Save(userentity, userentity.CreatedBy, false);
                        }



                        ShowSuccessMessage("Success!", "User data access updated successfully.", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("index") });
                    }
                    else
                    {
                        ModelState.AddModelError("Error!", "This user is not authorised for data access.Please contact to administrator !!");
                    }

                }
                ModelState.AddModelError("Error!", "form is not valid !!");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error!", "There is some internal problem");
            }

            return CreateModelStateErrors();
        }

        /// <summary>
        /// To delete the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [DataActionFilter("id", DataEnityNames.User)]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure you want to delete this User?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete User " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        [DataActionFilter("id", DataEnityNames.User)]
        public IActionResult Delete(int id, IFormCollection FC)
        {
            try
            {
                userPreferenceService.Delete(id);
                var fbaddress = addressService.GetFBAddressByUserid(id);
                if (fbaddress != null)
                {
                    addressService.FbaddressDelete(fbaddress.Id);
                }
                //mmoUserDataAccessibilityService.DeleteByUserId(id);
                userService.Delete(id);
                ShowSuccessMessage("Success!", "User deleted successfully.", false);
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                {
                    Regex pattern = new Regex(@"table .(?<tableName>\w+.*?\w+)");
                    Match match = pattern.Match(message);
                    string tableName = match.Groups["tableName"].Value;
                    message = "You can't delete this user because it has records attached to it in table => " + tableName;
                }
                ShowErrorMessage("Error!", message, false);
            }

            return RedirectToAction("Index");
        }

        [DataActionFilter("id", DataEnityNames.User)]
        public IActionResult CreateEditMSUser(int id)
        {
            GraphAccessDto graphAccess = new GraphAccessDto();
            try
            {
                MSUserDto model = new MSUserDto();
                var user = userService.GetUser(id);
                model.UserId = user.UserId;
                model.MSObjectID = user.MsuserObjectId;
                model.MSUserPrincipalName = user.MsuserPrincipalName;
                graphAccess = userService.GetMSGraphApiAccess(user.UserId);
                if (!graphAccess.TenantId.IsNotNullAndNotEmpty())
                {
                    return RedirectAccessDenied();

                }
                if (user.MsuserObjectId.IsNotNullAndNotEmpty())
                {
                    //var msUser = MSGraphAPI.GetUser(user.MsuserObjectId, graphAccess.TenantId);
                   // model.DisplayName = msUser.Result.DisplayName;
                    //model.UserName = user.UserName;
                }
                else
                {
                    model.DisplayName = string.Format("{0} {1}", user.FirstName, user.LastName);
                    model.UserName = user.UserName;

                }
                //model.Domain = MSGraphAPI.GetMSDomain(graphAccess.TenantId).Result;
                return PartialView("_CreateEditMSUser", model);
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;

                if (message.Contains("Authorization_IdentityNotFound"))
                {
                    message = string.Format(Constants.Authorization_IdentityNotFound, graphAccess.TenantId, AzureAdKeys.ClientId);
                }
                else if (message.Contains("Authorization_RequestDenied"))
                {
                    message = string.Format(Constants.Authorization_RequestDenied, AzureAdKeys.ClientId);
                }

                ShowErrorMessage("Error!", message, false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "redirect", IsSuccess = false, RedirectUrl = @Url.Action("index", "user") });
            }
        }


        /// <summary>
        /// To save the membership Code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEditMSUser(MSUserDto model)
        {
            try
            {
                if (model.UserId > 0 && model.IsPasswordChange == false)
                {
                    ModelState.Remove("Password");
                    ModelState.Remove("ConfirmPassword");
                }

                if (ModelState.IsValid)
                {
                    Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>();
                    dictDataIds.Add(DataEnityNames.User, model.UserId);
                    var isAccessible = false;// ; CheckAuthorisedData(dictDataIds);
                    if (isAccessible)
                    {
                        GraphAccessDto graphAccess = userService.GetMSGraphApiAccess(model.UserId);
                        if (!graphAccess.TenantId.IsNotNullAndNotEmpty())
                        {
                            return RedirectAccessDenied();
                        }
                        string MSUserId = string.Empty;
                        string MSUserPrincipalName = string.Empty;
                        //if (model.MSObjectID.IsNotNullAndNotEmpty())
                        //{
                        //    var userUpdate = new Microsoft.Graph.User
                        //    {
                        //        AccountEnabled = true,
                        //        DisplayName = model.DisplayName,
                        //        UserPrincipalName = string.Format("{0}@{1}", model.UserName, model.Domain),
                        //        MailNickname = model.UserName,
                        //        UsageLocation = "GB",
                        //    };
                        //    MSUserId = model.MSObjectID;
                        //    MSUserPrincipalName = model.MSUserPrincipalName;
                        //    var msuser = MSGraphAPI.CreateUpdateUser(userUpdate, model.MSObjectID, graphAccess.TenantId);
                        //}
                        //else
                        //{
                        //    var newUser = new Microsoft.Graph.User
                        //    {
                        //        AccountEnabled = true,
                        //        DisplayName = model.DisplayName,
                        //        UserPrincipalName = string.Format("{0}@{1}", model.UserName, model.Domain),
                        //        MailNickname = model.UserName,
                        //        UsageLocation = "GB",
                        //        PasswordProfile = new PasswordProfile
                        //        {
                        //            ForceChangePasswordNextSignIn = false,
                        //            Password = model.Password
                        //        }
                        //    };
                        //    var msuser = MSGraphAPI.CreateUpdateUser(newUser, null, graphAccess.TenantId).Result;
                        //    MSUserId = msuser.Id;
                        //    MSUserPrincipalName = msuser.UserPrincipalName;
                        //}
                        var user = userService.GetUser(model.UserId);
                        user.AuditUserId = CurrentUser.UserID;
                        user.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString(); //Request.UserHostAddress;                                                                                                                                      //user.MsuserObjectId = msuser.Id;
                        user.CreatedBy = CurrentUser.UserID;
                        user.MsuserObjectId = MSUserId;
                        user.TenantId = graphAccess.TenantId;
                        user.MsuserPrincipalName = MSUserPrincipalName;
                        userService.Save(user, user.CreatedBy, false);
                       // var userAssignLicense = MSGraphAPI.UserAssignLicense(MSUserId, graphAccess.TenantId);
                        //if (userAssignLicense.Result)
                        //{
                        //    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "User details updated with assign license successfully.", IsSuccess = true });
                        //}
                        //else
                        //{
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "User updated successfully,but license not assign. Pleaes contact with you Organisation or Charity.", IsSuccess = true });

                        //}
                    }
                    else
                    {
                        return RedirectAccessDenied();
                    }
                }
                else
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = Constants.CustomRequiredErrorMessage, IsSuccess = false });
                }
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (centralOfficeService != null)
            {
                centralOfficeService.Dispose();
                centralOfficeService = null;
            }

            if (charityService != null)
            {
                charityService.Dispose();
                charityService = null;
            }

            if (branchService != null)
            {
                branchService.Dispose();
                branchService = null;
            }
            if (userService != null)
            {
                userService.Dispose();
                userService = null;
            }

            if (userPreferenceService != null)
            {
                userPreferenceService.Dispose();
                userPreferenceService = null;
            }

            if (addressService != null)
            {
                addressService.Dispose();
                addressService = null;
            }

            if (roleService != null)
            {
                roleService.Dispose();
                roleService = null;
            }

            base.Dispose(disposing);
        }
    }
}
