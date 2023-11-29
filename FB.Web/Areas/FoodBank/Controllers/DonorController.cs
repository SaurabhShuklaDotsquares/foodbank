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
namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class DonorController : BaseController
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
        private IFoodbankService foodbankService;

        public DonorController(IUserService _userService, IMenuService _menuService, IPersonService _personService, IBranchService _branchService, ICharityService _charityService,
            ICentralOfficeService _centralofficeService, IForgotPasswordService _forgotPasswordService, IRoleService _roleService, IMapper _mapper,
            IQuickDonorGiftService _quickDonorGiftService, ICountryService _countryService, IAddressService _addressService, IMyReferralService _ReferralService, IContactService _contactService, IVolunteerService _volunteerService, IFoodbankService _foodbankService)
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

        #region Donor Registration Section

        public IActionResult DonorRegistration()
        {

            var personDto = new PersonDto();
            var foodbank = foodbankService.GetFoodbankByUserId(CurrentUser.UserID);
            ViewBag.Charities = new List<SelectListItem>();
            ViewBag.Branches = new List<SelectListItem>();

            BindOrganisationViewBag(foodbank.User.CentralOfficeId ?? 0, foodbank.User.CharityId ?? 0, foodbank.User.BranchId ?? 0);

            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            personDto.Addresses.Add(new PersonAddressDto());
            personDto.BranchIDs = foodbank.User.BranchId.ToString();
            return View(personDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorRegistration(PersonDto model)
        {
            ModelState.Remove("DefaultPurposeID");
            ModelState.Remove("PersonTypeID");
            ModelState.Remove("BranchID");
            ModelState.Remove("CharityID");
            ModelState.Remove("BranchIDs");

            var foodbank = foodbankService.GetFoodbankByUserId(CurrentUser.UserID);
            int BranchId = foodbank.User.BranchId ?? 0;
            
            model.Reference = GetLastReference(BranchId);
            ViewBag.Charities = new List<SelectListItem>();
            ViewBag.Branches = new List<SelectListItem>();
            BindOrganisationViewBag(foodbank.User.CentralOfficeId ?? 0, foodbank.User.CharityId ?? 0, foodbank.User.BranchId ?? 0);
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            if (model.Overseas)
                ModelState.Remove("Addresses[0].PostCode");

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.BranchIDs == null)
                        model.BranchIDs = CurrentUser.BranchID.ToString();

                    if (!string.IsNullOrEmpty(model.UserName))
                    {
                        if (userService.IsUserNameExist(model.UserName) || quickDonorGiftService.IsUserNameExist(model.UserName))
                        {
                            ModelState.AddModelError("Error!", "Username already exist.");
                            ShowErrorMessage("Error!", "Username already exist.", false);
                            //return CreateModelStateErrors();
                            return View(model);
                        }
                    }

                    foreach (var item in model.BranchIDs.Split(','))
                    {
                        if (quickDonorGiftService.IsBranchReferenceExist(model.Reference, Convert.ToInt32(item)))
                        {
                            ModelState.AddModelError("Error!", "Donor with this reference number already exists within selected branch");
                            ShowErrorMessage("Error!", "Donor with this reference number already exists within selected branch", false);
                            // return CreateModelStateErrors();
                            return View(model);
                        }

                        Branch branch = branchService.GetBranch(Convert.ToInt32(item));
                        //if (!branch.IsMmo && !branch.IsMgo.Value)
                        //{
                        //    ModelState.AddModelError("Error!", $"Please enable branch {branch.BranchDescription} with MGO or MMO.");
                        //    return CreateModelStateErrors();
                        //}

                        //if (branch.IsMmo && branch.IsMgo.Value && !model.IsMMO && !model.IsMGO)
                        //{
                        //    ModelState.AddModelError("Error!", $"Branch {branch.BranchDescription} is enabled with MGO & MMO. So, please enable person MGO Or MMO or both");
                        //    return CreateModelStateErrors();
                        //}
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

                    var objbranch = branchService.GetBranch(BranchId);
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
                        donorfoodbank.FoodBankId = foodbank.Id;
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
                            userService.Save(user, user.CreatedBy, true);
                        }
                    }

                    ShowSuccessMessage("Success!", "Donor saved successfully. Please login with your credentials.", false);
                    return RedirectToAction("donorregistration", "Donor");
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
                    return View(model);//RedirectToAction("donorregistration", "Donor");
                }
            }
            return View(model);
        }

        [NonAction]
        public void BindOrganisationViewBag(int CentralOfficeId, int charityID, int BranchID)
        {
            ViewBag.Organisations = centralofficeService.GetCentralOffices(CentralOfficeId).Select(c => new SelectListItem
            {
                Text = c.OrganisationName,
                Value = c.CentralOfficeId.ToString()
            }).ToList();

            var Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CentralOfficeId, CurrentUser.UserID, true, true, charityID).Select(c => new SelectListItem
            {
                Text = c.CharityName.AddCharityPrefix(c.Prefix),
                Value = c.CharityId.ToString()
            }).ToList();


            var Branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CentralOfficeId, CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();

            Branches.Insert(0,new SelectListItem("Select Branch",""));

            Charities.Insert(0, new SelectListItem("Select Charity", ""));
            ViewBag.Branches = Branches;
            ViewBag.Charities = Charities;
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
            var result = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, charityID, userID: CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();
            //result.Insert(0, new SelectListItem("Select Branch", ""));

           
            ViewBag.Branches = result;
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }

        /// <summary>
        /// To get the reference number of last added donor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLastReference(string BranchIds)
        {
            try
            {
                if (!string.IsNullOrEmpty(BranchIds))
                {
                    string[] branches = BranchIds.Split(',');
                    BranchReferenceDto BranchReference = new BranchReferenceDto();
                    if (branches.Count() > 1)
                    {
                        BranchReference.References = new List<DonorReferences>();
                        foreach (var item in branches)
                        {
                            BranchReference.References.Add(branchService.GetDonorReferenceByBranch(Convert.ToInt32(item)));
                        }
                        BranchReference.IsNextReference = false;
                        return NewtonSoftJsonResult(new RequestOutcome<BranchReferenceDto> { Data = BranchReference, IsSuccess = true });
                    }
                    else
                    {
                        var branchData = branchService.GetBranch(Convert.ToInt32(branches[0]));
                        if (branchData != null && branchData.ReferenceType == 2)
                        {
                            BranchReference.References = new List<DonorReferences>();
                            foreach (var item in branches)
                            {
                                BranchReference.References.Add(branchService.GetDonorReferenceByBranch(Convert.ToInt32(item)));
                            }
                            BranchReference.IsNextReference = false;
                            return NewtonSoftJsonResult(new RequestOutcome<BranchReferenceDto> { Data = BranchReference, IsSuccess = true });
                        }
                        else
                        {
                            BranchReference.IsNextReference = true;
                            BranchReference.DonorReference = branchService.GetDonorReference(Convert.ToInt32(branches[0]));
                            return NewtonSoftJsonResult(new RequestOutcome<BranchReferenceDto> { Data = BranchReference, IsSuccess = true });
                        }
                    }
                }
                else
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please select branch", IsSuccess = false });
                }
            }
            catch
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "There is some internal problem !", IsSuccess = false });
            }
        }
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
        public string UserAvailability(string userName)
        {
            if (userService.IsUserNameExist(userName) || quickDonorGiftService.IsUserNameExist(userName))
            {
                return "Username already exist";
            }
            return "";
        }
        #endregion
        #region Donor List , view 
        [HttpGet]
        public IActionResult DonorList()
        {
            BindOrganisationViewBag(CurrentUser.OrganisationID,0,0);
            return View();
        }
        [HttpPost]
        public IActionResult DonorList(DataTableServerSide model,int charitID,int BranchID)
        {
            KeyValuePair<int, List<FeedbackDonorDto>> donorlist = new KeyValuePair<int, List<FeedbackDonorDto>>();
            donorlist = foodbankService.GetPersonsByFoodbank(model, CurrentUser.FoodbankId, charitID, BranchID);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = donorlist.Key,
                recordsFiltered = donorlist.Key,
                data = donorlist.Value.Select((c, index) => new List<object> {
                    c.PersonID,
                    model.start+index+1,
                    $"{c.Title} {c.Forenames} {c.Surname}",
                    c.Email,
                    c.CentralOfficeName,
                    c.CharityName,
                    c.Branch,
                    c.Reference,
                    "<a href=" + Url.Action("ViewDonor", "Donor", new { id = c.PersonID })
                    + " class='view_btn'><img src='/Content/images/eye-icon.png' alt='' /></a><a href=" + Url.Action("DonorEdit", "Donor", new { id = c.PersonID })
                    + " class='view_btn'><img src='/Content/images/edit-icon.png' alt='' /></a><a  data-toggle='modal' data-target='#modal-delete-donor' href=" + Url.Action("Delete", "Donor", new { id = c.PersonID })
                    + " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"
                    ,
                    //c.PersonID
                   })
            });
            return View();
        }
        [HttpGet]
        public IActionResult FeedbackDetails(int Id)
        {
            //    var res = feedbackService.GetFeedbackDetailsByFeedbackID(Id);
            return View();
        }

        [HttpGet]
        public IActionResult DonorEdit(int Id)
        {
            var person = personService.GetPersonById(Id);

            int CentralOfficeID = 0;
            int charityId = 0;
            OrganistionType branchType = OrganistionType.MyGivingOnline;
            if (person != null)
            {
                CentralOfficeID = person.CentralOfficeId.Value;
                charityId = person.CharityId.Value;

                if (person.Branch.IsMmo && person.Branch.IsMgo.Value)
                {
                    branchType = OrganistionType.MyGivingOnlineAndMyMembership;
                }
            }
            var usrDetail = person.User.Where(x => x.PersonId == person.PersonId).FirstOrDefault();

            ViewBag.ReferenceType = (int)person.Branch.ReferenceType == 1 ? "Numeric" : "Character";
            var personDto = new EditPersonDto()
            {
                UserId = CurrentUser.UserID,
                PersonID = person.PersonId,
                Reference = person.Reference,
                EditTitle = person.Title,
                ForeName = person.Forenames,
                Surname = person.Surname,
                FAX = person.Fax,
                Suffix = person.Suffix,
                Initials = person.Initials,
                Email = person.Email,
                BranchName = person.Branch.BranchDescription,
                CharityName = person.Charity.CharityName,
                CentralOfficeName = person.CentralOffice.OrganisationName
            };

            personDto.charityId = charityId;
            if (usrDetail != null)
            {
                personDto.UserName = usrDetail.UserName.Trim();
                personDto.EditPassword = EncryptionUtils.Decrypt(usrDetail.Password, usrDetail.PasswordSalt);
                personDto.PasswordQuestion = usrDetail.PasswordQuestion;
                personDto.PasswordAnswer = EncryptionUtils.Decrypt(usrDetail.PasswordAnswer, usrDetail.PasswordSalt);
            }

            if (!string.IsNullOrEmpty(personDto.FullHMRCAddress))
                personDto.FullHMRCAddress = personDto.FullHMRCAddress.Replace(",", ",\n");

            //Bind Person Address
            var personAddress = UpdateDonorAddress(person.PersonId);
            if (personAddress != null && personAddress.AddressID > 0)
            {
                personDto.AddressID = personAddress.AddressID;
                personDto.CountryId = personAddress.CountryID.Value;
                personDto.PostCode = personAddress.PostCode;
                personDto.FullHMRCAddress = personAddress.HMRCAddress;
                personDto.StreetName = personAddress.StreetName;
                personDto.HouseName = personAddress.HouseName;
                personDto.HouseNumber = personAddress.HouseNumber;
                personDto.OtherAddressLine = personAddress.OtherAddressLine;
                personDto.City = personAddress.City;
                personDto.Overseas = personAddress.Overseas;
            }

            ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
            {
                Text = c.CountryName.ToTitle(),
                Value = c.CountryId.ToString(),
                Selected = (personAddress.CountryID == null) ? (c.CountryName == Constants.DefaultCountry) : personAddress.CountryID == c.CountryId
            }).ToList();
            //End

            var personAdditonalDetails = person.MmopersonAdditonalDetails;
            if (personAdditonalDetails != null)
            {
                personDto.EditIsMMO = personAdditonalDetails.IsMmo;
                personDto.EditIsMGO = personAdditonalDetails.IsMgo;
            }
            else
            {
                personDto.EditIsMGO = true;
            }

            personDto.branchType = branchType;
            return View(personDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DonorEdit(EditPersonDto model)
        {
            ModelState.Remove("DefaultPurposeID");
            ModelState.Remove("PersonTypeID");
            ModelState.Remove("BranchID");
            ModelState.Remove("CharityID");
            ModelState.Remove("BranchIDs");

            var foodbank = foodbankService.GetFoodbankByUserId(CurrentUser.UserID);
            int BranchId = foodbank.User.BranchId ?? 0;
            ViewBag.Charities = new List<SelectListItem>();
            ViewBag.Branches = new List<SelectListItem>();
            BindOrganisationViewBag(foodbank.User.CentralOfficeId ?? 0, foodbank.User.CharityId ?? 0, foodbank.User.BranchId ?? 0);
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            try
            {
                if (!model.ChangePassword)
                {
                    ModelState.Remove("EditPassword");
                    ModelState.Remove("PasswordQuestion");
                    ModelState.Remove("PasswordAnswer");
                    ModelState.Remove("ConfirmPassword");
                }
                if (model.PostCode == null)
                {
                    ModelState.Remove("PostCode");
                }
                if (ModelState.IsValid)
                {
                    string HomePhone = string.Empty, MobilePhone = string.Empty, WorkPhone = string.Empty, Email = string.Empty;
                    if (model.PersonID > 0)
                    {
                        //Update Person Detail
                        Person person = personService.GetPerson(model.PersonID);
                        person.Reference = model.Reference;
                        person.Title = model.EditTitle;
                        person.Forenames = model.ForeName;
                        person.Surname = model.Surname;
                        person.Fax = model.FAX;
                        person.Suffix = model.Suffix;
                        person.Initials = model.Initials;
                        //person.Email = model.Email;
                        var result = personService.Save(person);

                        var usrDetail = person.User.FirstOrDefault();
                        if (result)
                        {
                            if (model.ChangePassword)
                            {
                                //Upodate User login details
                                //usrDetail.UserName = model.UserName.Trim();
                                string randonSalt = Common.GetRandomPasswordSalt();
                                usrDetail.Password = EncryptionUtils.HashPassword(model.EditPassword, randonSalt, DateTime.Now);
                                usrDetail.PasswordSalt = randonSalt;
                                usrDetail.PasswordQuestion = model.PasswordQuestion;
                                usrDetail.PasswordAnswer = model.PasswordAnswer;
                                usrDetail.LastPasswordChange = DateTime.Now;
                                usrDetail.LastLoginDate = DateTime.Now;
                                usrDetail.ModifiedDate = DateTime.Now;
                                userService.Save(usrDetail, CurrentUser.UserID, false);
                                //End

                            }

                            //Update Address
                            if (model.AddressID > 0)
                            {
                                AddUpdatePersonAddress(model);
                            }
                            //End
                        }
                    }
                }
                ShowSuccessMessage("Success!", "Profile has been updated successfully.", false);
                return RedirectToAction("DonorEdit", "donor", new { id = model.PersonID });
            }
            catch (Exception ex)
            {

                ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                return View(model);
            }
            return View(model);
        }
        public IActionResult ViewDonor(int Id)
        {
            var person = personService.GetPersonById(Id);

            int CentralOfficeID = 0;
            int charityId = 0;
            OrganistionType branchType = OrganistionType.MyGivingOnline;
            if (person != null)
            {
                CentralOfficeID = person.CentralOfficeId.Value;
                charityId = person.CharityId.Value;

                if (person.Branch.IsMmo && person.Branch.IsMgo.Value)
                {
                    branchType = OrganistionType.MyGivingOnlineAndMyMembership;
                }
            }
            var usrDetail = person.User.Where(x => x.PersonId == person.PersonId).FirstOrDefault();

            ViewBag.ReferenceType = (int)person.Branch.ReferenceType == 1 ? "Numeric" : "Character";
            var personDto = new EditPersonDto()
            {
                UserId = CurrentUser.UserID,
                PersonID = person.PersonId,
                Reference = person.Reference,
                EditTitle = person.Title,
                ForeName = person.Forenames,
                Surname = person.Surname,
                FAX = person.Fax,
                Suffix = person.Suffix,
                Initials = person.Initials,
                Email = person.Email,
                BranchName = person.Branch.BranchDescription,
                CharityName = person.Charity.CharityName,
                CentralOfficeName = person.CentralOffice.OrganisationName
            };

            personDto.charityId = charityId;
            if (usrDetail != null)
            {
                personDto.UserName = usrDetail.UserName.Trim();
                personDto.EditPassword = EncryptionUtils.Decrypt(usrDetail.Password, usrDetail.PasswordSalt);
                personDto.PasswordQuestion = usrDetail.PasswordQuestion;
                personDto.PasswordAnswer = EncryptionUtils.Decrypt(usrDetail.PasswordAnswer, usrDetail.PasswordSalt);
            }

            if (!string.IsNullOrEmpty(personDto.FullHMRCAddress))
                personDto.FullHMRCAddress = personDto.FullHMRCAddress.Replace(",", ",\n");

            //Bind Person Address
            var personAddress = UpdateDonorAddress(person.PersonId);
            if (personAddress != null && personAddress.AddressID > 0)
            {
                personDto.AddressID = personAddress.AddressID;
                personDto.CountryId = personAddress.CountryID.Value;
                personDto.PostCode = personAddress.PostCode;
                personDto.FullHMRCAddress = personAddress.HMRCAddress;
                personDto.StreetName = personAddress.StreetName;
                personDto.HouseName = personAddress.HouseName;
                personDto.HouseNumber = personAddress.HouseNumber;
                personDto.OtherAddressLine = personAddress.OtherAddressLine;
                personDto.City = personAddress.City;
                personDto.Overseas = personAddress.Overseas;
            }

            ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
            {
                Text = c.CountryName.ToTitle(),
                Value = c.CountryId.ToString(),
                Selected = (personAddress.CountryID == null) ? (c.CountryName == Constants.DefaultCountry) : personAddress.CountryID == c.CountryId
            }).ToList();
            //End

            var personAdditonalDetails = person.MmopersonAdditonalDetails;
            if (personAdditonalDetails != null)
            {
                personDto.EditIsMMO = personAdditonalDetails.IsMmo;
                personDto.EditIsMGO = personAdditonalDetails.IsMgo;
            }
            else
            {
                personDto.EditIsMGO = true;
            }

            personDto.branchType = branchType;
            return View(personDto);

        }
        [HttpGet]
        public PersonAddressViewModel UpdateDonorAddress(int id)
        {
            PersonAddressViewModel personAddress = new PersonAddressViewModel();
            Person person = personService.GetPersonById(id);
            if (person.Address != null && person.Address.Count > 0)
            {
                var address = person.Address.FirstOrDefault();
                personAddress = new PersonAddressViewModel
                {
                    AddressID = address.AddressId,
                    CountryID = address.CountryId,
                    HouseName = address.HouseName,
                    HouseNumber = address.HouseNumber,
                    PostCode = address.Postcode,
                    StreetName = address.StreetName,
                    OtherAddressLine = address.OtherAddressLine,
                    City = address.City,
                    Overseas = person.Overseas,
                    HMRCAddress = person.Hmrcaddress,
                    HMRCAddressOverride = person.HmrcaddressOverride
                };
            }
            return personAddress;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this donor?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Donor" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the membership type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var person = personService.GetPerson(id);
                if (person != null)
                {
                    var user = userService.GetUser(person.User.FirstOrDefault().UserId);
                    user.Active = false;
                    userService.Save(user, 0, false);
                    person.Active = false;
                    personService.Save(person);
                }
                message = "Success";
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                // ShowErrorMessage("Error!", message, false);
            }

            // return CreateModelStateErrors();
            return message;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool AddUpdatePersonAddress(EditPersonDto model)
        {
            Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>
            {
                { DataEnityNames.Person, model.PersonID }
            };

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.PersonID > 0)
                    {
                        Person person = personService.GetPersonById(model.PersonID);
                        if (person != null)
                        {
                            Address address = model.AddressID > 0 ? addressService.GetAddress(model.AddressID) : new Address();
                            address.HouseName = model.HouseName;
                            address.HouseNumber = model.HouseNumber;
                            address.StreetName = model.StreetName;
                            address.OtherAddressLine = model.OtherAddressLine;
                            address.District = model.District;
                            address.City = model.City;
                            address.Postcode = model.PostCode == null ? model.OldPostCode : model.PostCode;
                            address.CountryId = model.CountryID;
                            address.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                            address.AuditUserId = CurrentUser.UserID;
                            addressService.Save(address, address.AddressId == 0);

                            var personAdditionalDetails = person.MmopersonAdditonalDetails;
                            if (personAdditionalDetails != null)
                            {
                                person.Hmrcaddress = string.Format("{0}{1}{2}{3}",
                                                            !string.IsNullOrWhiteSpace(address.HouseName) ? address.HouseName.Trim() + " ," : address.HouseName,
                                                            !string.IsNullOrWhiteSpace(address.HouseNumber) ? address.HouseNumber + ", " : string.Empty,
                                                            !string.IsNullOrWhiteSpace(address.StreetName) ? address.StreetName.Trim() + ", " : address.StreetName.Trim(),
                                                            !string.IsNullOrWhiteSpace(address.Postcode) ? address.Postcode.Trim().ToUpper() : address.Postcode).Trim();
                                person.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                                person.AuditUserId = CurrentUser.UserID;
                                personService.Save(person);
                            }

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.GetBaseException().Message;
                    ModelState.AddModelError("Error!", message);
                    return false;
                }
            }
            return true;
        }
        #endregion
        protected override void Dispose(bool disposing)
        {

            if (foodbankService != null)
            {
                foodbankService.Dispose();
                foodbankService = null;
            }
            if (userService != null)
            {
                userService.Dispose();
                //userService = null;
            }
            if (quickDonorGiftService != null)
            {
                quickDonorGiftService.Dispose();
                //quickDonorGiftService = null;
            }

            if (roleService != null)
            {
                roleService.Dispose();
                // roleService = null;
            }
            if (forgotPasswordService != null)
            {
                forgotPasswordService.Dispose();
                forgotPasswordService = null;
            }
            if (centralofficeService != null)
            {
                centralofficeService.Dispose();
                centralofficeService = null;
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
            if (personService != null)
            {
                personService.Dispose();
                personService = null;
            }
            if (countryService != null)
            {
                countryService.Dispose();
                //  countryService = null;
            }

            if (volunteerService != null)
            {
                volunteerService.Dispose();
                // volunteerService = null;
            }
            if (contactService != null)
            {
                contactService.Dispose();
                // contactService = null;
            }
            if (ReferralService != null)
            {
                ReferralService.Dispose();
                //  ReferralService = null;
            }
            if (addressService != null)
            {
                addressService.Dispose();
                //addressService = null;
            }
            base.Dispose(disposing);
        }
    }
}
