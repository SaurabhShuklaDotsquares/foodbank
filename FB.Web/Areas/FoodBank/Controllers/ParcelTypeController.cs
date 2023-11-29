using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class ParcelTypeController : BaseController
    {
        private IFoodbankService foodbankService;
        private readonly IParcelService parcelService;
        private readonly IFoodService foodService;

        public ParcelTypeController(IParcelService _parcelService, IFoodbankService _foodbankService, IFoodService _foodService)
        {
            foodbankService = _foodbankService;
            parcelService = _parcelService;
            foodService = _foodService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BindPacelTypeList(DataTableServerSide model)
        {

            KeyValuePair<int, List<ParcelType>> parcelType = new KeyValuePair<int, List<ParcelType>>();
            parcelType = parcelService.GetParcelTypeList(model, CurrentUser.FoodbankId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = parcelType.Key,
                recordsFiltered = parcelType.Key,
                data = parcelType.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    c.Name,
                    c.ParcelFoodItem.Count,
                    c.Adddate?.ToString("MM/dd/yyyy"),

                    "<a href=" + Url.Action("createedit", "parceltype", new { id = c.Id }) + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"

                    + "<a data-toggle='modal' data-target='#modal-delete-parceltype' href='" + Url.Action("deleteparceltype", "parceltype", new { id = c.Id })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    + "<a href=" + Url.Action("view", "parceltype", new { id = c.Id }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>",
                })
            });
        }

        #region Add Food Donation
        [HttpGet]
        public IActionResult CreateEdit(int? id = null)
        {

            ParcelTypeDto model = new ParcelTypeDto();
            if (id.HasValue)
            {
                var parceltype = parcelService.GetParcelTypeById(id.Value);
                if (parceltype != null)
                {
                    model.ParcelTypeId = parceltype.Id;
                
                model.Title = parceltype.Name;
                }
            }

            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

            var listItems = foodService.GetAllFoodListForParcel(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            listItems.Insert(0, selectvaule);
            ViewBag.FoodItemList = listItems;

            return View(model);
        }
        [HttpPost]
        public IActionResult CreateEdit(ParcelTypeDto model)
        {
            var obj = parcelService.GetParcelFoodItemById(model.ParcelTypeId);
            ParcelFoodItem parcelFoodItem = !obj.Select(x => x.FoodId).Contains(model.FoodItemId) ? new ParcelFoodItem() : null;

            if (parcelFoodItem != null)
            {
                if (model.ParcelTypeId == 0)
                {
                    ParcelType parcelType = new ParcelType();
                    parcelType.Name = model.Title;
                    parcelType.FoodbankId = CurrentUser.FoodbankId;

                    parcelFoodItem.FoodId = model.FoodItemId;
                    parcelFoodItem.Quantity = Convert.ToInt32(model.Quantity);
                    parcelType.ParcelFoodItem.Add(parcelFoodItem);
                    bool result = parcelService.SaveParcelType(parcelType);
                    if (result)
                    {
                        ShowSuccessMessage("Success!", "Record has been added successfully.", false);
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                    }
                    return RedirectToAction("createedit", "parceltype", new { id = model.ParcelTypeId });
                }
                else
                {
                    parcelFoodItem.FoodId = model.FoodItemId;
                    parcelFoodItem.ParcelTypeId = model.ParcelTypeId;
                    parcelFoodItem.Quantity = Convert.ToInt32(model.Quantity);
                    bool result = parcelService.SaveParcelFoodItem(parcelFoodItem);
                    if (result)
                    {
                        ShowSuccessMessage("Success!", "Record has been added successfully.", false);
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                    }
                    return RedirectToAction("createedit", "parceltype", new { id = model.ParcelTypeId });
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Food Item is already exists. Please add another food item or delete already exist item.", false);
                return RedirectToAction("createedit", "parceltype", new { id = model.ParcelTypeId });
            }
        }
        #endregion

        #region Delete ParcelType Section
        [HttpGet]
        public IActionResult DeleteParcelType(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this parcel type?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Parcel Type" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the parcel type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteParcelType(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var parcelType = parcelService.GetParcelTypeById(id);
                if (parcelType != null)
                {
                    var parcelTypeitem = parcelService.GetParcelFoodItemById(parcelType.Id);
                    foreach (var item in parcelTypeitem)
                    {
                       
                            parcelService.DeleteParcelFoodItem(item.Id);
                        
                    }
                    parcelService.DeleteParcelType(id);
                }
                ShowSuccessMessage("Success!", "Record has been deleted successfully.", false);
                return RedirectToAction("index", "parceltype");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error or this parcel type asscociate with parcel";

                ShowErrorMessage("Error!", message, false);
                return RedirectToAction("index", "parceltype");
            }
        }
        #endregion

        [HttpGet]
        public IActionResult GetFoodItemUnit(int foodId)
        {
            var result = foodService.GetFoodUnit(foodId);
            return NewtonSoftJsonResult(new RequestOutcome<Stock> { Data = result });
        }

        [HttpPost]
        public IActionResult BindPacelTypeFoodItemList(DataTableServerSide model, int parcelTypeId)
        {
            KeyValuePair<int, List<ParcelFoodItem>> parcelType = new KeyValuePair<int, List<ParcelFoodItem>>();
            parcelType = parcelService.GetParcelFoodItemList(model, parcelTypeId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = parcelType.Key,
                recordsFiltered = parcelType.Key,
                data = parcelType.Value.Select((c, index) => new List<object> {
                    c.Id,
                    model.start+index+1,
                    $"{c.Food.Name}",
                    $"{c.Quantity} {c.Food.Stock.FirstOrDefault().Unit}",

                    "<a data-toggle='modal' data-target='#modal-delete-parceltype-fooditem' href='" + Url.Action("deleteparcelfooditem", "parceltype", new { id = c.Id })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    ,
                })
            });
        }

        #region Delete ParcelType Section
        [HttpGet]
        public IActionResult DeleteParcelFoodItem(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this food item?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Food Item" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the parcel type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteParcelFoodItem(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var foodItem = parcelService.GetParcelFoodItem(id);
                if (foodItem != null)
                {

                    parcelService.DeleteParcelFoodItem(id);
                }
                ShowSuccessMessage("Success!", "Record has been deleted successfully.", false);
                return RedirectToAction("index", "parceltype");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("index", "parceltype");
            }
        }
        #endregion

        #region View Food Donation
        [HttpGet]
        public IActionResult View(int? id = null)
        {
            ParcelTypeDto model = new ParcelTypeDto();
            if (id.HasValue)
            {
                var parceltype = parcelService.GetParcelTypeById(id.Value);
                model.ParcelTypeId = parceltype.Id;
                model.Title = parceltype.Name;
            }

            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

            var listItems = foodService.GetAllFoodListForParcel(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            listItems.Insert(0, selectvaule);
            ViewBag.FoodItemList = listItems;

            return View(model);
        }
        #endregion
    }
}
