using AutoMapper;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto.Branch;
using FB.Dto.Foodbank;
using FB.ModalMapper;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class DashboardController : BaseController
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
        private readonly IStockService stockService;
        public DashboardController(IUserService _userService, IMenuService _menuService, IPersonService _personService, IBranchService _branchService, ICharityService _charityService,
            ICentralOfficeService _centralofficeService, IForgotPasswordService _forgotPasswordService, IRoleService _roleService, IMapper _mapper,
            IQuickDonorGiftService _quickDonorGiftService, ICountryService _countryService, IAddressService _addressService, IMyReferralService _ReferralService,
            IContactService _contactService, IVolunteerService _volunteerService, IFoodbankService _foodbankService, IStockService _stockService)
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
            stockService = _stockService;
        }

        public IActionResult Index()
        {
            FoodDashboardDto model = new FoodDashboardDto();

            List<SelectListItem> ddl_Month = new List<SelectListItem>();
            ddl_Month.Clear();
            foreach (var month in Enumerable.Range(0, 12).Select(i => DateTime.Now.AddMonths(-i).ToString("MMMM")).ToList())
            {
                ddl_Month.Add(new SelectListItem
                {
                    Text = month.ToString(),
                    Value = month.ToString()
                });
            }
            ViewBag.Months = ddl_Month.ToList();

            List<SelectListItem> ddl_Year = new List<SelectListItem>();
            int currentYear = DateTime.Today.Year;
            ddl_Year.Clear();
            for (int i = 10; i >= 0; i--)
            {
                int fy = currentYear - i;
                int fy1 = fy + 1;
                if (DateTime.Now.Date > Convert.ToDateTime(fy + "-03-31").Date)
                {
                    ddl_Year.Add(new SelectListItem
                    {
                        Text = fy.ToString(),
                        Value = fy.ToString()
                    });
                }
            }
            ViewBag.yearLoad = ddl_Year.OrderByDescending(x => x.Value).ToList();

            model.menuList = BindDynamicMenu();
            return View(model);
        }
        [HttpPost]
        public IActionResult GetParcelMonth(int year)
        {
            KeyValuePair<int, List<DashboardDto>> centralOffices = new KeyValuePair<int, List<DashboardDto>>();

            centralOffices = foodbankService.GetMyParcelMonth(year, CurrentUser.FoodbankId);

            List<SelectListItem> allMonthData = new List<SelectListItem>();
            string[] Data = new string[] { };
            string[] Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            foreach (var month in Labels)
            {
                int monthId = DateTime.ParseExact(month, "MMM", System.Globalization.CultureInfo.CurrentCulture).Month;
                allMonthData.Add(new SelectListItem
                {
                    Text = month,
                    Value = centralOffices.Value.Where(x => x.Date.Value.Month == Convert.ToInt32(monthId)).ToList().Count.ToString()
                });
            }
            Labels = allMonthData.Select(x => x.Text).ToArray();
            Data = allMonthData.Select(x => x.Value).ToArray();
            return Json(new
            {
                Labels,
                Data
            });
        }

        [HttpPost]
        public IActionResult GetAgeOfFamilyMember()
        {
            List<int> parcels = new List<int>();

            parcels = foodbankService.GetFamilyMemberMonth(DateTime.Now.Month, CurrentUser.FoodbankId);

            List<SelectListItem> allMonthData = new List<SelectListItem>();
            string[] Data = new string[] { };
            string[] Labels = new string[] { "0-15", "16-25", "26-40", "40-65", "65+" };
            foreach (var range in Labels)
            {
                allMonthData.Add(new SelectListItem
                {
                    Text = range,
                    Value = Convert.ToString(range == "0-15" ? parcels.Where(x => x <= 15).Count() :
                    (range == "16-25" ? parcels.Where(x => x >= 16 && x <= 25).Count() :
                    (range == "26-40" ? parcels.Where(x => x >= 26 && x <= 40).Count() :
                    (range == "40-65" ? parcels.Where(x => x >= 40 && x <= 65).Count() :
                    parcels.Where(x => x > 65).Count()))))
                });
            }
            Labels = allMonthData.Select(x => x.Text).ToArray();
            Data = allMonthData.Select(x => x.Value).ToArray();
            return Json(new
            {
                Labels,
                Data
            });
        }

        public IActionResult UpdateProfile()
        {
            BindCountriesViewBag();
            BindOrganisationViewBag();
            ViewBag.Branches = new List<SelectListItem>();
            var foodbank = foodbankService.GetFoodbankByUserId(CurrentUser.UserID);

            UpdateProfileDto model = new UpdateProfileDto();
            if (foodbank != null)
            {
                model.FoodBankId = foodbank.Id;
                model.FoodName = foodbank.Name;
                model.Email = foodbank.User.Email;
                model.ContactNumber = foodbank.User.PrimaryMobile;
                model.CentralOfficeID = foodbank.User.CentralOfficeId;
                model.CharityID = foodbank.User.CharityId;
                model.BranchID = foodbank.User.BranchId;

                model.CountryID = foodbank.Address != null ? foodbank.Address.CountryId : 248;
                model.CountryName = model.CountryID == 248 ? "United Kingdom" : string.Empty;

                model.AddressID = foodbank.AddressId != null ? foodbank.AddressId.Value : 0;

                if (foodbank.User != null)
                {
                    model.UserName = foodbank.User.UserName.Trim();
                    model.EditPassword = EncryptionUtils.Decrypt(foodbank.User.Password, foodbank.User.PasswordSalt);
                    model.PasswordQuestion = foodbank.User.PasswordQuestion;
                    model.PasswordAnswer = EncryptionUtils.Decrypt(foodbank.User.PasswordAnswer, foodbank.User.PasswordSalt);
                }
                if (foodbank.Address != null)
                {
                    model.PostCode = foodbank.Address.Postcode;
                    model.StreetName = foodbank.Address.Street;
                    model.HouseName = foodbank.Address.HouseName;
                    model.HouseNumber = foodbank.Address.HouseNumber;
                    model.City = foodbank.Address.City;
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UpdateProfileDto model)
        {
            try
            {
                Foodbank foodbank = foodbankService.GetFoodbankById(model.FoodBankId);
                if (foodbank != null)
                {
                    foodbank.Name = model.FoodName;
                    foodbank.User.PrimaryMobile = model.ContactNumber;

                    Fbaddress address = model.AddressID > 0 ? addressService.GetFBAddress(model.AddressID) : new Fbaddress();
                    address.HouseName = model.HouseName;
                    address.HouseNumber = model.HouseNumber;
                    address.Street = model.StreetName;
                    address.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                    address.City = model.City;
                    address.Postcode = model.PostCode == null ? model.OldPostCode : model.PostCode;
                    address.CountryId = model.CountryID.Value;
                    address.CountryName = model.CountryName;

                    foodbank.Address = address;
                    var result = foodbankService.Save(foodbank);

                    var usrDetail = foodbank.User;
                    if (result)
                    {
                        if (model.ChangePassword)
                        {
                            //Upodate User login details
                            string randonSalt = Common.GetRandomPasswordSalt();
                            usrDetail.Password = EncryptionUtils.HashPassword(model.EditPassword, randonSalt, DateTime.Now);
                            usrDetail.PasswordSalt = randonSalt;
                            usrDetail.PasswordQuestion = model.PasswordQuestion;
                            usrDetail.PasswordAnswer = model.PasswordAnswer;
                            userService.Save(usrDetail, CurrentUser.UserID, false);
                            //End
                        }

                        //Add/Update Address
                        //AddUpdateReferrerAddress(model);
                        //End

                        ShowSuccessMessage("Success!", "Profile has been updated successfully.", false);
                        return RedirectToAction("updateprofile", "dashboard");
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                        return RedirectToAction("updateprofile", "dashboard");
                    }
                }
                else
                {
                    ShowErrorMessage("Error!", "Record not found.", false);
                    return RedirectToAction("updateprofile", "dashboard");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                return RedirectToAction("updateprofile", "dashboard");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool AddUpdateReferrerAddress(UpdateProfileDto model)
        {
            try
            {

                Fbaddress address = model.AddressID > 0 ? addressService.GetFBAddress(model.AddressID) : new Fbaddress();
                address.HouseName = model.HouseName;
                address.HouseNumber = model.HouseNumber;
                address.Street = model.StreetName;
                address.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                address.City = model.City;
                address.Postcode = model.PostCode == null ? model.OldPostCode : model.PostCode;
                address.CountryId = model.CountryID.Value;
                address.CountryName = model.CountryName;
                addressService.Save(address, address.Id == 0);
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                ModelState.AddModelError("Error!", message);
                return false;
            }
            return true;
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
        public void BindCountriesViewBag(int? id = null)
        {
            ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
            {
                Text = c.CountryName.ToTitle(),
                Value = c.CountryId.ToString(),
                Selected = (id == null) ? (c.CountryName == Constants.DefaultCountry) : id == c.CountryId
            }).ToList();
        }

        [HttpPost]
        public IActionResult StockList(DataTableServerSide model)
        {
            KeyValuePair<int, List<StockDto>> stocklist = new KeyValuePair<int, List<StockDto>>();
            stocklist = stockService.GetStockListForDashboard(CurrentUser.FoodbankId);
            return Json(new
            {
                draw = model.draw,
                data = stocklist.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    c.FoodName,
                    c.TotalQuantity + c.Unit
                   })
            });
        }
        [HttpPost]
        public IActionResult DashboardCount()
        {

            DateTime startOfWeek = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek * -1).AddDays(1);
            DateTime lastOfWeek = startOfWeek.AddDays(6);
            var foodparcelscount = foodbankService.GetFoodParcelsCount(CurrentUser.FoodbankId, startOfWeek, lastOfWeek);
            var parcelsdeliveredcount = foodbankService.GetParcelsDeliveredCount(CurrentUser.FoodbankId);

            return Json(new { foodparcelscount, parcelsdeliveredcount });
        }

        public List<MenuDto> BindDynamicMenu()
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
            return menus;
        }
    }
}
