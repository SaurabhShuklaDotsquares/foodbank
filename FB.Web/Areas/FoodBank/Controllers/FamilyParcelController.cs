using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZXing;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class FamilyParcelController : BaseController
    {
        private IFoodbankService foodbankService;
        private readonly IParcelService parcelService;
        private readonly IFoodService foodService;
        private readonly IFamilyParcelService familyParcelService;
        private readonly IFamilyService familyService;
        private readonly IVolunteerService volunteerService;
        private readonly IRecipeService recipeService;
        private readonly IStockService stockService;
        private readonly IGrantorService grantorService;
        private readonly IVoucherService voucherService;
        private readonly IFeedbackService feedbackService;
        private static Random random = new Random();

        public FamilyParcelController(IParcelService _parcelService, IFoodbankService _foodbankService, IFoodService _foodService,
            IFamilyParcelService _familyParcelService, IFamilyService _familyService, IVolunteerService _volunteerService, IRecipeService _recipeService,
            IStockService _stockService, IGrantorService _grantorService, IVoucherService _voucherService, IFeedbackService _feedbackService)
        {
            foodbankService = _foodbankService;
            parcelService = _parcelService;
            foodService = _foodService;
            familyParcelService = _familyParcelService;
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
            return View();
        }
        [HttpPost]
        public IActionResult BindFamilyPacelList(DataTableServerSide model)
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

        #region Delete Family Parcel
        [HttpGet]
        public IActionResult DeleteParcel(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this parcel?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Parcel" },
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
        public IActionResult DeleteParcel(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var parcel = familyParcelService.GetParcelById(id);
                if (parcel != null)
                {
                    var historList = stockService.GetStockHistory(parcel.Id);
                    foreach (var item in historList)
                    {
                        //Update Stock Quantity
                        var stock = item.Stock;
                        stock.TotalQuantity = stock.TotalQuantity + Convert.ToInt32(Convert.ToString(item.Quantity).Replace("-", ""));
                        stockService.Save(stock, false);
                        //End

                        //Delete Stock history
                        stockService.DeleteStockHistory(item.Id);
                        //Emd
                    }
                    var FamilyParcelFoodItemList = parcel.FamilyParcelFoodItem.ToList();
                    foreach (var item in FamilyParcelFoodItemList)
                    {
                        //Delete FamilyParcelFoodItem List table
                        familyParcelService.DeleteFamilyParcelFoodItem(item.Id);
                        //Emd
                    }

                    foreach (var feedback in parcel.Feedback.ToList())
                    {
                        foreach (var obj in feedback.FeedbackFormDetails)
                        {
                            feedbackService.DeleteFeedbackFormDetails(obj.Id);
                        }
                        feedbackService.Delete(feedback.Id);
                    }

                    familyParcelService.DeleteParcel(id);
                }
                ShowSuccessMessage("Success!", "Parcel has been deleted successfully.", false);
                return RedirectToAction("index", "familyparcel");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("index", "familyparcel");
            }
        }
        #endregion

        [HttpGet]
        public IActionResult CreateEdit(int? id = null)
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

            var GrantorList = grantorService.GetAllGrantor(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = $"{x.ForeName} {x.SurName}",
                Value = x.Id.ToString()
            }).ToList();
            GrantorList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            ViewBag.GrantorList = GrantorList;

            var VoucherList = voucherService.GetVoucherList(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = $"{x.VoucherToken}",
                Value = x.Id.ToString()
            }).ToList();
            VoucherList.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
            ViewBag.VoucherList = VoucherList;

            var RecipeList = recipeService.GetRecipeList(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = $"{x.RecipeTitle} [{x.RecipeNumber}]",
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
                        model.StandardParcelId = parcel.StandardParcelTypeId;
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
                    model.PackOnDate = parcel.PackOnDate != null ? Extensions.ToFormatCustomString(parcel.PackOnDate.Value) : null;
                    model.Status = parcel.Status.Value;
                    model.SpecialNote = parcel.SpecialNote;
                    if (parcel.VoucherId != null)
                    {
                        model.VoucherId = parcel.VoucherId.Value;
                    }
                    if (parcel.GranterId != null)
                    {
                        model.GrantorId = parcel.GranterId.Value;
                    }

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
        public IActionResult CreateEdit(FamilyParcelDto model)
        {
            try
            {
                bool isExists = false;

                var parcel = model.ParcelId > 0 ? parcelService.GetParcelById(model.ParcelId) : new Parcels();

                isExists = model.ParcelId > 0 ? true : false;

                parcel.ParcelTypeId = model.ParcelTypeId;
                if (model.ParcelTypeId != (int)ParcelTypes.AnonymousParcel)
                {
                    parcel.StandardParcelTypeId = model.StandardParcelId;
                }
                parcel.FoodbankId = CurrentUser.FoodbankId;
                if (model.ParcelTypeId == (int)ParcelTypes.AnonymousParcel)
                {
                    parcel.FamilyId = null;
                }
                else
                {
                    parcel.FamilyId = model.FamilyId;
                }

                parcel.PackerId = model.PackerId;
                parcel.DelivererId = model.DeliverrerId;

                if (model.PackOnDate != null)
                {
                    parcel.PackOnDate = Convert.ToDateTime(model.PackOnDate);
                    parcel.Status = (int)ParcelStatus.Packed;
                }
                else
                {
                    parcel.PackOnDate = null;
                }

                if (model.DeliveredDate != null)
                {
                    parcel.DeliveredDate = Convert.ToDateTime(model.DeliveredDate);
                    parcel.Status = (int)ParcelStatus.Delivered;
                }
                else
                {
                    parcel.DeliveredDate = null;
                    parcel.Status = (int)ParcelStatus.Pending;
                }

                parcel.DeliveryDate = Convert.ToDateTime(model.DeliveryDate);
                parcel.AddedDate = DateTime.Now;
                if (parcel.Id == 0)
                {
                    parcel.ParcelToken = GeneraterToken(parcel.Id);
                    parcel.ParcelQrcode = GenerateParcelQRCode(parcel.ParcelToken);
                    parcel.ParcelFeedbackQrcode = GenerateFeedbackQRCode(parcel.ParcelToken);
                }

                parcel.SpecialNote = model.SpecialNote;

                if (model.GrantorId > 0)
                {
                    parcel.GranterId = model.GrantorId;
                }
                if (model.VoucherId > 0)
                {
                    parcel.VoucherId = model.VoucherId;
                }

                if (model.IncludeRecipe)
                {
                    parcel.RecipeId = model.RecipeId;
                }
                else
                {
                    parcel.RecipeId = null;
                }

                if (model.RecipeId > 0)
                {
                    parcel.RecipeId = model.RecipeId;
                }

                //Delete food parcel items
                var list = parcelService.GetFamilyParcelFoodItemById(parcel.Id);
                if (list.Count > 0)
                {
                    parcelService.DeleteFamilyParcelFoodItem(list);
                }
                //End

                List<FamilyParcelFoodItem> parcelItems = new List<FamilyParcelFoodItem>();
                int i = 0;
                foreach (var item in model.hdnfoodItemId)
                {
                    //Check stock quantity in stock
                    var stock = stockService.GeStockByFoodId(Convert.ToInt32(item), CurrentUser.FoodbankId);
                    if (stock != null)
                    {
                        if (model.itemquantity[i] != "")
                        {
                            if (!(stock.TotalQuantity > Convert.ToInt32(model.itemquantity[i])))
                            {
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Selected food item (" + stock.Food.Name + ") quantity is low in stock. Please enter sufficient  quantity.", IsSuccess = false });
                            }
                        }
                        else
                        {
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please check selected food item (" + stock.Food.Name + ") quantity in list. Please enter sufficient  quantity.", IsSuccess = false });
                        }
                    }
                    //End

                    FamilyParcelFoodItem obj = new FamilyParcelFoodItem
                    {
                        FoodId = Convert.ToInt32(item),
                        Quantity = Convert.ToInt32(model.itemquantity[i])
                    };
                    parcelItems.Add(obj);
                    i++;
                }
                parcel.FamilyParcelFoodItem = parcelItems;
                var result = parcelService.Save(parcel);

                if (result)
                {
                    //Voucher Redeeme Code
                    if (parcel.VoucherId > 0)
                    {
                        var voucher = voucherService.GetVoucherById(parcel.VoucherId.Value);
                        if (voucher.RedeemedDate == null )//&& voucher.VoucherQrcode == null
                        {
                            voucher.RedeemedDate = DateTime.Now;
                            voucher.VoucherQrcode = GenerateVoucherQRCode(voucher.Id);
                            voucherService.Save(voucher);
                        }
                    }
                    //End


                    #region manage stock item count

                    var historList = stockService.GetStockHistory(parcel.Id);
                    foreach (var item in historList)
                    {
                        //Update Stock Quantity
                        var stock = item.Stock;
                        stock.TotalQuantity = stock.TotalQuantity + Convert.ToInt32(Convert.ToString(item.Quantity).Replace("-", ""));
                        stockService.Save(stock, false);
                        //End

                        //Delete Stock history
                        stockService.DeleteStockHistory(item.Id);
                        //Emd
                    }

                    foreach (var item in parcelItems)
                    {
                        var stock = stockService.GeStockByFoodId(item.FoodId, CurrentUser.FoodbankId);
                        StockHistory stockHistory = new StockHistory
                        {
                            FoodItemId = item.FoodId,
                            ParcelId = parcel.Id,
                            StockId = stock.Id,
                            Quantity = Convert.ToInt32("-" + item.Quantity),
                            ModifiedDate = DateTime.Now,
                        };
                        stockService.SaveStockHistory(stockHistory);

                        //Manage stock quantity 
                        if (stock != null)
                        {
                            stock.TotalQuantity = stock.TotalQuantity - item.Quantity;
                            if (stock.TotalQuantity <= 1)
                            {
                                stock.IsItemLowInStock = 1;
                            }
                            stockService.Save(stock, false);
                        }
                        //
                    }

                    #endregion
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Parcel has been " + (isExists ? "updated" : "added") + " successfully.", IsSuccess = true });
                }
                else
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Something went wrong. Please try again after some time.", IsSuccess = false });
                }
            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Something went wrong. Please try again after some time.", IsSuccess = false });
            }
        }

        [HttpGet]
        public IActionResult GetFoodItemListByParcelTypeId(int standardParcelTypeId)
        {
            var obj = parcelService.GetParcelFoodItemById(standardParcelTypeId);
            if (obj != null)
            {
                List<FamilyParcel> model = obj.Select(x => new FamilyParcel()
                {
                    FoodItemId = x.FoodId,
                    FoodItemName = x.Food.Name,
                    Quantity = x.Quantity
                }).ToList();
                return NewtonSoftJsonResult(new RequestOutcome<List<FamilyParcel>> { Data = model });
            }
            else
            {
                List<FamilyParcel> model = new List<FamilyParcel>();
                return NewtonSoftJsonResult(new RequestOutcome<List<FamilyParcel>> { Data = model });

            }
        }
        [HttpGet]
        public IActionResult GetFoodItemListByRecipeId(int recipeId)
        {
            var obj = recipeService.GetRecipeFoodItemById(recipeId);
            if (obj != null)
            {
                List<FamilyParcel> model = obj.Select(x => new FamilyParcel()
                {
                    FoodItemId = x.FoodId??0,
                    FoodItemName = x.Food.Name,
                    Quantity = x.Quantity??0
                }).ToList();
                return NewtonSoftJsonResult(new RequestOutcome<List<FamilyParcel>> { Data = model });
            }
            else
            {
                List<FamilyParcel> model = new List<FamilyParcel>();
                return NewtonSoftJsonResult(new RequestOutcome<List<FamilyParcel>> { Data = model });

            }
        }

        [HttpGet]
        public IActionResult CheckFoodItemIsAlreadyExits(int standardParcelTypeId, int foodId)
        {
            var obj = parcelService.GetFamilyParcelFoodItemById(standardParcelTypeId);
            if (obj != null)
            {
                bool isExits = obj.Select(x => x.FoodId).Contains(foodId);
                if (isExits)
                    return NewtonSoftJsonResult(new RequestOutcome<bool> { Data = isExits });
                else
                    return NewtonSoftJsonResult(new RequestOutcome<bool> { Data = isExits });
            }
            else
            {
                List<FamilyParcel> model = new List<FamilyParcel>();
                return NewtonSoftJsonResult(new RequestOutcome<bool> { Data = false });
            }
        }

        [HttpGet]
        public IActionResult CheckStockItemAvailabilty(int foodId, int quantity)
        {
            var obj = stockService.GeStockByFoodId(foodId, CurrentUser.FoodbankId);
            if (obj != null)
            {
                if (obj.TotalQuantity > quantity)
                    return NewtonSoftJsonResult(new RequestOutcome<bool> { Data = true });
                else
                    return NewtonSoftJsonResult(new RequestOutcome<bool> { Data = false });
            }
            else
            {
                List<FamilyParcel> model = new List<FamilyParcel>();
                return NewtonSoftJsonResult(new RequestOutcome<bool> { Data = false });
            }
        }

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


        [HttpGet]
        public string GeneraterToken(int? id = null)
        {
        Checked:
            var token = RandomString(8);
            if (!string.IsNullOrWhiteSpace(token))
            {
                var isExist = parcelService.CheckToken(token);
                if (isExist)
                {
                    goto Checked;
                }
                else
                {
                    return token;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static byte[] GenerateFeedbackQRCode(string parcelToken)
        {
            byte[] QRbyte = null;
            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            string url = "";
            url = SiteKeys.DomainName + "FamilyFeedback?parceltoken=" + parcelToken;

            var map = new Bitmap(barcodeWriter.Write(url));
            using (MemoryStream memory = new MemoryStream())
            {
                map.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                QRbyte = bytes;
            }

            return QRbyte;
        }

        public static byte[] GenerateParcelQRCode(string parcelToken)
        {
            byte[] QRbyte = null;
            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            string url = "";
            url = SiteKeys.DomainName + "Common/BindPackedPacelList?parceltoken=" + parcelToken;

            var map = new Bitmap(barcodeWriter.Write(url));
            using (MemoryStream memory = new MemoryStream())
            {
                map.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                QRbyte = bytes;
            }

            return QRbyte;
        }

        public static byte[] GenerateVoucherQRCode(int voucherId)
        {
            byte[] QRbyte = null;
            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            string url = "";
            url = SiteKeys.DomainName + "Common/ParcelDetailView?voucherId=" + voucherId;

            var map = new Bitmap(barcodeWriter.Write(url));
            using (MemoryStream memory = new MemoryStream())
            {
                map.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                QRbyte = bytes;
            }

            return QRbyte;
        }


        [HttpGet]
        public IActionResult DownloadQRCode(int parcelId)
        {
            FamilyParcelDto model = new FamilyParcelDto();
            var parcel = parcelService.GetParcelById(parcelId);
            model.ParcelQrcode = parcel.ParcelQrcode != null ? Convert.ToBase64String(parcel.ParcelQrcode) : null;
            return PartialView("_GenerateQRCode", model);
        }
    }
}
