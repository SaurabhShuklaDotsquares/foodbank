using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Service;
using FB.Web.Code;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Web.Controllers
{
    [CustomActionFilterAttribute(UserRoles.Volunteer)]
    public class VolunteerController : BaseController
    {
        private IVolunteerService volunteerService;
        private IUserService userService;
        private IFoodbankService foodbankService;
        private readonly IParcelService parcelService;
        private readonly IFoodService foodService;
        private readonly IFamilyParcelService familyParcelService;
        private readonly IFamilyService familyService;
        private readonly IRecipeService recipeService;
        private readonly IStockService stockService;
        private readonly IGrantorService grantorService;
        private readonly IVoucherService voucherService;
        private readonly IFeedbackService feedbackService;
        public VolunteerController(IVolunteerService _volunteerService, IUserService _userService, IParcelService _parcelService, IFoodbankService _foodbankService, IFoodService _foodService,
            IFamilyParcelService _familyParcelService, IFamilyService _familyService,  IRecipeService _recipeService,
            IStockService _stockService, IGrantorService _grantorService, IVoucherService _voucherService, IFeedbackService _feedbackService)
        {
            volunteerService = _volunteerService;
            userService = _userService;
            foodService = _foodService;
            familyParcelService = _familyParcelService;
            parcelService = _parcelService;
            familyService = _familyService;
            volunteerService = _volunteerService;
            recipeService = _recipeService;
            stockService = _stockService;
            grantorService = _grantorService;
            voucherService = _voucherService;
            feedbackService = _feedbackService;
        }
        public IActionResult Index()
        {
            VolunteerDto model = new VolunteerDto();
            var volunteer = volunteerService.GetVolunteerByUserId(CurrentUser.UserID);
            ViewBag.VolunteerId = volunteer.Id;
            ViewBag.TotalDeliver = volunteer.ParcelsDeliverer.Where(x=>x.DeliveredDate != null).Count() ;
            ViewBag.TotalPacker = volunteer.ParcelsPacker.Where(x => x.PackedDate != null).Count();
            ViewBag.DayCount = "0";// volunteer.VolunteerAvailability.Where(x => x.PackedDate != null).fir();
            ViewBag.NextShiftDate = System.DateTime.Now.ToString("dd/MM/yyyy");// (volunteer.VolunteerAvailability.Where(x => x.FormDate > System.DateTime.Now && x.ToDate <= System.DateTime.Now).FirstOrDefault().!=null? volunteer.VolunteerAvailability.Where(x => x.FormDate >= System.DateTime.Now && x.ToDate <= System.DateTime.Now).FirstOrDefault() : "");
            return View();
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            VolunteerDto model = new VolunteerDto();
            var volunteer = volunteerService.GetVolunteerByUserId(CurrentUser.UserID);

            if (volunteer != null)
            {
                model.VolunteerName = $"{volunteer.Contact.ForeName} {volunteer.Contact.Surname}";
                model.WorkType = volunteer.Packingordelivery.Value;
                model.MaritalStatus = volunteer.IndividualCouple != null ? volunteer.IndividualCouple.Value : 1;
                model.HowCanYouHelp = volunteer.Howelsecanyouhelp;

                model.UserName = volunteer.User.UserName;
                model.UserId = volunteer.User.UserId;
                model.VolunteerId = volunteer.Id;
                model.ProfilePic = volunteer.ProfilePic;
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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(VolunteerDto model)
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
            if (!model.IsUnavailability)
            {
                ModelState.Remove("Unavailability.FromDate");
                ModelState.Remove("Unavailability.ToDate");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    #region Availbility Section
                    if (model.IsRegularAvailability)
                    {
                        if (model.Availability.FromDate.ToDateTimeNullable() == null)
                        {
                            ShowErrorMessage("Error!", "From date required.", false);
                            return RedirectToAction("updateprofile", "volunteer");
                        }

                        if (model.Availability.ToDate.ToDateTimeNullable() == null)
                        {
                            ShowErrorMessage("Error!", "Finish date required.", false);
                            return RedirectToAction("updateprofile", "volunteer");
                        }
                        else if (model.Availability.ToDate.ToDateTime() < model.Availability.ToDate.ToDateTime())
                        {
                            ShowErrorMessage("Error!", "The finish date must be after the start date.", false);
                            return RedirectToAction("updateprofile", "volunteer");
                        }

                        if (model.Availability.UnavailabilityTimeType == 2)
                        {
                            if (string.IsNullOrEmpty(model.Availability.TimeForm))
                            {
                                ShowErrorMessage("Error!", "Unavilability start time required.", false);
                                return RedirectToAction("updateprofile", "volunteer");
                            }

                            if (string.IsNullOrEmpty(model.Availability.TimeTo))
                            {
                                ShowErrorMessage("Error!", "Unavilability end time required.", false);
                                return RedirectToAction("updateprofile", "volunteer");
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
                                    return RedirectToAction("updateprofile", "volunteer");
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
                                return RedirectToAction("updateprofile", "volunteer");
                            }
                            else if (!model.Availability.IsWeeklyMonday && !model.Availability.IsWeeklyTuesday && !model.Availability.IsWeeklyWednesday && !model.Availability.IsWeeklyThursday && !model.Availability.IsWeeklyFriday && !model.Availability.IsWeeklySaturday && !model.Availability.IsWeeklySunday)
                            {
                                ShowErrorMessage("Error!", "You must select at least one day of the week.", false);
                                return RedirectToAction("updateprofile", "volunteer");
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
                                    return RedirectToAction("updateprofile", "volunteer");
                                }
                                else if (model.Availability.MonthlyMonths < 1 || model.Availability.MonthlyMonths > 99)
                                {
                                    ShowErrorMessage("Error!", "Enter 1 for every month, 2 for every other month and so on.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
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
                                    return RedirectToAction("updateprofile", "volunteer");
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
                                return RedirectToAction("updateprofile", "volunteer");
                            }

                            if (model.Availability.AnnualType == 1)
                            {
                                if (model.Availability.AnnualMonthDay < 1 || model.Availability.AnnualMonthDay > 31)
                                {
                                    ShowErrorMessage("Error!", "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
                                }
                                else if (model.Availability.AnnualMonth == 2 && (model.Availability.AnnualMonthDay == 30 || model.Availability.AnnualMonthDay == 31))
                                {
                                    ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
                                }
                                else if ((model.Availability.AnnualMonth == 4 || model.Availability.AnnualMonth == 6 || model.Availability.AnnualMonth == 9 || model.Availability.AnnualMonth == 11) && model.Availability.AnnualMonthDay == 31)
                                {
                                    ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
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
                    }
                    #endregion

                    #region Unavailbility Section
                    if (model.IsUnavailability)
                    {
                        if (model.Unavailability.FromDate.ToDateTimeNullable() == null)
                        {
                            ShowErrorMessage("Error!", "From date required.", false);
                            return RedirectToAction("updateprofile", "volunteer");
                        }

                        if (model.Unavailability.ToDate.ToDateTimeNullable() == null)
                        {
                            ShowErrorMessage("Error!", "Finish date required.", false);
                            return RedirectToAction("updateprofile", "volunteer");
                        }
                        else if (model.Unavailability.ToDate.ToDateTime() < model.Unavailability.ToDate.ToDateTime())
                        {
                            ShowErrorMessage("Error!", "The finish date must be after the start date.", false);
                            return RedirectToAction("updateprofile", "volunteer");
                        }

                        if (model.Unavailability.UnavailabilityTimeType == 2)
                        {
                            if (string.IsNullOrEmpty(model.Unavailability.TimeForm))
                            {
                                ShowErrorMessage("Error!", "Unavilability start time required.", false);
                                return RedirectToAction("updateprofile", "volunteer");
                            }

                            if (string.IsNullOrEmpty(model.Unavailability.TimeTo))
                            {
                                ShowErrorMessage("Error!", "Unavilability end time required.", false);
                                return RedirectToAction("updateprofile", "volunteer");
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
                                    return RedirectToAction("updateprofile", "volunteer");
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
                                return RedirectToAction("updateprofile", "volunteer");
                            }
                            else if (!model.Unavailability.IsWeeklyMonday && !model.Unavailability.IsWeeklyTuesday && !model.Unavailability.IsWeeklyWednesday && !model.Unavailability.IsWeeklyThursday && !model.Unavailability.IsWeeklyFriday && !model.Unavailability.IsWeeklySaturday && !model.Unavailability.IsWeeklySunday)
                            {
                                ShowErrorMessage("Error!", "You must select at least one day of the week.", false);
                                return RedirectToAction("updateprofile", "volunteer");
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
                                    return RedirectToAction("updateprofile", "volunteer");
                                }
                                else if (model.Unavailability.MonthlyMonths < 1 || model.Unavailability.MonthlyMonths > 99)
                                {
                                    ShowErrorMessage("Error!", "Enter 1 for every month, 2 for every other month and so on.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
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
                                    return RedirectToAction("updateprofile", "volunteer");
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
                                return RedirectToAction("updateprofile", "volunteer");
                            }

                            if (model.Unavailability.AnnualType == 1)
                            {
                                if (model.Unavailability.AnnualMonthDay < 1 || model.Unavailability.AnnualMonthDay > 31)
                                {
                                    ShowErrorMessage("Error!", "Enter 1 for the 1st of the month, 2 for the 2nd and so on.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
                                }
                                else if (model.Unavailability.AnnualMonth == 2 && (model.Unavailability.AnnualMonthDay == 30 || model.Unavailability.AnnualMonthDay == 31))
                                {
                                    ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
                                }
                                else if ((model.Unavailability.AnnualMonth == 4 || model.Unavailability.AnnualMonth == 6 || model.Unavailability.AnnualMonth == 9 || model.Unavailability.AnnualMonth == 11) && model.Unavailability.AnnualMonthDay == 31)
                                {
                                    ShowErrorMessage("Error!", "Enter valid date on selected month.", false);
                                    return RedirectToAction("updateprofile", "volunteer");
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
                    }
                    #endregion

                    #region Unavailbility save 
                    if (model.IsUnavailability)
                    {
                        VolunteerUnavailability entity;
                        if (model.Unavailability.UnavailabilityId == 0)
                            entity = new VolunteerUnavailability();
                        else
                            entity = volunteerService.GetUnavailabilityById(model.Unavailability.UnavailabilityId);

                        entity = VolunteerAvailabilityUnavailabilityDtoMapper.MapUnavailability(model, entity);
                        volunteerService.Save(entity, true);
                    }
                    #endregion

                    #region Availibility Save
                    if (model.IsRegularAvailability)
                    {
                        VolunteerAvailability entityAvailability;
                        if (model.Availability.AvailabilityId == 0)
                            entityAvailability = new VolunteerAvailability();
                        else
                            entityAvailability = volunteerService.GetAvailabilityById(model.Availability.AvailabilityId);

                        entityAvailability = VolunteerAvailabilityUnavailabilityDtoMapper.MapAvailability(model, entityAvailability);
                        volunteerService.Saveavailability(entityAvailability, true);
                    }
                    #endregion

                    #region Change Password Save
                    if (model.IsChangePassword)
                    {
                        User entityuser;
                        if (model.UserId == 0)
                            entityuser = new User();
                        else
                            entityuser = userService.GetUser(model.UserId);

                        entityuser = VolunteerAvailabilityUnavailabilityDtoMapper.MapUser(model, entityuser);
                        userService.Save(entityuser, CurrentUser.UserID, model.Availability.AvailabilityId == 0);
                    }
                    #endregion
                    Volunteer entityvolunteer = volunteerService.GetVolunteerById(model.VolunteerId);
                    // upload profile pic
                    if (Request.Form.Files.Count > 0)
                    {
                        var fileToUpload = Request.Form.Files.GetFile("ProfilePic");

                        if (fileToUpload != null)
                        {
                          
                            var imageStream = fileToUpload.OpenReadStream();
                            var image = Image.FromStream(imageStream);
                            int Width = 40;
                            int Height = 40;
                            int newWidth = Width;
                            int newHeight = Convert.ToInt32(Width * ((double)image.Height / (double)image.Width));
                            if (newHeight < 40)
                                newHeight = Height;
                            var newImage = FB.Core.Extensions.GetReducedImage(newWidth, newHeight, imageStream);
                            if (newImage == null)
                            {
                                throw new Exception("Geeting error for create thumbnail image.");
                            }

                            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(newImage, typeof(byte[]));
                            string path = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "VolunteerPhotos");
                            string fileName = $"P{entityvolunteer.Id}.jpg";

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            // Save the image in JPEG format.
                            image.Save(Path.Combine(path, fileName), ImageFormat.Jpeg);
                            entityvolunteer.ProfilePic = fileName;
                        }
                        var fileToUpload2 = Request.Form.Files.GetFile("DocPath");

                        if (fileToUpload2 != null)
                        {

                            var formFile = fileToUpload2;
                            // Save the image in JPEG format.
                            if (formFile.Length > 0)
                            {
                                // full path to file in temp location
                                string path = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "VolunteerPhotos");
                                string fileName = "docpath_" + $"P{entityvolunteer.Id}" + Path.GetExtension(formFile.FileName);

                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }

                                using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                                {
                                    await formFile.CopyToAsync(stream);
                                }
                                entityvolunteer.DocPath = fileName;
                            }
                        }

                        
                    }

                    if (entityvolunteer != null)
                    {
                        entityvolunteer = VolunteerAvailabilityUnavailabilityDtoMapper.MapVolunteer(model, entityvolunteer);
                        volunteerService.Save(entityvolunteer, model.VolunteerId == 0);
                    }

                    ShowSuccessMessage("Success!", "Profile Save Successfully.", false);
                    return RedirectToAction("updateprofile", "volunteer");

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return RedirectToAction("updateprofile", "volunteer");
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return RedirectToAction("updateprofile", "volunteer");
            }
        }

        #region My Delivery Section 
        [HttpPost]
        public IActionResult MyDelivery(DataTableServerSide model)
        {
            KeyValuePair<int, List<VolunteerDeliveryListDto>> centralOffices = new KeyValuePair<int, List<VolunteerDeliveryListDto>>();
            var volunteer = volunteerService.GetVolunteerByUserId(CurrentUser.UserID);


            centralOffices = volunteerService.GetDeliveryList(model, volunteer.Id);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    model.start+index+1,
                    c.DateOfDelivery.ToString("dd/MM/yyyy"),
                    c.FamilyName,
                    c.ParcelType,
                      ((ParcelStatus)c.Status).GetDescription(),
                    "<a  href='" + Url.Action("ParcelView", "Volunteer", new { Id = c.Id })
                        + "' class='btn btn-primary grid-btn btn-sm ps3 delete-btn'><i class='fa fa-eye'></i></a>"
                })
            });
        }
        #endregion

        #region Packing List Section 
        [HttpGet]
        public IActionResult PackingListing()
        {
            VolunteerPackingListDto model = new VolunteerPackingListDto();
            var User = userService.GetUser(CurrentUser.UserID);
            if (User != null)
            {
                //model.UserName = User.UserName;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult PackingListing(DataTableServerSide model)
        {
            var volunteer = volunteerService.GetVolunteerByUserId(CurrentUser.UserID);

            KeyValuePair<int, List<VolunteerPackingListDto>> centralOffices = new KeyValuePair<int, List<VolunteerPackingListDto>>();
            centralOffices = volunteerService.GetPackingList(model, volunteer.Id);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    model.start+index+1,
                    c.AssignedDate.ToString("dd/MM/yyyy"),
                    c.PacelType,
                    c.DueDateDelivery==null?"":c.DueDateDelivery?.ToString("dd/MM/yyyy"),
                    ((ParcelStatus)c.Status).GetDescription(),
                    "<a  href='" + Url.Action("ParcelView", "Volunteer", new { Id = c.Id })
                        + "' class='btn btn-primary grid-btn btn-sm ps3 delete-btn'><i class='fa fa-eye'></i></a>"
                })
            });
        }
        #endregion

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
                     "<a data-toggle='modal' data-target='#modal-delete-volunteer-availability' href='" + Url.Action("DeleteAvaliability", "Volunteer", new { id = c.AvailabilityId })
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
                    (c.AllDay ? "All Day" : (c.TimeForm + "to"+ c.TimeTo )),
                     "<a data-toggle='modal' data-target='#modal-delete-volunteer-unavailability' href='" + Url.Action("DeleteUnavaliability", "Volunteer", new {id = c.UnavailabilityId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>"
                })
            });
        }
        #endregion

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
                    return RedirectToAction("updateprofile", "volunteer");
                }
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("updateprofile", "volunteer");
            }
            return RedirectToAction("updateprofile", "volunteer");
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
                    return RedirectToAction("updateprofile", "volunteer");
                }
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("updateprofile", "volunteer");
            }
            return RedirectToAction("updateprofile", "volunteer");
        }
        #endregion
        [HttpGet]
        public IActionResult ParcelView(int? id = null)
        {
            var model = new FamilyParcelDto();
            if (id.HasValue)
            {
                int parcelid = id.Value;
                var parcel = parcelService.GetParcelById(parcelid);
                if (parcel != null)
                {
                   
                    model.FoodBankId = parcel.FoodbankId??0;

                    var FamilyList = familyService.GetAllFamily(parcel.FoodbankId ?? 0).Select(x => new SelectListItem
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

                    var StandardParcelList = parcelService.GetAllParcelType(parcel.FoodbankId ?? 0).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                    StandardParcelList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                    ViewBag.StandardParcelList = StandardParcelList;

                    var PackerList = volunteerService.GetVolunteerList(parcel.FoodbankId ?? 0).Select(x => new SelectListItem
                    {
                        Text = $"{x.Contact.ForeName}",
                        Value = x.Id.ToString()
                    }).ToList();
                    PackerList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                    ViewBag.PackerList = PackerList;

                    var RecipeList = recipeService.GetRecipeList(parcel.FoodbankId ?? 0).Select(x => new SelectListItem
                    {
                        Text = $"{x.RecipeTitle}",
                        Value = x.Id.ToString()
                    }).ToList();
                    RecipeList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                    ViewBag.RecipeList = RecipeList;

                    var listItems = foodService.GetAllFoodListForParcel(parcel.FoodbankId ?? 0).Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                    listItems.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                    ViewBag.FoodItemList = listItems;

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
    }
}
