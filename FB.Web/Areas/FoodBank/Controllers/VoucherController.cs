using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ExportReport;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class VoucherController : BaseController
    {
        private IVoucherService voucherService;
        private IFoodbankService foodbankService;
        private IMyReferralService myReferralService;
        private IFamilyService familyService;
        private IParcelService parcelService;
        private static Random random = new Random();

        public VoucherController(IVoucherService _voucherService, IFoodbankService _foodbankService, IMyReferralService _myReferralService, IFamilyService _familyService
            , IParcelService _parcelService)
        {
            voucherService = _voucherService;
            foodbankService = _foodbankService;
            myReferralService = _myReferralService;
            familyService = _familyService;
            parcelService = _parcelService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BindVoucherList(DataTableServerSide model)
        {
            KeyValuePair<int, List<VoucherDto>> voucher = new KeyValuePair<int, List<VoucherDto>>();
            voucher = voucherService.GetVoucherList(model, Convert.ToInt32(CurrentUser.FoodbankId));
            return Json(new
            {
                draw = model.draw,
                recordsTotal = voucher.Key,
                recordsFiltered = voucher.Key,
                data = voucher.Value.Select((c, index) => new List<object> {
                    c.VoucherId,
                    model.start+index+1,
                     $"{c.FamilyName}",
                    $"{c.ReferrerName}",
                   
                    c.AddedDate.ToString("MM/dd/yyyy"),
                    c.RedeemedDate!=null? c.RedeemedDate.Value.ToString("MM/dd/yyyy"):"-",
                    string.IsNullOrWhiteSpace(c.VoucherToken)? "<a href=" + Url.Action("VoucherToken", "Voucher", new { id = c.VoucherId }) + " class='btn btn-primary grid-btn btn-sm'>Generate</a>": c.VoucherToken,

                    "<a href=" + Url.Action("CreateEdit", "Voucher", new { id = c.VoucherId }) + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"

                    + "<a data-toggle='modal' data-target='#modal-delete-voucher' href='" + Url.Action("Delete", "Voucher", new { id = c.VoucherId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    + "<a href=" + Url.Action("View", "Voucher", new { id = c.VoucherId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>",
                })
            });
        }

        #region Add/Edit Voucher
        ///// <summary>
        ///// To get or open the partial view to edit/create voucher type
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        public IActionResult CreateEdit(int? id = null)
        {
            var model = new VoucherDto();
            if (id.HasValue)
            {
                var voucher = voucherService.GetVoucherById(id.Value);
                model.ReferrerId = voucher.ReferrerId??0;
                model.FamilyId = voucher.FamilyId;
                model.hdnFamilyId = voucher.FamilyId;
                model.AddedDate = voucher.AddedDate;
                model.RedeemedDate = voucher.RedeemedDate;
                model.VoucherToken = voucher.VoucherToken;
                model.FoodBankId = CurrentUser.FoodbankId;
                model.VoucherId = voucher.Id;
            }
            else
            {
                model.FoodBankId = CurrentUser.FoodbankId;
                model.AddedDate = DateTime.Now;
            }

            List<SelectListItem> referrerList = new List<SelectListItem>();

            var referrers = myReferralService.GetAllReferralForVoucher(CurrentUser.FoodbankId);
            referrerList.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (var item in referrers)
            {
                referrerList.Add(new SelectListItem
                {
                    Value = ((int)item.Id).ToString(),
                    Text = ($"{item.Name}").ToString(),
                });
            }
            ViewBag.ReferrerList = referrerList;

            var familylist = familyService.GetAllFamily(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.FamilyName,
                Value = x.Id.ToString()
            }).ToList();
            familylist.Insert(0, new SelectListItem { Text = "Select", Value = "" });
            ViewBag.FamilyList = familylist;

            return View(model);
        }
        [HttpPost]
        public IActionResult CreateEdit(VoucherDto model)
        {
            try
            {
                var isExits = model.VoucherId > 0 ? true : false;
                var obj = voucherService.GetVoucherById(model.VoucherId);

                Voucher voucher = obj != null ? obj : new Voucher();
                voucher.VoucherToken = GeneraterToken();
                if (model.ReferrerId > 0)
                { 
                    voucher.ReferrerId = (int?)model.ReferrerId; 
                }
                voucher.FamilyId = model.FamilyId;
                voucher.ParcelTypeId = 1;//model.ParcelTypeId;
                voucher.RedeemedDate = model.RedeemedDate;
                voucher.AddedDate = isExits ? voucher.AddedDate : DateTime.Now;
                voucher.ModfiedDate = DateTime.Now;

                var result = voucherService.Save(voucher);
                if (result)
                {
                    ShowSuccessMessage("Success!", "Voucher has been " + (isExits ? "updated" : "added") + " successfully.", false);
                    return RedirectToAction("createedit", "voucher");
                }
                else
                {
                    ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                    return RedirectToAction("createedit", "voucher");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                return RedirectToAction("createedit", "voucher");
            }
        }

        [HttpGet]
        public string GeneraterToken()
        {
        Checked:
            var token = RandomString(8);
            if (!string.IsNullOrWhiteSpace(token))
            {
                var isExist = voucherService.CheckToken(token);
                if (isExist)
                {
                    goto Checked;
                }
                else
                {
                    return token;
                    //var grantor = voucherService.GetVoucherById(id.Value);
                    //grantor.VoucherToken = token;
                    //voucherService.Save(grantor);
                }
            }
            else
            {
                return string.Empty;
            }
        }

        [HttpGet]
        public IActionResult GetFamilyList(int referrerId)
        {
            var referrerFamilyList = familyService.GetReferrerFamily(referrerId).Select(x => new SelectListItem
            {
                Text = x.Family.FamilyName,
                Value = x.FamilyId.ToString()
            }).ToList();
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = referrerFamilyList });
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region Delete Section
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this voucher?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Voucher" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the voucher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var voucher = voucherService.GetVoucherById(id);
                if (voucher != null)
                {
                    voucherService.Delete(id);
                }
                ShowSuccessMessage("Success!", "Voucher has been deleted successfully.", false);
                return RedirectToAction("index", "voucher");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("index", "voucher");
            }
        }
        #endregion

        [HttpGet]
        public IActionResult View(int? id = null)
        {
            var model = new VoucherDto();

            if (id.HasValue)
            {
                var voucher = voucherService.GetVoucherById(id.Value);
                model.ReferrerId = voucher.ReferrerId??0;
                model.FamilyId = voucher.FamilyId;
                model.hdnFamilyId = voucher.FamilyId;
                model.AddedDate = voucher.AddedDate;
                model.RedeemedDate = voucher.RedeemedDate;
                model.VoucherToken = voucher.VoucherToken;
                model.FoodBankId = CurrentUser.FoodbankId;
                model.VoucherId = voucher.Id;
            }

            List<SelectListItem> referrerList = new List<SelectListItem>();

            var referrers = myReferralService.GetAllReferralForVoucher(CurrentUser.FoodbankId);
            referrerList.Add(new SelectListItem { Text = "Select", Value = "" });
            foreach (var item in referrers)
            {
                referrerList.Add(new SelectListItem
                {
                    Value = ((int)item.Id).ToString(),
                    Text = ($"{item.Name}").ToString(),
                });
            }
            ViewBag.ReferrerList = referrerList;

            List<SelectListItem> familyList = new List<SelectListItem>();
            familyList = familyService.GetAllFamily(CurrentUser.FoodbankId).Select(x => new SelectListItem
            {
                Text = x.FamilyName,
                Value = x.Id.ToString()
            }).ToList();
            familyList.Add(new SelectListItem { Text = "Select", Value = "" });

            ViewBag.FamilyList = familyList;

            return View(model);
        }

        [HttpGet]
        public IActionResult DownloadQRCode(int voucherId)
        {
            FamilyParcelDto model = new FamilyParcelDto();
            var parcel = parcelService.GetParcelByVoucherToken(voucherId);
            model.ParcelQrcode = parcel != null && parcel.ParcelQrcode != null ? Convert.ToBase64String(parcel.ParcelQrcode) : null;
            model.VoucherId = voucherId;
            return PartialView("_GenerateQRCode", model);
        }

        [HttpGet]
        public IActionResult DownloadQRImage(int voucherId)
        {
            var voucher = voucherService.GetVoucherById(voucherId);
            MemoryStream ms = new MemoryStream(voucher.VoucherQrcode);
            string fileName = "Voucher_QRCode" + "_" + DateTime.Now + ".pdf";
            ms.Position = 0;

            FamilyParcelDto model = new FamilyParcelDto();
            var parcel = parcelService.GetParcelByVoucherToken(voucherId);
            model.ParcelQrcode = parcel != null && parcel.ParcelQrcode != null ? Convert.ToBase64String(parcel.ParcelQrcode) : null;

            ViewData["ReportTitle"] = "QR Code";

            string page = @"<div>
            <section class=''>
            <div class='modal-header'>
            <h4 class='modal-title'>Scan for parcel detail.</h4>
            </div>
            <div style='text-align: center;'>
                <img src='data:image/gif;base64," + model.ParcelQrcode + @"' style='width:60%' />
            </div>
            </section>
            </div>";

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
