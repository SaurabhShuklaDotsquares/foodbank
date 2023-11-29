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
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class FoodbankReferrerController : BaseController
    {
        private readonly IMyReferralService referrerService;
        private readonly ICountryService countryService;
        private readonly IUserService userService;
        private readonly IQuickDonorGiftService quickDonorGiftService;
        private readonly IRoleService roleService;
        private readonly IFoodbankService foodbankService;
        private IGrantorService grantorService;
        private readonly IFamilyService familyService;
        private readonly IAllergiesService allergyService;
        private IBranchService branchService;
        private ICharityService charityService;
        public FoodbankReferrerController(IMyReferralService _referrerService, ICountryService _countryService, 
            IUserService _userService, IQuickDonorGiftService _quickDonorGiftService, IRoleService _roleService,
            IFoodbankService _foodbankService, IMyReferralService _ReferralService, IGrantorService _grantorService, IFamilyService _familyService, IAllergiesService _allergyService, ICharityService _charityService,
           IBranchService _branchService )
        {
            referrerService = _referrerService;
            countryService = _countryService;
            userService = _userService;
            quickDonorGiftService = _quickDonorGiftService;
            roleService = _roleService;
            foodbankService = _foodbankService;
            charityService = _charityService;
            grantorService = _grantorService; familyService = _familyService;
            allergyService = _allergyService;
            this.branchService = _branchService;
        }
        public IActionResult Index()
        {
            int organisationId =  CurrentUser.OrganisationID;
            ViewBag.OrganisationId = organisationId;
            if (organisationId > 0)
            {
                var Branches = Enumerable.Empty<SelectListItem>().ToList();
                var Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, organisationId, CurrentUser.UserID, true, true, 0).Select(c => new SelectListItem
                {
                    Text = c.CharityName.AddCharityPrefix(c.Prefix),
                    Value = c.CharityId.ToString()
                }).ToList();
                Branches.Insert(0, new SelectListItem("Select Branch", ""));

                Charities.Insert(0, new SelectListItem("Select Charity", ""));
                ViewBag.Branches = Branches;
                ViewBag.Charities = Charities;
            }
            return View();
        }
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
        [HttpPost]
        public IActionResult ReferrerList(DataTableServerSide model, int CharityId)
        {
            KeyValuePair<int, List<FoodbankReferrerDto>> referrerlist = new KeyValuePair<int, List<FoodbankReferrerDto>>();
            referrerlist = referrerService.GetReferrer(model, CurrentUser.FoodbankId, CharityId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = referrerlist.Key,
                recordsFiltered = referrerlist.Key,
                data = referrerlist.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    c.UserName,
                    c.Profession,
                    c.ContactNumber,
                   ((ReferrersStatus)(int)c.Status).GetDescription(),
                    "<a title='View'  href=" + Url.Action("ViewReferrer", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/eye-icon.png' alt='' /></a>" +
                    "<a title='Edit'  href=" + Url.Action("EditReferrer", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/edit-icon.png' alt='' /></a>" +
                  
                    "<a  data-toggle='modal' data-target='#modal-delete-Referrer' title='Delete' href=" + Url.Action("Delete", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"+
                    (c.Status==(int)(ReferrersStatus.Pending)?     "<a  data-toggle='modal' data-target='#modal-accept-Referrer' title='Accept'  href=" + Url.Action("Accept", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src = '/Content/images/accept-icon.png' alt='' /></a>"+
                    "<a  data-toggle='modal' data-target='#modal-reject-Referrer' title='Reject' href=" + Url.Action("Reject", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/block-icon.png' alt='' /></a>"
                    //+"<a  data-toggle='modal' data-target='#modal-postpone-Referrer' title='Postpone' href=" + Url.Action("Postpone", "FoodbankReferrer", new { Id = c.Id})+" class='view_btn'><img src='/Content/images/arrow-down.png' alt='' /></a>"
                    :"")

                    ,
                   })
            });
        }
        [HttpPost]
        public IActionResult ReferralList(DataTableServerSide model, int CharityId, int BranchId)
        {
            KeyValuePair<int, List<MyReferralsDto>> referrerlist = new KeyValuePair<int, List<MyReferralsDto>>();
            referrerlist = referrerService.GetMyReferralByFoodbank(model, CurrentUser.FoodbankId, CharityId, BranchId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = referrerlist.Key,
                recordsFiltered = referrerlist.Key,
                data = referrerlist.Value.Select((c, index) => new List<object> {
                    c.Id,
                     model.start+index+1,
                    c.ReferralDate.ToString("dd/MM/yyyy"),
                    c.FamilyName,
                    c.Mobile,
                    ((ReferrersStatus)(int)c.Status).GetDescription()
                   ,
                    "<a title='View'  href=" + Url.Action("ViewMyReferrals", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/eye-icon.png' alt='' /></a>" +
                    "<a title='Edit'  href=" + Url.Action("EditReferrals", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/edit-icon.png' alt='' /></a>" +
                    "<a  data-toggle='modal' data-target='#modal-delete-Referral' title='Delete' href=" + Url.Action("DeleteReferral", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"+
                      (c.Status==(int)(ReferrersStatus.Pending)?     "<a  data-toggle='modal' data-target='#modal-accept-Referral' title='Accept'  href=" + Url.Action("AcceptFamily", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src = '/Content/images/accept-icon.png' alt='' /></a>"+
                    "<a  data-toggle='modal' data-target='#modal-reject-Referral' title='Reject' href=" + Url.Action("RejectFamily", "FoodbankReferrer", new { Id = c.Id })+ " class='view_btn'><img src='/Content/images/block-icon.png' alt='' /></a>"+
                    "<a  data-toggle='modal' data-target='#modal-postpone-Referral' title='Postpone' href=" + Url.Action("PostponeFamily", "FoodbankReferrer", new { Id = c.Id})+" class='view_btn'><i  class='fa fa-solid fa-pause' style='color: #0874cc;'></i></a>"
                    :"")

                ,
                   })
            });
        }
        [HttpGet]
        public IActionResult AddReferrer()
        {
            ReferrerRegisterDto model = new ReferrerRegisterDto();
            List<SelectListItem> professionsList = new List<SelectListItem>();

            professionsList = referrerService.GetReferrerType().Select(c => new SelectListItem
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
            var Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.OrganisationID, CurrentUser.UserID, true, true, 0).Select(c => new SelectListItem
            {
                Text = c.CharityName.AddCharityPrefix(c.Prefix),
                Value = c.CharityId.ToString()
            }).ToList();
            Charities.Insert(0, new SelectListItem("Select Charity", ""));


            ViewBag.Charities = Charities;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddReferrer(ReferrerRegisterDto model)
        {
            if(model.OrganisationName == null)
            {
                ModelState.Remove("OrganisationName");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    List<SelectListItem> professionsList = new List<SelectListItem>();

                    professionsList = referrerService.GetReferrerType().Select(c => new SelectListItem
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

                    ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.OrganisationID, CurrentUser.UserID, true, true, 0).Select(c => new SelectListItem
                    {
                        Text = c.CharityName.AddCharityPrefix(c.Prefix),
                        Value = c.CharityId.ToString()
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
                    Referrers referrers = new Referrers();

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
                    referrers.Address = fbaddress;
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
                    referrers.Contact = fbcontact;
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
                    user.Ip = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    user.UserRole = new UserRole();
                    user.UserRole.Role = roleService.GetRoleByName("Admin");
                    user.AuditUserId = CurrentUser.UserID;
                    user.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    user.IsFoodbank = true;
                    user.CentralOfficeId = CurrentUser.OrganisationID;
                    user.CharityId = model.CharityID;
                    referrers.User = user;
                    /////

                    ///// Referres save
                    referrers.Name = model.FirstName + " " + model.LastName;
                    referrers.ReffToken = GeneraterToken();
                    referrers.ServiceDescription = model.Profession;
                    referrers.RefTypeId = model.ProfessionId;
                    //referrers.DefaultParcelType = 1;
                    referrers.IsVoucher = true;
                    referrers.Active = true;
                    referrers.IsStatus = (int)ReferrersStatus.Pending; 
                    referrers.PostponeDate = DateTime.Now;
                    referrers.FoodbankId = CurrentUser.FoodbankId;

                    if (referrerService.Save(referrers))
                    {
                        ShowSuccessMessage("Success!", "Referrer Save Successfully.", false);
                        return RedirectToAction("AddReferrer", "FoodbankReferrer");
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Some Error.", false);
                        return View(model);
                    }

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return View(model);
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return View(model);
            }
        }

        public string GeneraterToken(int? id = null)
        {
        Checked:
            var token =Extensions.RandomString(8);
            if (!string.IsNullOrWhiteSpace(token))
            {
                var isExist = referrerService.CheckToken(token);
                if (isExist)
                {
                    goto Checked;
                }
            }
            return token;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this Referrer ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Referrer " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }       
        [HttpPost]
        public string Delete(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var Referrers = referrerService.GetReferrerById(id);
                var myrederrel = referrerService.GetMyReferralByReferrerId(id);
                if (myrederrel==0)
                {
                    var user = userService.GetUser(Referrers.UserId);
                    user.Active = false;
                    userService.Save(user, 0, false);
                    Referrers.Active = false;
                    referrerService.UpdateReferres(Referrers);
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
        [HttpGet]
        public IActionResult Accept(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to Accept this Referrer ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Accept Referrer " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string Accept(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var Referrers = referrerService.GetReferrerById(id);  
                if (Referrers !=null)
                {
                    Referrers.IsStatus = (int)ReferrersStatus.Accept;
                    referrerService.UpdateReferres(Referrers);
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
        [HttpGet]
        public IActionResult Reject(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to Reject this Referrer ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Reject Referrer " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string Reject(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var Referrers = referrerService.GetReferrerById(id);
                if (Referrers != null)
                {
                    Referrers.IsStatus = (int)ReferrersStatus.Reject;
                    referrerService.UpdateReferres(Referrers);
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
        [HttpGet]
        public IActionResult Postpone(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to Postpone this Referrer ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Postpone Referrer " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string Postpone(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var Referrers = referrerService.GetReferrerById(id);
                if (Referrers != null)
                {
                    Referrers.IsStatus = (int)ReferrersStatus.Postpone;
                    Referrers.PostponeDate = DateTime.Now.AddDays(1);
                    referrerService.UpdateReferres(Referrers);
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
        [HttpGet]
        public IActionResult ViewReferrer(int id)
        {
            ReferrerRegisterDto model = new ReferrerRegisterDto();
            List<SelectListItem> professionsList = new List<SelectListItem>();

            professionsList = referrerService.GetReferrerType().Select(c => new SelectListItem
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

            var referrer = referrerService.GetReferrerById(id);
            model = MyReferralDtoMapper.MyReferrerEditMap(referrer);
            return View(model);
        }
        [HttpGet]
        public IActionResult EditReferrer(int id)
        {
            ReferrerRegisterDto model = new ReferrerRegisterDto();
            List<SelectListItem> professionsList = new List<SelectListItem>();

            professionsList = referrerService.GetReferrerType().Select(c => new SelectListItem
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
            var referrer = referrerService.GetReferrerById(id);
            model = MyReferralDtoMapper.MyReferrerEditMap(referrer);

            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteReferral(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this Referrer ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Referrer " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string DeleteReferral(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var myfamily = familyService.GetFamilyDetails(id);
               
                if (myfamily!=null)
                {
                    myfamily.Active = false;
                    familyService.UpdateFamily(myfamily);
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
        public IActionResult EditReferrer(ReferrerRegisterDto model)
        {
            if (model.OrganisationName == null)
            {
                ModelState.Remove("OrganisationName");
            }
            if (!model.IsChangePassword)
            {
                ModelState.Remove("EditPassword");
                ModelState.Remove("PasswordQuestion");
                ModelState.Remove("PasswordAnswer");
                ModelState.Remove("ConfirmPassword");
            }
            List<SelectListItem> professionsList = new List<SelectListItem>();

            professionsList = referrerService.GetReferrerType().Select(c => new SelectListItem
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

            
            if (ModelState.IsValid)
            {
                try
                {
                    var referrers = referrerService.GetReferrerById(model.Id); 
                   
                    ///// Address Save
                  
                    referrers.Address.HouseName = model.HouseName;
                    referrers.Address.Street = model.StreetName;
                    referrers.Address.HouseNumber = model.HouseNumber;
                    referrers.Address.City = model.City;
                    referrers.Address.Postcode = model.PostCode;
                    referrers.Address.CountryId = model.CountryID;
                    referrers.Address.CountryName = "";
                    referrers.Address.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                    
                    /////
                    ///// Contact Save
                  
                    referrers.Contact.OrganisationName = model.OrganisationName;
                    referrers.Contact.ForeName = model.FirstName;
                    referrers.Contact.Surname = model.LastName;
                    referrers.Contact.Mobile = model.ContactNumber;
                    referrers.Contact.Email = model.Email;
                    referrers.Contact.AddedDate = DateTime.Now;
                    referrers.Contact.ModifiedDate = DateTime.Now;
             
                 

                    ///// Referres save
                    referrers.Name = model.FirstName + " " + model.LastName;
                    referrers.ReffToken = GeneraterToken();
                    referrers.ServiceDescription = model.Profession;
                    referrers.RefTypeId = model.ProfessionId;
                    referrers.Id = model.Id;
                  
                    if (referrerService.Save(referrers))
                    {
                        if (model.IsChangePassword)
                        {
                            //Upodate User login details
                            //usrDetail.UserName = model.UserName.Trim();
                            var usrDetail = referrers.User;
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
                        ShowSuccessMessage("Success!", "Referrer update successfully.", false);
                        return RedirectToAction("EditReferrer", "FoodbankReferrer");
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Some Error.", false);
                        return View(model);
                    }

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return View(model);
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return View(model);
            }
        }
        public IActionResult EditReferrals(int ID)
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();
            var res = familyService.GetFamilyDetails(ID);
            ViewBag.resaddress = familyService.GetFamilyAddessDetails(res.FamilyAddress.FirstOrDefault().AddressId);
            if (res.FamilyMember.Count > 0)
            {
                foreach (var item in res.FamilyMember)
                {
                    item.FamilyMemberAllergy = familyService.GetFamilyMemberAllergyDetails(item.Id).Where(x => x.FamilyMemberId == item.Id).ToList();
                }
            }
            var model = FamilyMapper.EditFamilyMapper(res);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditReferrals(EditFamilyDto familydto)
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();
           
            var family = familyService.GetFamilyDetails(familydto.Id);
            family.FamilyName = familydto.FamilyName;
            family.ModifiedDate = System.DateTime.Now;
            family.Email = familydto.Email;
            family.Contactno = familydto.Contactno;
            family.TotalFamily = familydto.TotalFamily;
            family.TotalAdults = familydto.TotalAdults;
            family.TotalChild = familydto.TotalChild;

            Fbaddress fa = familyService.GetFamilyAddessDetails(familydto.Addressid??0);
            fa.HouseName = familydto.HouseName ?? "";
            fa.HouseNumber = familydto.HouseNumber ?? "";
            fa.District = familydto.District ?? "";
            fa.Street = familydto.StreetName ?? "";
            fa.City = (familydto.City == null ? "" : familydto.City);
            fa.CountryId = familydto.CountryID ?? 0;
            fa.Postcode = familydto.PostCode;
            familyService.SaveFbAddress(fa);


            var adultarray = familydto.subfamilyisadult2.Split(',');
            var SubFamilyAllergryarry = familydto.SubFamilyAllergries.Split(',');
            for (int i = 0; i < familydto.subfamilyname.Count; i++)
            {
                FamilyMember fnsub; 
                if (familydto.subfamilynameIds.Count-1 >= i)
                {
                    fnsub = familyService.GetFamilyMember(Convert.ToInt32(familydto.subfamilynameIds[i]));
                    foreach (var item in fnsub.FamilyMemberAllergy.ToList())
                    {
                        familyService.DeleteFamilyMemberAllergy(item.Id);
                    }
                }
                else
                {
                    fnsub = new FamilyMember();
                }
              
                fnsub.FamilyId = family.Id;
                fnsub.ForeName = familydto.subfamilyname[i];
                familydto.subfamilydob[i] = familydto.subfamilydob[i].Replace("-", "/");
                fnsub.Dob = DateTime.ParseExact(familydto.subfamilydob[i], "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);
                fnsub.IsAdult = Convert.ToBoolean(adultarray[i]);
                fnsub.IsPrimaryContact = true;
                var SubFamilymulitple = SubFamilyAllergryarry[i].Split('%');
                List<FamilyMemberAllergy> fmAllergyList = new List<FamilyMemberAllergy>();
                for (int j = 0; j < SubFamilymulitple.Length; j++)
                {
                    if (SubFamilymulitple[j].Length > 0)
                    {
                        FamilyMemberAllergy falry = new FamilyMemberAllergy();
                        falry.AllergyId = Convert.ToInt32(SubFamilymulitple[j]);
                        fmAllergyList.Add(falry);
                    }
                }
                fnsub.FamilyMemberAllergy = fmAllergyList;
                familyService.Savesubfamily(fnsub);
            }

            familyService.UpdateFamily(family);
            ShowSuccessMessage("Success!", "Family has been updated successfully.", false);
            return RedirectToAction("EditReferrals", "FoodbankReferrer", new { });

        }
        public IActionResult ViewMyReferrals(int ID)
        {
            var res = familyService.GetFamilyDetails(ID);
            ViewBag.resaddress = familyService.GetFamilyAddessDetails(res.FamilyAddress.FirstOrDefault().AddressId);

            if (res.FamilyMember.Count > 0)
            {
                foreach (var item in res.FamilyMember)
                {
                    item.FamilyMemberAllergy = familyService.GetFamilyMemberAllergyDetails(item.Id).Where(x => x.FamilyMemberId == item.Id).ToList();
                }
            }
            return View(res);
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
        [NonAction]
        public void BindAlleriesViewBag(int? id = null)
        {
            var allergy = allergyService.GetAllergies().Select(c => new SelectListItem
            {
                Text = c.Name.ToTitle(),
                Value = c.Id.ToString(),

            }).ToList();
         
            ViewBag.AllergyList = allergy;
        }
        [HttpGet]
        public IActionResult AcceptFamily(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to Accept this Family ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Accept Family " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string AcceptFamily(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var FoodbankSetting = foodbankService.GetFoodbankSettingByFoodbankID(CurrentUser.FoodbankId);
                var dailyapprovecount = familyService.CountDailyReferrelLimitByFoodbankId(CurrentUser.FoodbankId);
                if (  dailyapprovecount >= FoodbankSetting.DailyReferralLimit )
                {
                    return " Daily referrals accept limit excced";
                }
                var myfamily = familyService.GetFamilyDetails(id);
                if (myfamily != null)
                {
                    myfamily.Confirmed = true;
                    myfamily.ConfirmedById = CurrentUser.FoodbankId;
                    myfamily.AcceptDate = System.DateTime.Now;
                    familyService.UpdateFamily(myfamily);
                }
                message = "Success";
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "You can't Accept this Family  because it something.";
                // ShowErrorMessage("Error!", message, false);
            }

            // return CreateModelStateErrors();
            return message;
        }
        [HttpGet]
        public IActionResult RejectFamily(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to Reject this Family ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Reject Family " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string RejectFamily(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var myfamily = familyService.GetFamilyDetails(id);

                if (myfamily != null)
                {
                    myfamily.Confirmed = false;
                    myfamily.ConfirmedById = CurrentUser.UserID;
                    familyService.UpdateFamily(myfamily);
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
        [HttpGet]
        public IActionResult PostponeFamily(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to Postpone this Family ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Postpone Family " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string PostponeFamily(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var myfamily = familyService.GetFamilyDetails(id);
                if (myfamily != null)
                {

                    myfamily.PostponeDate = DateTime.Now.AddDays(1);
                    familyService.UpdateFamily(myfamily);
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
        [HttpGet]
        public string DeleteFamilyMember(int id)
        {
            string message;
            try
            {
                FamilyMember fnsub = familyService.GetFamilyMember(id);
                foreach (var item in fnsub.FamilyMemberAllergy.ToList())
                {
                    familyService.DeleteFamilyMemberAllergy(item.Id);
                }
                familyService.DeleteFamilyMember(id);
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
    }
}
