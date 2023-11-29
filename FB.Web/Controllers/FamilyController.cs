using FB.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace FB.Web.Controllers
{
    public class FamilyController : BaseController
    {
        private readonly ICountryService countryService;
        private readonly IFamilyService familyService;
        private ICentralOfficeService centralofficeService;
        private IBranchService branchService;
        private readonly IAllergiesService allergyService;
        private readonly IMyReferralService myReferralService;
        private readonly IFoodbankService foodbankService; 
        private ICharityService charityService;

        public FamilyController(ICentralOfficeService _centralofficeService, IBranchService _branchService, ICountryService _countryService, IFamilyService _familyService, IAllergiesService _allergyService, IMyReferralService _myReferralService, IFoodbankService _foodbankService, ICharityService _charityService )
        {
            countryService = _countryService;
            familyService = _familyService;
            centralofficeService = _centralofficeService;
            branchService = _branchService;
            allergyService = _allergyService;
            myReferralService = _myReferralService;
            foodbankService = _foodbankService;
            this.charityService = _charityService;
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
        public void BindOrganisationViewBag()
        {
            ViewBag.Organisations = centralofficeService.GetCentralOffices().Select(c => new SelectListItem
            {
                Text = c.OrganisationName,
                Value = c.CentralOfficeId.ToString()
            }).ToList();

            ViewBag.Charities = new List<SelectListItem>();
            ViewBag.Branches = new List<SelectListItem>();
        }
        [NonAction]
        public void BindAlleriesViewBag(int? id = null)
        {
            ViewBag.AllergyList = allergyService.GetAllergies().Select(c => new SelectListItem
            {
                Text = c.Name.ToTitle(),
                Value = c.Id.ToString(),

            }).ToList();
        }
        public IActionResult AddFamily(string id = "")
        {
            if (CurrentUser.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            string foodbanktoken = id;
            BindAlleriesViewBag();
            //if(FoodbankId>0)
            //{
            //    var enumData = from eFamilyReferred e in Enum.GetValues(typeof(eFamilyReferred))

            //                   select new
            //               {
            //                   ID = (int)e,
            //                   Name = e.ToString()
            //               };
            //     enumData = enumData.Where(x=>x.ID==(int)eFamilyReferred.Manual);
            //     ViewBag.SelfRefferedList = new SelectList(enumData, "ID", "Name");
            //}
            //else
            //{
            var enumData = from eFamilyReferred e in Enum.GetValues(typeof(eFamilyReferred))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            enumData = enumData.Where(x => x.ID != (int)eFamilyReferred.Manual);
            ViewBag.SelfRefferedList = new SelectList(enumData, "ID", "Name");
            //}
            var referrallist = myReferralService.GetAllReferral().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),



            }).ToList();
            ViewBag.ReferralList = referrallist;

            BindCountriesViewBag();   //Bind country dropdown with default value value UK
            ViewBag.Charities = new List<SelectListItem>();
            ViewBag.Branches = new List<SelectListItem>();


            BindOrganisationViewBag();
            var foodbank = foodbankService.GetFoodbankByToken(foodbanktoken);
            ViewBag.Branches = new List<SelectListItem>();
            AddFamilyDto model = new AddFamilyDto();
            
            if (foodbank != null)
            {
                model.FoodbankId = foodbank.Id;

                model.CentralOfficeID = foodbank.User.CentralOfficeId??0;
                model.BranchName = foodbank.User.CentralOfficeNavigation.OrganisationName;
            }
            else
            {
                model.BranchName = string.Empty;
            }
            int organisationId = model.CentralOfficeID;
            ViewBag.OrganisationId = organisationId;

            model.FamilyToken = foodbanktoken;


            ViewBag.Branches = Enumerable.Empty<SelectListItem>().ToList();
           
            ViewBag.Charities = charityService.GetCharitiesByDataAccessibility(userDataAccess: Common.GetDataAccessibility(foodbank.User.UserRole.RoleId, foodbank.UserId ?? 0), roleID: foodbank.User.UserRole.RoleId, organisationId: organisationId, userID: foodbank.UserId??0).Select(c => new SelectListItem { Text = c.CharityName.AddCharityPrefix(c.Prefix), Value = c.CharityId.ToString() }).ToList();

            return View(model);
        }
        [HttpGet]
        //  [DataActionFilter("orgId", DataEnityNames.CentralOffice)]
        public IActionResult BindBranches(int charityID,string foodbanktoken)
        {
            if (charityID == 0 || charityID == null)
            {
                var result1 = new List<SelectListItem>();
                result1.Insert(0, new SelectListItem("Select Branch", ""));
                return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result1 });
            }

            var foodbank = foodbankService.GetFoodbankByToken(foodbanktoken);
            var result = branchService.GetBranchesByDataAccessibility(Common.GetDataAccessibility(foodbank.User.UserRole.RoleId, foodbank.UserId ?? 0), foodbank.User.UserRole.RoleId, charityID, userID: foodbank.UserId??0).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();
            result.Insert(0, new SelectListItem("Select Branch", ""));
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }
        [HttpPost]
        public IActionResult AddFamily(AddFamilyDto addFamilyDto)
        {
            BindCountriesViewBag(); //Bind country dropdown with default value value UK
            var referrallist = myReferralService.GetAllReferral().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),

            }).ToList();
            ViewBag.ReferralList = referrallist;
            BindAlleriesViewBag();

            Family family = new Family();
            family.FamilyName = addFamilyDto.FamilyName;
            family.Email = addFamilyDto.Email;
            family.Contactno = addFamilyDto.Contactno;
            family.AddedDate = System.DateTime.Now;
            family.ModifiedDate = System.DateTime.Now;
            family.PostponeDate = System.DateTime.Now;
            //family.LocalAuthCodeId = addFamilyDto.LocalAuthCodeId;
            //family.DeliveryNote = addFamilyDto.DeliveryNote ?? "";
            //family.VoucherNumber = addFamilyDto.VoucherNumber;
            //family.CentralOfficeId = addFamilyDto.CentralOfficeID;
            //family.CharityId = addFamilyDto.CharityID;
            //family.BranchId = addFamilyDto.BranchID;
            //family.ParcelDeliverDate = addFamilyDto.ParcelDeliver;
            //family.Gdprpreferences = addFamilyDto.GDPRPreferences;
            //family.OtherAgency = addFamilyDto.OtherAgency == 1 ? true : false;
            family.CentralOfficeId = addFamilyDto.CentralOfficeID;
            family.BranchId = addFamilyDto.BranchID;
            family.CharityId = addFamilyDto.CharityID;
            family.FamilyToken = addFamilyDto.FamilyToken ?? "";
            family.Confirmed = addFamilyDto.Confirmed;
            family.ConfirmedById = addFamilyDto.ConfirmedById;
            family.SelfReffered = (int)eFamilyReferred.Self;            
            //family.Allergies = addFamilyDto.Allergies;
            family.TotalFamily = addFamilyDto.TotalFamily;
            family.TotalAdults = addFamilyDto.TotalAdults;
            family.TotalChild = addFamilyDto.TotalChild;
            family.Active=true;
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

            if (addFamilyDto.SelfReffered == (int)eFamilyReferred.ByReferrer)
            {
                ReferrerFamily reffuser = new ReferrerFamily();
                reffuser.FamilyId = family.Id;
                reffuser.ReferrerId = addFamilyDto.ReferralId;
                reffuser.Inward = false;
                reffuser.ReferralDate = System.DateTime.Now;
                familyService.SaveFamilyreferral(reffuser);

            }

            FoodbankFamily reff = new FoodbankFamily();
            reff.FamilyId = family.Id;
            reff.FoodbankId = addFamilyDto.FoodbankId;
            reff.Inward = false;
           
            familyService.SaveFamilyreferralFoodbank(reff);

            var adultarray = addFamilyDto.subfamilyisadult2.Split(',');
            var SubFamilyAllergryarry = addFamilyDto.SubFamilyAllergries.Split(',');
            for (int i = 0; i < addFamilyDto.subfamilyname.Count; i++)
            {

                FamilyMember fnsub = new FamilyMember();
                fnsub.FamilyId = family.Id;
                fnsub.ForeName = addFamilyDto.subfamilyname[i];
                addFamilyDto.subfamilydob[i] = addFamilyDto.subfamilydob[i].Replace("-", "/");
                fnsub.Dob = DateTime.ParseExact(addFamilyDto.subfamilydob[i], "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None);//DateTime.ParseExact(familydto.subfamilydob[i], "dd/MM/yyyy", null);
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
            return RedirectToAction("AddFamily", "Family", new { });

        }
    }
}
