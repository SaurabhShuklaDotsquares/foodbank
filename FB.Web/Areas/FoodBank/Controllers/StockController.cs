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

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class StockController : BaseController
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
        private IGrantorService grantorService;
        private readonly IMyReferralService ReferralService;
        private readonly IFoodService foodService;
        private IStockService stockService;
        private IFoodbankService foodbankService;

        public StockController(IUserService _userService, IMenuService _menuService, IPersonService _personService, IBranchService _branchService, ICharityService _charityService,
            ICentralOfficeService _centralofficeService, IForgotPasswordService _forgotPasswordService, IRoleService _roleService, IMapper _mapper,
            IQuickDonorGiftService _quickDonorGiftService, ICountryService _countryService, IGrantorService _grantorService, IMyReferralService _ReferralService, IFoodService _foodService, IStockService _stockService, IFoodbankService _foodbankService)
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
            grantorService = _grantorService;
            ReferralService = _ReferralService;
            foodService = _foodService;
            stockService = _stockService;
            foodbankService = _foodbankService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StockList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StockList(DataTableServerSide model)
        {
            KeyValuePair<int, List<StockDto>> stocklist = new KeyValuePair<int, List<StockDto>>();
            stocklist = stockService.GetStockList(model, CurrentUser.FoodbankId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = stocklist.Key,
                recordsFiltered = stocklist.Key,
                data = stocklist.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    c.FoodName,
                    c.TotalQuantity,
                    c.Unit,
                    c.PricePerItem,
                    ((StockIsLow)(int)c.IsItemLowInStock).GetDescription(),
                    "<a href=" + Url.Action("ViewStock", "Stock", new { id = c.Id })
                    + " class='view_btn'><img src='/Content/images/eye-icon.png' alt='' /></a><a href=" + Url.Action("AddStock", "Stock", new { id = c.Id })
                    + " class='view_btn'><img src='/Content/images/edit-icon.png' alt='' /></a><a  data-toggle='modal' data-target='#modal-delete-stock' href=" + Url.Action("Delete", "Stock", new { id = c.Id })
                    + " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"
                    ,
                   })
            });
            return View();
        }
        [HttpGet]
        public IActionResult AddStock(int id = 0)
        {
            StockDto model = new StockDto();
            if (id > 0)
            {
                model = stockService.GeStockDetailsById(id);
            }
            var StockIsLow = Enum.GetValues(typeof(StockIsLow));
            List<SelectListItem> StockIsLowList = new List<SelectListItem>();
            foreach (var workType in StockIsLow)
            {
                StockIsLowList.Add(new SelectListItem
                {
                    Value = ((int)workType).ToString(),
                    Text = ((StockIsLow)(int)workType).GetDescription(),
                });
            }
            ViewBag.StockIsLowList = StockIsLowList;


            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

            var foodItemCatories = OpenFoodApi.GetFoodItemCatogoriesList();

            var foodlist = new List<SelectListItem>();

            var grantorlist = grantorService.GetAllGrantor(CurrentUser.FoodbankId).Select(c => new SelectListItem
            {
                Text = c.Contact.ForeName + " " + c.Contact.Surname,
                Value = c.Id.ToString()
            }).ToList();

            grantorlist.Insert(0, selectvaule);
            ViewBag.grantorlist = grantorlist;

            foodlist.Insert(0, selectvaule);
            ViewBag.Foodlist = foodlist;

            foodItemCatories.Insert(0, selectvaule);
            ViewBag.FoodItemCatogoriesList = foodItemCatories;

            return View(model);
        }
        [HttpPost]
        public IActionResult AddStock(StockDto model)
        {
            try
            {
                model.FoodbankId = CurrentUser.FoodbankId;

                if (!foodService.GetAllFoodList().Select(x => x.ProductIdApi).Contains(model.ProductApiId.ToString()))
                {
                    Food obj = new Food()
                    {
                        ProductIdApi = model.ProductApiId.ToString(),
                        Name = model.FoodName,
                        CategoryApiId = model.FoodCategoryId
                    };
                    var food = foodService.SaveFood(obj);
                    if (food != null)
                    {
                        model.FoodId = food.Id;
                    }
                }
                else
                {
                    model.FoodId = Convert.ToInt32(foodService.GetAllFoodList().Where(x => x.ProductIdApi == model.ProductApiId.ToString()).FirstOrDefault().Id);
                }

                if (model.Id > 0)
                {
                    stockService.Save(StockDtoMapper.MapUpdate(model), false);
                    ShowSuccessMessage("Success!", "Stock updated successfully.", false);
                    return RedirectToAction("StockList", "Stock", new { id = model.Id });
                }
                else
                {
                    stockService.Save(StockDtoMapper.MapSave(model), true);
                    ShowSuccessMessage("Success!", "Stock saved successfully.", false);
                    return RedirectToAction("StockList", "Stock");
                }

            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                if (message.Contains("UNIQUE KEY"))
                {
                    if (message.Contains("uc_branch_reference"))
                        message = "Something wrong";
                }
                ShowErrorMessage("Error!", message, false);

                #region  Re-Bind section
                var StockIsLow = Enum.GetValues(typeof(StockIsLow));
                List<SelectListItem> StockIsLowList = new List<SelectListItem>();
                foreach (var workType in StockIsLow)
                {
                    StockIsLowList.Add(new SelectListItem
                    {
                        Value = ((int)workType).ToString(),
                        Text = ((StockIsLow)(int)workType).GetDescription(),
                    });
                }
                ViewBag.StockIsLowList = StockIsLowList;


                SelectListItem selectvaule = new SelectListItem();
                selectvaule.Value = "";
                selectvaule.Text = "Select";

                var foodItemCatories = OpenFoodApi.GetFoodItemCatogoriesList();

                var foodlist = new List<SelectListItem>();

                var grantorlist = grantorService.GetAllGrantor(CurrentUser.FoodbankId).Select(c => new SelectListItem
                {
                    Text = c.Contact.ForeName + " " + c.Contact.Surname,
                    Value = c.Id.ToString()
                }).ToList();
                grantorlist.Insert(0, selectvaule);
                ViewBag.grantorlist = grantorlist;

                foodItemCatories.Insert(0, selectvaule);
                ViewBag.FoodItemCatogoriesList = foodItemCatories;

                foodlist.Insert(0, selectvaule);
                ViewBag.Foodlist = foodlist;
                #endregion

                return View(model);
            }
        }
        public int GetStockAvalability(string Foodid)
        {
            var stocklist = stockService.GetStockAvialability(Foodid, CurrentUser.FoodbankId);
            return stocklist ?? 0;
        }
        public IActionResult ViewStock(int id)
        {
            var stocklist = stockService.GeStockDetailsById(id);
            return View(stocklist);
        }

        [HttpGet]
        public IActionResult GetFoodItemList(string categoryId)
        {
            var result = OpenFoodApi.GetFoodItemList(categoryId);
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }

        [HttpGet]
        public IActionResult GetFoodItemAllergyList(string foodItemId)
        {
            var result = OpenFoodApi.GetFoodItemAllergyList(foodItemId);
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this Stock?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Stock" },
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
                var stock = stockService.GeStockById(id);
                if (stock != null)
                {
                    stock.Active = false;
                    stockService.Save(stock, false);
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
        public IActionResult GrantorStockQuantityList(DataTableServerSide model, int stockId)
        {
            KeyValuePair<int, List<StockManageGrantorDto>> stocklist = new KeyValuePair<int, List<StockManageGrantorDto>>();
            stocklist = foodService.GetStockList(model, CurrentUser.FoodbankId, stockId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = stocklist.Key,
                recordsFiltered = stocklist.Key,
                data = stocklist.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    c.IsDonorDonation? c.DonorName+"(Donor)": c.GrantorName+"(Grantor)",
                    c.Quantity,
                    c.TotalPrice,
                    c.AddedDate.Value.ToString("MM/dd/yyyy"),
                    "<a  data-toggle='modal' data-target='#modal-delete-stock' href=" + Url.Action("DeleteDonation", "Stock", new { id = c.Id })
                    + " class='view_btn'><img src='/Content/images/delete.png' alt='' /></a>"
                    ,
                   })
            });
        }

        [HttpGet]
        public IActionResult AddFoodDonation(int? id = null)
        {
            var stock = stockService.GeStockById(id.Value);

            StockFoodDonationDto model = new StockFoodDonationDto();
            model.StockId = id.Value;
            model.FoodItemId = stock.FoodId;
            model.QuantityUnit = stock.Unit;
            model.FoodItemName = stock.Food.Name;
            model.hdnFoodCategoryId = stock.Food.CategoryApiId;
            model.hdnFoodProductId = stock.Food.ProductIdApi;

            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

            var donorList = foodbankService.GetDonorList(CurrentUser.FoodbankId,0,0);
            List<SelectListItem> donorItemList = new List<SelectListItem>();
            foreach (var items in donorList)
            {
                donorItemList.Add(new SelectListItem
                {
                    Value = ((int)items.Donor.PersonId).ToString(),
                    Text = ($"{items.Donor.Forenames}{items.Donor.Surname}").ToString(),
                });
            }
            donorItemList.Insert(0, selectvaule);
            ViewBag.donorList = donorItemList;

            var grantorList = grantorService.GetAllGrantor(CurrentUser.FoodbankId);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var items in grantorList)
            {
                list.Add(new SelectListItem
                {
                    Value = ((int)items.Id).ToString(),
                    Text = $"{items.ForeName}{items.SurName}",
                });
            }
            list.Insert(0, selectvaule);
            ViewBag.grantorList = list;

            return PartialView("_AddGrantorStock", model);
        }

        [HttpPost]
        public IActionResult AddFooddonation(StockFoodDonationDto model)
        {
            try
            {
                FoodItem entity = new FoodItem();
                entity.Foodid = model.FoodItemId;
                entity.FoodbankId = CurrentUser.FoodbankId;
                entity.StockId = model.StockId;
                if (model.DonorId > 0)
                {
                    entity.Donorid = model.DonorId;
                }
                if (model.GrantorId > 0)
                {
                    entity.GrantorId = model.GrantorId;
                }
                entity.OwnPurchase = model.GrantorId != 0 ? true : false;
                entity.CauseofDonation = model.CauseOfDonation;
                entity.Quntity = Convert.ToInt32(model.Quantity);
                entity.QuantityUnit = model.QuantityUnit;
                entity.ExpiryDate = DateTime.Now;
                entity.AddedDate = DateTime.Now;
                entity.Status = (int)DonationStatus.PledgedAndDonated;
                bool result = foodbankService.SaveFoodDonation(entity);

                if (result)
                {
                    //Update Stock Quantity
                    var stock = stockService.GeStockById(model.StockId);
                    stock.TotalQuantity = stock.TotalQuantity + Convert.ToInt32(model.Quantity);
                    stockService.Save(stock, false);
                    //End

                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Donation has been added successfully.", IsSuccess = true });
                }
                else
                {
                    string message = "Error";
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
                }
            }
            catch (Exception Ex)
            {
                string message = Ex.GetBaseException().Message;
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });

            }

        }

        #region Delete Donation
        [HttpGet]
        public IActionResult DeleteDonation(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this donatiion?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Donation" },
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
        public IActionResult DeleteDonation(int id, IFormCollection FC)
        {
            string message;
            int stockId = 0;
            try
            {
                var donation = foodbankService.GetFoodDonationById(id);

                var stock = stockService.GeStockByFoodId(donation.Foodid.Value, CurrentUser.FoodbankId);
                stockId = stock.Id;
                if (donation != null)
                {
                    //Manage stock quantity 
                    if (stock != null && stock.TotalQuantity > donation.Quntity)
                    {
                        stock.TotalQuantity = stock.TotalQuantity - donation.Quntity;
                        if (stock.TotalQuantity <= 1)
                        {
                            stock.IsItemLowInStock = 1;
                        }
                        stockService.Save(stock, false);

                        foodbankService.DeleteFoodDonation(id);
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Total quantity is low so donation record not deleted.", false);
                        return RedirectToAction("addstock", "stock", new { id = stock.Id });
                    }
                    //
                }
                ShowSuccessMessage("Success!", "Donation has been deleted successfully.", false);
                return RedirectToAction("addstock", "stock", new { id = stock.Id });
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Error!", message, false);
                return RedirectToAction("addstock", "stock", new { id = stockId });
            }
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

            if (stockService != null)
            {
                stockService.Dispose();
                stockService = null;
            }
            if (foodService != null)
            {
                foodService.Dispose();
                // contactService = null;
            }
            if (ReferralService != null)
            {
                ReferralService.Dispose();
                //  ReferralService = null;
            }
            if (grantorService != null)
            {
                grantorService.Dispose();
                grantorService = null;
            }
            base.Dispose(disposing);
        }
    }
}
