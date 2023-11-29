using FB.Data.Models;
using FB.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FB.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using FB.Service;
using FB.Web.Code;

namespace FB.Web.Controllers
{
    public class CommonController : BaseController
    {
        private readonly IFoodService foodService;
        private readonly IUserService userService;
        private readonly IParcelService parcelService;
        private readonly IFamilyService familyService;
        private readonly IVolunteerService volunteerService;
        private readonly IRecipeService recipeService;
        private readonly IGrantorService grantorService;
        private readonly ICountryService countryService;
        private readonly IFamilyParcelService familyParcelService;
        private readonly IVoucherService voucherService;
        public CommonController(IFoodService _foodService, IUserService _userService, IParcelService _parcelService,
             IFamilyService _familyService, IVolunteerService _volunteerService,
            IRecipeService _recipeService, IGrantorService _grantorService, ICountryService _countryService, IFamilyParcelService _familyParcelService, IVoucherService _voucherService)
        {
            foodService = _foodService;
            userService = _userService;
            parcelService = _parcelService;
            familyService = _familyService;
            volunteerService = _volunteerService;
            recipeService = _recipeService;
            grantorService = _grantorService;
            countryService = _countryService;
            familyParcelService = _familyParcelService;
            voucherService = _voucherService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetPledgeCount()
        {
            PledgeList model = new PledgeList();
            if (CurrentUser.UserID > 0 && CurrentUser.RoleID == (int)UserRoles.Donor)
            {
                var user = userService.GetUser(CurrentUser.UserID);
                int foodbankId = user.Person.DonorFoodbank.FirstOrDefault().FoodBankId;
                model.FoodItemCount = foodService.GetAllFoodListForPledge(foodbankId).Count;

                return PartialView("_PledgeNotification", model);
            }
            else
            {
                return PartialView("_PledgeNotification", model);
            }
        }

        #region Add Donor Pledge
        [HttpGet]
        public IActionResult AddPledge(int? id = null)
        {
            var user = userService.GetUser(CurrentUser.UserID);
            int foodbankId = user.Person.DonorFoodbank.FirstOrDefault().FoodBankId;

            DonorDonationDto model = new DonorDonationDto();
            model.DonorId = user.PersonId.Value;
            model.Refrence = user.Person.Reference;
            ViewBag.FoodItemList = foodService.GetAllFoodListForPledge(foodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            return PartialView("_DonorPledgePartial", model);
        }
        [HttpPost]
        public IActionResult AddPledge(DonorDonationDto model)
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
                    Status = 0
                };
                foodService.SaveFoodDonation(objFoodItem);
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Pledge has been saved successfully.", IsSuccess = true });
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
        }
        #endregion

        #region Qr Code 
        [HttpGet]
        public IActionResult ParcelDetailView(int voucherId)
        {
            var voucher = voucherService.GetVoucherById(voucherId);

            var model = new FamilyParcelDto();
            model.FoodBankId = voucher.FoodbankId;

            var FamilyList = familyService.GetAllFamily(voucher.FoodbankId).Select(x => new SelectListItem
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

            var StandardParcelList = parcelService.GetAllParcelType(voucher.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            StandardParcelList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.StandardParcelList = StandardParcelList;

            var PackerList = volunteerService.GetVolunteerList(voucher.FoodbankId).Select(x => new SelectListItem
            {
                Text = $"{x.Contact.ForeName}",
                Value = x.Id.ToString()
            }).ToList();
            PackerList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.PackerList = PackerList;

            var RecipeList = recipeService.GetRecipeList(voucher.FoodbankId).Select(x => new SelectListItem
            {
                Text = $"{x.RecipeTitle}",
                Value = x.Id.ToString()
            }).ToList();
            RecipeList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.RecipeList = RecipeList;

            var listItems = foodService.GetAllFoodListForParcel(voucher.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            listItems.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.FoodItemList = listItems;

            if (voucherId > 0)
            {
                var parcel = parcelService.GetParcelByVoucherToken(voucherId);
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
        public IActionResult BindGrantorTableView(DataTableServerSide model, int FoodbankId, int GrantorId)
        {
            var grantor = grantorService.GetGrantorById(GrantorId);

            KeyValuePair<int, List<Parcels>> parcels = new KeyValuePair<int, List<Parcels>>();
            parcels = familyParcelService.GetParcelfoodlists(model, grantor.FoodBankId, GrantorId);

            return Json(new
            {
                draw = model.draw,
                recordsTotal = parcels.Key,
                recordsFiltered = parcels.Key,
                data = parcels.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    ((ParcelTypes)c.ParcelTypeId).GetDescription(),
                    c.Family == null ? " ":c.Family.FamilyName,
                    Extensions.ToFormatCustomString(c.DeliveryDate),
                    c.DeliveredDate != null ? Extensions.ToFormatCustomString(c.DeliveredDate.Value) : "-",
                    ((ParcelStatus)c.Status).GetDescription(),

                     "<a href=" + Url.Action("ViewItems", "Grantor", new { id = c.Id }) + " class='btn btn-primary grid-btn btn-sm'>View Item <i class='fa fa-eye'></i></a>&nbsp;"

                })
            });
        }
        [HttpGet]
        public IActionResult GrantorView(string token)
        {
            var model = new GrantorDto();

            if (!string.IsNullOrWhiteSpace(token))
            {
                var grantor = grantorService.GetGrantorByToken(token);
                if (grantor != null)
                {
                    model.ForeName = grantor.ForeName;
                    model.SurName = grantor.SurName;
                    model.Email = grantor.Contact.Email;
                    model.ContactNumber = grantor.Contact.Mobile;
                    model.TotalAmount = Convert.ToString(grantor.TotalAmount.Value);
                    model.FoodBankId = grantor.FoodBankId;
                    model.GrantorId = grantor.Id;
                    model.ContactId = grantor.ContactId;
                    model.AddressID = grantor.AddressId;

                    if (grantor.Address != null)
                    {
                        model.PostCode = grantor.Address.Postcode;
                        model.StreetName = grantor.Address.Street;
                        model.HouseName = grantor.Address.HouseName;
                        model.HouseNumber = grantor.Address.HouseNumber;
                        model.City = grantor.Address.City;
                    }
                }
                else
                {
                    ShowErrorMessage("Error!", "No record found. Please check again with another grantor.", false);
                    return RedirectToAction("index", "grantor");
                }
            }
            else
            {
                model.FoodBankId = CurrentUser.UserID;
            }
            BindCountriesViewBag();
            model.CountryID = 248;
            model.CountryName = model.CountryID == 248 ? "United Kingdom" : string.Empty;
            return View(model);
        }

        [HttpGet]
        public IActionResult PurchaseItems(int? id = null)
        {
            var grantor = grantorService.GetGrantorById(id != null ? id.Value : 0);

            var model = new FamilyParcelDto();
            model.FoodBankId = grantor.FoodBankId;

            var FamilyList = familyService.GetAllFamily(model.FoodBankId).Select(x => new SelectListItem
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

            var StandardParcelList = parcelService.GetAllParcelType(model.FoodBankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            StandardParcelList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.StandardParcelList = StandardParcelList;

            var PackerList = volunteerService.GetVolunteerList(model.FoodBankId).Select(x => new SelectListItem
            {
                Text = $"{x.Contact.ForeName}",
                Value = x.Id.ToString()
            }).ToList();
            PackerList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.PackerList = PackerList;

            var RecipeList = recipeService.GetRecipeList(model.FoodBankId).Select(x => new SelectListItem
            {
                Text = $"{x.RecipeTitle}",
                Value = x.Id.ToString()
            }).ToList();
            RecipeList.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.RecipeList = RecipeList;

            var listItems = foodService.GetAllFoodListForParcel(model.FoodBankId).Select(x => new SelectListItem
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

        [NonAction]
        public void BindCountriesViewBag(int? id = null)
        {
            ViewBag.CountryList = countryService.GetCountries().Select(c => new SelectListItem
            {
                Text = c.CountryName.ToTitle(),
                Value = c.CountryId.ToString(),
            }).ToList();
        }
        #endregion


        [HttpPost]
        public IActionResult BindPackedPacelList(DataTableServerSide model, string parceltoken)
        {
            KeyValuePair<int, List<FamilyParcelDto>> parcelType = new KeyValuePair<int, List<FamilyParcelDto>>();
            parcelType = familyParcelService.GetFamilyParcelList(model, CurrentUser.FoodbankId);
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

                    (c.DeliveredDate!=null && c.DeliveredDate!="-" ?"":"<a href=" + Url.Action("createedit", "familyparcel", new { id = c.ParcelId }) + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;")

                    + "<a data-toggle='modal' data-target='#modal-delete-parcel' href='" + Url.Action("deleteparcel", "familyparcel", new { id = c.ParcelId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    + "<a href=" + Url.Action("view", "familyparcel", new { id = c.ParcelId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>&nbsp;"
                    + "<a href=" + Url.Action("downloadqrcode", "familyparcel", new { parcelId = c.ParcelId }) + " data-toggle='modal' data-target='#modal-download-qrcode' class='btn btn-primary grid-btn btn-sm'><i class='fa fa-qrcode fa-2x'></i></a>"
                    ,
                })
            });
        }
    }
}
