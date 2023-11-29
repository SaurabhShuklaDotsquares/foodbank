using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using FB.Web.Code;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FB.ExportReport;
using FB.Web.Models;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing.QrCode;
using System.Reflection.Metadata;
using PdfSharpCore;

namespace FB.Web.Controllers
{
    [CustomActionFilterAttribute(UserRoles.Referrer)]
    public class ReferrerController : BaseController
    {
        private readonly IMyReferralService myReferralService;
        private readonly ICountryService countryService;
        private readonly IAddressService addressService;
        private readonly IUserService userService;
        private readonly IFamilyService familyService;
        private readonly IAllergiesService allergyService;
        private IVoucherService voucherService;
        private IBranchService branchService;
        public ReferrerController(IMyReferralService _myReferralService, ICountryService _countryService, IAddressService _addressService,
            IUserService _userService, IFamilyService _familyService, IAllergiesService _allergyService, IBranchService _branchService, IVoucherService _voucherService)
        {
            myReferralService = _myReferralService;
            countryService = _countryService;
            addressService = _addressService;
            userService = _userService;
            familyService = _familyService;
            allergyService = _allergyService;
            branchService = _branchService; voucherService = _voucherService;
        }
        public IActionResult Index()
        {
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
            ViewBag.Countpending = myReferralService.CountPendingReferralByID(CurrentUser.UserID);
            return View();
        }
        [HttpPost]
        public IActionResult GetReferralMonth(string month)
        {
            KeyValuePair<int, List<MyReferralsDto>> centralOffices = new KeyValuePair<int, List<MyReferralsDto>>();
            int monthId = DateTime.ParseExact(month, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
            centralOffices = myReferralService.GetMyReferralMonth(monthId, CurrentUser.UserID);

            var firstOftargetMonth = new DateTime(DateTime.Now.Year, monthId, 1);
            var firstOfNextMonth = firstOftargetMonth.AddMonths(1);
            List<SelectListItem> allDates = new List<SelectListItem>();
            List<SelectListItem> allMonthData = new List<SelectListItem>();
            string[] Labels = new string[] { };
            string[] Data = new string[] { };
            for (DateTime date = firstOftargetMonth; date < firstOfNextMonth; date = date.AddDays(1))
            {
                allDates.Add(new SelectListItem
                {
                    Text = date.ToString("MMM/dd"),
                    Value = date.ToString("dd")
                });
            }
            foreach (var day in allDates)
            {
                allMonthData.Add(new SelectListItem
                {
                    Text = day.Text,
                    Value = centralOffices.Value.Where(x => x.ReferralDate.Day == Convert.ToInt32(day.Value)).ToList().Count.ToString()
                });
            }
            Labels = allMonthData.Select(x => x.Text).ToArray();
            Data = allMonthData.Select(x => x.Value).ToArray();
            //Data = new string[] { "10", "40", "30", "20", "10", "0", "60", "50", "40", "30", "20", "10", "0" };
            return Json(new
            {
                Labels,
                Data
            });
        }
        [HttpPost]
        public IActionResult GetReferralYear(string year)
        {
            KeyValuePair<int, List<MyReferralsDto>> centralOffices = new KeyValuePair<int, List<MyReferralsDto>>();
            centralOffices = myReferralService.GetMyReferralYear(year, CurrentUser.UserID);
            List<SelectListItem> allMonthData = new List<SelectListItem>();
            string[] Data = new string[] { };
            string[] Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            foreach (var month in Labels)
            {
                int monthId = DateTime.ParseExact(month, "MMM", System.Globalization.CultureInfo.CurrentCulture).Month;
                allMonthData.Add(new SelectListItem
                {
                    Text = month,
                    Value = centralOffices.Value.Where(x => x.ReferralDate.Month == Convert.ToInt32(monthId)).ToList().Count.ToString()
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
        public IActionResult GetReferralDate(string sdate, string edate)
        {
            //KeyValuePair<int, List<MyReferralsDto>> centralOffices = new KeyValuePair<int, List<MyReferralsDto>>();
            //centralOffices = myReferralService.GetMyReferralDate(sdate,edate);    
            //string[] Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
            //string[] Data = new string[] { "10", "40", "30", "20", "10", "0", "60", "50", "40", "30", "20", "10", "0" };
            //return Json(new
            //{
            //    Labels,
            //    Data
            //});
            KeyValuePair<int, List<MyReferralsDto>> centralOffices = new KeyValuePair<int, List<MyReferralsDto>>();
            centralOffices = myReferralService.GetMyReferralDate(DateTime.ParseExact(sdate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None), DateTime.ParseExact(edate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None), CurrentUser.UserID);
            //List<SelectListItem> allMonthData = new List<SelectListItem>();
            //string[] Data = new string[] { };
            //string[] Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //foreach (var month in Labels)
            //{
            //    int monthId = DateTime.ParseExact(month, "MMM", System.Globalization.CultureInfo.CurrentCulture).Month;
            //    allMonthData.Add(new SelectListItem
            //    {
            //        Text = month,
            //        Value = centralOffices.Value.Where(x => x.ReferralDate.Month == Convert.ToInt32(monthId)).ToList().Count.ToString()
            //    });
            //}
            //Labels = allMonthData.Select(x => x.Text).ToArray();
            //Data = allMonthData.Select(x => x.Value).ToArray();
            //return Json(new
            //{
            //    Labels,
            //    Data
            //});
            //int monthId = DateTime.ParseExact(sdate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None).Month;
            var firstOftargetMonth = DateTime.ParseExact(sdate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);
            var firstOfNextMonth = DateTime.ParseExact(edate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);
            List<SelectListItem> allDates = new List<SelectListItem>();
            List<SelectListItem> allMonthData = new List<SelectListItem>();
            string[] Labels = new string[] { };
            string[] Data = new string[] { };
            for (DateTime date = firstOftargetMonth; date <= firstOfNextMonth; date = date.AddDays(1))
            {
                allDates.Add(new SelectListItem
                {
                    Text = date.ToString("MMM/dd"),
                    Value = date.ToString("dd")
                });
            }
            foreach (var day in allDates)
            {
                allMonthData.Add(new SelectListItem
                {
                    Text = day.Text,
                    Value = centralOffices.Value.Where(x => x.ReferralDate.ToString("MMM/dd") == (day.Text)).ToList().Count.ToString()
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
        [HttpGet]
        public IActionResult MyReferrals()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MyReferrals(DataTableServerSide model)
        {
            var referrer = myReferralService.GetReferrerByUserId(CurrentUser.UserID);
            if (referrer != null)
            {
                KeyValuePair<int, List<MyReferralsDto>> centralOffices = new KeyValuePair<int, List<MyReferralsDto>>();
                centralOffices = myReferralService.GetMyReferral(model, referrer.Id);
                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = centralOffices.Key,
                    recordsFiltered = centralOffices.Key,
                    data = centralOffices.Value.Select((c, index) => new List<object> {
                    model.start+index+1,
                    c.ReferralDate.ToString("dd/MM/yyyy"),
                    c.FamilyName,
                    c.Mobile,
                    c.CountVoucher>0? "<a href='/referrer/DownloadVoucher/" + c.Id + "' class='btn btn-primary'>Download</a>":"-",
                   ((ReferrersStatus)(int)c.Status).GetDescription(),
                   c.Id
                })
                });
            }
            else
            {
                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = 1,
                    recordsFiltered = 1,
                    data = new object()
                });
            }
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            ReferrerProfileDto model = new ReferrerProfileDto();
            Referrers referrer = myReferralService.GetReferrerByUserId(CurrentUser.UserID);
            if (referrer != null)
            {
                model.ReferrerId = referrer.Id;
                model.CountryID = referrer.Address != null ? referrer.Address.CountryId : 0;
                model.AddressID = referrer.AddressId != null ? referrer.AddressId.Value : 0;

                List<SelectListItem> professionsList = new List<SelectListItem>();

                professionsList = myReferralService.GetReferrerType().Select(c => new SelectListItem
                {
                    Text = c.Name.ToTitle(),
                    Value = c.Id.ToString(),
                    Selected = (((int)referrer.RefTypeId) == c.Id ? true : false)
                }).ToList();
                professionsList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                ViewBag.ProfessionList = professionsList;

                ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
                {
                    Text = c.CountryName.ToTitle(),
                    Value = c.CountryId.ToString()
                }).ToList();

                model.ReferrerName = referrer.Name;
                model.Contact = referrer.Contact.Mobile;
                model.Email = referrer.Contact.Email;
                model.Profession = referrer.ServiceDescription;
                model.ProfessionId = referrer.RefTypeId;
                if (referrer.User != null)
                {
                    model.UserName = referrer.User.UserName.Trim();
                    model.FirstName = referrer.User.FirstName.Trim();
                    model.LastName = referrer.User.LastName.Trim();
                    model.EditPassword = EncryptionUtils.Decrypt(referrer.User.Password, referrer.User.PasswordSalt);
                    model.PasswordQuestion = referrer.User.PasswordQuestion;
                    model.PasswordAnswer = EncryptionUtils.Decrypt(referrer.User.PasswordAnswer, referrer.User.PasswordSalt);
                }
                if (referrer.Address != null)
                {
                    model.PostCode = referrer.Address.Postcode;
                    model.StreetName = referrer.Address.Street;
                    model.HouseName = referrer.Address.HouseName;
                    model.HouseNumber = referrer.Address.HouseNumber;
                    model.City = referrer.Address.City;
                }
            }
            else
            {
                List<SelectListItem> professionsList = new List<SelectListItem>();

                professionsList = myReferralService.GetReferrerType().Select(c => new SelectListItem
                {
                    Text = c.Name.ToTitle(),
                    Value = c.Id.ToString(),
                    Selected = (((int)referrer.RefTypeId) == c.Id ? true : false)
                }).ToList();
                professionsList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
                ViewBag.ProfessionList = professionsList;


                ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
                {
                    Text = c.CountryName.ToTitle(),
                    Value = c.CountryId.ToString()
                }).ToList();
            }


            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProfile(ReferrerProfileDto model)
        {
            try
            {
                Referrers referrer = myReferralService.GetReferrerById(model.ReferrerId);
                if (referrer != null)
                {

                    referrer.Contact.Mobile = model.Contact;
                    referrer.Name = model.FirstName + " " + model.LastName;
                    var result = myReferralService.Save(referrer);

                    var usrDetail = referrer.User;
                    if (result)
                    {
                        if (model.ChangePassword)
                        {
                            //Upodate User login details
                            //usrDetail.UserName = model.UserName.Trim();
                            string randonSalt = Common.GetRandomPasswordSalt();
                            usrDetail.Password = EncryptionUtils.HashPassword(model.EditPassword, randonSalt, DateTime.Now);
                            usrDetail.PasswordQuestion = model.PasswordQuestion;
                            usrDetail.FirstName = model.FirstName;
                            usrDetail.LastName = model.LastName;
                            usrDetail.PasswordAnswer = model.PasswordAnswer;
                            userService.Save(usrDetail, CurrentUser.UserID, false);
                            //End
                        }
                        else
                        {
                            usrDetail.FirstName = model.FirstName;
                            usrDetail.LastName = model.LastName;
                            userService.Save(usrDetail, CurrentUser.UserID, false);
                        }

                        //Update Address
                        if (model.AddressID > 0)
                        {
                            AddUpdateReferrerAddress(model);
                        }
                        //End
                    }
                    ShowSuccessMessage("Success!", "Profile has been updated successfully.", false);
                    return RedirectToAction("updateprofile", "referrer");
                }
                else
                {
                    ShowSuccessMessage("Error!", "Record not found.", false);
                    return RedirectToAction("updateprofile", "referrer");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                return RedirectToAction("updateprofile", "referrer");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool AddUpdateReferrerAddress(ReferrerProfileDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.AddressID > 0)
                    {
                        Fbaddress address = model.AddressID > 0 ? addressService.GetFBAddress(model.AddressID) : new Fbaddress();
                        address.HouseName = model.HouseName;
                        address.HouseNumber = model.HouseNumber;
                        address.Street = model.StreetName;
                        address.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                        address.City = model.City;
                        address.Postcode = model.PostCode == null ? model.OldPostCode : model.PostCode;
                        address.CountryId = model.CountryID.Value;
                        addressService.Save(address, address.Id == 0);
                        return true;
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
            //allergy.Insert(0, new SelectListItem { Text = "Select Allery", Value = "" });

            ViewBag.AllergyList = allergy;
        }
        public IActionResult NewReferrals()
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();
            List<SelectListItem> branches = new List<SelectListItem>();
            if (CurrentUser.CharityID > 0)
            {
                branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.CharityID ?? 0, userID: CurrentUser.UserID).Select(c => new SelectListItem
                {
                    Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                    Value = c.BranchId.ToString()
                }).ToList();
            }
            ViewBag.Branches = branches;
            return View(new FamilyDTo());
        }
        [HttpPost]
        public IActionResult NewReferrals(FamilyDTo familydto)
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();
            List<SelectListItem> branches = new List<SelectListItem>();
            if (CurrentUser.CharityID > 0)
            {
                branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.CharityID ?? 0, userID: CurrentUser.UserID).Select(c => new SelectListItem
                {
                    Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                    Value = c.BranchId.ToString()
                }).ToList();
            }
            ViewBag.Branches = branches;
            var referrer = myReferralService.GetReferrerByUserId(CurrentUser.UserID);
            Family family = new Family();
            family.FamilyName = familydto.FamilyName;
            family.AddedDate = System.DateTime.Now;
            family.ModifiedDate = System.DateTime.Now;
            family.PostponeDate = System.DateTime.Now;
            family.FamilyToken = familydto.FamilyToken ?? "";
            family.LocalAuthCodeId = familydto.LocalAuthCodeId;
            family.DeliveryNote = familydto.DeliveryNote ?? "";
            //family.Confirmed = familydto.Confirmed;
            //family.ConfirmedById = familydto.ConfirmedById;
            family.CentralOfficeId = CurrentUser.OrganisationID;
            family.CharityId = CurrentUser.CharityID;
            family.BranchId = familydto.BranchID;
            family.SelfReffered = (int)eFamilyReferred.ByReferrer;
            family.Email = familydto.Email;
            family.Contactno = familydto.Contactno;
            family.TotalFamily = familydto.TotalFamily;
            family.TotalAdults = familydto.TotalAdults;
            family.TotalChild = familydto.TotalChild;
            family.Active = true;
            familyService.Save(family);

            FamilyAddress fa = new FamilyAddress();
            fa.FamilyId = family.Id;
            fa.AddedDate = System.DateTime.Now;
            fa.Address = new Fbaddress
            {
                HouseName = familydto.HouseName ?? "",
                HouseNumber = familydto.HouseNumber ?? "",
                District = familydto.District ?? "",
                Street = familydto.StreetName ?? "",
                CountryName = "",
                City = familydto.City ?? "",
                CountryId = familydto.CountryID ?? 0,
                Postcode = familydto.PostCode,
            };
            familyService.SaveFamilyAddress(fa);

            ReferrerFamily reff = new ReferrerFamily();
            reff.FamilyId = family.Id;
            reff.ReferrerId = referrer.Id;
            reff.Inward = false;
            reff.ReferralDate = System.DateTime.Now;
            familyService.SaveFamilyreferral(reff);

            FoodbankFamily reff_family = new FoodbankFamily();
            reff_family.FamilyId = family.Id;
            reff_family.FoodbankId = referrer.FoodbankId;
            reff_family.Inward = false;

            familyService.SaveFamilyreferralFoodbank(reff_family);

            var adultarray = familydto.subfamilyisadult2.Split(',');
            var SubFamilyAllergryarry = familydto.SubFamilyAllergries.Split(',');
            for (int i = 0; i < familydto.subfamilyname.Count; i++)
            {
                FamilyMember fnsub = new FamilyMember();
                fnsub.FamilyId = family.Id;
                fnsub.ForeName = familydto.subfamilyname[i];
                familydto.subfamilydob[i] = familydto.subfamilydob[i].Replace("-", "/");
                fnsub.Dob = DateTime.ParseExact(familydto.subfamilydob[i], "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);//DateTime.ParseExact(familydto.subfamilydob[i], "dd/MM/yyyy", null);
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

            ShowSuccessMessage("Success!", "Family has been saved successfully.", false);
            return RedirectToAction("NewReferrals", "Referrer", new { });

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
        public IActionResult DownloadVoucher(int ID)
        {
            var res = voucherService.GetVoucherListByFamilyId(ID);
            string fileName = "Voucher_QRCode" + "_" + DateTime.Now + ".pdf";
            string page = string.Empty;
            foreach (var item in res)
            {
                page += @"<div>
            <section class=''>
            <div class='modal-header'>
            <h4 class='modal-title'>Scan for parcel detail.</h4>
            </div>
            <div style='text-align: center;'>
                <img src='data:image/gif;base64," + item.VoucherQrcode + @"' style='width:60%' />
            </div>
            </section>
            </div>";

            }

            var html = page;
            PdfDocument document = new PdfDocument()
            {
                Html = html
            };

            Pdf pdf = new Pdf();
            pdf.Document = document;
            PdfConvertEnvironment environment = new PdfConvertEnvironment() { WkHtmlToPdfPath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "TempPdfFiles/wkhtmltopdf.exe"), TempFolderPath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "TempPdfFiles/"), Timeout = 60000 };
            pdf.ConvertEnvironment = environment;
            pdf.GeneratePDF();

            pdf.OutputResult.OutputStream.Position = 0;
            var memoryStream = new MemoryStream();
            pdf.OutputResult.OutputStream.CopyTo(memoryStream);
            return File(memoryStream.ToArray(), "application/pdf", fileName);
        }
    }
}
