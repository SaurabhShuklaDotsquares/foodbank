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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class GrantorController : BaseController
    {
        private IGrantorService grantorService;
        private IFoodbankService foodbankService;
        private readonly ICountryService countryService;
        private readonly IAddressService addressService;
        private readonly IContactService contactService;
        private readonly IFamilyParcelService familyParcelService;
        private readonly IFamilyService familyService;
        private readonly IParcelService parcelService;
        private readonly IVolunteerService volunteerService;
        private readonly IRecipeService recipeService;
        private readonly IFoodService foodService;
        private static Random random = new Random();
        public GrantorController(IGrantorService _grantorService, IFoodbankService _foodbankService, ICountryService _countryService, IAddressService _addressService,
            IContactService _contactService, IFamilyParcelService _familyParcelService, IParcelService _parcelService, IFamilyService _familyService, IVolunteerService _volunteerService, IRecipeService _recipeService,
            IFoodService _foodService)
        {
            grantorService = _grantorService;
            foodbankService = _foodbankService;
            countryService = _countryService;
            addressService = _addressService;
            contactService = _contactService;
            familyParcelService = _familyParcelService;
            parcelService = _parcelService;
            familyService = _familyService;
            volunteerService = _volunteerService;
            recipeService = _recipeService;
            foodService = _foodService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BindGrantorList(DataTableServerSide model)
        {

            KeyValuePair<int, List<GrantorDto>> grantor = new KeyValuePair<int, List<GrantorDto>>();
            grantor = grantorService.GetGrantorList(model, CurrentUser.FoodbankId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = grantor.Key,
                recordsFiltered = grantor.Key,
                data = grantor.Value.Select((c, index) => new List<object> {
                    c.GrantorId,
                    model.start+index+1,
                    $"{ c.ForeName} {c.SurName}",
                    c.ContactNumber,
                    c.TotalAmount,
                    string.IsNullOrWhiteSpace(c.GrantorToken)?
                    "<a href=" + Url.Action("GeneraterToken", "Grantor", new { id = c.GrantorId })
                    + " class='btn btn-primary grid-btn btn-sm'>Generate</a>":c.GrantorToken,

                    "<a href=" + Url.Action("CreateEdit", "Grantor", new { id = c.GrantorId }) + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"

                    + "<a data-toggle='modal' data-target='#modal-delete-donor' href='" + Url.Action("Delete", "Grantor", new { id = c.GrantorId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    + "<a href=" + Url.Action("View", "Grantor", new { id = c.GrantorId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>",
                })
            });
        }

        #region Add Edit Section
        ///// <summary>
        ///// To get or open the partial view to edit/create membership type
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        public IActionResult CreateEdit(int? id = null)
        {
            var model = new GrantorDto();

            if (id.HasValue)
            {
                var grantor = grantorService.GetGrantorById(id.Value);
                model.ForeName = grantor.ForeName;
                model.SurName = grantor.SurName;
                model.Email = grantor.Contact.Email;
                model.ContactNumber = grantor.Contact.Mobile;
                model.TotalAmount = Convert.ToString(grantor.TotalAmount.Value);
                model.FoodBankId = CurrentUser.FoodbankId;
                model.GrantorId = id.Value;
                model.ContactId = grantor.ContactId;
                model.AddressID = grantor.AddressId;
                model.GrantorToken = grantor.GrantorToken;

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
                model.FoodBankId = CurrentUser.FoodbankId;
            }
            BindCountriesViewBag();
            model.CountryID = Convert.ToInt32(SiteKeys.DefaultCountryId);
            model.CountryName = SiteKeys.DefaultCountryName;
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateEdit(GrantorDto model)
        {
            try
            {
                var isExits = model.GrantorId > 0 ? true : false;
                var obj = grantorService.GetGrantorById(model.GrantorId);
                Grantor grantor = obj != null ? obj : new Grantor();

                grantor.ForeName = model.ForeName;
                grantor.SurName = model.SurName;
                grantor.TotalAmount = Convert.ToDecimal(model.TotalAmount);
                grantor.FoodBankId = model.FoodBankId;
                if (!isExits)
                {
                    grantor.AddedDate = DateTime.Now;
                }
                grantor.ModifiedDate = DateTime.Now;

                #region Save Grantor Address
                Fbaddress address = model.AddressID > 0 ? addressService.GetFBAddress(model.AddressID) : new Fbaddress();
                address.HouseName = model.HouseName;
                address.HouseNumber = model.HouseNumber;
                address.Street = model.StreetName;
                address.District = string.IsNullOrWhiteSpace(model.District) ? string.Empty : model.District;
                address.City = model.City;
                address.Postcode = model.PostCode == null ? model.OldPostCode : model.PostCode;
                address.CountryId = model.CountryID.Value;
                address.CountryName = model.CountryName;
                grantor.Address = address;
                #endregion End

                #region Save Grantor Contact
                Fbcontact fbcontact = model.ContactId > 0 ? contactService.GetContactById(model.ContactId) : new Fbcontact();
                fbcontact.ForeName = model.ForeName;
                fbcontact.Surname = model.SurName;
                fbcontact.Mobile = model.ContactNumber;
                fbcontact.Email = model.Email;
                if (!isExits)
                {
                    fbcontact.AddedDate = DateTime.Now;
                }
                fbcontact.ModifiedDate = DateTime.Now;
                grantor.Contact = fbcontact;
                #endregion End

                var result = grantorService.Save(grantor);

                if (result && grantor.GrantorQrcode == null)
                {
                    grantor.GrantorQrcode = GenerateGrantorQRCode(grantor.GrantorToken);
                    grantorService.Save(grantor);
                }

                ShowSuccessMessage("Success!", "Grantor has been " + (isExits ? "updated" : "added") + " successfully.", false);
                return RedirectToAction("createedit", "grantor");

            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                return RedirectToAction("createedit", "grantor");
            }
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

        #region Delete Section
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this grantor?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Grantor" },
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
        public IActionResult Delete(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var grantor = grantorService.GetGrantorById(id);
                if (grantor != null)
                {
                    grantorService.Delete(id);
                }
                ShowSuccessMessage("Success!", "Grantor has been deleted successfully.", false);
                return RedirectToAction("index", "grantor");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("index", "grantor");
            }
        }
        #endregion

        [HttpGet]
        public IActionResult GeneraterToken(int? id = null)
        {
        Checked:
            var token = RandomString(8);
            if (!string.IsNullOrWhiteSpace(token))
            {
                var isExist = grantorService.CheckToken(token);
                if (isExist)
                {
                    goto Checked;
                }
                else
                {
                    var grantor = grantorService.GetGrantorById(id.Value);
                    grantor.GrantorToken = token;
                    grantorService.Save(grantor);
                }
            }
            return RedirectToAction("index", "grantor");
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        public IActionResult BindGrantorTableView(DataTableServerSide model, int FoodbankId, int GrantorId)
        {
            KeyValuePair<int, List<Parcels>> parcels = new KeyValuePair<int, List<Parcels>>();
            parcels = familyParcelService.GetParcelfoodlists(model, CurrentUser.FoodbankId, GrantorId);

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
        public IActionResult View(int? id = null)
        {
            var model = new GrantorDto();

            if (id.HasValue)
            {
                var grantor = grantorService.GetGrantorById(id.Value);
                if (grantor != null)
                {
                    model.ForeName = grantor.ForeName;
                    model.SurName = grantor.SurName;
                    model.Email = grantor.Contact.Email;
                    model.ContactNumber = grantor.Contact.Mobile;
                    model.TotalAmount = Convert.ToString(grantor.TotalAmount.Value);
                    model.FoodBankId = CurrentUser.FoodbankId;
                    model.GrantorId = id.Value;
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
        public IActionResult ViewItems(int? id = null)
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

        public static byte[] GenerateGrantorQRCode(string grantorToken)
        {
            byte[] QRbyte = null;
            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            string url = "";
            url = SiteKeys.DomainName + "Common/GrantorView?token=" + grantorToken;

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
        public IActionResult DownloadQRCode(string grantorToken)
        {
            GrantorDto model = new GrantorDto();
            var grantor = grantorService.GetGrantorByToken(grantorToken);
            model.GrantorQrCode = grantor != null && grantor.GrantorQrcode != null ? Convert.ToBase64String(grantor.GrantorQrcode) : null;
            model.GrantorToken = grantorToken;
            return PartialView("_GenerateQRCode", model);
        }

        [HttpGet]
        public IActionResult DownloadQRImage(string grantorToken)
        {
            var grantor = grantorService.GetGrantorByToken(grantorToken);
            MemoryStream ms = new MemoryStream(grantor.GrantorQrcode);
            string fileName = "Grantor_QRCode" + "_" + DateTime.Now + ".pdf";
            ms.Position = 0;
            ViewData["ReportTitle"] = "QR Code";

            string page = @"<div>
            <section class=''>
            <div class='modal-header'>
            <h4 class='modal-title'>Scan for grantor detail.</h4>
            </div>
            <div style='text-align: center;'>
                <img src='data:image/gif;base64," + grantor.GrantorQrcode + @"' style='width:60%' />
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
