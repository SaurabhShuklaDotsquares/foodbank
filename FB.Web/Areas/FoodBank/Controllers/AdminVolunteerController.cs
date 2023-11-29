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
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class AdminVolunteerController : BaseController
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
        private readonly IFamilyService familyService;
        private readonly IRecipeService recipeService;
        private readonly IFoodService foodService;
        private readonly IFamilyParcelService familyParcelService;
        private readonly IParcelService parcelService;

        public AdminVolunteerController(IUserService _userService, IMenuService _menuService, IPersonService _personService, IBranchService _branchService, ICharityService _charityService,
            ICentralOfficeService _centralofficeService, IForgotPasswordService _forgotPasswordService, IRoleService _roleService, IMapper _mapper,
            IQuickDonorGiftService _quickDonorGiftService, ICountryService _countryService, IAddressService _addressService, IMyReferralService _ReferralService, IContactService _contactService,
            IVolunteerService _volunteerService, IFoodbankService _foodbankService, IFamilyService _familyService, IRecipeService _recipeService, IFoodService _foodService
            , IFamilyParcelService _familyParcelService, IParcelService _parcelService)
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
            familyService = _familyService;
            recipeService = _recipeService;
            foodService = _foodService;
            familyParcelService = _familyParcelService;
            parcelService = _parcelService;
        }

        public IActionResult Index()
        {
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
        [HttpGet]
        public IActionResult  VolunteerList()
        {
            int organisationId = CurrentUser.OrganisationID;
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
        [HttpPost]
        public IActionResult VolunteerList(DataTableServerSide model,int CharityId)
        {
            KeyValuePair<int, List<VolunteerDto>> volunteerlist = new KeyValuePair<int, List<VolunteerDto>>();
           volunteerlist = volunteerService.GetVolunteersByFoodbank(model, CurrentUser.FoodbankId, CharityId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = volunteerlist.Key,
                recordsFiltered = volunteerlist.Key,
                data = volunteerlist.Value.Select((c, index) => new List<object> {
                    c.VolunteerId,
                    model.start+index+1,
                    c.VolunteerName,
                    c.Mobile,
                    c.IndividualCouplename,
                    c.Packingordeliveryname,
                    c.AddedDate.ToString("dd/MM/yyyy"),
                     "<a href=" + Url.Action("VolunteerView", "AdminVolunteer", new { id = c.VolunteerId })
                    + " class='view_btn'><img src='/Content/images/eye-icon.png' alt='' /></a><a href=" + Url.Action("VolunteerEdit", "AdminVolunteer", new { id = c.VolunteerId })
                    + " class='view_btn'><img src='/Content/images/edit-icon.png' alt='' /></a><a  data-toggle='modal' data-target='#modal-delete-volunteer' href=" + Url.Action("Delete", "AdminVolunteer", new { id = c.VolunteerId })
                    + " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"
                    ,
                    c.VolunteerId
                   })
            }); ;
            return View();
        }
        [HttpPost]
        public IActionResult VolunteerDeliveryList(DataTableServerSide model,int CharityId)
        {
            KeyValuePair<int, List<VolunteerPackingAdminListDto>> volunteerlist = new KeyValuePair<int, List<VolunteerPackingAdminListDto>>();
            volunteerlist = volunteerService.GetPackingListByFoodbank(model, CurrentUser.FoodbankId, CharityId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = volunteerlist.Key,
                recordsFiltered = volunteerlist.Key,
                data = volunteerlist.Value.Select((c, index) => new List<object> {
                    c.VolunteerId,
                    model.start+index+1,
                    c.VolunteerName,
                    c.Mobile,
                    c.IndividualCouplename,
                    c.Packingordeliveryname,
                    c.PendingDeliveries,
                     "<a href=" + Url.Action("VolunteerParcelView", "AdminVolunteer", new { id = c.VolunteerId })
                    + " class='view_btn'><img src='/Content/images/eye-icon.png' alt='' /></a><a href=" + Url.Action("VolunteerEdit", "AdminVolunteer", new { id = c.VolunteerId })
                    + " class='view_btn'><img src='/Content/images/edit-icon.png' alt='' /></a><a  data-toggle='modal' data-target='#modal-delete-delivilery' href=" + Url.Action("DeleteDelivery", "AdminVolunteer", new { id = c.VolunteerId })
                    + " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"
                    ,
                    c.VolunteerId
                   })
            }); ;
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this volunteer?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete volunteer" },
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
                var person = volunteerService.GetVolunteerById(id);
                if (person != null)
                {
                    var user = userService.GetUser(person.UserId ?? 0);
                    user.Active = false;
                    userService.Save(user, 0, false);
                    person.Active = false;
                    volunteerService.Save(person,false);
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
        public IActionResult DeleteDelivery(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this volunteer?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete volunteer" },
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
        public string DeleteDelivery(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var person = volunteerService.GetVolunteerById(id);
                if (person != null)
                {
                    var user = userService.GetUser(person.UserId ?? 0);
                    user.Active = false;
                    userService.Save(user, 0, false);
                    person.Active = false;
                    volunteerService.Save(person, false);
                }
                message = "Success";
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
        public IActionResult VolunteerEdit(int Id)
        {
            VolunteerAdminDto model = new VolunteerAdminDto();
            var volunteer = volunteerService.GetVolunteerById(Id);

            if (volunteer != null)
            {
                model.VolunteerName = $"{volunteer.Contact.ForeName} {volunteer.Contact.Surname}";
                model.WorkType = volunteer.Packingordelivery.Value;
                model.IndividualCouple = volunteer.IndividualCouple != null ? volunteer.IndividualCouple.Value : 1;
                model.HowCanYouHelp = volunteer.Howelsecanyouhelp;
                model.Mobile = volunteer.Contact == null ? "": volunteer.Contact.Mobile;
                model.ContactId = volunteer.ContactId;
                model.UserName = volunteer.User.UserName;
                model.UserId = volunteer.User.UserId;
                model.VolunteerId = volunteer.Id;
                model.IsDBScheck = volunteer.IsDbscheck;
                model.FoodbankId = volunteer.FoodbankId ?? 0;
                model.UserId = volunteer.UserId ?? 0 ;
                model.VolunteerSkill = volunteer.VolunteerSkill.ToList();
                model.DocPath = volunteer.DocPath;
                #region Map Availability
                var volunteerAvailability = volunteerService.GetAvailability(volunteer.Id);
                if (volunteerAvailability != null)
                {
                    model.Availability = new AvailabilityDto();
                    model.Availability.FromDate = volunteerAvailability.FormDate.ToFormatCustomString();
                    model.Availability.ToDate = volunteerAvailability.ToDate.ToFormatCustomString();
                    model.Availability.TimeForm = Convert.ToString(volunteerAvailability.TimeForm);
                    model.Availability.TimeTo = Convert.ToString(volunteerAvailability.TimeTo);
                    model.Availability.AllDay = volunteerAvailability.AllDay;
                    model.Availability.Pattern = volunteerAvailability.Pattern;
                    model.Availability.AvailabilityId = volunteerAvailability.Id;

                    if (volunteerAvailability.Id != 0)
                    {
                        if (volunteerAvailability.ToDate.HasValue)
                        {
                            model.Availability.UnavailabilityUntilType = 2;
                            model.Availability.ToDate = volunteerAvailability.ToDate.ToFormatCustomString();
                        }
                        if (volunteerAvailability.AllDay == false)
                        {
                            model.Availability.UnavailabilityTimeType = 2;
                            model.Availability.FromDate = volunteerAvailability.FormDate.ToFormatCustomString();
                            model.Availability.ToDate = volunteerAvailability.TimeTo.ToString();
                        }
                        //unavailabilityDto.AllDayCheckbox = entity.Checkbox ?? false;
                        //unavailabilityDto.Pattern = entity.Pattern;
                        if (volunteerAvailability.Pattern.IsNotNullAndNotEmpty())
                        {
                            if (volunteerAvailability.Pattern.StartsWith("D"))
                            {
                                model.Availability.FrequencyType = 1;
                                if (volunteerAvailability.Pattern.StartsWith("D1"))
                                {
                                    model.Availability.DailyType = 1;

                                    Int32 dailydays = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Split("D1")[1], out dailydays) && dailydays > 0)
                                    {
                                        model.Availability.DailyDays = dailydays;
                                    }
                                }

                                if (volunteerAvailability.Pattern.StartsWith("D2"))
                                {
                                    model.Availability.DailyType = 2;
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("W"))
                            {
                                model.Availability.FrequencyType = 2;
                                if (volunteerAvailability.Pattern[1] == 'Y')
                                    model.Availability.IsWeeklyMonday = true;

                                if (volunteerAvailability.Pattern[2] == 'Y')
                                    model.Availability.IsWeeklyTuesday = true;

                                if (volunteerAvailability.Pattern[3] == 'Y')
                                    model.Availability.IsWeeklyWednesday = true;

                                if (volunteerAvailability.Pattern[4] == 'Y')
                                    model.Availability.IsWeeklyThursday = true;

                                if (volunteerAvailability.Pattern[5] == 'Y')
                                    model.Availability.IsWeeklyFriday = true;

                                if (volunteerAvailability.Pattern[6] == 'Y')
                                    model.Availability.IsWeeklySaturday = true;

                                if (volunteerAvailability.Pattern[7] == 'Y')
                                    model.Availability.IsWeeklySunday = true;

                                Int32 weekdays = 0;
                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(volunteerAvailability.Pattern.Length - 1), out weekdays) && weekdays > 0)
                                {
                                    model.Availability.WeeklyDays = weekdays;
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("M"))
                            {
                                model.Availability.FrequencyType = 3;

                                if (volunteerAvailability.Pattern.StartsWith("M1"))
                                {
                                    model.Availability.MonthlyType = 1;

                                    Int32 monthlydays = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(2, 2), out monthlydays) && monthlydays > 0)
                                    {
                                        model.Availability.MonthlyDays = monthlydays;
                                    }

                                    Int32 monthlymonths = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4), out monthlymonths) && monthlymonths > 0)
                                    {
                                        model.Availability.MonthlyMonths = monthlymonths;
                                    }
                                }

                                if (volunteerAvailability.Pattern.StartsWith("M2"))
                                {
                                    model.Availability.MonthlyType = 2;

                                    Int32 monthlyweek = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(2, 1), out monthlyweek))
                                    {

                                        model.Availability.MonthlyWeek = monthlyweek;
                                    }

                                    Int32 monthlyweekday = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(3, 1), out monthlyweekday))
                                    {
                                        model.Availability.MonthlyWeekDay = monthlyweekday;
                                    }

                                    Int32 monthlyweekmonth = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4), out monthlyweekmonth) && monthlyweekmonth > 0)
                                    {
                                        model.Availability.MonthlyWeekMonth = monthlyweekmonth;
                                    }
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("A"))
                            {
                                model.Availability.FrequencyType = 4;

                                Int32 annualyears = 0;
                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(1, 2), out annualyears) && annualyears > 0)
                                {
                                    model.Availability.AnnualYears = annualyears;
                                }

                                Int32 annualtype = 0;

                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(3, 1), out annualtype))
                                {
                                    if (annualtype == 1)
                                    {
                                        model.Availability.AnnualType = 1;

                                        Int32 annualmonth = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4, 2), out annualmonth) && annualmonth > 0)
                                        {
                                            model.Availability.AnnualMonth = annualmonth;
                                        }

                                        Int32 annualmonthday = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(6), out annualmonthday) && annualmonthday > 0)
                                        {
                                            model.Availability.AnnualMonthDay = annualmonthday;
                                        }
                                    }

                                    if (annualtype == 2)
                                    {
                                        model.Availability.AnnualType = 2;

                                        Int32 annualweek = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4, 1), out annualweek) && annualweek > 0)
                                        {
                                            model.Availability.AnnualWeek = annualweek;
                                        }

                                        Int32 annualweekday = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(5, 1), out annualweekday))
                                        {
                                            model.Availability.AnnualWeekDay = annualweekday;

                                        }

                                        Int32 annualmonthweek = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(6), out annualmonthweek) && annualmonthweek > 0)
                                        {
                                            model.Availability.AnnualMonthWeek = annualmonthweek;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            model.Availability.FrequencyType = 1;
                            model.Availability.DailyDays = 1;
                        }

                    }
                }
                #endregion

                #region Map Unavailability
                var volunteerUnavailability = volunteerService.GetUnavailability(volunteer.Id);
                if (volunteerUnavailability != null)
                {
                    model.Unavailability = new UnavailabilityDto();
                    model.Unavailability.FromDate = volunteerUnavailability.FormDate.ToFormatCustomString();
                    model.Unavailability.ToDate = volunteerUnavailability.ToDate.ToFormatCustomString();
                    model.Unavailability.TimeForm = Convert.ToString(volunteerUnavailability.TimeForm);
                    model.Unavailability.TimeTo = Convert.ToString(volunteerUnavailability.TimeTo);
                    model.Unavailability.AllDay = volunteerUnavailability.AllDay;
                    model.Unavailability.Pattern = volunteerUnavailability.Pattern;
                    model.Unavailability.UnavailabilityId = volunteerUnavailability.Id;

                    if (volunteerUnavailability.Id != 0)
                    {
                        if (volunteerUnavailability.ToDate.HasValue)
                        {
                            model.Unavailability.UnavailabilityTimeType = 2;
                            model.Unavailability.ToDate = volunteerUnavailability.ToDate.ToFormatCustomString();
                        }
                        if (volunteerUnavailability.AllDay == false)
                        {
                            model.Unavailability.UnavailabilityTimeType = 2;
                            model.Unavailability.FromDate = volunteerUnavailability.FormDate.ToFormatCustomString();
                            model.Unavailability.ToDate = volunteerUnavailability.TimeTo.ToString();
                        }
                        //unavailabilityDto.AllDayCheckbox = entity.Checkbox ?? false;
                        //unavailabilityDto.Pattern = entity.Pattern;
                        if (volunteerUnavailability.Pattern.IsNotNullAndNotEmpty())
                        {
                            if (volunteerUnavailability.Pattern.StartsWith("D"))
                            {
                                model.Unavailability.FrequencyType = 1;
                                if (volunteerUnavailability.Pattern.StartsWith("D1"))
                                {
                                    model.Unavailability.DailyType = 1;

                                    Int32 dailydays = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Split("D1")[1], out dailydays) && dailydays > 0)
                                    {
                                        model.Unavailability.DailyDays = dailydays;
                                    }
                                }

                                if (volunteerUnavailability.Pattern.StartsWith("D2"))
                                {
                                    model.Unavailability.DailyType = 2;
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("W"))
                            {
                                model.Unavailability.FrequencyType = 2;
                                if (volunteerUnavailability.Pattern[1] == 'Y')
                                    model.Unavailability.IsWeeklyMonday = true;

                                if (volunteerUnavailability.Pattern[2] == 'Y')
                                    model.Unavailability.IsWeeklyTuesday = true;

                                if (volunteerUnavailability.Pattern[3] == 'Y')
                                    model.Unavailability.IsWeeklyWednesday = true;

                                if (volunteerUnavailability.Pattern[4] == 'Y')
                                    model.Unavailability.IsWeeklyThursday = true;

                                if (volunteerUnavailability.Pattern[5] == 'Y')
                                    model.Unavailability.IsWeeklyFriday = true;

                                if (volunteerUnavailability.Pattern[6] == 'Y')
                                    model.Unavailability.IsWeeklySaturday = true;

                                if (volunteerUnavailability.Pattern[7] == 'Y')
                                    model.Unavailability.IsWeeklySunday = true;

                                Int32 weekdays = 0;
                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(volunteerUnavailability.Pattern.Length - 1), out weekdays) && weekdays > 0)
                                {
                                    model.Unavailability.WeeklyDays = weekdays;
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("M"))
                            {
                                model.Unavailability.FrequencyType = 3;

                                if (volunteerUnavailability.Pattern.StartsWith("M1"))
                                {
                                    model.Unavailability.MonthlyType = 1;

                                    Int32 monthlydays = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(2, 2), out monthlydays) && monthlydays > 0)
                                    {
                                        model.Unavailability.MonthlyDays = monthlydays;
                                    }

                                    Int32 monthlymonths = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4), out monthlymonths) && monthlymonths > 0)
                                    {
                                        model.Unavailability.MonthlyMonths = monthlymonths;
                                    }
                                }

                                if (volunteerUnavailability.Pattern.StartsWith("M2"))
                                {
                                    model.Unavailability.MonthlyType = 2;

                                    Int32 monthlyweek = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(2, 1), out monthlyweek))
                                    {

                                        model.Unavailability.MonthlyWeek = monthlyweek;
                                    }

                                    Int32 monthlyweekday = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(3, 1), out monthlyweekday))
                                    {
                                        model.Unavailability.MonthlyWeekDay = monthlyweekday;
                                    }

                                    Int32 monthlyweekmonth = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4), out monthlyweekmonth) && monthlyweekmonth > 0)
                                    {
                                        model.Unavailability.MonthlyWeekMonth = monthlyweekmonth;
                                    }
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("A"))
                            {
                                model.Unavailability.FrequencyType = 4;

                                Int32 annualyears = 0;
                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(1, 2), out annualyears) && annualyears > 0)
                                {
                                    model.Unavailability.AnnualYears = annualyears;
                                }

                                Int32 annualtype = 0;

                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(3, 1), out annualtype))
                                {
                                    if (annualtype == 1)
                                    {
                                        model.Unavailability.AnnualType = 1;

                                        Int32 annualmonth = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4, 2), out annualmonth) && annualmonth > 0)
                                        {
                                            model.Unavailability.AnnualMonth = annualmonth;
                                        }

                                        Int32 annualmonthday = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(6), out annualmonthday) && annualmonthday > 0)
                                        {
                                            model.Unavailability.AnnualMonthDay = annualmonthday;
                                        }
                                    }

                                    if (annualtype == 2)
                                    {
                                        model.Unavailability.AnnualType = 2;

                                        Int32 annualweek = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4, 1), out annualweek) && annualweek > 0)
                                        {
                                            model.Unavailability.AnnualWeek = annualweek;
                                        }

                                        Int32 annualweekday = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(5, 1), out annualweekday))
                                        {
                                            model.Unavailability.AnnualWeekDay = annualweekday;

                                        }

                                        Int32 annualmonthweek = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(6), out annualmonthweek) && annualmonthweek > 0)
                                        {
                                            model.Unavailability.AnnualMonthWeek = annualmonthweek;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            model.Unavailability.FrequencyType = 1;
                            model.Unavailability.DailyDays = 1;
                        }

                    }
                }
                #endregion
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

            ViewBag.Skiils = volunteerService.GetSkillByFoodbank(volunteer.FoodbankId).Select(c => new SelectListItem
            {
                Text = c.SkillName.ToTitle(),
                Value = c.Id.ToString(),
                Selected = ((model.VolunteerSkill.Select(x => Convert.ToInt32(x.SkillId)).ToArray()).Contains(c.Id)) ? true : false

            }).ToList();
             return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VolunteerEdit(VolunteerAdminDto model)
        {

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

            ViewBag.Skiils = volunteerService.GetSkillByFoodbank(model.FoodbankId).Select(c => new SelectListItem
            {
                Text = c.SkillName.ToTitle(),
                Value = c.Id.ToString(),
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
                    Fbcontact contact = contactService.GetContactById(model.ContactId);
                    Volunteer entityvolunteer = volunteerService.GetVolunteerById(model.VolunteerId);
                    // upload profile pic
                    if (Request.Form.Files.Count > 0)
                    {
                        var formFile = Request.Form.Files[0];
                        // Save the image in JPEG format.
                        if (formFile.Length > 0)
                        {
                            // full path to file in temp location
                            string path = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "VolunteerPhotos");
                            string fileName ="docpath_"+ $"P{entityvolunteer.Id}"+ Path.GetExtension(formFile.FileName);

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                           
                            using (var stream = new FileStream(Path.Combine(path,fileName), FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                            entityvolunteer.DocPath = fileName;
                        }
                       
                    }
                    if (entityvolunteer != null)
                    {
                        var subSkillsIds = model.SkillsIds.Split(',');
                        var VolunteerSkilllist = volunteerService.GetVolunteerSkillByVolunteerId(entityvolunteer.Id);
                        foreach (var item in VolunteerSkilllist)
                        {
                            volunteerService.DeleteVolunteerSkillById(item.Id);
                        }
                        List<VolunteerSkill> volskiils = new List<VolunteerSkill>();
                        for (int j = 0; j < subSkillsIds.Length; j++)
                        {
                            if (subSkillsIds[j].Length > 0)
                            {
                                VolunteerSkill skills = new VolunteerSkill();
                                skills.SkillId = Convert.ToInt32(subSkillsIds[j]);
                                volskiils.Add(skills);
                            }
                        }
                        entityvolunteer.Howelsecanyouhelp = model.HowCanYouHelp;
                        entityvolunteer.IsDbscheck = model.IsDBScheck;
                        entityvolunteer.IsDbscheckDate = System.DateTime.Now;
                        entityvolunteer.IndividualCouple = model.IndividualCouple;
                        entityvolunteer.Packingordelivery = model.WorkType;
                        entityvolunteer.VolunteerSkill=volskiils;
                        volunteerService.Save(entityvolunteer, false);
                    }
                    if (model.IsChangePassword)
                    {
                        //Upodate User login details
                        var usrDetail = userService.GetUser(model.UserId);
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
                    if (contact !=null)
                    {
                        contact.ForeName = model.VolunteerName;
                        contact.Mobile = model.Mobile;
                        contactService.Save(contact, false);
                    }
                    ShowSuccessMessage("Success!", "Volunteer updated successfully.", false);
                    return RedirectToAction("VolunteerEdit", "AdminVolunteer");

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return View(model);//return RedirectToAction("updateprofile", "volunteer");
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return View(model);//return RedirectToAction("updateprofile", "volunteer");
            }
        }
        [HttpGet]
        public IActionResult VolunteerView(int Id)
        {
            VolunteerDto model = new VolunteerDto();
            var volunteer = volunteerService.GetVolunteerById(Id);

            if (volunteer != null)
            {
                model.VolunteerName = $"{volunteer.Contact.ForeName} {volunteer.Contact.Surname}";
                model.WorkType = volunteer.Packingordelivery.Value;
                model.MaritalStatus = volunteer.IndividualCouple != null ? volunteer.IndividualCouple.Value : 1;
                model.HowCanYouHelp = volunteer.Howelsecanyouhelp;
                model.Mobile = (volunteer.Contact==null?"":volunteer.Contact.Mobile);
                model.UserName = volunteer.User.UserName;
                model.UserId = volunteer.User.UserId;
                model.VolunteerId = volunteer.Id;
                model.IsDBScheck = volunteer.IsDbscheck;
                model.DocPath = volunteer.DocPath;
                model.VolunteerSkill = volunteer.VolunteerSkill.ToList();
                model.Skills = volunteerService.GetSkillByFoodbank(volunteer.FoodbankId).Where(m => model.VolunteerSkill.Select(x => x.SkillId).Contains(m.Id)).ToList();
                #region Map Availability
                var volunteerAvailability = volunteerService.GetAvailability(volunteer.Id);
                if (volunteerAvailability != null)
                {
                    model.Availability = new AvailabilityDto();
                    model.Availability.FromDate = volunteerAvailability.FormDate.ToFormatCustomString();
                    model.Availability.ToDate = volunteerAvailability.ToDate.ToFormatCustomString();
                    model.Availability.TimeForm = Convert.ToString(volunteerAvailability.TimeForm);
                    model.Availability.TimeTo = Convert.ToString(volunteerAvailability.TimeTo);
                    model.Availability.AllDay = volunteerAvailability.AllDay;
                    model.Availability.Pattern = volunteerAvailability.Pattern;
                    model.Availability.AvailabilityId = volunteerAvailability.Id;

                    if (volunteerAvailability.Id != 0)
                    {
                        if (volunteerAvailability.ToDate.HasValue)
                        {
                            model.Availability.UnavailabilityUntilType = 2;
                            model.Availability.ToDate = volunteerAvailability.ToDate.ToFormatCustomString();
                        }
                        if (volunteerAvailability.AllDay == false)
                        {
                            model.Availability.UnavailabilityTimeType = 2;
                            model.Availability.FromDate = volunteerAvailability.FormDate.ToFormatCustomString();
                            model.Availability.ToDate = volunteerAvailability.TimeTo.ToString();
                        }
                        //unavailabilityDto.AllDayCheckbox = entity.Checkbox ?? false;
                        //unavailabilityDto.Pattern = entity.Pattern;
                        if (volunteerAvailability.Pattern.IsNotNullAndNotEmpty())
                        {
                            if (volunteerAvailability.Pattern.StartsWith("D"))
                            {
                                model.Availability.FrequencyType = 1;
                                if (volunteerAvailability.Pattern.StartsWith("D1"))
                                {
                                    model.Availability.DailyType = 1;

                                    Int32 dailydays = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Split("D1")[1], out dailydays) && dailydays > 0)
                                    {
                                        model.Availability.DailyDays = dailydays;
                                    }
                                }

                                if (volunteerAvailability.Pattern.StartsWith("D2"))
                                {
                                    model.Availability.DailyType = 2;
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("W"))
                            {
                                model.Availability.FrequencyType = 2;
                                if (volunteerAvailability.Pattern[1] == 'Y')
                                    model.Availability.IsWeeklyMonday = true;

                                if (volunteerAvailability.Pattern[2] == 'Y')
                                    model.Availability.IsWeeklyTuesday = true;

                                if (volunteerAvailability.Pattern[3] == 'Y')
                                    model.Availability.IsWeeklyWednesday = true;

                                if (volunteerAvailability.Pattern[4] == 'Y')
                                    model.Availability.IsWeeklyThursday = true;

                                if (volunteerAvailability.Pattern[5] == 'Y')
                                    model.Availability.IsWeeklyFriday = true;

                                if (volunteerAvailability.Pattern[6] == 'Y')
                                    model.Availability.IsWeeklySaturday = true;

                                if (volunteerAvailability.Pattern[7] == 'Y')
                                    model.Availability.IsWeeklySunday = true;

                                Int32 weekdays = 0;
                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(volunteerAvailability.Pattern.Length - 1), out weekdays) && weekdays > 0)
                                {
                                    model.Availability.WeeklyDays = weekdays;
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("M"))
                            {
                                model.Availability.FrequencyType = 3;

                                if (volunteerAvailability.Pattern.StartsWith("M1"))
                                {
                                    model.Availability.MonthlyType = 1;

                                    Int32 monthlydays = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(2, 2), out monthlydays) && monthlydays > 0)
                                    {
                                        model.Availability.MonthlyDays = monthlydays;
                                    }

                                    Int32 monthlymonths = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4), out monthlymonths) && monthlymonths > 0)
                                    {
                                        model.Availability.MonthlyMonths = monthlymonths;
                                    }
                                }

                                if (volunteerAvailability.Pattern.StartsWith("M2"))
                                {
                                    model.Availability.MonthlyType = 2;

                                    Int32 monthlyweek = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(2, 1), out monthlyweek))
                                    {

                                        model.Availability.MonthlyWeek = monthlyweek;
                                    }

                                    Int32 monthlyweekday = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(3, 1), out monthlyweekday))
                                    {
                                        model.Availability.MonthlyWeekDay = monthlyweekday;
                                    }

                                    Int32 monthlyweekmonth = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4), out monthlyweekmonth) && monthlyweekmonth > 0)
                                    {
                                        model.Availability.MonthlyWeekMonth = monthlyweekmonth;
                                    }
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("A"))
                            {
                                model.Availability.FrequencyType = 4;

                                Int32 annualyears = 0;
                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(1, 2), out annualyears) && annualyears > 0)
                                {
                                    model.Availability.AnnualYears = annualyears;
                                }

                                Int32 annualtype = 0;

                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(3, 1), out annualtype))
                                {
                                    if (annualtype == 1)
                                    {
                                        model.Availability.AnnualType = 1;

                                        Int32 annualmonth = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4, 2), out annualmonth) && annualmonth > 0)
                                        {
                                            model.Availability.AnnualMonth = annualmonth;
                                        }

                                        Int32 annualmonthday = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(6), out annualmonthday) && annualmonthday > 0)
                                        {
                                            model.Availability.AnnualMonthDay = annualmonthday;
                                        }
                                    }

                                    if (annualtype == 2)
                                    {
                                        model.Availability.AnnualType = 2;

                                        Int32 annualweek = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4, 1), out annualweek) && annualweek > 0)
                                        {
                                            model.Availability.AnnualWeek = annualweek;
                                        }

                                        Int32 annualweekday = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(5, 1), out annualweekday))
                                        {
                                            model.Availability.AnnualWeekDay = annualweekday;

                                        }

                                        Int32 annualmonthweek = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(6), out annualmonthweek) && annualmonthweek > 0)
                                        {
                                            model.Availability.AnnualMonthWeek = annualmonthweek;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            model.Availability.FrequencyType = 1;
                            model.Availability.DailyDays = 1;
                        }

                    }
                }
                #endregion

                #region Map Unavailability
                var volunteerUnavailability = volunteerService.GetUnavailability(volunteer.Id);
                if (volunteerUnavailability != null)
                {
                    model.Unavailability = new UnavailabilityDto();
                    model.Unavailability.FromDate = volunteerUnavailability.FormDate.ToFormatCustomString();
                    model.Unavailability.ToDate = volunteerUnavailability.ToDate.ToFormatCustomString();
                    model.Unavailability.TimeForm = Convert.ToString(volunteerUnavailability.TimeForm);
                    model.Unavailability.TimeTo = Convert.ToString(volunteerUnavailability.TimeTo);
                    model.Unavailability.AllDay = volunteerUnavailability.AllDay;
                    model.Unavailability.Pattern = volunteerUnavailability.Pattern;
                    model.Unavailability.UnavailabilityId = volunteerUnavailability.Id;

                    if (volunteerUnavailability.Id != 0)
                    {
                        if (volunteerUnavailability.ToDate.HasValue)
                        {
                            model.Unavailability.UnavailabilityTimeType = 2;
                            model.Unavailability.ToDate = volunteerUnavailability.ToDate.ToFormatCustomString();
                        }
                        if (volunteerUnavailability.AllDay == false)
                        {
                            model.Unavailability.UnavailabilityTimeType = 2;
                            model.Unavailability.FromDate = volunteerUnavailability.FormDate.ToFormatCustomString();
                            model.Unavailability.ToDate = volunteerUnavailability.TimeTo.ToString();
                        }
                        //unavailabilityDto.AllDayCheckbox = entity.Checkbox ?? false;
                        //unavailabilityDto.Pattern = entity.Pattern;
                        if (volunteerUnavailability.Pattern.IsNotNullAndNotEmpty())
                        {
                            if (volunteerUnavailability.Pattern.StartsWith("D"))
                            {
                                model.Unavailability.FrequencyType = 1;
                                if (volunteerUnavailability.Pattern.StartsWith("D1"))
                                {
                                    model.Unavailability.DailyType = 1;

                                    Int32 dailydays = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Split("D1")[1], out dailydays) && dailydays > 0)
                                    {
                                        model.Unavailability.DailyDays = dailydays;
                                    }
                                }

                                if (volunteerUnavailability.Pattern.StartsWith("D2"))
                                {
                                    model.Unavailability.DailyType = 2;
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("W"))
                            {
                                model.Unavailability.FrequencyType = 2;
                                if (volunteerUnavailability.Pattern[1] == 'Y')
                                    model.Unavailability.IsWeeklyMonday = true;

                                if (volunteerUnavailability.Pattern[2] == 'Y')
                                    model.Unavailability.IsWeeklyTuesday = true;

                                if (volunteerUnavailability.Pattern[3] == 'Y')
                                    model.Unavailability.IsWeeklyWednesday = true;

                                if (volunteerUnavailability.Pattern[4] == 'Y')
                                    model.Unavailability.IsWeeklyThursday = true;

                                if (volunteerUnavailability.Pattern[5] == 'Y')
                                    model.Unavailability.IsWeeklyFriday = true;

                                if (volunteerUnavailability.Pattern[6] == 'Y')
                                    model.Unavailability.IsWeeklySaturday = true;

                                if (volunteerUnavailability.Pattern[7] == 'Y')
                                    model.Unavailability.IsWeeklySunday = true;

                                Int32 weekdays = 0;
                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(volunteerUnavailability.Pattern.Length - 1), out weekdays) && weekdays > 0)
                                {
                                    model.Unavailability.WeeklyDays = weekdays;
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("M"))
                            {
                                model.Unavailability.FrequencyType = 3;

                                if (volunteerUnavailability.Pattern.StartsWith("M1"))
                                {
                                    model.Unavailability.MonthlyType = 1;

                                    Int32 monthlydays = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(2, 2), out monthlydays) && monthlydays > 0)
                                    {
                                        model.Unavailability.MonthlyDays = monthlydays;
                                    }

                                    Int32 monthlymonths = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4), out monthlymonths) && monthlymonths > 0)
                                    {
                                        model.Unavailability.MonthlyMonths = monthlymonths;
                                    }
                                }

                                if (volunteerUnavailability.Pattern.StartsWith("M2"))
                                {
                                    model.Unavailability.MonthlyType = 2;

                                    Int32 monthlyweek = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(2, 1), out monthlyweek))
                                    {

                                        model.Unavailability.MonthlyWeek = monthlyweek;
                                    }

                                    Int32 monthlyweekday = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(3, 1), out monthlyweekday))
                                    {
                                        model.Unavailability.MonthlyWeekDay = monthlyweekday;
                                    }

                                    Int32 monthlyweekmonth = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4), out monthlyweekmonth) && monthlyweekmonth > 0)
                                    {
                                        model.Unavailability.MonthlyWeekMonth = monthlyweekmonth;
                                    }
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("A"))
                            {
                                model.Unavailability.FrequencyType = 4;

                                Int32 annualyears = 0;
                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(1, 2), out annualyears) && annualyears > 0)
                                {
                                    model.Unavailability.AnnualYears = annualyears;
                                }

                                Int32 annualtype = 0;

                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(3, 1), out annualtype))
                                {
                                    if (annualtype == 1)
                                    {
                                        model.Unavailability.AnnualType = 1;

                                        Int32 annualmonth = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4, 2), out annualmonth) && annualmonth > 0)
                                        {
                                            model.Unavailability.AnnualMonth = annualmonth;
                                        }

                                        Int32 annualmonthday = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(6), out annualmonthday) && annualmonthday > 0)
                                        {
                                            model.Unavailability.AnnualMonthDay = annualmonthday;
                                        }
                                    }

                                    if (annualtype == 2)
                                    {
                                        model.Unavailability.AnnualType = 2;

                                        Int32 annualweek = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4, 1), out annualweek) && annualweek > 0)
                                        {
                                            model.Unavailability.AnnualWeek = annualweek;
                                        }

                                        Int32 annualweekday = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(5, 1), out annualweekday))
                                        {
                                            model.Unavailability.AnnualWeekDay = annualweekday;

                                        }

                                        Int32 annualmonthweek = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(6), out annualmonthweek) && annualmonthweek > 0)
                                        {
                                            model.Unavailability.AnnualMonthWeek = annualmonthweek;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            model.Unavailability.FrequencyType = 1;
                            model.Unavailability.DailyDays = 1;
                        }

                    }
                }
                #endregion
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

          
            return View(model);
        }
        [HttpGet]
        public IActionResult VolunteerParcelView(int Id)
        {
            VolunteerDto model = new VolunteerDto();
            var volunteer = volunteerService.GetVolunteerById(Id);

            if (volunteer != null)
            {
                model.VolunteerName = $"{volunteer.Contact.ForeName} {volunteer.Contact.Surname}";
                model.WorkType = volunteer.Packingordelivery.Value;
                model.MaritalStatus = volunteer.IndividualCouple != null ? volunteer.IndividualCouple.Value : 1;
                model.HowCanYouHelp = volunteer.Howelsecanyouhelp;
                model.Mobile = (volunteer.Contact == null ? "" : volunteer.Contact.Mobile);
                model.UserName = volunteer.User.UserName;
                model.UserId = volunteer.User.UserId;
                model.VolunteerId = volunteer.Id;
                model.IsDBScheck = volunteer.IsDbscheck;
                model.TotalDelivery = volunteer.ParcelsDeliverer.Where(x => x.DeliveredDate != null && x.DeliveryDate != null).ToList().Count;
                model.TotalPacking = volunteer.ParcelsPacker.Where(x => x.PackOnDate != null).ToList().Count;
                #region Map Availability
                var volunteerAvailability = volunteerService.GetAvailability(volunteer.Id);
                if (volunteerAvailability != null)
                {
                    model.Availability = new AvailabilityDto();
                    model.Availability.FromDate = volunteerAvailability.FormDate.ToFormatCustomString();
                    model.Availability.ToDate = volunteerAvailability.ToDate.ToFormatCustomString();
                    model.Availability.TimeForm = Convert.ToString(volunteerAvailability.TimeForm);
                    model.Availability.TimeTo = Convert.ToString(volunteerAvailability.TimeTo);
                    model.Availability.AllDay = volunteerAvailability.AllDay;
                    model.Availability.Pattern = volunteerAvailability.Pattern;
                    model.Availability.AvailabilityId = volunteerAvailability.Id;

                    if (volunteerAvailability.Id != 0)
                    {
                        if (volunteerAvailability.ToDate.HasValue)
                        {
                            model.Availability.UnavailabilityUntilType = 2;
                            model.Availability.ToDate = volunteerAvailability.ToDate.ToFormatCustomString();
                        }
                        if (volunteerAvailability.AllDay == false)
                        {
                            model.Availability.UnavailabilityTimeType = 2;
                            model.Availability.FromDate = volunteerAvailability.FormDate.ToFormatCustomString();
                            model.Availability.ToDate = volunteerAvailability.TimeTo.ToString();
                        }
                        //unavailabilityDto.AllDayCheckbox = entity.Checkbox ?? false;
                        //unavailabilityDto.Pattern = entity.Pattern;
                        if (volunteerAvailability.Pattern.IsNotNullAndNotEmpty())
                        {
                            if (volunteerAvailability.Pattern.StartsWith("D"))
                            {
                                model.Availability.FrequencyType = 1;
                                if (volunteerAvailability.Pattern.StartsWith("D1"))
                                {
                                    model.Availability.DailyType = 1;

                                    Int32 dailydays = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Split("D1")[1], out dailydays) && dailydays > 0)
                                    {
                                        model.Availability.DailyDays = dailydays;
                                    }
                                }

                                if (volunteerAvailability.Pattern.StartsWith("D2"))
                                {
                                    model.Availability.DailyType = 2;
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("W"))
                            {
                                model.Availability.FrequencyType = 2;
                                if (volunteerAvailability.Pattern[1] == 'Y')
                                    model.Availability.IsWeeklyMonday = true;

                                if (volunteerAvailability.Pattern[2] == 'Y')
                                    model.Availability.IsWeeklyTuesday = true;

                                if (volunteerAvailability.Pattern[3] == 'Y')
                                    model.Availability.IsWeeklyWednesday = true;

                                if (volunteerAvailability.Pattern[4] == 'Y')
                                    model.Availability.IsWeeklyThursday = true;

                                if (volunteerAvailability.Pattern[5] == 'Y')
                                    model.Availability.IsWeeklyFriday = true;

                                if (volunteerAvailability.Pattern[6] == 'Y')
                                    model.Availability.IsWeeklySaturday = true;

                                if (volunteerAvailability.Pattern[7] == 'Y')
                                    model.Availability.IsWeeklySunday = true;

                                Int32 weekdays = 0;
                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(volunteerAvailability.Pattern.Length - 1), out weekdays) && weekdays > 0)
                                {
                                    model.Availability.WeeklyDays = weekdays;
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("M"))
                            {
                                model.Availability.FrequencyType = 3;

                                if (volunteerAvailability.Pattern.StartsWith("M1"))
                                {
                                    model.Availability.MonthlyType = 1;

                                    Int32 monthlydays = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(2, 2), out monthlydays) && monthlydays > 0)
                                    {
                                        model.Availability.MonthlyDays = monthlydays;
                                    }

                                    Int32 monthlymonths = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4), out monthlymonths) && monthlymonths > 0)
                                    {
                                        model.Availability.MonthlyMonths = monthlymonths;
                                    }
                                }

                                if (volunteerAvailability.Pattern.StartsWith("M2"))
                                {
                                    model.Availability.MonthlyType = 2;

                                    Int32 monthlyweek = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(2, 1), out monthlyweek))
                                    {

                                        model.Availability.MonthlyWeek = monthlyweek;
                                    }

                                    Int32 monthlyweekday = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(3, 1), out monthlyweekday))
                                    {
                                        model.Availability.MonthlyWeekDay = monthlyweekday;
                                    }

                                    Int32 monthlyweekmonth = 0;
                                    if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4), out monthlyweekmonth) && monthlyweekmonth > 0)
                                    {
                                        model.Availability.MonthlyWeekMonth = monthlyweekmonth;
                                    }
                                }
                            }
                            else if (volunteerAvailability.Pattern.StartsWith("A"))
                            {
                                model.Availability.FrequencyType = 4;

                                Int32 annualyears = 0;
                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(1, 2), out annualyears) && annualyears > 0)
                                {
                                    model.Availability.AnnualYears = annualyears;
                                }

                                Int32 annualtype = 0;

                                if (Int32.TryParse(volunteerAvailability.Pattern.Substring(3, 1), out annualtype))
                                {
                                    if (annualtype == 1)
                                    {
                                        model.Availability.AnnualType = 1;

                                        Int32 annualmonth = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4, 2), out annualmonth) && annualmonth > 0)
                                        {
                                            model.Availability.AnnualMonth = annualmonth;
                                        }

                                        Int32 annualmonthday = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(6), out annualmonthday) && annualmonthday > 0)
                                        {
                                            model.Availability.AnnualMonthDay = annualmonthday;
                                        }
                                    }

                                    if (annualtype == 2)
                                    {
                                        model.Availability.AnnualType = 2;

                                        Int32 annualweek = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(4, 1), out annualweek) && annualweek > 0)
                                        {
                                            model.Availability.AnnualWeek = annualweek;
                                        }

                                        Int32 annualweekday = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(5, 1), out annualweekday))
                                        {
                                            model.Availability.AnnualWeekDay = annualweekday;

                                        }

                                        Int32 annualmonthweek = 0;
                                        if (Int32.TryParse(volunteerAvailability.Pattern.Substring(6), out annualmonthweek) && annualmonthweek > 0)
                                        {
                                            model.Availability.AnnualMonthWeek = annualmonthweek;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            model.Availability.FrequencyType = 1;
                            model.Availability.DailyDays = 1;
                        }

                    }
                }
                #endregion

                #region Map Unavailability
                var volunteerUnavailability = volunteerService.GetUnavailability(volunteer.Id);
                if (volunteerUnavailability != null)
                {
                    model.Unavailability = new UnavailabilityDto();
                    model.Unavailability.FromDate = volunteerUnavailability.FormDate.ToFormatCustomString();
                    model.Unavailability.ToDate = volunteerUnavailability.ToDate.ToFormatCustomString();
                    model.Unavailability.TimeForm = Convert.ToString(volunteerUnavailability.TimeForm);
                    model.Unavailability.TimeTo = Convert.ToString(volunteerUnavailability.TimeTo);
                    model.Unavailability.AllDay = volunteerUnavailability.AllDay;
                    model.Unavailability.Pattern = volunteerUnavailability.Pattern;
                    model.Unavailability.UnavailabilityId = volunteerUnavailability.Id;

                    if (volunteerUnavailability.Id != 0)
                    {
                        if (volunteerUnavailability.ToDate.HasValue)
                        {
                            model.Unavailability.UnavailabilityTimeType = 2;
                            model.Unavailability.ToDate = volunteerUnavailability.ToDate.ToFormatCustomString();
                        }
                        if (volunteerUnavailability.AllDay == false)
                        {
                            model.Unavailability.UnavailabilityTimeType = 2;
                            model.Unavailability.FromDate = volunteerUnavailability.FormDate.ToFormatCustomString();
                            model.Unavailability.ToDate = volunteerUnavailability.TimeTo.ToString();
                        }
                        //unavailabilityDto.AllDayCheckbox = entity.Checkbox ?? false;
                        //unavailabilityDto.Pattern = entity.Pattern;
                        if (volunteerUnavailability.Pattern.IsNotNullAndNotEmpty())
                        {
                            if (volunteerUnavailability.Pattern.StartsWith("D"))
                            {
                                model.Unavailability.FrequencyType = 1;
                                if (volunteerUnavailability.Pattern.StartsWith("D1"))
                                {
                                    model.Unavailability.DailyType = 1;

                                    Int32 dailydays = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Split("D1")[1], out dailydays) && dailydays > 0)
                                    {
                                        model.Unavailability.DailyDays = dailydays;
                                    }
                                }

                                if (volunteerUnavailability.Pattern.StartsWith("D2"))
                                {
                                    model.Unavailability.DailyType = 2;
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("W"))
                            {
                                model.Unavailability.FrequencyType = 2;
                                if (volunteerUnavailability.Pattern[1] == 'Y')
                                    model.Unavailability.IsWeeklyMonday = true;

                                if (volunteerUnavailability.Pattern[2] == 'Y')
                                    model.Unavailability.IsWeeklyTuesday = true;

                                if (volunteerUnavailability.Pattern[3] == 'Y')
                                    model.Unavailability.IsWeeklyWednesday = true;

                                if (volunteerUnavailability.Pattern[4] == 'Y')
                                    model.Unavailability.IsWeeklyThursday = true;

                                if (volunteerUnavailability.Pattern[5] == 'Y')
                                    model.Unavailability.IsWeeklyFriday = true;

                                if (volunteerUnavailability.Pattern[6] == 'Y')
                                    model.Unavailability.IsWeeklySaturday = true;

                                if (volunteerUnavailability.Pattern[7] == 'Y')
                                    model.Unavailability.IsWeeklySunday = true;

                                Int32 weekdays = 0;
                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(volunteerUnavailability.Pattern.Length - 1), out weekdays) && weekdays > 0)
                                {
                                    model.Unavailability.WeeklyDays = weekdays;
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("M"))
                            {
                                model.Unavailability.FrequencyType = 3;

                                if (volunteerUnavailability.Pattern.StartsWith("M1"))
                                {
                                    model.Unavailability.MonthlyType = 1;

                                    Int32 monthlydays = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(2, 2), out monthlydays) && monthlydays > 0)
                                    {
                                        model.Unavailability.MonthlyDays = monthlydays;
                                    }

                                    Int32 monthlymonths = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4), out monthlymonths) && monthlymonths > 0)
                                    {
                                        model.Unavailability.MonthlyMonths = monthlymonths;
                                    }
                                }

                                if (volunteerUnavailability.Pattern.StartsWith("M2"))
                                {
                                    model.Unavailability.MonthlyType = 2;

                                    Int32 monthlyweek = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(2, 1), out monthlyweek))
                                    {

                                        model.Unavailability.MonthlyWeek = monthlyweek;
                                    }

                                    Int32 monthlyweekday = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(3, 1), out monthlyweekday))
                                    {
                                        model.Unavailability.MonthlyWeekDay = monthlyweekday;
                                    }

                                    Int32 monthlyweekmonth = 0;
                                    if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4), out monthlyweekmonth) && monthlyweekmonth > 0)
                                    {
                                        model.Unavailability.MonthlyWeekMonth = monthlyweekmonth;
                                    }
                                }
                            }
                            else if (volunteerUnavailability.Pattern.StartsWith("A"))
                            {
                                model.Unavailability.FrequencyType = 4;

                                Int32 annualyears = 0;
                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(1, 2), out annualyears) && annualyears > 0)
                                {
                                    model.Unavailability.AnnualYears = annualyears;
                                }

                                Int32 annualtype = 0;

                                if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(3, 1), out annualtype))
                                {
                                    if (annualtype == 1)
                                    {
                                        model.Unavailability.AnnualType = 1;

                                        Int32 annualmonth = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4, 2), out annualmonth) && annualmonth > 0)
                                        {
                                            model.Unavailability.AnnualMonth = annualmonth;
                                        }

                                        Int32 annualmonthday = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(6), out annualmonthday) && annualmonthday > 0)
                                        {
                                            model.Unavailability.AnnualMonthDay = annualmonthday;
                                        }
                                    }

                                    if (annualtype == 2)
                                    {
                                        model.Unavailability.AnnualType = 2;

                                        Int32 annualweek = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(4, 1), out annualweek) && annualweek > 0)
                                        {
                                            model.Unavailability.AnnualWeek = annualweek;
                                        }

                                        Int32 annualweekday = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(5, 1), out annualweekday))
                                        {
                                            model.Unavailability.AnnualWeekDay = annualweekday;

                                        }

                                        Int32 annualmonthweek = 0;
                                        if (Int32.TryParse(volunteerUnavailability.Pattern.Substring(6), out annualmonthweek) && annualmonthweek > 0)
                                        {
                                            model.Unavailability.AnnualMonthWeek = annualmonthweek;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            model.Unavailability.FrequencyType = 1;
                            model.Unavailability.DailyDays = 1;
                        }

                    }
                }
                #endregion
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

            return View(model);
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
                    (c.AllDay ? "All Day" : (c.TimeForm + "to"+ c.TimeTo )),
                    //"<a data-toggle='modal' data-target='#modal-create-edit-unavailability'  href=" + Url.Action("CreateEdit", "Unavailability", new {c.AvailabilityId })
                    //+ " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"
                    // "<a data-toggle='modal' data-target='#modal-delete-volunteer-availability' href='" + Url.Action("DeleteAvaliability", "Volunteer", new { id = c.AvailabilityId })
                    //+ "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>"
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
                    (c.AllDay ? "All Day" : (c.TimeForm + "to"+ c.TimeTo )),
                    // "<a data-toggle='modal' data-target='#modal-delete-volunteer-unavailability' href='" + Url.Action("DeleteUnavaliability", "Volunteer", new {id = c.UnavailabilityId })
                    //+ "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>"
                })
            });
        }
        #endregion


        [HttpGet]
        public IActionResult View(int? id = null)
        {
            var model = new FamilyParcelDto();
            model.FoodBankId = CurrentUser.FoodbankId;

            var FamilyList = familyService.GetAllFamily(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.FamilyName,
                Value = x.Id.ToString()
            }).ToList();
            FamilyList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.FamilyList = FamilyList;

            var enumList = Enum.GetValues(typeof(ParcelTypes));
            List<SelectListItem> parcelList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                parcelList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((ParcelTypes)(int)items).GetDescription(),
                });
            }
            parcelList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.ParcelTypeList = parcelList;

            var StandardParcelList = parcelService.GetAllParcelType(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            StandardParcelList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.StandardParcelList = StandardParcelList;

            var PackerList = volunteerService.GetVolunteerList(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = $"{x.Contact.ForeName}",
                Value = x.Id.ToString()
            }).ToList();
            PackerList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.PackerList = PackerList;

            var RecipeList = recipeService.GetRecipeList(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = $"{x.RecipeTitle}",
                Value = x.Id.ToString()
            }).ToList();
            RecipeList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.RecipeList = RecipeList;

            var listItems = foodService.GetAllFoodListForParcel(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            listItems.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.FoodItemList = listItems;

            if (id.HasValue)
            {
                var parcel = parcelService.GetParcelById(id.Value);
                if (parcel != null)
                {
                    model.ParcelId = parcel.Id;
                    if (parcel.ParcelTypeId != null)
                    {
                        model.ParcelTypeId = parcel.ParcelTypeId.Value;
                    }
                    else
                    {
                        model.ParcelTypeId = 0;
                    }
                    model.FamilyId = parcel.FamilyId != null ? parcel.FamilyId.Value : 0;
                    model.PackerId = parcel.PackerId.Value;
                    model.DeliverrerId = parcel.DelivererId.Value;
                    model.DeliveredDate = parcel.DeliveredDate != null ? Extensions.ToFormatCustomString(parcel.DeliveredDate.Value) : null;
                    model.DeliveryDate = parcel.DeliveryDate != null ? Extensions.ToFormatCustomString(parcel.DeliveryDate.Value) : null;
                    model.Status = parcel.Status.Value;
                    model.SpecialNote = parcel.SpecialNote;
                    if (parcel.RecipeId != null)
                    {
                        model.RecipeId = parcel.RecipeId.Value;
                    }

                    model.FamilyParcelList = parcelService.GetFamilyParcelFoodItemById(parcel.Id).Select(x => new FamilyParcel
                    {
                        FoodItemName = x.Food.Name,
                        FoodItemId = x.FoodId,
                        Quantity = x.Quantity
                    }).ToList();
                }
                else
                {
                    model.FamilyParcelList = new List<FamilyParcel>();
                }
            }
            else
            {
                model.ParcelTypeId = null;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult VolunteerDeliveryDetails(DataTableServerSide model, int VolunteerId)
        {
            KeyValuePair<int, List<FamilyParcelDto>> parcelType = new KeyValuePair<int, List<FamilyParcelDto>>();
            parcelType = familyParcelService.GetVolunteerDeliveryDetailsID(model, CurrentUser.FoodbankId, VolunteerId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = parcelType.Key,
                recordsFiltered = parcelType.Key,
                data = parcelType.Value.Select((c, index) => new List<object> {
                    c.ParcelId,//0
                    model.start+index+1,//1
                    c.ParcelType,//2
                    c.FamilyName,//3
                    c.DeliveredDate,//4
                    //5
                   "<a href=" + Url.Action("View", "AdminVolunteer", new { id = c.ParcelId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>",
                })
            });
        }

        [HttpPost]

        public IActionResult VolunteerParcelDetails(DataTableServerSide model, int VolunteerId)
        {

            KeyValuePair<int, List<FamilyParcelDto>> parcelType = new KeyValuePair<int, List<FamilyParcelDto>>();
            parcelType = familyParcelService.GetVolunteerParcelDetailsID(model, CurrentUser.FoodbankId, VolunteerId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = parcelType.Key,
                recordsFiltered = parcelType.Key,
                data = parcelType.Value.Select((c, index) => new List<object> {
                    c.ParcelId,//0
                    model.start+index+1,//1
                    c.ParcelType,//2
                    c.FamilyName,//3
                    c.PackOnDate,//4
                   // ((ParcelStatus)c.Status).GetDescription(), //5
                    //6
                   "<a href=" + Url.Action("View", "AdminVolunteer", new { id = c.ParcelId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>",
                })
            });
        }
    }
}
