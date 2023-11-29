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
    public class AgenciesController : BaseController
    {
        private IAgenciesService agenciesService;
        private IFoodbankService foodbankService;
        private readonly ICountryService countryService;
        private readonly IAddressService addressService;
        private readonly IContactService contactService;
        private readonly IFamilyService repoFamily;
        public AgenciesController(IAgenciesService _agenciesService, IFoodbankService _foodbankService, ICountryService _countryService, IAddressService _addressService,
            IContactService _contactService, IFamilyService _repoFamily)
        {
            agenciesService = _agenciesService;
            foodbankService = _foodbankService;
            countryService = _countryService;
            addressService = _addressService;
            contactService = _contactService;
            repoFamily = _repoFamily;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BindAgenciesList(DataTableServerSide model)
        {
            KeyValuePair<int, List<AgenciesDto>> grantor = new KeyValuePair<int, List<AgenciesDto>>();
            grantor = agenciesService.GetGrantorList(model, CurrentUser.FoodbankId);
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
                    "<a href=" + Url.Action("CreateEdit", "Agencies", new { id = c.AgencyId }) + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"

                    + "<a data-toggle='modal' data-target='#modal-delete-donor' href='" + Url.Action("Delete", "Agencies", new { id = c.AgencyId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    + "<a href=" + Url.Action("View", "Agencies", new { id = c.AgencyId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>",
                })
            });
        }

        ///// <summary>
        ///// To get or open the partial view to edit/create membership type
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        public IActionResult CreateEdit(int? id = null)
        {
            var model = new AgenciesDto();

            if (id.HasValue)
            {
                var grantor = agenciesService.GetGrantorById(id.Value);
                model.AgencyName = grantor.Name;
                model.Email = grantor.Contact.Email;
                model.ContactNumber = grantor.Contact.Mobile;
                model.BriefSummary = grantor.Contact.Mobile;
                model.FoodBankId = CurrentUser.FoodbankId;
                model.AgencyId = id.Value;
                model.ContactId = grantor.ContactId;
                model.AddressID = grantor.AddressId;
                model.BriefSummary = grantor.BriefSummary;

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
            model.CountryID = Convert.ToInt32(model.AddressID == 0 ? SiteKeys.DefaultCountryId : model.AddressID.ToString());
            model.CountryName = SiteKeys.DefaultCountryName;
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateEdit(AgenciesDto model)
        {
            try
            {
                var isExits = model.AgencyId > 0 ? true : false;
                var obj = agenciesService.GetGrantorById(model.AgencyId);
                Agencies grantor = obj != null ? obj : new Agencies();

                grantor.Name = model.AgencyName;
                grantor.BriefSummary = model.BriefSummary;
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
                fbcontact.ForeName = model.AgencyName;
                fbcontact.Surname = string.Empty;
                fbcontact.Mobile = model.ContactNumber;
                fbcontact.Email = model.Email;
                if (!isExits)
                {
                    fbcontact.AddedDate = DateTime.Now;
                }
                fbcontact.ModifiedDate = DateTime.Now;
                grantor.Contact = fbcontact;
                #endregion End

                var result = agenciesService.Save(grantor);

                if (result)
                {
                    ShowSuccessMessage("Success!", "Agency has been " + (isExits ? "updated" : "added") + " successfully.", false);
                    return RedirectToAction("createedit", "Agencies");
                }
                else
                {
                    ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                    return RedirectToAction("createedit", "Agencies");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!", "Something went wrong. Please try again after some time.", false);
                return RedirectToAction("createedit", "Agencies");
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


        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this Agency?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Agency" },
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
                var grantor = agenciesService.GetGrantorById(id);
                if (grantor != null)
                {
                    agenciesService.Delete(id);
                }
                ShowSuccessMessage("Success!", "Agency has been deleted successfully.", false);
                return RedirectToAction("index", "Agencies");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("index", "Agencies");
            }
        }

        [HttpGet]
        public IActionResult View(int? id = null)
        {
            try
            {
                var model = new AgenciesDto();

                if (id.HasValue)
                {
                    var agencie = agenciesService.GetGrantorById(id.Value);
                    model.AgencyName = agencie.Name;
                    model.Email = agencie.Contact.Email;
                    model.ContactNumber = agencie.Contact.Mobile;
                    model.FoodBankId = CurrentUser.FoodbankId;
                    model.AgencyId = id.Value;
                    model.ContactId = agencie.ContactId;
                    model.AddressID = agencie.AddressId;
                    model.BriefSummary = agencie.BriefSummary;

                    if (agencie.Address != null)
                    {
                        model.PostCode = agencie.Address.Postcode;
                        model.StreetName = agencie.Address.Street;
                        model.HouseName = agencie.Address.HouseName;
                        model.HouseNumber = agencie.Address.HouseNumber;
                        model.City = agencie.Address.City;
                    }
                }
                else
                {
                    model.FoodBankId = CurrentUser.UserID;
                }
                BindCountriesViewBag();
                model.CountryID = Convert.ToInt32(model.AddressID == 0 ? SiteKeys.DefaultCountryId : model.AddressID.ToString());
                model.CountryName = SiteKeys.DefaultCountryName;
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", "Agencies");
            }
        }

        [HttpPost]
        public IActionResult Createfamily(DataTableServerSide model, int AgencyId)
        {
            var foodbank = foodbankService.GetFoodbankByUserId(CurrentUser.UserID);
            KeyValuePair<int, List<FamilyAgency>> list = new KeyValuePair<int, List<FamilyAgency>>();
            list = repoFamily.GetFeedbackListByFoodbank(model, foodbank.Id, AgencyId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = list.Key,
                recordsFiltered = list.Key,
                data = list.Value.Select((c, index) => new List<object>
                {
                c.FamilyId,//0
                model.start+index+1,//1
                c.Family.FamilyName, //2
                c.Family.Contactno, //3
                c.Family.Email,//4
                c.Family.TotalFamily,//5
               })
            });
        }
    }
}
