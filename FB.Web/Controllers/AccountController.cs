using AutoMapper;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto.Branch;
using FB.ModalMapper;
using FB.Service;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;
        private IPersonService personService;
        private IBranchService branchService;
        private ICharityService charityService;
        private ICentralOfficeService centralofficeService;
        private IMenuService menuService;
        private IForgotPasswordService forgotPasswordService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        private readonly IQuickDonorGiftService quickDonorGiftService;
        private readonly ICountryService countryService;
        private readonly IAddressService addressService;
        private readonly IMyReferralService ReferralService;
        private readonly IContactService contactService;
        private readonly IVolunteerService volunteerService;
        private readonly IFoodbankService foodbankService;
        public AccountController(IUserService _userService, IMenuService _menuService, IPersonService _personService, IBranchService _branchService, ICharityService _charityService,
            ICentralOfficeService _centralofficeService, IForgotPasswordService _forgotPasswordService, IRoleService _roleService, IMapper _mapper,
            IQuickDonorGiftService _quickDonorGiftService, ICountryService _countryService, IAddressService _addressService, IMyReferralService _ReferralService, IContactService _contactService,
            IVolunteerService _volunteerService, IFoodbankService _foodbankService)
        {
            userService = _userService;
            menuService = _menuService;
            personService = _personService;
            branchService = _branchService;
            charityService = _charityService;
            centralofficeService = _centralofficeService;
            forgotPasswordService = _forgotPasswordService;
            roleService = _roleService;
            mapper = _mapper;
            quickDonorGiftService = _quickDonorGiftService;
            countryService = _countryService;
            addressService = _addressService;
            ReferralService = _ReferralService;
            contactService = _contactService;
            volunteerService = _volunteerService;
            foodbankService = _foodbankService;
        }
        public IActionResult Login()
        {
            if (CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View(new LoginViewDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewDto model)
        {
            try
            {
                 if (ModelState.IsValid)
                {
                    var user = userService.GetUserEntityByUserName(model.UserName);

                    if (user != null)
                    {
                        bool isCOFE = CurrentUser.IsAuthenticated;
                        string message = "";
                        if (user.LoginAttempt.HasValue && user.LoginAttemptDate.HasValue && user.LoginAttempt.Value >= 7)
                        {
                            if (Math.Abs(DateTime.Now.Subtract(user.LoginAttemptDate.Value).TotalMinutes) <= 30)
                            {
                                #region Remove session
                                await RemoveAuthentication();
                                #endregion
                                message = String.Format("{0} {1}", "For security reasons your user account has been locked for 30 minutes.", isCOFE ? " Link to Church of England failed." : "");
                                ShowInfoMessage("Info!", message, true);
                                return View(model);
                            }
                            else
                            {
                                user.LoginAttempt = 0;
                                userService.Save(user, user.CreatedBy, false);
                            }
                        }

                        if (ModelState.IsValid)
                        {
                            List<User> users = userService.GetUser(model.UserName, model.Password);

                            //Logic for update password with new password hash algo
                            if (users.Any())
                            {
                                User loginUser = users.FirstOrDefault();
                                var encrptedPwd = EncryptionUtils.HashPassword(model.Password, user.PasswordSalt, DateTime.Now);
                                if (loginUser.Password != encrptedPwd)
                                {
                                    if (loginUser != null)
                                    {
                                        loginUser.Password = encrptedPwd;
                                        loginUser.LastPasswordChange = DateTime.Now;
                                        loginUser.LastLoginDate = DateTime.Now;
                                        userService.Save(loginUser, loginUser.CreatedBy, false);
                                    }
                                }
                            }
                            //End

                            if (users != null && users.Count() > 0)
                            {
                                //if (!IsvalidLicence(users.First()))
                                //{
                                //    #region Remove session
                                //    await RemoveAuthentication();
                                //    #endregion
                                //    message = String.Format("{0} {1}", "Your organisation's License has expired, please contact the sales team on 01902 714030 for further information", isCOFE ? " Link to Church of England failed." : "");
                                //    ShowErrorMessage("Error!", message);
                                //    return View(model);
                                //}

                                if (!IsActiveUser(users.First()))
                                {
                                    #region Remove session
                                    await RemoveAuthentication();
                                    #endregion
                                    message = String.Format("{0} {1}", "The Organisation/Charity/Branch you are trying to access is currently unavailable. Please contact Data Developments for further information", isCOFE ? " Link to Church of England failed." : "");
                                    ShowErrorMessage("Error!", message);
                                    return View(model);
                                }
                                string TenantId = string.Empty;
                                GraphAccessDto graphAccess = userService.GetMSGraphApiAccess(user.UserId);
                                if (graphAccess != null)
                                {
                                    TenantId = graphAccess.TenantId;
                                }

                                var foodbank = foodbankService.GetFoodbankByUserId(user.UserId);

                                List<UserDataAccessDto> userDataAccesslist = new List<UserDataAccessDto>();
                                if (users.Count() > 1)
                                {
                                    foreach (var item in users)
                                    {
                                        item.LastLoginDate = DateTime.Now;
                                        item.LoginAttempt = 0;
                                        item.Cofeguid = string.Empty;
                                        userService.Save(item, item.CreatedBy, false);
                                    }


                                    var userDto = new UserSessionDto
                                    {
                                        UserId = user.UserId,
                                        UserName = user.UserName,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        Email = user.Email,
                                        RoleID = user.UserRole.RoleId,
                                        OrganisationID = user.CentralOfficeId ?? 0,
                                        BranchID = user.BranchId,
                                        CharityID = user.CharityId,
                                        MSApiUserId = user.MsuserObjectId,
                                        FoodbankId = foodbank != null ? foodbank.Id : 0,
                                        IsTeamManager = user.IsTeamManager ?? false
                                    };
                                    await CreateAuthenticationTicket(userDto);
                                }
                                else
                                {
                                    users.First().LastLoginDate = DateTime.Now;
                                    users.First().LoginAttempt = 0;
                                    users.First().Cofeguid = string.Empty;
                                    if (users.First().UserRole.RoleId != (int)UserRoles.SuperAdmin && users.First().UserRole.RoleId != (int)UserRoles.Internal && users.First().UserRole.RoleId != (int)UserRoles.Branch && users.First().UserRole.RoleId != (int)UserRoles.Donor && users.First().UserRole.RoleId != (int)UserRoles.TechnicalSupport)
                                    {
                                        var dataAccessibility = users.First().MmouserDataAccessibility;
                                        var branchIDs = new Dictionary<int, List<int>>();
                                        if (dataAccessibility.Count > 0)
                                        {
                                            foreach (var data in dataAccessibility.Where(p => p.CentralOfficeId != null).GroupBy(x => x.CentralOfficeId).ToList())
                                            {
                                                foreach (var item in data.Where(p => p.CharityId != null).GroupBy(x => x.CharityId).ToList())
                                                {
                                                    branchIDs.Add(item.Key.Value, item.Where(p => p.BranchId != null).Select(x => x.BranchId.Value).ToList());
                                                }
                                                userDataAccesslist.Add(new UserDataAccessDto
                                                {
                                                    CentralOfficeID = data.Key.Value,
                                                    CharityBranches = branchIDs
                                                });
                                            }
                                        }
                                    }
                                    userService.Save(users.First(), users.First().CreatedBy, false);
                                    var userDto = new UserSessionDto
                                    {
                                        UserId = user.UserId,
                                        UserName = user.UserName,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        Email = user.Email,
                                        RoleID = user.UserRole.RoleId,
                                        OrganisationID = user.CentralOfficeId ?? 0,
                                        BranchID = user.BranchId,
                                        CharityID = user.CharityId,
                                        MSApiUserId = user.MsuserObjectId ?? string.Empty,
                                        FoodbankId = foodbank != null ? foodbank.Id : 0,
                                        IsTeamManager = user.IsTeamManager ?? false
                                    };
                                    await CreateAuthenticationTicket(userDto);
                                    HttpContext.Session.SetObjectAsJson("SessionUserActionAccess", menuService.GetActionMethodAccessbility(users.First().UserRole.RoleId));
                                }

                                switch (user.UserRole.RoleId)
                                {
                                    case (int)UserRoles.Donor:
                                        return RedirectToAction("index", "donor");

                                    case (int)UserRoles.Volunteer:
                                        return RedirectToAction("index", "volunteer");

                                    case (int)UserRoles.Referrer:
                                        return RedirectToAction("index", "referrer");

                                    //case (int)UserRoles.Foodbank:
                                    //    return RedirectToAction("Index", "Dashboard", new { area = "Foodbank" });

                                    default:
                                        return RedirectToAction("Index", "Dashboard", new { area = "Foodbank" });
                                }
                            }
                            else
                            {
                                if (user.LoginAttempt.HasValue && user.LoginAttempt.Value >= 4)
                                    model.IsRecaptcha = true;
                                user.LoginAttempt = user.LoginAttempt.HasValue ? user.LoginAttempt.Value + 1 : 1;
                                user.LoginAttemptDate = DateTime.Now;
                                userService.Save(user, user.CreatedBy, false);
                            }
                        }
                    }
                    ShowErrorMessage("Error!", "An Invalid user ID and / or Password has been entered, please re-enter.");
                    return View(model);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                //ShowErrorMessage("Error!", "Failed to login due to some internal error");
                ShowErrorMessage("Error!", ex.Message);
                return View(model);
            }
        }

        private bool IsvalidLicence(User item)
        {
            bool status = true;
            if (item.PersonId != null)
            {
                var person = personService.GetPersonById(item.PersonId.Value);
                if (person != null)
                {
                    if (person.CentralOffice.LicenceEndDate < DateTime.Now)
                    {
                        status = false;
                    }
                }
            }
            else if (item.BranchId != null)
            {
                var branch = branchService.GetBranch(item.BranchId.Value);
                if (branch != null)
                {
                    if (branch.Charity.CentralOffice.MmolicenceEndDate < DateTime.Now)
                    {
                        status = false;
                    }
                }
            }
            else if (item.CharityId != null)
            {
                var charity = charityService.GetCharity(item.CharityId.Value);
                if (charity != null)
                {
                    if (charity.CentralOffice.MmolicenceEndDate < DateTime.Now)
                    {
                        status = false;
                    }
                }
            }
            else if (item.CentralOfficeId != null)
            {
                var centraloffice = centralofficeService.GetCentralOffice(item.CentralOfficeId.Value);
                if (centraloffice != null)
                {
                    if (centraloffice.MmolicenceEndDate < DateTime.Now)
                    {
                        status = false;
                    }
                }
            }
            return status;
        }

        private bool IsActiveUser(User item)
        {
            bool isActive = true;
            if (item.UserRole.RoleId != (int)UserRoles.SuperAdmin)
            {
                if (item.CentralOfficeNavigation != null)
                {
                    if (item.CentralOfficeNavigation.Active.HasValue && item.CentralOfficeNavigation.Active != true)
                        isActive = false;
                }
                if (item.Charity != null)
                {
                    if (!item.Charity.IsActive)
                        isActive = false;
                }
                if (item.BranchNavigation != null)
                {
                    if (!item.BranchNavigation.IsActive)
                        isActive = false;
                }
            }
            return isActive;
        }

        private bool IsMMOUser(User item)
        {
            bool isActive = true;
            if (item.UserRole.RoleId != (int)UserRoles.SuperAdmin)
            {
                if (item.CentralOfficeNavigation != null)
                {
                    if (!item.CentralOfficeNavigation.IsMmo)
                        isActive = false;
                }
                if (item.Charity != null)
                {
                    if (!item.Charity.IsMmo)
                        isActive = false;
                }
                if (item.BranchNavigation != null)
                {
                    if (!item.BranchNavigation.IsMmo)
                        isActive = false;
                }
            }
            return isActive;
        }

        #region Forgot Password Section
        /// <summary>
        /// Open popup to request about to recover credentials
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ForgotCredentials(int id)
        {
            ForgotCredentialsViewDto model = new ForgotCredentialsViewDto();
            if (id == (int)CredentialsRequest.UserPassword)
                model.CredentialsRequest = CredentialsRequest.UserPassword.ToString();
            else
                model.CredentialsRequest = CredentialsRequest.UserId.ToString();
            return PartialView("_forgotcredentials", model);
        }

        /// <summary>
        /// Open popup to request about to recover credentials
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotCredentials(ForgotCredentialsViewDto model)
        {
            var credentialsRequest = model.CredentialsRequest.ParseEnum<CredentialsRequest>();

            if (credentialsRequest == CredentialsRequest.UserPassword)
                ModelState.Remove("Email");
            else
            {
                ModelState.Remove("UserName");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    UserLiteDto user = new UserLiteDto();
                    if (credentialsRequest == CredentialsRequest.UserPassword)
                    {
                        user = userService.GetUserByUserName(model.UserName);
                    }
                    else
                    {
                        user = userService.GetUserByUserNameOrEmail(null, model.Email);
                    }

                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        string forgotPasswordLink = string.Empty;
                        string userName = string.Empty;
                        string subject = string.Empty;

                        if (credentialsRequest == CredentialsRequest.UserPassword)
                        {
                            ForgotPassword entity = new ForgotPassword
                            {
                                UserId = user.UserID,
                                Guid = Guid.NewGuid().ToString(),
                                CreatedDate = DateTime.Now,
                                IsExpire = false
                            };

                            forgotPasswordService.Save(entity, true);
                            forgotPasswordLink = SiteKeys.DomainName + "Account/SetupNewPassword/" + entity.Guid;
                        }
                        else
                            userName = user.UserName;

                        subject = "Recover " + (credentialsRequest == CredentialsRequest.UserId ? "User Id" : "Password") + " for MyFoodBank.Online";

                        FlexiMail flexiMail = new FlexiMail();
                        flexiMail.Subject = subject;
                        flexiMail.From = SiteKeys.EmailFrom;
                        flexiMail.FromName = "MyFoodBank.Online";
                        flexiMail.To = user.Email;
                        flexiMail.CC = "";
                        flexiMail.BCC = SiteKeys.BCC;
                        flexiMail.MailBodyManualSupply = false;
                        flexiMail.EmailTemplateFileName = (credentialsRequest == CredentialsRequest.UserPassword) ? "ForgotPassword.html" : "ForgotUserID.html";
                        flexiMail.ValueArray = new string[] { user.FirstName, (credentialsRequest == CredentialsRequest.UserPassword) ? forgotPasswordLink : userName };
                        flexiMail.Send();

                        if (credentialsRequest == CredentialsRequest.UserPassword)
                        {
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "An email has been sent to the email associated with the account of the given username.  Please follow the instructions in the email to reset your password.", IsSuccess = true });
                        }
                        else
                        {
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "If an account is found you will be sent the associated Username to the entered email address.", IsSuccess = true });
                        }
                    }
                    else
                    {
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Your account not found, Please contact to administrator!!", IsSuccess = false });
                    }
                }
                catch (Exception ex)
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
                }
            }

            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please fill all required fields!", IsSuccess = false });
        }
        #endregion

        #region Setup New Password
        /// <summary>
        /// Get user forgot password details and open form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetupNewPassword(string id)
        {
            try
            {
                ForgotPassword forgotPassword = forgotPasswordService.GetForgotPassword(id);
                if (forgotPassword != null && DateTime.Now.Subtract(forgotPassword.CreatedDate.Value).TotalMinutes <= 120)
                {
                    ForgotPasswordDto model = new ForgotPasswordDto();
                    model.UserID = forgotPassword.UserId;
                    model.Guid = forgotPassword.Guid;
                    model.PasswordQuestion = forgotPassword.User.PasswordQuestion;
                    return View(model);
                }
            }
            catch { }
            return RedirectToAction("accessDenied", "error");
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetupNewPassword(ForgotPasswordDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User entity = userService.GetUser(model.UserID);
                    if (entity != null)
                    {
                        List<User> users = userService.GetMultiUser(entity.UserName, entity.Password);
                        if (users.Count > 0 && users != null)
                        {
                            foreach (User user in users)
                            {
                                string saltValue = Common.GetRandomPasswordSalt();
                                user.Password = EncryptionUtils.HashPassword(model.NewPassword, saltValue, DateTime.Now);
                                user.PasswordSalt = saltValue;
                                user.LastPasswordChange = DateTime.Now;
                                user.LastLoginDate = DateTime.Now;
                                user.ModifiedDate = DateTime.Now;
                                user.PasswordAnswer = model.PasswordAnswer;
                                userService.Save(user, user.CreatedBy, false);
                            }
                            ForgotPassword forgotPassword = forgotPasswordService.GetForgotPassword(model.Guid);
                            forgotPassword.IsExpire = true;
                            forgotPassword.UpdatedDate = DateTime.Now;
                            forgotPasswordService.Save(forgotPassword, false);
                            ShowSuccessMessage("Success!", "Your password has been changed successfully", false);
                            return RedirectToAction("login", "account");
                        }
                        else
                        {
                            ShowErrorMessage("Error!", "User not found", true);
                        }

                    }
                    else
                        ShowErrorMessage("Error!", "User not found", true);
                }
                else
                {
                    var errors = string.Join("<br/>", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                    ShowErrorMessage("Error!", errors, true);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!", ex.Message, true);
            }
            return View();
        }
        #endregion

        #region Donor Registration Section

        public IActionResult DonorRegistration(string id)
        {
            string foodbanktoken = id;
            if (CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            var personDto = new PersonDto();

            ViewBag.Charities = new List<SelectListItem>();
            ViewBag.Branches = new List<SelectListItem>();


            BindOrganisationViewBag();
            ViewBag.Branches = new List<SelectListItem>();

            if (TempData["LockFilter"] != null)
            {
                var lockFilter = (LockFilter)TempData["LockFilter"];
                if (lockFilter.Charity > 0)
                {
                    personDto.IsCharityLocked = true;
                    personDto.CharityID = lockFilter.Charity;
                    personDto.CentralOfficeID = lockFilter.Organisation;
                    BindBranchViewBag(lockFilter.Charity, branchIDs: lockFilter.Branches);
                }
            }

            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            personDto.Addresses.Add(new PersonAddressDto());

            //Fetch Foodbank Detail
            var charity = charityService.GetCharityByToken(foodbanktoken);
            if (charity != null)
            {
                personDto.CharityID = charity.CharityId;
                personDto.CentralOfficeID = charity.CentralOffice.CentralOfficeId;
                BindBranchViewBag(charity.CharityId);

                //var branch = branchService.GetBranch(11669);// foodbank.User.BranchId.Value);
                personDto.BranchName = charity.CharityName;
            }
            else
            {
                personDto.BranchName = string.Empty;
            }
            personDto.FoodbankToken = foodbanktoken;
            //personDto.FoodbankId = foodbank.Id;
            //End

            return View(personDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorRegistration(PersonDto model)
        {
            ModelState.Remove("DefaultPurposeID");
            ModelState.Remove("PersonTypeID");
            //     ModelState.Remove("BranchID");
            ModelState.Remove("CharityID");
            ModelState.Remove("BranchIDs");
            var charity = charityService.GetCharityByToken(model.FoodbankToken);
            BindOrganisationViewBagSeleted(model.CentralOfficeID ?? 0, model.CharityID ?? 0, model.BranchID ?? 0);
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindBranchViewBag(charity.CharityId);
            if (ModelState.IsValid)
            {
                try
                {



                    model.CentralOfficeID = charity.CentralOfficeId; //foodbankService.GetFoodbankByToken(model.FoodbankToken).User.CentralOfficeId.Value;
                    model.CharityID = charity.CharityId;// foodbankService.GetFoodbankByToken(model.FoodbankToken).User.CharityId.Value;

                    model.Reference = GetLastReference(model.BranchID.Value);
                    var Foodbankdetails = foodbankService.GetFoodbankByCenterOfficerId(model.CentralOfficeID ?? 0);
                    ViewBag.Charities = new List<SelectListItem>();
                    ViewBag.Branches = new List<SelectListItem>();

                    if (model.Overseas)
                        ModelState.Remove("Addresses[0].PostCode");


                    model.BranchIDs = CurrentUser.BranchID.ToString();

                    if (!string.IsNullOrEmpty(model.UserName))
                    {
                        if (userService.IsUserNameExist(model.UserName) || quickDonorGiftService.IsUserNameExist(model.UserName))
                        {
                            ModelState.AddModelError("Error!", "Username already exist.");
                            ShowErrorMessage("Error!", "Username already exist.", false);
                            return View(model);
                        }
                    }

                    if (model.BranchID > 0)
                    {
                        if (quickDonorGiftService.IsBranchReferenceExist(model.Reference, Convert.ToInt32(model.BranchID)))
                        {
                            ModelState.AddModelError("Error!", "Donor with this reference number already exists within selected branch");
                            ShowErrorMessage("Error!", "Donor with this reference number already exists within selected branch", false);
                            return View(model); ;
                        }

                        Branch branch = branchService.GetBranch(Convert.ToInt32(model.BranchID));
                    }

                    model.Gender = Gender.Male;
                    model.Title = !string.IsNullOrWhiteSpace(model.Title) ? model.Title.Trim() : model.Title;
                    model.Forenames = !string.IsNullOrWhiteSpace(model.Forenames) ? model.Forenames.Trim() : model.Forenames;
                    model.Surname = !string.IsNullOrWhiteSpace(model.Surname) ? model.Surname.Trim() : model.Surname;
                    model.Initials = !string.IsNullOrWhiteSpace(model.Initials) ? model.Initials.Trim().Replace(" ", "") : model.Initials;
                    model.Suffix = !string.IsNullOrWhiteSpace(model.Suffix) ? model.Suffix.Trim().Replace(" ", "") : model.Suffix;
                    model.HMRCAddress = string.Format("{0}{1}{2}{3}", !string.IsNullOrWhiteSpace(model.Addresses[0].HouseName) ? model.Addresses[0].HouseName.Trim() + " ," : model.Addresses[0].HouseName,
                                                  !string.IsNullOrWhiteSpace(model.Addresses[0].HouseNumber) ? model.Addresses[0].HouseNumber + ", " : string.Empty,
                                                  !string.IsNullOrWhiteSpace(model.Addresses[0].StreetName) ? model.Addresses[0].StreetName.Trim() + ", " : model.Addresses[0].StreetName.Trim(),
                                                  !string.IsNullOrWhiteSpace(model.Addresses[0].PostCode) ? model.Addresses[0].PostCode.Trim().ToUpper() : model.Addresses[0].PostCode).Trim();

                    model.Addresses[0].StreetName = !string.IsNullOrWhiteSpace(model.Addresses[0].StreetName) ? model.Addresses[0].StreetName.Trim() : model.Addresses[0].StreetName;
                    model.Addresses[0].OtherAddressLine = !string.IsNullOrWhiteSpace(model.Addresses[0].OtherAddressLine) ? model.Addresses[0].OtherAddressLine.Trim() : model.Addresses[0].OtherAddressLine;
                    model.Addresses[0].District = !string.IsNullOrWhiteSpace(model.Addresses[0].District) ? model.Addresses[0].District.Trim() : model.Addresses[0].District;
                    model.Addresses[0].PostCode = !string.IsNullOrWhiteSpace(model.Addresses[0].PostCode) ? model.Addresses[0].PostCode.Trim().ToUpper() : model.Addresses[0].PostCode;
                    model.Addresses[0].MMOAddressType = (byte)AddressTypes.Primary;
                    model.Addresses[0].MMODescripton = "Home";
                    model.DateAdded = DateTime.Now;
                    model.Active = true;
                    model.DateModified = DateTime.Now;
                    model.AuditIP = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    model.AuditUserId = CurrentUser.UserID;
                    string randonSalt = Common.GetRandomPasswordSalt();

                    /*Locking Process*/
                    var lockModel = new LockFilter();
                    if (model.IsCharityLocked && model.CharityID.HasValue && model.CentralOfficeID.HasValue)
                    {
                        lockModel.Charity = model.CharityID.Value;
                        lockModel.Organisation = model.CentralOfficeID.Value;
                        lockModel.Branches = model.BranchIDs.Split(',').Select(e => int.Parse(e)).ToArray();
                        TempData["LockFilter"] = lockModel;
                    }
                    else
                    {
                        TempData.Remove("LockFilter");
                    }
                    /*end*/

                    var donorLinkCode = Extensions.GetDonorLinkCode();

                    var objbranch = branchService.GetBranch(model.BranchID.Value);
                    if (objbranch != null)
                    {
                        if (objbranch.IsMgo.Value && objbranch.IsMmo)
                        {
                            model.IsMGO = model.IsMGO;
                            model.IsMMO = model.IsMMO;
                        }
                        else
                        {
                            model.IsMGO = objbranch.IsMgo.Value;
                            model.IsMMO = objbranch.IsMmo;
                        }

                        Person person = new Person();
                        person.DateAdded = model.DateAdded;
                        person.DateModified = model.DateModified;
                        person.Forenames = model.Forenames;
                        person.Hmrcaddress = model.HMRCAddress;
                        person.Deceased = model.Deceased;
                        person.Deceased = model.Deceased;
                        person.Active = model.Active;
                        person.IsDefaultClaimTax = model.IsDefaultClaimTax;
                        person.IsHomePhoneExt = model.IsHomePhoneExt;
                        person.IsMobilePhoneExt = model.IsMobilePhoneExt;
                        person.IsTagged = model.IsTagged;
                        person.Overseas = model.Overseas;
                        person.Reference = model.Reference;
                        person.Surname = model.Surname;
                        person.Title = model.Title;
                        person.Email = model.Email;
                        person.Initials = model.Initials;
                        person.Suffix = model.Suffix;

                        person.CentralOfficeId = model.CentralOfficeID; // we only need branchID, central office id and charity should not be included directly
                        person.CharityId = model.CharityID != null ? model.CharityID : objbranch.CharityId;
                        person.BranchId = objbranch.BranchId;
                        person.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                        person.AuditUserId = CurrentUser.UserID;
                        if (model.BranchIDs.Trim().Trim(',').Contains(','))
                        {
                            person.LinkedCode = donorLinkCode;
                        }

                        person.MmopersonAdditonalDetails = new MmopersonAdditonalDetails
                        {
                            IsMgo = model.IsMGO,
                            IsMmo = model.IsMMO,
                            AddedDate = DateTime.Now,
                            LastModifiedDate = DateTime.Now,
                        };

                        personService.Save(person);

                        // save Link table between Foodbank and Donor 
                        DonorFoodbank donorfoodbank = new DonorFoodbank();
                        donorfoodbank.DonorId = person.PersonId;
                        donorfoodbank.FoodBankId = Foodbankdetails.Id;
                        foodbankService.SaveDonorFoodbank(donorfoodbank);

                        //Save Address
                        Address address = new Address();
                        address.HouseName = string.IsNullOrWhiteSpace(model.Addresses[0].HouseName) ? string.Empty : model.Addresses[0].HouseName;
                        address.HouseNumber = model.Addresses[0].HouseNumber;
                        address.StreetName = model.Addresses[0].StreetName;
                        address.OtherAddressLine = model.Addresses[0].OtherAddressLine;
                        address.District = string.IsNullOrWhiteSpace(model.Addresses[0].District) ? string.Empty : model.Addresses[0].District;
                        address.City = string.IsNullOrWhiteSpace(model.Addresses[0].City) ? string.Empty : model.Addresses[0].City;
                        address.Postcode = model.Addresses[0].PostCode;
                        address.CountryId = model.Addresses[0].CountryID;
                        address.PersonId = person.PersonId;
                        address.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                        address.AuditUserId = null;
                        addressService.Save(address, address.AddressId == 0);
                        //End

                        TempData["DonorID"] = person.PersonId;

                        if (!string.IsNullOrEmpty(model.UserName) &&
                            !string.IsNullOrEmpty(model.PasswordAnswer) &&
                            !string.IsNullOrEmpty(model.PasswordQuestion) &&
                            !string.IsNullOrEmpty(model.Password))
                        {
                            var user = new User();
                            user.FirstName = model.Forenames;
                            user.LastName = model.Surname;
                            user.UserName = model.UserName;
                            user.Email = model.Email;
                            user.PasswordSalt = randonSalt;
                            user.Password = EncryptionUtils.HashPassword(model.Password, user.PasswordSalt, DateTime.Now);
                            user.PasswordAnswer = EncryptionUtils.Encrypt(model.PasswordAnswer, user.PasswordSalt);
                            user.PasswordQuestion = model.PasswordQuestion;
                            user.Active = true;
                            user.AddedDate = DateTime.Now;
                            user.ModifiedDate = DateTime.Now;
                            user.LastLoginDate = DateTime.Now;
                            user.LastPasswordChange = DateTime.Now;
                            user.CreatedBy = CurrentUser.UserID;
                            user.IsBlockedBySuperAdmin = false;
                            user.Ip = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                            user.PersonId = person.PersonId;
                            user.UserRole = new UserRole();
                            user.UserRole.Role = roleService.GetRoleByName("Donor");
                            user.AuditUserId = CurrentUser.UserID;
                            user.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                            user.CentralOfficeId = person.CentralOfficeId;
                            user.CharityId = person.CharityId;
                            user.BranchId = person.BranchId;
                            user.IsFoodbank = true;
                            userService.Save(user, user.CreatedBy, true);
                        }
                    }

                    ShowSuccessMessage("Success!", "Donor saved successfully. Please login with your credentials.", false);
                    return RedirectToAction("login", "account");
                }
                catch (Exception ex)
                {
                    string message = ex.GetBaseException().Message;
                    if (message.Contains("UNIQUE KEY"))
                    {
                        if (message.Contains("uc_branch_reference"))
                            message = "Donor with this reference number already exists within selected branch";
                    }
                    //ModelState.AddModelError("Error!", message);

                    ShowErrorMessage("Error!", message, false);
                    return View(model);
                    //return RedirectToAction("donorregistration", "account");
                }
            }
            return View(model);//CreateModelStateErrors();
        }
        public string UserAvailability(string userName)
        {
            if (userService.IsUserNameExist(userName) || quickDonorGiftService.IsUserNameExist(userName))
            {
                return "Username already exist";
            }
            return "";
        }
        [NonAction]
        public void BindOrganisationViewBag()
        {
            ViewBag.Organisations = centralofficeService.GetCentralOffices().Select(c => new SelectListItem
            {
                Text = c.OrganisationName,
                Value = c.CentralOfficeId.ToString()
            }).ToList();

            ViewBag.Charities = new List<SelectListItem>();
            ViewBag.Branches = new List<SelectListItem>();
            ViewBag.PersonTypes = new List<SelectListItem>();
            ViewBag.Methods = new List<SelectListItem>();
            ViewBag.Purposes = new List<SelectListItem>();
        }
        [NonAction]
        public void BindOrganisationViewBagSeleted(int CentralOfficeId, int charityID, int BranchID)
        {
            ViewBag.Organisations = centralofficeService.GetCentralOffices(CentralOfficeId).Select(c => new SelectListItem
            {
                Text = c.OrganisationName,
                Value = c.CentralOfficeId.ToString()
            }).ToList();

            ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CentralOfficeId, CurrentUser.UserID, true, true, charityID).Select(c => new SelectListItem
            {
                Text = c.CharityName.AddCharityPrefix(c.Prefix),
                Value = c.CharityId.ToString()
            }).ToList();


            ViewBag.Branches = branchService.GetBranchesByDataAccessibilityBranchId(charityID, BranchID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();


            ViewBag.PersonTypes = new List<SelectListItem>();
            ViewBag.Methods = new List<SelectListItem>();
            ViewBag.Purposes = new List<SelectListItem>();
        }
        [NonAction]
        public void BindCountriesViewBag(int? id = null)
        {
            ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
            {
                Text = c.CountryName.ToTitle(),
                Value = c.CountryId.ToString(),
                Selected = (id == null) ? (c.CountryName == Constants.DefaultCountry) : id == c.CountryId
            }).ToList();
        }


        /// <summary>
        /// To bind branches in viewbag by data accessibility as per the current user's permission
        /// </summary>
        /// <param name="charityID"></param>
        public void BindBranchViewBag(int charityID, QuickDonorGiftDto quickDonorDto = null, int[] branchIDs = null, int[] excludeBranches = null)
        {
            List<SelectListItem> lstBranches = branchService.GetBranchesByDataAccessibility(charityID)
                .Where(e => excludeBranches.IsNotNullAndNotEmpty() ? !excludeBranches.Contains(e.BranchId) : true)
                .Select(c => new SelectListItem
                {
                    Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                    Value = c.BranchId.ToString(),
                    Selected = branchIDs != null ? branchIDs.Contains(c.BranchId) : false
                }).ToList();

            if (lstBranches.Count > 0)
            {
                lstBranches.Insert(0, new SelectListItem("Select Branch", ""));
                ViewBag.Branches = lstBranches;
            }
            else
            {
                ViewBag.Branches = new List<SelectListItem>();
            }
        }

        [HttpGet]
        public IActionResult BindCharities(int organisationID)
        {
            var result = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, organisationID, CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.CharityName.AddCharityPrefix(c.Prefix),
                Value = c.CharityId.ToString()
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
            var result = branchService.GetBranchesByDataAccessibility(charityID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();

            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }

        /// <summary>
        /// To get the reference number of last added donor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetLastReference(int BranchId)
        {
            try
            {
                if (BranchId > 0)
                {
                    BranchReferenceDto BranchReference = new BranchReferenceDto();
                    if (BranchId > 1)
                    {
                        BranchReference.References = new List<DonorReferences>();
                        BranchReference.References.Add(branchService.GetDonorReferenceByBranch(BranchId));
                        BranchReference.IsNextReference = false;
                        return BranchReference.References.FirstOrDefault().DonorReference;
                    }
                    else
                    {
                        var branchData = branchService.GetBranch(BranchId);
                        if (branchData != null && branchData.ReferenceType == 2)
                        {
                            BranchReference.References = new List<DonorReferences>();

                            BranchReference.References.Add(branchService.GetDonorReferenceByBranch(BranchId));

                            BranchReference.IsNextReference = false;
                            return BranchReference.References.FirstOrDefault().DonorReference;
                        }
                        else
                        {
                            BranchReference.IsNextReference = true;
                            BranchReference.DonorReference = branchService.GetDonorReference(BranchId);
                            return BranchReference.References.FirstOrDefault().DonorReference;
                        }
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        [HttpGet]
        public ActionResult GetDonorsWithName(string text, int? orgID = null, int? charityID = null, int? branchID = null, string branchesIDs = null)
        {
            List<PersonLiteDto> donors = new List<PersonLiteDto>();
            var persons = personService.GetPersons(
                userDataAccess: CurrentUser.DataAccessibilities,
                roleID: CurrentUser.RoleID,
                surname: text,
                organisationId: GetOrgLevelID(orgID, UserRoles.Organisation),
                charityId: GetOrgLevelID(charityID, UserRoles.Charity),
                branchId: GetOrgLevelID(branchID, UserRoles.Branch)
                );

            donors = persons.Select(x => new PersonLiteDto()
            {
                CharityName = x.Charity.CharityName,
                BranchName = x.Branch.BranchDescription,
                PersonName = $"{x.Forenames} {x.Surname}",
                Address = x.Hmrcaddress,
                Reference = x.Reference,
                Active = x.Active,
                PersonID = x.PersonId
            }).ToList();

            if (donors.IsNotNullAndNotEmpty() && !string.IsNullOrWhiteSpace(branchesIDs))
            {
                var branches = branchesIDs.Split(',').Select(e => int.Parse(e));
                return PartialView("_DonorsWithName", donors.Where(e => branches.Contains(e.BranchID ?? 0)).ToList());
            }
            else
            {
                return PartialView("_DonorsWithName", donors);
            }
        }

        [HttpGet]
        public IActionResult GetReferenceType(int BranchId)
        {
            try
            {
                int Referencetype;
                var branch = branchService.GetBranch(BranchId);
                if (branch != null)
                    Referencetype = (int)branch.ReferenceType;
                else
                    Referencetype = (int)ReferenceType.Character;
                return NewtonSoftJsonResult(new RequestOutcome<int> { Data = Referencetype, IsSuccess = true });
            }
            catch
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "There is some internal problem.", IsSuccess = false });
            }
        }
        #endregion

        /// <summary>
        /// To logout the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Logout()
        {
            var abc = RemoveAuthentication();
            return Redirect("Login");
        }

        #region Referrer Registration
        [HttpGet]
        public IActionResult ReferrerRegisteration(string id)
        {
            if (CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            string foodbanktoken = id;
            ReferrerRegisterDto model = new ReferrerRegisterDto();


            List<SelectListItem> professionsList = new List<SelectListItem>();

            professionsList = ReferralService.GetReferrerType().Select(c => new SelectListItem
            {
                Text = c.Name.ToTitle(),
                Value = c.Id.ToString()
            }).ToList();
            professionsList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.ProfessionList = professionsList;
            ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
            {
                Text = c.CountryName.ToTitle(),
                Value = c.CountryId.ToString()
            }).ToList();

            //Fetch Foodbank Detail
            var charity = charityService.GetCharityByToken(foodbanktoken);
            if (charity != null)
            {
                //var branch = branchService.GetBranch(11669);// foodbank.User.BranchId.Value);
                model.BranchName = charity.CharityName;
            }
            else
            {
                model.BranchName = string.Empty;
            }
            model.FoodbankToken = foodbanktoken;
            //End

            return View(model);
        }
        [HttpPost]
        public IActionResult ReferrerRegisteration(ReferrerRegisterDto model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    var charity = charityService.GetCharityByToken(model.FoodbankToken);
                    var Foodbankdetails = foodbankService.GetFoodbankByCenterOfficerId(charity.CentralOfficeId ?? 0);

                    List<SelectListItem> professionsList = new List<SelectListItem>();

                    professionsList = ReferralService.GetReferrerType().Select(c => new SelectListItem
                    {
                        Text = c.Name.ToTitle(),
                        Value = c.Id.ToString()
                    }).ToList();
                    professionsList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                    ViewBag.ProfessionList = professionsList;

                    ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
                    {
                        Text = c.CountryName.ToTitle(),
                        Value = c.CountryId.ToString()
                    }).ToList();

                    if (!string.IsNullOrEmpty(model.UserName))
                    {
                        if (userService.IsUserNameExist(model.UserName) || quickDonorGiftService.IsUserNameExist(model.UserName))
                        {

                            ShowErrorMessage("Error!", "Username already exist.", false);
                            return View(model);
                        }
                    }
                    string randonSalt = Common.GetRandomPasswordSalt();

                    ///// Address Save
                    Fbaddress fbaddress = new Fbaddress();
                    fbaddress.HouseName = model.HouseName;
                    fbaddress.Street = model.StreetName;
                    fbaddress.HouseNumber = model.HouseNumber;
                    fbaddress.City = model.City;
                    fbaddress.Postcode = model.PostCode;
                    fbaddress.CountryId = model.CountryID;
                    fbaddress.CountryName = "";
                    fbaddress.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                    addressService.Save(fbaddress, fbaddress.Id == 0);
                    /////
                    ///// Contact Save
                    Fbcontact fbcontact = new Fbcontact();
                    fbcontact.OrganisationName = model.OrganisationName;
                    fbcontact.ForeName = model.FirstName;
                    fbcontact.Surname = model.LastName;
                    fbcontact.Mobile = model.ContactNumber;
                    fbcontact.Email = model.Email;
                    fbcontact.AddedDate = DateTime.Now;
                    fbcontact.ModifiedDate = DateTime.Now;
                    contactService.Save(fbcontact, fbcontact.Id == 0);
                    /////

                    ///// User Save
                    User user = new User();
                    user.UserName = model.UserName;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.Address = model.OtherAddressLine;
                    user.PasswordSalt = randonSalt;
                    user.Password = EncryptionUtils.HashPassword(model.EditPassword, randonSalt, DateTime.Now);
                    user.PasswordAnswer = EncryptionUtils.Encrypt(model.PasswordAnswer, randonSalt);
                    user.PasswordQuestion = model.PasswordQuestion;
                    user.Active = true;
                    user.AddedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.LastLoginDate = DateTime.Now;
                    user.LastPasswordChange = DateTime.Now;
                    user.IsBlockedBySuperAdmin = false;
                    user.CentralOfficeId = charity.CentralOfficeId;
                    user.CharityId = charity.CharityId;
                    user.Ip = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    user.UserRole = new UserRole();
                    user.UserRole.Role = roleService.GetRoleByName("Referrer");
                    user.AuditUserId = CurrentUser.UserID;
                    user.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    user.IsFoodbank = true;
                    userService.Save(user, CurrentUser.UserID, user.UserId == 0);
                    /////

                    ///// Referres save
                    Referrers referrers = new Referrers();
                    referrers.Name = model.FirstName + " " + model.LastName;
                    referrers.ReffToken = GeneraterToken();
                    referrers.ServiceDescription = model.Profession;
                    referrers.RefTypeId = model.ProfessionId;
                    //referrers.DefaultParcelType = 1;
                    referrers.IsVoucher = true;
                    referrers.AddressId = fbaddress.Id;
                    referrers.ContactId = fbcontact.Id;
                    referrers.UserId = user.UserId;
                    referrers.IsStatus = (int)ReferrersStatus.Pending;
                    referrers.PostponeDate = DateTime.Now;
                    referrers.Active = true;
                    referrers.FoodbankId = Foodbankdetails.Id;
                    bool IsSave = ReferralService.Save(referrers);
                    /////

                    if (IsSave)
                    {
                        ShowSuccessMessage("Success!", "Referrer Save Successfully.", false);
                        return RedirectToAction("ReferrerRegisteration", "Account");
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Some Error.", false);
                        //ShowErrorMessage("Error!", "Some Error..", false);
                        return View(model);// RedirectToAction("ReferrerRegisteration", "Account");
                    }

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return View(model); //RedirectToAction("ReferrerRegisteration", "Account");
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return View(model); //RedirectToAction("ReferrerRegisteration", "Account");
            }
        }
        public string GeneraterToken(int? id = null)
        {
        Checked:
            var token = Extensions.RandomString(8);
            if (!string.IsNullOrWhiteSpace(token))
            {
                var isExist = ReferralService.CheckToken(token);
                if (isExist)
                {
                    goto Checked;
                }
            }
            return token;
        }
        #endregion

        #region Volunteer Registration
        [HttpGet]
        public IActionResult VolunteerRegistration(string id)
        {
            if (CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            string foodbanktoken = id;
            VolunteerRegisterDto model = new VolunteerRegisterDto();
            var enumList = Enum.GetValues(typeof(MaritalType));
            List<SelectListItem> maritalList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                maritalList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((MaritalType)(int)items).GetDescription(),
                    Selected = (int)MaritalType.Individual == (int)items ? true : false
                });
            }
            ViewBag.MaritalTypeList = maritalList;


            var workTypes = Enum.GetValues(typeof(WorkType));
            List<SelectListItem> workTypeList = new List<SelectListItem>();
            foreach (var workType in workTypes)
            {
                workTypeList.Add(new SelectListItem
                {
                    Value = ((int)workType).ToString(),
                    Text = ((WorkType)(int)workType).GetDescription(),
                    Selected = (int)WorkType.Packing == (int)workType ? true : false
                });
            }
            ViewBag.WorkTypeList = workTypeList;

            ViewBag.MonthWeek = Enum.GetValues(typeof(MonthWeek)).Cast<MonthWeek>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            ViewBag.WeekDay = Enum.GetValues(typeof(WeekDay)).Cast<WeekDay>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();


            ViewBag.Month = Enum.GetValues(typeof(Month)).Cast<Month>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            //Fetch Foodbank Detail
            var charity = charityService.GetCharityByToken(foodbanktoken);
            if (charity != null)
            {
                //var branch = branchService.GetBranch(11669);// foodbank.User.BranchId.Value);
                model.BranchName = charity.CharityName;
            }
            else
            {
                model.BranchName = string.Empty;
            }
            model.FoodbankToken = foodbanktoken;
            //End

            return View(model);
        }
        [HttpPost]
        public IActionResult VolunteerRegistration(VolunteerRegisterDto model)
        {

            var charity = charityService.GetCharityByToken(model.FoodbankToken);
            var Foodbankdetails = foodbankService.GetFoodbankByCenterOfficerId(charity.CentralOfficeId ?? 0);

            var enumList = Enum.GetValues(typeof(MaritalType));
            List<SelectListItem> maritalList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                maritalList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((MaritalType)(int)items).GetDescription(),
                    Selected = (int)MaritalType.Individual == (int)items ? true : false
                });
            }
            ViewBag.MaritalTypeList = maritalList;


            var workTypes = Enum.GetValues(typeof(WorkType));
            List<SelectListItem> workTypeList = new List<SelectListItem>();
            foreach (var workType in workTypes)
            {
                workTypeList.Add(new SelectListItem
                {
                    Value = ((int)workType).ToString(),
                    Text = ((WorkType)(int)workType).GetDescription(),
                    Selected = (int)WorkType.Packing == (int)workType ? true : false
                });
            }
            ViewBag.WorkTypeList = workTypeList;

            ViewBag.MonthWeek = Enum.GetValues(typeof(MonthWeek)).Cast<MonthWeek>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            ViewBag.WeekDay = Enum.GetValues(typeof(WeekDay)).Cast<WeekDay>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();


            ViewBag.Month = Enum.GetValues(typeof(Month)).Cast<Month>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            if (!model.IsChangePassword)
            {
                ModelState.Remove("PasswordQuestion");
                ModelState.Remove("PasswordAnswer");
                ModelState.Remove("EditPassword");
                ModelState.Remove("ConfirmPassword");
            }
            if (!model.IsRegularAvailability)
            {
                ModelState.Remove("Availability.FromDate");
                ModelState.Remove("Availability.ToDate");
            }
            if (!model.IsUnavailability)
            {
                ModelState.Remove("Unavailability.FromDate");
                ModelState.Remove("Unavailability.ToDate");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(model.UserName))
                    {
                        if (userService.IsUserNameExist(model.UserName) || quickDonorGiftService.IsUserNameExist(model.UserName))
                        {

                            ShowErrorMessage("Error!", "Username already exist.", false);
                            return View(model);
                        }
                    }

                    ///// Volunteer Save
                    Volunteer entityvolunteer = new Volunteer();
                    string randonSalt = Common.GetRandomPasswordSalt();

                    ///// Contact Save
                    Fbcontact fbcontact = new Fbcontact();
                    fbcontact.ForeName = model.VolunteerName;
                    fbcontact.Email = model.Email;
                    fbcontact.Mobile = model.ContactNumber;
                    fbcontact.AddedDate = DateTime.Now;
                    fbcontact.ModifiedDate = DateTime.Now;
                    entityvolunteer.Contact = fbcontact;

                    ///// User Save
                    if (model.IsChangePassword)
                    {
                        User user = new User();
                        user.UserName = model.UserName;
                        user.FirstName = model.VolunteerName;
                        user.Email = model.Email;
                        user.PasswordSalt = randonSalt;
                        user.Password = EncryptionUtils.HashPassword(model.EditPassword, randonSalt, DateTime.Now);
                        user.PasswordAnswer = EncryptionUtils.Encrypt(model.PasswordAnswer, randonSalt);
                        user.PasswordQuestion = model.PasswordQuestion;
                        user.Active = true;
                        user.AddedDate = DateTime.Now;
                        user.ModifiedDate = DateTime.Now;
                        user.LastLoginDate = DateTime.Now;
                        user.LastPasswordChange = DateTime.Now;
                        user.IsBlockedBySuperAdmin = false;
                        user.Ip = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                        user.UserRole = new UserRole();
                        user.UserRole.Role = roleService.GetRoleByName("Volunteer");
                        user.AuditUserId = CurrentUser.UserID;
                        user.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                        user.IsFoodbank = true;
                        user.CentralOfficeId = charity.CentralOfficeId;
                        user.CharityId = charity.CharityId;
                        entityvolunteer.User = user;
                    }

                    ///// Volunteer Save
                    entityvolunteer.AddedDate = DateTime.Now;
                    entityvolunteer.ModifiedDate = DateTime.Now;
                    entityvolunteer.CanDrive = false;
                    entityvolunteer.DeliveryDriver = false;
                    entityvolunteer.IndividualCouple = model.MaritalStatus;
                    entityvolunteer.Packingordelivery = model.WorkType;
                    entityvolunteer.Howelsecanyouhelp = model.HowCanYouHelp;
                    entityvolunteer.LocationId = 0;
                    entityvolunteer.PartnerId = 0;
                    entityvolunteer.FoodbankId = Foodbankdetails.Id;
                    volunteerService.Save(entityvolunteer, model.VolunteerId == 0);
                    model.VolunteerId = entityvolunteer.Id;

                    ShowSuccessMessage("Success!", "Volunteer has been registered successfully.", false);
                    return NewtonSoftJsonResult(new RequestOutcome<int> { Data = model.VolunteerId, IsSuccess = true });

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = Ex.Message, IsSuccess = false });
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all the required fields!", false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please fill all the required fields!", IsSuccess = false });
            }
        }

        [HttpGet]
        public IActionResult GetPartialView(int? volunteerId = null)
        {
            ViewBag.VolunteerId = volunteerId;
            return PartialView("_VolunteerAvailabilityUnAvailability");
        }

        #region Availability List Section
        public IActionResult GetAvailabilityList(DataTableServerSide model, string volunteerId)
        {
            var centralOffices = new KeyValuePair<int, List<AvailabilityDto>>();
            centralOffices = volunteerService.GetAvailabilityList(model, Convert.ToInt32(volunteerId));
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    c.AvailabilityId,
                    c.FromDate,
                    c.PatternName,
                    c.ToDate,
                    (c.AllDay ? "All Day" : (c.TimeForm + " to "+ c.TimeTo )),
                    //"<a data-toggle='modal' data-target='#modal-create-edit-unavailability'  href=" + Url.Action("CreateEdit", "Unavailability", new {c.AvailabilityId })
                    //+ " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"
                     "<a data-toggle='modal' data-target='#modal-delete-volunteer-availability' href='" + Url.Action("DeleteAvaliability", "account", new { id = c.AvailabilityId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>"
            })
            });
        }
        #endregion

        #region UnAvailability List Section
        public IActionResult GetUnvailabilityList(DataTableServerSide model, string volunteerId)
        {
            KeyValuePair<int, List<UnavailabilityDto>> centralOffices = new KeyValuePair<int, List<UnavailabilityDto>>();
            centralOffices = volunteerService.GetUnavailabilityList(model, Convert.ToInt32(volunteerId));
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    c.UnavailabilityId,
                    c.FromDate,
                    c.PatternName,
                    c.ToDate,
                    (c.AllDay ? "All Day" : (c.TimeForm + " to "+ c.TimeTo )),
                     "<a data-toggle='modal' data-target='#modal-delete-volunteer-unavailability' href='" + Url.Action("DeleteUnavaliability", "account", new {id = c.UnavailabilityId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>"
                })
            });
        }
        #endregion

        [HttpGet]
        public IActionResult AddAvailability(int volunteerId)
        {
            VolunteerDto model = new VolunteerDto();
            var volunteer = volunteerService.GetVolunteerById(volunteerId);

            if (volunteer != null)
            {
                model.VolunteerId = volunteerId;
                model.VolunteerName = $"{volunteer.Contact.ForeName} {volunteer.Contact.Surname}";
            }

            var enumList = Enum.GetValues(typeof(MaritalType));
            List<SelectListItem> maritalList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                maritalList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((MaritalType)(int)items).GetDescription(),
                    Selected = (int)MaritalType.Individual == (int)items ? true : false
                });
            }
            ViewBag.MaritalTypeList = maritalList;


            var workTypes = Enum.GetValues(typeof(WorkType));
            List<SelectListItem> workTypeList = new List<SelectListItem>();
            foreach (var workType in workTypes)
            {
                workTypeList.Add(new SelectListItem
                {
                    Value = ((int)workType).ToString(),
                    Text = ((WorkType)(int)workType).GetDescription(),
                    Selected = (int)WorkType.Packing == (int)workType ? true : false
                });
            }
            ViewBag.WorkTypeList = workTypeList;

            ViewBag.MonthWeek = Enum.GetValues(typeof(MonthWeek)).Cast<MonthWeek>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            ViewBag.WeekDay = Enum.GetValues(typeof(WeekDay)).Cast<WeekDay>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();


            ViewBag.Month = Enum.GetValues(typeof(Month)).Cast<Month>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            return PartialView("_AddAvailability", model);
        }

        [HttpPost]
        public IActionResult AddAvailability(VolunteerDto model)
        {
            if (!model.IsChangePassword)
            {
                ModelState.Remove("PasswordQuestion");
                ModelState.Remove("PasswordAnswer");
                ModelState.Remove("EditPassword");
                ModelState.Remove("ConfirmPassword");
            }
            if (!model.IsUnavailability)
            {
                ModelState.Remove("Unavailability.FromDate");
                ModelState.Remove("Unavailability.ToDate");
            }
            ModelState.Remove("MaritalStatus");
            ModelState.Remove("WorkType");

            if (ModelState.IsValid)
            {
                try
                {
                    #region Availbility Section

                    if (model.Availability.FromDate.ToDateTimeNullable() == null)
                    {
                        ShowErrorMessage("Error!", "From date required.", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "From date required.", IsSuccess = false });
                    }

                    if (model.Availability.ToDate.ToDateTimeNullable() == null)
                    {
                        ShowErrorMessage("Error!", "Finish date required.", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Finish date required.", IsSuccess = false });
                    }
                    else if (model.Availability.ToDate.ToDateTime() < model.Availability.ToDate.ToDateTime())
                    {
                        ShowErrorMessage("Error!", "The finish date must be after the start date.", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "The finish date must be after the start date.", IsSuccess = false });
                    }

                    if (model.Availability.UnavailabilityTimeType == 2)
                    {
                        if (string.IsNullOrEmpty(model.Availability.TimeForm))
                        {
                            ShowErrorMessage("Error!", "Unavilability start time required.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Unavilability start time required.", IsSuccess = false });
                        }

                        if (string.IsNullOrEmpty(model.Availability.TimeTo))
                        {
                            ShowErrorMessage("Error!", "Unavilability end time required.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Unavilability end time required.", IsSuccess = false });
                        }
                    }


                    string Availbilitypattern = string.Empty;
                    string AvailbilitypatternName = string.Empty;

                    // Frequenct type for one
                    if (model.Availability.FrequencyType == 1)
                    {
                        if (model.Availability.DailyType == 1)
                        {
                            if (model.Availability.DailyDays < 1 || model.Availability.DailyDays > 99)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for every day, 2 for every other day and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every day, 2 for every other day and so on.", IsSuccess = false });
                            }
                            else
                            {
                                Availbilitypattern = "D1" + model.Availability.DailyDays;
                                AvailbilitypatternName = string.Format("Every {0}day",
                                    (model.Availability.DailyDays == 1 ? "" : (model.Availability.DailyDays == 2 ? "other " : (model.Availability.DailyDays + model.Availability.DailyDays.ToOccurrenceSuffix())))
                                    );
                            }
                        }
                        else
                        {
                            Availbilitypattern = "D2";
                            AvailbilitypatternName = "Every weekday";
                        }
                    }
                    // frequency type for two
                    else if (model.Availability.FrequencyType == 2)
                    {
                        if (model.Availability.WeeklyDays < 1 || model.Availability.WeeklyDays > 99)
                        {
                            ShowErrorMessage("Error!", "Enter 1 for every week, 2 for every other week and so on.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every week, 2 for every other week and so on.", IsSuccess = false });
                        }
                        else if (!model.Availability.IsWeeklyMonday && !model.Availability.IsWeeklyTuesday && !model.Availability.IsWeeklyWednesday && !model.Availability.IsWeeklyThursday && !model.Availability.IsWeeklyFriday && !model.Availability.IsWeeklySaturday && !model.Availability.IsWeeklySunday)
                        {
                            ShowErrorMessage("Error!", "You must select at least one day of the week.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "You must select at least one day of the week.", IsSuccess = false });
                        }
                        else
                        {
                            Availbilitypattern = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", "W",
                                (model.Availability.IsWeeklyMonday ? "Y" : "N"),
                                (model.Availability.IsWeeklyTuesday ? "Y" : "N"),
                                (model.Availability.IsWeeklyWednesday ? "Y" : "N"),
                                (model.Availability.IsWeeklyThursday ? "Y" : "N"),
                                (model.Availability.IsWeeklyFriday ? "Y" : "N"),
                                (model.Availability.IsWeeklySaturday ? "Y" : "N"),
                                (model.Availability.IsWeeklySunday ? "Y" : "N"),
                                 model.Availability.WeeklyDays
                                );

                            //patternName = "Every";
                            AvailbilitypatternName = string.Empty;
                            if (model.Availability.IsWeeklyMonday)
                                AvailbilitypatternName += "," + DayOfWeek.Monday.ToString();

                            if (model.Availability.IsWeeklyTuesday)
                                AvailbilitypatternName += "," + DayOfWeek.Tuesday.ToString();

                            if (model.Availability.IsWeeklyWednesday)
                                AvailbilitypatternName += "," + DayOfWeek.Wednesday.ToString();

                            if (model.Availability.IsWeeklyThursday)
                                AvailbilitypatternName += "," + DayOfWeek.Thursday.ToString();

                            if (model.Availability.IsWeeklyFriday)
                                AvailbilitypatternName += "," + DayOfWeek.Friday.ToString();

                            if (model.Availability.IsWeeklySaturday)
                                AvailbilitypatternName += "," + DayOfWeek.Saturday.ToString();

                            if (model.Availability.IsWeeklySunday)
                                AvailbilitypatternName += "," + DayOfWeek.Sunday.ToString();

                            AvailbilitypatternName = AvailbilitypatternName.TrimStart(',');
                            StringBuilder sb = new StringBuilder(AvailbilitypatternName);
                            int lastComma = AvailbilitypatternName.LastIndexOf(',');
                            if (lastComma != -1)
                            {
                                sb.Remove(lastComma, 1);
                                sb.Insert(lastComma, " and ");
                                AvailbilitypatternName = sb.ToString();
                            }
                            AvailbilitypatternName = "Every " + AvailbilitypatternName;
                        }

                    }
                    // Frequency type for three
                    else if (model.Availability.FrequencyType == 3)
                    {
                        if (model.Availability.MonthlyType == 1)
                        {
                            if (model.Availability.MonthlyDays < 1 || model.Availability.MonthlyDays > 31)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", IsSuccess = false });
                            }
                            else if (model.Availability.MonthlyMonths < 1 || model.Availability.MonthlyMonths > 99)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for every month, 2 for every other month and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every month, 2 for every other month and so on.", IsSuccess = false });
                            }
                            else
                            {
                                Availbilitypattern = string.Format("M1{0}{1}",
                                    (model.Availability.MonthlyDays.ToString().Length > 1 ? model.Availability.MonthlyDays.ToString() : "0" + model.Availability.MonthlyDays.ToString()),
                                    model.Availability.MonthlyMonths
                                    );

                                AvailbilitypatternName = string.Format("On the {0} day of every {1}month",
                                    model.Availability.MonthlyDays + model.Availability.MonthlyDays.ToOccurrenceSuffix(),
                                    (model.Availability.MonthlyMonths == 1 ? "" : (model.Availability.MonthlyMonths == 2) ? "other " : (model.Availability.MonthlyMonths + model.Availability.MonthlyMonths.ToOccurrenceSuffix() + " "))
                                    );

                            }
                        }
                        else
                        {
                            if (model.Availability.MonthlyWeekMonth < 1 || model.Availability.MonthlyWeekMonth > 99)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for every month, 2 for every other month and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every month, 2 for every other month and so on.", IsSuccess = false });
                            }
                            else
                            {
                                Availbilitypattern = string.Format("M2{0}{1}{2}",
                                    model.Availability.MonthlyWeek,
                                    model.Availability.MonthlyWeekDay,
                                    model.Availability.MonthlyWeekMonth
                                    );

                                AvailbilitypatternName = string.Format("On the {0} of every {1}month",
                                    ((MonthWeek)model.Availability.MonthlyWeek).GetDescription().ToLower() + " " +
                                    ((WeekDay)model.Availability.MonthlyWeekDay).GetDescription(),
                                   (model.Availability.MonthlyWeekMonth == 1 ? "" : (model.Availability.MonthlyWeekMonth == 2) ? "other " : (model.Availability.MonthlyWeekMonth + model.Availability.MonthlyWeekMonth.ToOccurrenceSuffix() + " "))
                                   );
                            }
                        }
                    }
                    // Frequency type for four
                    else if (model.Availability.FrequencyType == 4)
                    {
                        if (model.Availability.AnnualYears < 1 || model.Availability.AnnualYears > 31)
                        {
                            ShowErrorMessage("Error!", "Enter 1 for every year, 2 for every other year and so on.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every year, 2 for every other year and so on.", IsSuccess = false });
                        }

                        if (model.Availability.AnnualType == 1)
                        {
                            if (model.Availability.AnnualMonthDay < 1 || model.Availability.AnnualMonthDay > 31)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", IsSuccess = false });
                            }
                            else if (model.Availability.AnnualMonth == 2 && (model.Availability.AnnualMonthDay == 30 || model.Availability.AnnualMonthDay == 31))
                            {
                                ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter valid date on selected month.", IsSuccess = false });
                            }
                            else if ((model.Availability.AnnualMonth == 4 || model.Availability.AnnualMonth == 6 || model.Availability.AnnualMonth == 9 || model.Availability.AnnualMonth == 11) && model.Availability.AnnualMonthDay == 31)
                            {
                                ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter valid date on selected month.", IsSuccess = false });
                            }
                            else
                            {
                                Availbilitypattern = string.Format("A{0}1{1}{2}",
                                    (model.Availability.AnnualYears.ToString().Length > 1 ? model.Availability.AnnualYears.ToString() : "0" + model.Availability.AnnualYears.ToString()),
                                    (model.Availability.AnnualMonth.ToString().Length > 1 ? model.Availability.AnnualMonth.ToString() : "0" + model.Availability.AnnualMonth.ToString()),
                                    model.Availability.AnnualMonthDay
                                    );
                                AvailbilitypatternName = string.Format("Every {0}year on {1} {2}",
                                              (model.Availability.AnnualYears == 1 ? "" : (model.Availability.AnnualYears == 2) ? "other " : (model.Availability.AnnualYears + model.Availability.AnnualYears.ToOccurrenceSuffix() + " ")),
                                              ((Month)model.Availability.AnnualMonth).GetDescription().ToLower(),
                                              model.Availability.AnnualMonthDay + model.Availability.AnnualMonthDay.ToOccurrenceSuffix()
                                              );

                            }
                        }
                        else
                        {
                            Availbilitypattern = string.Format("A{0}2{1}{2}{3}",
                                   (model.Availability.AnnualYears.ToString().Length > 1 ? model.Availability.AnnualYears.ToString() : "0" + model.Availability.AnnualYears.ToString()),
                                   model.Availability.AnnualWeek,
                                   model.Availability.AnnualWeekDay,
                                   (model.Availability.AnnualMonthWeek.ToString().Length > 1 ? model.Availability.AnnualMonthWeek.ToString() : "0" + model.Availability.AnnualMonthWeek.ToString())
                                   );

                            AvailbilitypatternName = string.Format("Every {0}year on {1} {2} of {3}",
                                          (model.Availability.AnnualYears == 1 ? "" : (model.Availability.AnnualYears == 2) ? "other " : (model.Availability.AnnualYears + model.Availability.AnnualYears.ToOccurrenceSuffix() + " ")),
                                          ((MonthWeek)model.Availability.AnnualWeek).GetDescription().ToLower(),
                                          ((WeekDay)model.Availability.AnnualWeekDay).GetDescription(),
                                          ((Month)model.Availability.AnnualMonthWeek).GetDescription().ToLower()
                                          );
                        }
                    }

                    model.Availability.AuditUserId = CurrentUser.UserID;
                    model.Availability.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    model.Availability.Pattern = Availbilitypattern;
                    model.Availability.PatternName = AvailbilitypatternName;

                    #endregion


                    #region Availibility Save

                    VolunteerAvailability entityAvailability;
                    if (model.Availability.AvailabilityId == 0)
                        entityAvailability = new VolunteerAvailability();
                    else
                        entityAvailability = volunteerService.GetAvailabilityById(model.Availability.AvailabilityId);

                    entityAvailability = VolunteerAvailabilityUnavailabilityDtoMapper.MapAvailability(model, entityAvailability);
                    volunteerService.Saveavailability(entityAvailability, true);

                    #endregion

                    ShowSuccessMessage("Success!", "Your availability added successfully.", false);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Your availability added successfully.", IsSuccess = true });

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = Ex.Message, IsSuccess = true });
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please fill all required fields!", IsSuccess = true });
            }
        }

        [HttpGet]
        public IActionResult AddUnAvailability(int volunteerId)
        {
            VolunteerDto model = new VolunteerDto();
            var volunteer = volunteerService.GetVolunteerById(volunteerId);

            if (volunteer != null)
            {
                model.VolunteerId = volunteerId;
                model.VolunteerName = $"{volunteer.Contact.ForeName} {volunteer.Contact.Surname}";
            }

            var enumList = Enum.GetValues(typeof(MaritalType));
            List<SelectListItem> maritalList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                maritalList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((MaritalType)(int)items).GetDescription(),
                    Selected = (int)MaritalType.Individual == (int)items ? true : false
                });
            }
            ViewBag.MaritalTypeList = maritalList;


            var workTypes = Enum.GetValues(typeof(WorkType));
            List<SelectListItem> workTypeList = new List<SelectListItem>();
            foreach (var workType in workTypes)
            {
                workTypeList.Add(new SelectListItem
                {
                    Value = ((int)workType).ToString(),
                    Text = ((WorkType)(int)workType).GetDescription(),
                    Selected = (int)WorkType.Packing == (int)workType ? true : false
                });
            }
            ViewBag.WorkTypeList = workTypeList;

            ViewBag.MonthWeek = Enum.GetValues(typeof(MonthWeek)).Cast<MonthWeek>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            ViewBag.WeekDay = Enum.GetValues(typeof(WeekDay)).Cast<WeekDay>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();


            ViewBag.Month = Enum.GetValues(typeof(Month)).Cast<Month>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            return PartialView("_AddUnavailability", model);
        }

        [HttpPost]
        public IActionResult AddUnAvailability(VolunteerDto model)
        {
            if (!model.IsChangePassword)
            {
                ModelState.Remove("PasswordQuestion");
                ModelState.Remove("PasswordAnswer");
                ModelState.Remove("EditPassword");
                ModelState.Remove("ConfirmPassword");
            }
            if (!model.IsRegularAvailability)
            {
                ModelState.Remove("Availability.FromDate");
                ModelState.Remove("Availability.ToDate");
            }
            ModelState.Remove("MaritalStatus");
            ModelState.Remove("WorkType");

            if (ModelState.IsValid)
            {
                try
                {
                    #region Unavailbility Section

                    if (model.Unavailability.FromDate.ToDateTimeNullable() == null)
                    {
                        ShowErrorMessage("Error!", "From date required.", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "From date required.", IsSuccess = false });
                        //return RedirectToAction("updateprofile", "volunteer");
                    }

                    if (model.Unavailability.ToDate.ToDateTimeNullable() == null)
                    {
                        ShowErrorMessage("Error!", "Finish date required.", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Finish date required.", IsSuccess = false });
                        //return RedirectToAction("updateprofile", "volunteer");
                    }
                    else if (model.Unavailability.ToDate.ToDateTime() < model.Unavailability.ToDate.ToDateTime())
                    {
                        ShowErrorMessage("Error!", "The finish date must be after the start date.", false);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "The finish date must be after the start date.", IsSuccess = false });
                        //return RedirectToAction("updateprofile", "volunteer");
                    }

                    if (model.Unavailability.UnavailabilityTimeType == 2)
                    {
                        if (string.IsNullOrEmpty(model.Unavailability.TimeForm))
                        {
                            ShowErrorMessage("Error!", "Unavilability start time required.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Unavilability start time required.", IsSuccess = false });
                        }

                        if (string.IsNullOrEmpty(model.Unavailability.TimeTo))
                        {
                            ShowErrorMessage("Error!", "Unavilability end time required.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Unavilability end time required.", IsSuccess = false });
                        }
                    }


                    string pattern = string.Empty;
                    string patternName = string.Empty;

                    // Frequenct type for one
                    if (model.Unavailability.FrequencyType == 1)
                    {
                        if (model.Unavailability.DailyType == 1)
                        {
                            if (model.Unavailability.DailyDays < 1 || model.Unavailability.DailyDays > 99)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for every day, 2 for every other day and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every day, 2 for every other day and so on.", IsSuccess = false });
                            }
                            else
                            {
                                pattern = "D1" + model.Unavailability.DailyDays;
                                patternName = string.Format("Every {0}day",
                                    (model.Unavailability.DailyDays == 1 ? "" : (model.Unavailability.DailyDays == 2 ? "other " : (model.Unavailability.DailyDays + model.Unavailability.DailyDays.ToOccurrenceSuffix())))
                                    );
                            }
                        }
                        else
                        {
                            pattern = "D2";
                            patternName = "Every weekday";
                        }
                    }
                    // frequency type for two
                    else if (model.Unavailability.FrequencyType == 2)
                    {
                        if (model.Unavailability.WeeklyDays < 1 || model.Unavailability.WeeklyDays > 99)
                        {
                            ShowErrorMessage("Error!", "Enter 1 for every week, 2 for every other week and so on.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every week, 2 for every other week and so on.", IsSuccess = false });
                        }
                        else if (!model.Unavailability.IsWeeklyMonday && !model.Unavailability.IsWeeklyTuesday && !model.Unavailability.IsWeeklyWednesday && !model.Unavailability.IsWeeklyThursday && !model.Unavailability.IsWeeklyFriday && !model.Unavailability.IsWeeklySaturday && !model.Unavailability.IsWeeklySunday)
                        {
                            ShowErrorMessage("Error!", "You must select at least one day of the week.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "You must select at least one day of the week.", IsSuccess = false });
                        }
                        else
                        {
                            pattern = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", "W",
                                (model.Unavailability.IsWeeklyMonday ? "Y" : "N"),
                                (model.Unavailability.IsWeeklyTuesday ? "Y" : "N"),
                                (model.Unavailability.IsWeeklyWednesday ? "Y" : "N"),
                                (model.Unavailability.IsWeeklyThursday ? "Y" : "N"),
                                (model.Unavailability.IsWeeklyFriday ? "Y" : "N"),
                                (model.Unavailability.IsWeeklySaturday ? "Y" : "N"),
                                (model.Unavailability.IsWeeklySunday ? "Y" : "N"),
                                 model.Unavailability.WeeklyDays
                                );

                            //patternName = "Every";
                            patternName = string.Empty;
                            if (model.Unavailability.IsWeeklyMonday)
                                patternName += "," + DayOfWeek.Monday.ToString();

                            if (model.Unavailability.IsWeeklyTuesday)
                                patternName += "," + DayOfWeek.Tuesday.ToString();

                            if (model.Unavailability.IsWeeklyWednesday)
                                patternName += "," + DayOfWeek.Wednesday.ToString();

                            if (model.Unavailability.IsWeeklyThursday)
                                patternName += "," + DayOfWeek.Thursday.ToString();

                            if (model.Unavailability.IsWeeklyFriday)
                                patternName += "," + DayOfWeek.Friday.ToString();

                            if (model.Unavailability.IsWeeklySaturday)
                                patternName += "," + DayOfWeek.Saturday.ToString();

                            if (model.Unavailability.IsWeeklySunday)
                                patternName += "," + DayOfWeek.Sunday.ToString();

                            patternName = patternName.TrimStart(',');
                            StringBuilder sb = new StringBuilder(patternName);
                            int lastComma = patternName.LastIndexOf(',');
                            if (lastComma != -1)
                            {
                                sb.Remove(lastComma, 1);
                                sb.Insert(lastComma, " and ");
                                patternName = sb.ToString();
                            }
                            patternName = "Every " + patternName;
                        }

                    }
                    // Frequency type for three
                    else if (model.Unavailability.FrequencyType == 3)
                    {
                        if (model.Unavailability.MonthlyType == 1)
                        {
                            if (model.Unavailability.MonthlyDays < 1 || model.Unavailability.MonthlyDays > 31)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", IsSuccess = false });
                            }
                            else if (model.Unavailability.MonthlyMonths < 1 || model.Unavailability.MonthlyMonths > 99)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for every month, 2 for every other month and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every month, 2 for every other month and so on.", IsSuccess = false });
                            }
                            else
                            {
                                pattern = string.Format("M1{0}{1}",
                                    (model.Unavailability.MonthlyDays.ToString().Length > 1 ? model.Unavailability.MonthlyDays.ToString() : "0" + model.Unavailability.MonthlyDays.ToString()),
                                    model.Unavailability.MonthlyMonths
                                    );

                                patternName = string.Format("On the {0} day of every {1}month",
                                    model.Unavailability.MonthlyDays + model.Unavailability.MonthlyDays.ToOccurrenceSuffix(),
                                    (model.Unavailability.MonthlyMonths == 1 ? "" : (model.Unavailability.MonthlyMonths == 2) ? "other " : (model.Unavailability.MonthlyMonths + model.Unavailability.MonthlyMonths.ToOccurrenceSuffix() + " "))
                                    );

                            }
                        }
                        else
                        {
                            if (model.Unavailability.MonthlyWeekMonth < 1 || model.Unavailability.MonthlyWeekMonth > 99)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for every month, 2 for every other month and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every month, 2 for every other month and so on.", IsSuccess = false });
                            }
                            else
                            {
                                pattern = string.Format("M2{0}{1}{2}",
                                    model.Unavailability.MonthlyWeek,
                                    model.Unavailability.MonthlyWeekDay,
                                    model.Unavailability.MonthlyWeekMonth
                                    );

                                patternName = string.Format("On the {0} of every {1}month",
                                    ((MonthWeek)model.Unavailability.MonthlyWeek).GetDescription().ToLower() + " " +
                                    ((WeekDay)model.Unavailability.MonthlyWeekDay).GetDescription(),
                                   (model.Unavailability.MonthlyWeekMonth == 1 ? "" : (model.Unavailability.MonthlyWeekMonth == 2) ? "other " : (model.Unavailability.MonthlyWeekMonth + model.Unavailability.MonthlyWeekMonth.ToOccurrenceSuffix() + " "))
                                   );
                            }
                        }
                    }
                    // Frequency type for four
                    else if (model.Unavailability.FrequencyType == 4)
                    {
                        if (model.Unavailability.AnnualYears < 1 || model.Unavailability.AnnualYears > 31)
                        {
                            ShowErrorMessage("Error!", "Enter 1 for every year, 2 for every other year and so on.", false);
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for every year, 2 for every other year and so on.", IsSuccess = false });
                        }

                        if (model.Unavailability.AnnualType == 1)
                        {
                            if (model.Unavailability.AnnualMonthDay < 1 || model.Unavailability.AnnualMonthDay > 31)
                            {
                                ShowErrorMessage("Error!", "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", IsSuccess = false });
                            }
                            else if (model.Unavailability.AnnualMonth == 2 && (model.Unavailability.AnnualMonthDay == 30 || model.Unavailability.AnnualMonthDay == 31))
                            {
                                ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter valid date on selected month.", IsSuccess = false });
                            }
                            else if ((model.Unavailability.AnnualMonth == 4 || model.Unavailability.AnnualMonth == 6 || model.Unavailability.AnnualMonth == 9 || model.Unavailability.AnnualMonth == 11) && model.Unavailability.AnnualMonthDay == 31)
                            {
                                ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Enter valid date on selected month.", IsSuccess = false });
                            }
                            else
                            {
                                pattern = string.Format("A{0}1{1}{2}",
                                    (model.Unavailability.AnnualYears.ToString().Length > 1 ? model.Unavailability.AnnualYears.ToString() : "0" + model.Unavailability.AnnualYears.ToString()),
                                    (model.Unavailability.AnnualMonth.ToString().Length > 1 ? model.Unavailability.AnnualMonth.ToString() : "0" + model.Unavailability.AnnualMonth.ToString()),
                                    model.Unavailability.AnnualMonthDay
                                    );
                                patternName = string.Format("Every {0}year on {1} {2}",
                                              (model.Unavailability.AnnualYears == 1 ? "" : (model.Unavailability.AnnualYears == 2) ? "other " : (model.Unavailability.AnnualYears + model.Unavailability.AnnualYears.ToOccurrenceSuffix() + " ")),
                                              ((Month)model.Unavailability.AnnualMonth).GetDescription().ToLower(),
                                              model.Unavailability.AnnualMonthDay + model.Unavailability.AnnualMonthDay.ToOccurrenceSuffix()
                                              );

                            }
                        }
                        else
                        {
                            pattern = string.Format("A{0}2{1}{2}{3}",
                                   (model.Unavailability.AnnualYears.ToString().Length > 1 ? model.Unavailability.AnnualYears.ToString() : "0" + model.Unavailability.AnnualYears.ToString()),
                                   model.Unavailability.AnnualWeek,
                                   model.Unavailability.AnnualWeekDay,
                                   (model.Unavailability.AnnualMonthWeek.ToString().Length > 1 ? model.Unavailability.AnnualMonthWeek.ToString() : "0" + model.Unavailability.AnnualMonthWeek.ToString())
                                   );

                            patternName = string.Format("Every {0}year on {1} {2} of {3}",
                                          (model.Unavailability.AnnualYears == 1 ? "" : (model.Unavailability.AnnualYears == 2) ? "other " : (model.Unavailability.AnnualYears + model.Unavailability.AnnualYears.ToOccurrenceSuffix() + " ")),
                                          ((MonthWeek)model.Unavailability.AnnualWeek).GetDescription().ToLower(),
                                          ((WeekDay)model.Unavailability.AnnualWeekDay).GetDescription(),
                                          ((Month)model.Unavailability.AnnualMonthWeek).GetDescription().ToLower()
                                          );
                        }
                    }

                    model.Unavailability.AuditUserId = CurrentUser.UserID;
                    model.Unavailability.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    model.Unavailability.Pattern = pattern;
                    model.Unavailability.PatternName = patternName;

                    #endregion

                    #region Unavailbility save 

                    VolunteerUnavailability entity;
                    if (model.Unavailability.UnavailabilityId == 0)
                        entity = new VolunteerUnavailability();
                    else
                        entity = volunteerService.GetUnavailabilityById(model.Unavailability.UnavailabilityId);

                    entity = VolunteerAvailabilityUnavailabilityDtoMapper.MapUnavailability(model, entity);
                    volunteerService.Save(entity, true);
                    #endregion

                    var message = "Your unavailability added successfully.";
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = true });

                }
                catch (Exception Ex)
                {
                    var message = Ex.Message;
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
                }
            }
            else
            {
                var message = "Please fill all required fields!";
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
        }

        #region Delete Availability
        public IActionResult DeleteAvaliability()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure you want to delete this period of Availability?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Availability" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public IActionResult DeleteAvaliability(int id)
        {
            string message = "Error";
            try
            {
                VolunteerAvailability availability = volunteerService.GetAvailabilityById(id);
                if (availability != null)
                {
                    volunteerService.DeleteVolunteerAvailability(id);
                    message = "Success";
                    ShowSuccessMessage("Success!", "Avalibality Deleted Successfully.", false);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Avalibality Deleted Successfully.", IsSuccess = true });
                }
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                ShowErrorMessage("Success!", message, false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Something went wrong. Please try again later.", IsSuccess = true });
        }
        #endregion

        #region Delete Unavailability
        public IActionResult DeleteUnavaliability()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure you want to delete this period of Unavailability?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Unavailability" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public IActionResult DeleteUnavaliability(int id)
        {
            string message = "Error";
            try
            {
                VolunteerUnavailability unavailability = volunteerService.GetUnavailabilityById(id);
                if (unavailability != null)
                {
                    volunteerService.DeleteVolunteerUnavailability(id);
                    message = "Success";
                    ShowSuccessMessage("Success!", "Unavalibality Deleted Successfully.", false);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Unavalibality Deleted Successfully.", IsSuccess = true });
                }
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                ShowErrorMessage("Success!", message, false);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Something went wrong. Please try again later.", IsSuccess = true });
        }
        #endregion

        public IActionResult loginpageredirection()
        {
            ShowSuccessMessage("Success!", "Your account has been created successfully. Please wait for approval from foodbank.", false);
            return RedirectToAction("login", "account");
        }

        #endregion
    }
}
