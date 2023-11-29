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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class FamilyRecordController : BaseController
    {
        private readonly IMyReferralService referrerService;

        private IVoucherService voucherService;
        private IFeedbackService feedbackService;
        private readonly ICountryService countryService;
        private readonly IUserService userService;
        private readonly IQuickDonorGiftService quickDonorGiftService;
        private readonly IRoleService roleService;
        private readonly IFoodbankService foodbankService;
        private IGrantorService grantorService;
        private readonly IFamilyService familyService;
        private readonly IAllergiesService allergyService;
        private ICentralOfficeService centralOfficeService;
        private ICharityService charityService;
        private IBranchService branchService;
        private IAgenciesService agenciesService;
        private readonly IFamilyParcelService familyParcelService;
        public FamilyRecordController(IMyReferralService _referrerService, ICountryService _countryService,
            IUserService _userService, IQuickDonorGiftService _quickDonorGiftService, IRoleService _roleService,
            IFoodbankService _foodbankService, IMyReferralService _ReferralService, IGrantorService _grantorService, IFamilyService _familyService, IAllergiesService _allergyService,
             ICentralOfficeService _centralOfficeService,
            ICharityService _charityService,
            IBranchService _branchService, IVoucherService _voucherService, IFeedbackService _feedbackService, IAgenciesService _agenciesService, IFamilyParcelService _familyParcelService)
        {
            voucherService = _voucherService;
            this.centralOfficeService = _centralOfficeService;
            this.charityService = _charityService;
            this.branchService = _branchService;
            referrerService = _referrerService;
            countryService = _countryService;
            userService = _userService;
            quickDonorGiftService = _quickDonorGiftService;
            roleService = _roleService;
            foodbankService = _foodbankService;
            feedbackService = _feedbackService;
            grantorService = _grantorService;
            familyService = _familyService;
            allergyService = _allergyService;
            agenciesService = _agenciesService;
            familyParcelService = _familyParcelService;
        }

        public IActionResult Index(int? id = null)
        {

            int organisationId = id ?? CurrentUser.OrganisationID;
            ViewBag.OrganisationId = organisationId;
            if (CurrentUser != null && CurrentUser.CharityID.HasValue)
            {
                ViewBag.Branches = branchService.GetBranchesByDataAccessibility(userDataAccess: CurrentUser.DataAccessibilities, roleID: CurrentUser.RoleID, charityid: CurrentUser.CharityID.Value, userID: CurrentUser.UserID).Select(c => new SelectListItem
                {
                    Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                    Value = c.BranchId.ToString()
                }).ToList();
            }
            else if (organisationId > 0)
            {
                ViewBag.Branches = Enumerable.Empty<SelectListItem>().ToList();
                ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(userDataAccess: CurrentUser.DataAccessibilities, roleID: CurrentUser.RoleID, organisationId: organisationId, userID: CurrentUser.UserID).Select(c => new SelectListItem { Text = c.CharityName.AddCharityPrefix(c.Prefix), Value = c.CharityId.ToString() }).ToList();
            }
            ViewBag.Alphabets = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            return View();




        }
        [HttpGet]
        // [DataActionFilter("charityID", DataEnityNames.Charity)]
        public IActionResult BindBranches(int charityID)
        {
            var result = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, charityID, userID: CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();
            result.Insert(0, new SelectListItem("Select Branch", ""));


            ViewBag.Branches = result;
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }
        [HttpPost]
        // [DataActionFilter("id", DataEnityNames.CentralOffice)]
        public IActionResult GetPersons(DataTableServerSide model, int? id, int? CharitID, int? BranchID)
        {
            KeyValuePair<int, List<MyReferralsDto>> referrerlist = new KeyValuePair<int, List<MyReferralsDto>>();
            referrerlist = familyService.GetMyFamilyByFoodbank(model, CurrentUser.DataAccessibilities, CurrentUser.FoodbankId, CurrentUser.RoleID, userId: CurrentUser.UserID,
                organisationId: GetOrgLevelID(id, UserRoles.Organisation), charityId: CharitID, branchId: BranchID);

            return Json(new
            {
                draw = model.draw,
                recordsTotal = referrerlist.Value.Count,
                recordsFiltered = referrerlist.Value.Count,
                data = referrerlist.Value.Select((c, index) => new List<object> {
                   string.Format("{0}",string.Format("{0}", c.FamilyName)),
                  c.Id,
                   ""
                   })
            });

        }
        [HttpGet]
        //[DataActionFilter("id", DataEnityNames.Person)]
        public IActionResult EditPerson(int id)
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();
            var res = familyService.GetFamilyMoreDetails(id);
            ViewBag.resaddress = familyService.GetFamilyAddessDetails(res.FamilyAddress.Count > 0 ? res.FamilyAddress.FirstOrDefault().AddressId : 0);
            if (res.FamilyMember.Count > 0)
            {
                foreach (var item in res.FamilyMember)
                {
                    item.FamilyMemberAllergy = familyService.GetFamilyMemberAllergyDetails(item.Id).Where(x => x.FamilyMemberId == item.Id).ToList();
                }
            }
            var model = FamilyMapper.EditFamilyMapper(res);
            model.Agencieslist = agenciesService.GetAgencyByFoodbankId(CurrentUser.FoodbankId);
            ViewBag.Agencieslist = agenciesService.GetAgencyByFoodbankId(CurrentUser.FoodbankId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            return PartialView("_Family", model);
        }
        [HttpGet]
        //[DataActionFilter("id", DataEnityNames.Person)]
        public IActionResult UpdatePersonName(int id)
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();
            var res = familyService.GetFamilyDetails(id);
            ViewBag.resaddress = familyService.GetFamilyAddessDetails(res.FamilyAddress.Count > 0 ? res.FamilyAddress.FirstOrDefault().AddressId : 0);
            if (res.FamilyMember.Count > 0)
            {
                foreach (var item in res.FamilyMember)
                {
                    item.FamilyMemberAllergy = familyService.GetFamilyMemberAllergyDetails(item.Id).Where(x => x.FamilyMemberId == item.Id).ToList();
                }
            }

            var model = FamilyMapper.AdminEditFamilyMapper(res);
            return PartialView("_EditFamilyName", model);
        }


        /// <summary>
        /// save edit person name deails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[DataActionFilterAttribute("id", DataEnityNames.Donor)]
        public IActionResult UpdatePersonName(AdminEditFamilyDto familydto)
        {
            Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>();
            dictDataIds.Add(DataEnityNames.CentralOffice, familydto.CentralOfficeID);
            dictDataIds.Add(DataEnityNames.Charity, familydto.CharityID);
            dictDataIds.Add(DataEnityNames.Branch, familydto.BranchID);
            dictDataIds.Add(DataEnityNames.Person, familydto.Id);

            try
            {

                BindCountriesViewBag();   //Bind country dropdown with default value value UK
                BindAlleriesViewBag();
                var family = familyService.GetFamilyDetails(familydto.Id);
                family.FamilyName = familydto.FamilyName;
                family.ModifiedDate = System.DateTime.Now;
                family.Email = familydto.Email;
                family.Contactno = familydto.Contactno;
                family.LocalAuthCodeId = Convert.ToInt32(familydto.LocalAuthCodeId);
                family.DeliveryNote = familydto.DeliveryNote ?? "";
                family.ParcelDeliverDate = DateTime.ParseExact(familydto.ParcelDeliverDate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);
                family.Gdprpreferences = familydto.GDPRPreferences;
                family.OtherAgency = familydto.OtherAgency == 1 ? true : false;
                familyService.UpdateFamily(family);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "Family contact updated successfully." });
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                ModelState.AddModelError("Error!", message);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = message });

            }

            return CreateModelStateErrors();
        }


        [HttpGet]
        //[DataActionFilter("id", DataEnityNames.Person)]
        public IActionResult UpdateFamilyAddress(int id)
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();
            var res = familyService.GetFamilyDetails(id);
            ViewBag.resaddress = familyService.GetFamilyAddessDetails(res.FamilyAddress.Count > 0 ? res.FamilyAddress.FirstOrDefault().AddressId : 0);
            if (res.FamilyMember.Count > 0)
            {
                foreach (var item in res.FamilyMember)
                {
                    item.FamilyMemberAllergy = familyService.GetFamilyMemberAllergyDetails(item.Id).Where(x => x.FamilyMemberId == item.Id).ToList();
                }
            }

            var model = FamilyMapper.AdminEditFamilyMapper(res);
            return PartialView("_EditFamilyAddress", model);
        }


        /// <summary>
        /// save edit person name deails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[DataActionFilterAttribute("id", DataEnityNames.Donor)]
        public IActionResult UpdateFamilyAddress(AdminEditFamilyDto familydto)
        {
            Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>();
            dictDataIds.Add(DataEnityNames.CentralOffice, familydto.CentralOfficeID);
            dictDataIds.Add(DataEnityNames.Charity, familydto.CharityID);
            dictDataIds.Add(DataEnityNames.Branch, familydto.BranchID);
            dictDataIds.Add(DataEnityNames.Person, familydto.Id);

            try
            {

                BindCountriesViewBag();   //Bind country dropdown with default value value UK

                if (familydto.Addressid == null)
                {
                    FamilyAddress fa = new FamilyAddress();
                    fa.FamilyId = familydto.Id;
                    fa.AddedDate = System.DateTime.Now;
                    fa.Address = new Fbaddress
                    {
                        HouseName = familydto.HouseName ?? "",
                        HouseNumber = familydto.HouseNumber ?? "",
                        District = familydto.District ?? "",
                        Street = familydto.StreetName ?? "",
                        CountryName = "",
                        City = (familydto.City == null ? "" : familydto.City),
                        CountryId = familydto.CountryID ?? 0,
                        Postcode = familydto.PostCode,
                    };
                    familyService.SaveFamilyAddress(fa);
                }
                else
                {
                    Fbaddress fa = familyService.GetFamilyAddessDetails(familydto.Addressid ?? 0);
                    fa.HouseName = familydto.HouseName ?? "";
                    fa.HouseNumber = familydto.HouseNumber ?? "";
                    fa.District = familydto.District ?? "";
                    fa.Street = familydto.StreetName ?? "";
                    fa.City = (familydto.City == null ? "" : familydto.City);
                    fa.CountryId = familydto.CountryID ?? 0;
                    fa.Postcode = familydto.PostCode;
                    familyService.SaveFbAddress(fa);

                }
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "Family address updated successfully." });
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                ModelState.AddModelError("Error!", message);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = message });
            }

            return CreateModelStateErrors();
        }
        [HttpGet]
        //[DataActionFilter("id", DataEnityNames.Person)]
        public IActionResult UpdateFamilyMember(int id)
        {
            BindAlleriesViewBag();
            var res = familyService.GetFamilyDetails(id);
            ViewBag.resaddress = familyService.GetFamilyAddessDetails(res.FamilyAddress.Count > 0 ? res.FamilyAddress.FirstOrDefault().AddressId : 0);
            if (res.FamilyMember.Count > 0)
            {
                foreach (var item in res.FamilyMember)
                {
                    item.FamilyMemberAllergy = familyService.GetFamilyMemberAllergyDetails(item.Id).Where(x => x.FamilyMemberId == item.Id).ToList();
                }
            }

            var model = FamilyMapper.AdminEditFamilyMapper(res);
            return PartialView("_EditFamilyMember", model);
        }


        /// <summary>
        /// save edit person name deails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[DataActionFilterAttribute("id", DataEnityNames.Donor)]
        public IActionResult UpdateFamilyMember(AdminEditFamilyDto familydto)
        {
            Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>();
            dictDataIds.Add(DataEnityNames.CentralOffice, familydto.CentralOfficeID);
            dictDataIds.Add(DataEnityNames.Charity, familydto.CharityID);
            dictDataIds.Add(DataEnityNames.Branch, familydto.BranchID);
            dictDataIds.Add(DataEnityNames.Person, familydto.Id);

            try
            {

                BindAlleriesViewBag();
                var family = familyService.GetFamilyDetails(familydto.Id);
                family.TotalFamily = familydto.TotalFamily;
                family.TotalChild = familydto.TotalChild;
                family.TotalAdults = familydto.TotalAdults;
                familyService.UpdateFamily(family);
                var adultarray = familydto.subfamilyisadult2.Split(',');
                var SubFamilyAllergryarry = familydto.SubFamilyAllergries.Split(',');
                for (int i = 0; i < familydto.subfamilyname.Count; i++)
                {
                    FamilyMember fnsub;
                    if (familydto.subfamilynameIds.Count - 1 >= i)
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

                    fnsub.FamilyId = familydto.Id;
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



                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "Family members updated successfully." });

            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                ModelState.AddModelError("Error!", message);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = message });
            }
            return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "" });
        }




        [HttpGet]
        //  [DataActionFilter("orgId", DataEnityNames.CentralOffice)]
        public IActionResult BindBranchesForFilter(int orgId, int? charityID = null)
        {
            List<SelectListItem> branches = new List<SelectListItem>();
            if (charityID.HasValue)
            {
                branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, charityID.Value, userID: CurrentUser.UserID).Select(c => new SelectListItem
                {
                    Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                    Value = c.BranchId.ToString()
                }).ToList();
            }
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = branches });
        }
        public IActionResult AddFamily()
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();

            int organisationId = CurrentUser.OrganisationID;
            ViewBag.OrganisationId = organisationId;




            ViewBag.Branches = Enumerable.Empty<SelectListItem>().ToList();
            ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(userDataAccess: CurrentUser.DataAccessibilities, roleID: CurrentUser.RoleID, organisationId: organisationId, userID: CurrentUser.UserID).Select(c => new SelectListItem { Text = c.CharityName.AddCharityPrefix(c.Prefix), Value = c.CharityId.ToString() }).ToList();

            return View(new AdminEditFamilyDto());
        }
        [HttpPost]
        public IActionResult AddFamily(AdminEditFamilyDto addFamilyDto)
        {
            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            BindAlleriesViewBag();

            var family = addFamilyDto.Id > 0 ? familyService.GetFamilyDetails(addFamilyDto.Id) : new Family();
            family.FamilyName = addFamilyDto.FamilyName;
            family.Email = addFamilyDto.Email;
            family.Contactno = addFamilyDto.Contactno;
            family.AddedDate = System.DateTime.Now;
            family.ModifiedDate = System.DateTime.Now;
            family.LocalAuthCodeId = Convert.ToInt32(addFamilyDto.LocalAuthCodeId);
            family.DeliveryNote = addFamilyDto.DeliveryNote ?? "";
            family.CentralOfficeId = CurrentUser.OrganisationID;
            family.CharityId = addFamilyDto.CharityID;
            family.BranchId = addFamilyDto.BranchID;
            family.PostponeDate = System.DateTime.Now;
            family.ParcelDeliverDate = DateTime.ParseExact(addFamilyDto.ParcelDeliverDate, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);
            family.Gdprpreferences = addFamilyDto.GDPRPreferences;
            family.OtherAgency = addFamilyDto.OtherAgency == 1 ? true : false;
            family.FamilyToken = addFamilyDto.FamilyToken ?? "";
            family.SelfReffered = (int)eFamilyReferred.Manual;
            family.TotalFamily = addFamilyDto.TotalFamily;
            family.TotalAdults = addFamilyDto.TotalAdults;
            family.TotalChild = addFamilyDto.TotalChild;
            family.Active = true;
            familyService.Save(family);
            FamilyAddress fa = new FamilyAddress();
            fa.FamilyId = family.Id;
            fa.AddedDate = System.DateTime.Now;
            fa.Address = new Fbaddress
            {
                HouseName = addFamilyDto.HouseName ?? "",
                HouseNumber = addFamilyDto.HouseNumber ?? "",
                District = addFamilyDto.District ?? "",
                Street = addFamilyDto.StreetName ?? "",
                CountryName = "",
                City = (addFamilyDto.City == null ? "" : addFamilyDto.City),
                CountryId = addFamilyDto.CountryID ?? 0,
                Postcode = addFamilyDto.PostCode,
            };
            familyService.SaveFamilyAddress(fa);
            FoodbankFamily reff = new FoodbankFamily();
            reff.FamilyId = family.Id;
            reff.FoodbankId = CurrentUser.FoodbankId;
            reff.Inward = false;
            familyService.SaveFamilyreferralFoodbank(reff);

            var adultarray = addFamilyDto.subfamilyisadult2.Split(',');
            var SubFamilyAllergryarry = addFamilyDto.SubFamilyAllergries.Split(',');
            for (int i = 0; i < addFamilyDto.subfamilyname.Count; i++)
            {
                FamilyMember fnsub;
                fnsub = new FamilyMember();
                fnsub.FamilyId = family.Id;
                fnsub.ForeName = addFamilyDto.subfamilyname[i];
                addFamilyDto.subfamilydob[i] = addFamilyDto.subfamilydob[i].Replace("-", "/");
                fnsub.Dob = DateTime.ParseExact(addFamilyDto.subfamilydob[i], "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);
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
            return RedirectToAction("Index", "FamilyRecord", new { });

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
        public IActionResult FeedbackFamilyView(int Familyid)
        {
            return PartialView("_FeedbackListView", new FamilyDTo { Id = Familyid });
        }

        [HttpPost]
        //  [DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult GetFeedbackByfamilyid(DataTableServerSide model, int familyid)
        {
            KeyValuePair<int, List<FeedbackDto>> centralOffices = new KeyValuePair<int, List<FeedbackDto>>();
            centralOffices = feedbackService.GetFeedbackListByFoodbank(model, CurrentUser.FoodbankId, familyid);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    model.start+index+1,
                    c.DateCompletd?.ToString("dd/MM/yyyy"),
                    c.FamilyName,
                    c.DeliveryDate,
                    c.ParcelTypeName,
                    c.PackingDate,
                    c.Id
                   })
            });
        }

        [HttpGet]

        public IActionResult FeedbackListViewone(int id)
        {
            var res = feedbackService.GetFeedbackDetailsByFeedbackID(id);

            return PartialView("_FeedbackListViewone", res);

        }
        public IActionResult VoucherFamilyView(int Familyid)
        {
            return PartialView("_VoucherFamily",new FamilyDTo { Id = Familyid } );
            return PartialView("_VoucherFamily", new FamilyDTo { Id = Familyid });
        }

        [HttpPost]
        //  [DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult BindVoucherListByFamily(DataTableServerSide model, int familyid)
        {
            KeyValuePair<int, List<VoucherDto>> voucher = new KeyValuePair<int, List<VoucherDto>>();
            voucher = voucherService.GetVoucherListByFamilyId(model, familyid);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = voucher.Key,
                recordsFiltered = voucher.Key,
                data = voucher.Value.Select((c, index) => new List<object> {
                    c.VoucherId,
                    model.start+index+1,
                    $"{c.ReferrerName}",
                    $"{c.FamilyName}",
                    c.AddedDate.ToString("MM/dd/yyyy"),
                    c.RedeemedDate!=null? c.RedeemedDate.Value.ToString("MM/dd/yyyy"):"-",
                    c.VoucherToken
                })
            });
        }

        [HttpGet]
        public IActionResult AgencyView(int personId)
        {
            AgenciesFamilyDto rel = new AgenciesFamilyDto();
            rel.Familyid = personId;
            rel.Agencieslist = agenciesService.GetAgencyByFoodbankId(CurrentUser.FoodbankId);
            ViewBag.Agencieslist = agenciesService.GetAgencyByFoodbankId(CurrentUser.FoodbankId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();
            //return PartialView("_FamilyAgencyView", rel);
            return PartialView("_AgencyFamily", rel);
        }
        //public IActionResult AgencyFamily(int Familyid)
        //{
        //    return PartialView("_AgencyFamily", new FamilyDTo { Id = Familyid });
        //}
        //public IActionResult ParcelFamily(int Familyid)
        //{
        //    return PartialView("_ParcelFamily", new FamilyDTo { Id = Familyid });
        //}
        //public IActionResult VoucherFamily(int Familyid)
        //{
        //    return PartialView("_VoucherFamily", new FamilyDTo { Id = Familyid });
        //}

        /// <summary>
        /// To get the notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //  [DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult GetAgencyByfamilyId(DataTableServerSide model, int personId)
        {
            KeyValuePair<int, List<AgenciesDto>> grantor = new KeyValuePair<int, List<AgenciesDto>>();
            grantor = agenciesService.GetGrantorListByFamilyid(model, personId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = grantor.Key,
                recordsFiltered = grantor.Key,
                data = grantor.Value.Select((c, index) => new List<object> {
                    c.AgencyId,
                    model.start+index+1,
                    c.AgencyName,
                    c.Email,
                    c.ContactNumber,
                    "<a data-id='" +  c.AgencyId+ "' class='btn btn-danger grid-btn btn-sm ps3 agcecyremoveforfamily delete-btn'>Remove Agecny <i class='fa fa-trash-o'></i></a>&nbsp;"

                    ,
                })
            });
        }
        public IActionResult PacelView(int personId)
        {
            AgenciesFamilyDto rel = new AgenciesFamilyDto();
            rel.Familyid = personId;
            return PartialView("_FamilyParcelView", rel);
        }


        /// <summary>
        /// To get the notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //  [DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult GetParcelByfamilyId(DataTableServerSide model, int personId)
        {
            KeyValuePair<int, List<FamilyParcelDto>> parcelType = new KeyValuePair<int, List<FamilyParcelDto>>();
            parcelType = familyParcelService.GetFamilyParcelListByFamilyID(model, personId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = parcelType.Key,
                recordsFiltered = parcelType.Key,
                data = parcelType.Value.Select((c, index) => new List<object> {
                    c.ParcelId,
                    model.start+index+1,
                    c.ParcelType,
                    c.FamilyName,
                    Extensions.ToFormatCustomString(c.DueDate),
                    c.DeliveredDate,
                    ((ParcelStatus)c.Status).GetDescription(),
                    //"<a href=" + Url.Action("view", "familyparcel", new { id = c.ParcelId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>",
                })
            });
        }

        [HttpGet]
        public IActionResult FamilyagencyDelete(int id)
        {
            string message;
            try
            {
                agenciesService.DeleteFamilyAgency(id);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "Associate agency has been removed successfully." });
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = message });
            }
        }
        [HttpPost]
        public IActionResult FamilyagencySave(int agencyid, int familyid)
        {
            string message;
            try
            {
                if (agencyid == 0)
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = " Please select agecny." });

                }
                var list = agenciesService.GetFamilyAgecnyByFamilyid(familyid, agencyid);
                if (list.Count > 0)
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = " Agency already associated with family." });

                }
                FamilyAgency fm = new FamilyAgency();
                fm.AgencyId = agencyid;
                fm.FamilyId = familyid;
                agenciesService.SaveFamilyAgency(fm);
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = " Agency associated successfully." });
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = message });
            }
        }
        [HttpGet]
        public IActionResult DeleteFamily(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this Family ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Family " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }
        [HttpPost]
        public string DeleteFamily(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var myfamily = familyService.GetFamilyDetails(id);

                if (myfamily != null)
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
                var myfamily = familyService.GetFamilyDetails(id);
                if (myfamily != null)
                {
                    myfamily.Confirmed = true;
                    myfamily.ConfirmedById = CurrentUser.FoodbankId;
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
                    myfamily.ConfirmedById = CurrentUser.FoodbankId;
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
        protected override void Dispose(bool disposing)
        {

            //if (centralOfficeService != null)
            //{
            //    centralOfficeService.Dispose();
            //    centralOfficeService = null;
            //}

            //if (charityService != null)
            //{
            //    charityService.Dispose();
            //    charityService = null;
            //}

            //if (branchService != null)
            //{
            //    branchService.Dispose();
            //    branchService = null;
            //}

            //if (personService != null)
            //{
            //    personService.Dispose();
            //    personService = null;
            //}

            //if (countryService != null)
            //{
            //    countryService.Dispose();
            //    countryService = null;
            //}

            //if (houseHoldService != null)
            //{
            //    houseHoldService.Dispose();
            //    houseHoldService = null;
            //}

            //if (userPreferenceService != null)
            //{
            //    userPreferenceService.Dispose();
            //    userPreferenceService = null;
            //}

            //if (addressService != null)
            //{
            //    addressService.Dispose();
            //    addressService = null;
            //}

            //if (membershipTypeService != null)
            //{
            //    membershipTypeService.Dispose();
            //    membershipTypeService = null;
            //}

            //if (userService != null)
            //{
            //    userService.Dispose();
            //    userService = null;
            //}

            //if (maritalStatusService != null)
            //{
            //    maritalStatusService.Dispose();
            //    maritalStatusService = null;
            //}

            //if (quickDonorGiftService != null)
            //{
            //    quickDonorGiftService.Dispose();
            //    quickDonorGiftService = null;
            //}

            //if (donationService != null)
            //{
            //    donationService.Dispose();
            //    donationService = null;
            //}

            //if (groupService != null)
            //{
            //    groupService.Dispose();
            //    groupService = null;
            //}

            base.Dispose(disposing);
        }

    }
}
