using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
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

namespace FB.Web.Controllers
{
    [CustomActionFilterAttribute(UserRoles.Donor)]
  
    public class DonorController : BaseController
    {
        private readonly IPersonService personService;
        private readonly IUserService userService;
        private readonly ICountryService countryService;
        private readonly IAddressService addressService;
        private readonly IMapper mapper;
        private readonly IDonorService donor;
        private readonly IFoodService foodService;
        public DonorController(IPersonService _personService, IUserService _userService, ICountryService _countryService, IAddressService _addressService, IMapper _mapper, IDonorService _donor,
            IFoodService _foodService)
        {
            personService = _personService;
            userService = _userService;
            countryService = _countryService;
            addressService = _addressService;
            mapper = _mapper;
            donor = _donor;
            foodService = _foodService;
        }
        
        public IActionResult Index()
        {
            var user = userService.GetUser(CurrentUser.UserID);
            ViewBag.FoodDonation = donor.GetFoodDonationCount(user.PersonId.Value);
            ViewBag.CashDonation = donor.GetCashDonationCount(user.PersonId.Value);
            ViewBag.CardDonation = donor.GetCardDonationCount(user.PersonId.Value);

            var totalDonation = ViewBag.FoodDonation + ViewBag.CashDonation + ViewBag.CardDonation;
            ViewBag.TotalDonation = totalDonation;

            return View();
        }

        #region Donor Profile Section
        [HttpGet]
        public IActionResult UpdateProfile(int id)
        {
            var user = userService.GetUser(id);
            var person = personService.GetPersonById(user.PersonId.Value);

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
                UserId = id,
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
        public ActionResult UpdateProfile(EditPersonDto model)
        {
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
                return RedirectToAction("updateprofile", "donor", new { id = model.UserId });
            }
            catch (Exception ex)
            {

                ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                return RedirectToAction("updateprofile", "donor", new { id = model.UserId });
            }
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

        /// <summary>
        /// To open the popup to change the donor address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        #region Donor My Donation Section
        [HttpGet]
        public IActionResult MyDonation()
        {
            DeclarationDto model = new DeclarationDto();
            var User = userService.GetUser(CurrentUser.UserID);
            if (User != null)
            {
                model.UserName = User.UserName;
                model.Reference = User.Person.Reference;
                model.PersonId = User.Person.PersonId;
            }


            return View(model);
        }
        [HttpPost]
        public IActionResult MyDonation(DataTableServerSide model, string personId)
        {
            KeyValuePair<int, List<DonorDonationDto>> centralOffices = new KeyValuePair<int, List<DonorDonationDto>>();
            centralOffices = donor.GetFoodDonations(model, Convert.ToInt32(personId));
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    c.FoodItemId,
                    model.start+index+1,
                    c.DonationDate.ToString("dd/MM/yyyy"),
                    c.FoodItemName,
                    c.Quantity + c.QuantityUnit,
                    ((DonationStatus)c.Status).GetDescription()
                })
            });
        }

        /// <summary>
        /// To get or open the partial view to create Donation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddDonation(int? id = null)
        {
            var user = userService.GetUser(CurrentUser.UserID);

            DonorDonationDto model = new DonorDonationDto();
            model.DonorId = user.PersonId.Value;
            model.Refrence = user.Person.Reference;
            model.DonationType = (int)TypeOfDonation.Food;
            model.DonationTypeName = TypeOfDonation.Food.GetDescription();

            var typeList = Enum.GetValues(typeof(TypeOfDonation));
            List<SelectListItem> typeOfDonationList = new List<SelectListItem>();
            foreach (var items in typeList)
            {
                typeOfDonationList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((TypeOfDonation)(int)items).GetDescription(),
                    Selected = (int)TypeOfDonation.Food == (int)items ? true : false
                });
            }
            ViewBag.DonationTypeList = typeOfDonationList;

            var enumList = Enum.GetValues(typeof(FoodQuantityList));
            List<SelectListItem> foodItemList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                foodItemList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((FoodQuantityList)(int)items).GetDescription(),
                });
            }

            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

            var foodItemCatories = OpenFoodApi.GetFoodItemCatogoriesList();
            var foodlist = new List<SelectListItem>();

            foodItemCatories.Insert(0, selectvaule);
            ViewBag.FoodItemCatogoriesList = foodItemCatories;

            foodlist.Insert(0, selectvaule);
            ViewBag.FoodItemList = foodlist;

            ViewBag.QuantityList = foodItemList;
            return PartialView("_AddEditDonation", model);
        }

        /// <summary>
        /// To get or open the partial view to create Donation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddDonation(DonorDonationDto model)
        {
            try
            {
                FoodItem objFoodItem = new FoodItem
                {
                    Donorid = model.DonorId,
                    Foodid = model.FoodItemId,
                    Quntity = Convert.ToInt32(model.Quantity),
                    QuantityUnit = model.QuantityUnit,
                    AddedDate = DateTime.Now,
                    CauseofDonation = model.CauseofDonation,
                    ExpiryDate = model.DonationDate,
                    Status = (int)DonationStatus.PledgedAndDonated
                };

                if (!foodService.GetAllFoodList().Select(x => x.ProductIdApi).Contains(model.ProductApiId.ToString()))
                {
                    Food obj = new Food()
                    {
                        ProductIdApi = model.ProductApiId.ToString(),
                        Name = model.FoodItemName,
                        CategoryApiId = model.FoodCategoryId
                    };
                    objFoodItem.Food = obj;
                }
                else
                {
                    objFoodItem.Foodid = Convert.ToInt32(foodService.GetAllFoodList().Where(x => x.ProductIdApi == model.ProductApiId.ToString()).FirstOrDefault().Id);
                }

                foodService.SaveFoodDonation(objFoodItem);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Donation has been saved successfully.", IsSuccess = true });
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
        }

        [HttpPost]
        public IActionResult MyDonationPayment(DataTableServerSide model, string personId)
        {
            KeyValuePair<int, List<DonorDonationPaymentDto>> centralOffices = new KeyValuePair<int, List<DonorDonationPaymentDto>>();
            centralOffices = donor.GetFoodDonationPayment(model, Convert.ToInt32(personId));
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    c.PaymentGateway,
                    model.start+index+1,
                    c.CreatedDate.Value.ToString("dd/MM/yyyy"),
                    c.PaymentGateway,
                    c.PaymentGateway=="Cash"?"-": "Credit Card",
                    c.Amount,
                    c.IsGADecleared?"Yes":"No"
                })
            });
        }

        [HttpGet]
        public IActionResult IsDeclarationNotExist(Dictionary<string, string> declarationDates)
        {
            try
            {
                DateTime SignedDate, ValidFrom, ValidTo;
                var flag = true;
                if (DateTime.TryParse(declarationDates["ValidTo"], out ValidTo))
                {
                    var declarationHistories = personService.GetDeclarationHistories(Convert.ToInt32(declarationDates["PersonID"]));

                    if (declarationHistories != null && declarationHistories.Count() > 0)
                    {
                        bool isSigDt = DateTime.TryParse(declarationDates["SignedDate"], out SignedDate);
                        bool isVFDt = DateTime.TryParse(declarationDates["ValidFrom"], out ValidFrom);

                        flag = declarationHistories.Count(e => e.DateDeclarationValidTo == ValidTo && (isSigDt ? e.DateDeclarationSigned == SignedDate : true) && (isVFDt ? e.DateDeclarationValidFrom == ValidFrom : true)) <= 0;
                    }

                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = flag, Data = "" });
                }
                else
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = "Valid to date should not be blank" });
            }
            catch
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = "Failed due to some internal error" });
            }
        }

        [HttpPost]
        public IActionResult SaveDeclaration(DeclarationDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Declaration obj = new Declaration();
                    obj.DateDeclarationSigned = model.DeclarationDate;
                    obj.DateDeclarationValidFrom = model.ValidForm;
                    obj.DateDeclarationValidTo = model.ValidTo;
                    obj.PersonId = model.PersonId;
                    obj.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                    obj.AuditUserId = CurrentUser.UserID;

                    if (obj != null)
                    {
                        personService.SavePersonDeclaration(obj);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Declaration saved successfully.", IsSuccess = true });
                    }
                    else
                    {
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Something went wrong please try again.", IsSuccess = false });
                    }
                }
                else
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = Constants.CustomRequiredErrorMessage, IsSuccess = false });
                }

            }
            catch (Exception Ex)
            {
                string message = Ex.GetBaseException().Message;
                if (message.ToLower().Contains("unique key"))
                    message = "Contact type already exists for organisation.";
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
        }

        public IActionResult GetDeclarationList(DataTableServerSide model, string personId)
        {
            if (model.multisearch != null)
            {
                KeyValuePair<int, List<DeclarationDto>> centralOffices = new KeyValuePair<int, List<DeclarationDto>>();
                centralOffices = personService.GetDeclarationListById(model, Convert.ToInt32(personId));
                return Json(new
                {
                    draw = model.draw,
                    recordsTotal = centralOffices.Key,
                    recordsFiltered = centralOffices.Key,
                    data = centralOffices.Value.Select((c, index) => new List<object> {
                    c.PersonId,
                    model.start+index+1,
                    c.DeclarationDate?.ToString("dd/MM/yyyy"),
                    c.ValidForm?.ToString("dd/MM/yyyy"),
                    c.ValidTo?.ToString("dd/MM/yyyy")
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
        #endregion

        [HttpGet]
        public IActionResult GetFoodItemList(string categoryId)
        {
            var result = OpenFoodApi.GetFoodItemList(categoryId);
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }

    }
}
