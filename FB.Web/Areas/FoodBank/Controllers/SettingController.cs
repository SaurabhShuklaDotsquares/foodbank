using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class SettingController : BaseController
    {
        private readonly IFoodbankService foodbankService;
        public SettingController(IFoodbankService _foodbankService)
        {
            foodbankService = _foodbankService;
        }

        #region Foodbank Setting Save
        [HttpGet]
        public IActionResult Index()
        {
            FoodbankSettingDto model = new FoodbankSettingDto();
            if (CurrentUser.FoodbankId > 0)
            {
                var objFoodbankSetting = foodbankService.GetFoodbankSetting(CurrentUser.FoodbankId);
                model.TailoredNotes = objFoodbankSetting.TailoredNotes;
                model.Email = objFoodbankSetting.Email;
                model.PhoneNumber = objFoodbankSetting.PhoneNumber;
                model.ParcelLimit = Convert.ToString(objFoodbankSetting.ParcelLimit);
                model.ReferralLimit = Convert.ToString(objFoodbankSetting.DailyReferralLimit);
                model.DashboardImage = Convert.ToString(objFoodbankSetting.DashboardImage);
                model.LogoImage = Convert.ToString(objFoodbankSetting.LogoImage);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(FoodbankSettingDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string LogoImagefileName = null;
                    string DashboardImagefileName = null;
                    if (Request.Form.Files.Count > 0)
                    {
                        var fileToUpload1 = Request.Form.Files.GetFile("LogoImage");

                        if (fileToUpload1 != null)
                        {
                            var imageStream = fileToUpload1.OpenReadStream();
                            var image = Image.FromStream(imageStream);
                            int Width = 40;
                            int Height = 40;
                            int newWidth = Width;
                            int newHeight = Convert.ToInt32(Width * ((double)image.Height / (double)image.Width));
                            if (newHeight < 40)
                                newHeight = Height;

                            var newImage = FB.Core.Extensions.GetReducedImage(newWidth, newHeight, imageStream);
                            if (newImage == null)
                            {
                                throw new Exception("Geeting error for create thumbnail image.");
                            }
                            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(newImage, typeof(byte[]));
                            string path = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "FoodbankSettingPhotos");
                            LogoImagefileName = $"{Guid.NewGuid()}.jpg";

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            // Save the image in JPEG format.
                            image.Save(Path.Combine(path, LogoImagefileName), ImageFormat.Jpeg);
                        }
                        var fileToUpload2 = Request.Form.Files.GetFile("DashboardImage");
                        if (fileToUpload2 != null)
                        {
                            var imageStream = fileToUpload2.OpenReadStream();
                            var image = Image.FromStream(imageStream);
                            int Width = 40;
                            int Height = 40;
                            int newWidth = Width;
                            int newHeight = Convert.ToInt32(Width * ((double)image.Height / (double)image.Width));
                            if (newHeight < 40)
                                newHeight = Height;

                            var newImage = FB.Core.Extensions.GetReducedImage(newWidth, newHeight, imageStream);
                            if (newImage == null)
                            {
                                throw new Exception("Geeting error for create thumbnail image.");
                            }
                            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(newImage, typeof(byte[]));
                            string path = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "FoodbankSettingPhotos");
                            DashboardImagefileName = $"{Guid.NewGuid()}.jpg";

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            // Save the image in JPEG format.
                            image.Save(Path.Combine(path, DashboardImagefileName), ImageFormat.Jpeg);
                        }
                    }
                    FoodbankSetting entity;
                    var objFoodbankSetting = foodbankService.GetFoodbankSetting(CurrentUser.FoodbankId);
                    if (objFoodbankSetting == null)
                    {
                        entity = new FoodbankSetting();
                        entity.AddedDate = DateTime.Now;
                        entity.FoodBankId = CurrentUser.FoodbankId;
                    }
                    else
                    {
                        entity =  objFoodbankSetting;
                    }
                    if (LogoImagefileName != null)
                    {
                        entity.LogoImage = LogoImagefileName;
                    }
                    if (DashboardImagefileName != null)
                    {
                        entity.DashboardImage = DashboardImagefileName;
                    }
   
                    entity.TailoredNotes = model.TailoredNotes;
                    entity.Email = model.Email;
                    entity.PhoneNumber = model.PhoneNumber;
                    entity.DailyReferralLimit = Convert.ToInt32(model.ReferralLimit);
                    entity.ParcelLimit = Convert.ToInt32(model.ParcelLimit);

                    if (foodbankService.SaveFoodbankSetting(entity))
                    {
                        ShowSuccessMessage("Success!", "Setting has been save successfully.", false);
                        return RedirectToAction("Index", "Setting");
                    }
                    else
                    {
                        ShowErrorMessage("Error!", "Error", false);
                        return View(model);
                    }
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage("Error!", Ex.Message, false);
                    return View(model);
                }
            }
            else
            {
                ShowErrorMessage("Error!", "Please fill all required fields!", false);
                return View(model);
            }
        }
        #endregion
    }
}
