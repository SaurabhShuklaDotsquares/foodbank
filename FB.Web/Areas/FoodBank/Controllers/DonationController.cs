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
    public class DonationController : BaseController
    {
        private readonly IDonorService donorservices;
        private readonly IFoodbankService foodbankService;
        private readonly IFoodService foodService;
        private IBranchService branchService;
        private ICharityService charityService;
        public DonationController(IDonorService _donorservices, IFoodbankService _foodbankService, IFoodService _foodService, IBranchService _branchService, ICharityService _charityService)
        {
            donorservices = _donorservices;
            foodbankService = _foodbankService;
            foodService = _foodService;
            branchService = _branchService;
            charityService = _charityService;
        }

        #region Food Donation List
        [HttpGet]
        public IActionResult MyFoodDonation()
        {
            FoodDonationDto model = new FoodDonationDto();
            model.FoodBankId = CurrentUser.FoodbankId;
            BindOrganisationViewBag(CurrentUser.OrganisationID, 0, 0);
            return View(model);
        }
        [NonAction]
        public void BindOrganisationViewBag(int CentralOfficeId, int charityID, int BranchID)
        {
            //ViewBag.Organisations = centralofficeService.GetCentralOffices(CentralOfficeId).Select(c => new SelectListItem
            //{
            //    Text = c.OrganisationName,
            //    Value = c.CentralOfficeId.ToString()
            //}).ToList();

            var Charities = charityService.GetCharitiesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, CentralOfficeId, CurrentUser.UserID, true, true, charityID).Select(c => new SelectListItem
            {
                Text = c.CharityName.AddCharityPrefix(c.Prefix),
                Value = c.CharityId.ToString(),
                Selected= c.CharityId== charityID?true:false
            }).ToList();


            var Branches = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, charityID, CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString(),
                Selected = c.BranchId == BranchID ? true : false
            }).ToList();

            Branches.Insert(0, new SelectListItem("Select Branch", ""));

            Charities.Insert(0, new SelectListItem("Select Charity", ""));
            ViewBag.Branches = Branches;
            ViewBag.Charities = Charities;
            ViewBag.PersonTypes = new List<SelectListItem>();
            ViewBag.Methods = new List<SelectListItem>();
            ViewBag.Purposes = new List<SelectListItem>();
        }
        [HttpGet]
        public IActionResult BindBranches(int charityID)
        {
            var result = branchService.GetBranchesByDataAccessibility(CurrentUser.DataAccessibilities, CurrentUser.RoleID, charityID, userID: CurrentUser.UserID).Select(c => new SelectListItem
            {
                Text = c.BranchDescription.AddBranchPrefix(c.BranchReference, c.Charity?.Prefix),
                Value = c.BranchId.ToString()
            }).ToList();
            //result.Insert(0, new SelectListItem("Select Branch", ""));


            ViewBag.Branches = result;
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = result });
        }
        [HttpPost]
        public IActionResult MyFoodDonation(DataTableServerSide model, int foodbankId, int charitID, int BranchID)
        {
            KeyValuePair<int, List<DonorDonationDto>> centralOffices = new KeyValuePair<int, List<DonorDonationDto>>();
            centralOffices = donorservices.GetFoodDonationsByFoodBankId(model, foodbankId,  charitID,  BranchID);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    c.FoodItemId,
                    model.start+index+1,
                    c.DonorName,
                    c.DonationDate.ToString("dd/MM/yyyy"),
                    c.FoodItemName,
                    c.Quantity + c.QuantityUnit,
                    ((DonationStatus)c.Status).GetDescription(),

                    "<a data-toggle='modal' data-target='#modal-add-food-donations' onclick='ShowLoader()' href=" + Url.Action("addfooddonation", "donation", new { id = c.FoodItemId }) + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"

                    + "<a data-toggle='modal' data-target='#modal-delete-donations' href='" + Url.Action("delete", "donation", new { id = c.FoodItemId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>&nbsp;"

                    + "<a data-toggle='modal' data-target='#modal-view-donations' href=" + Url.Action("viewdonation", "donation", new { id = c.FoodItemId }) + " class='btn btn-primary grid-btn btn-sm'>View <i class='fa fa-eye'></i></a>"
                })
            });
        }
        #endregion

        #region Add Food Donation
        [HttpGet]
        public IActionResult AddFoodDonation(int? id = null)
        {
            var donorList = foodbankService.GetDonorList(CurrentUser.FoodbankId,0,0);
            BindOrganisationViewBag(CurrentUser.OrganisationID, 0, 0);
            FoodDonationDto model = new FoodDonationDto();
            if (id.HasValue)
            {
                var foodItem = foodbankService.GetFoodDonationById(id.Value);
                if (foodItem.Id > 0)
                {
                    model.DonationDate = (foodItem.ExpiryDate == null ? "" : foodItem.ExpiryDate.Value.Date.ToString("dd/MM/yyyy"));
                    model.Quantity = foodItem.Quntity.Value;
                    model.QuantityUnit = foodItem.QuantityUnit;
                    model.DonorId = foodItem.Donorid.Value;
                    model.FoodIId = foodItem.Food.Id;
                    model.FoodItemId = foodItem.Id;
                    model.Status = foodItem.Status.Value;

                    model.FoodItemName = foodItem.Food.Name;
                    model.FoodCategoryId = foodItem.Food.CategoryApiId;
                    model.hdnFoodCategoryId = foodItem.Food.CategoryApiId;
                    model.ProductApiId = foodItem.Food.ProductIdApi;
                    model.hdnFoodProductId = foodItem.Food.ProductIdApi;
                }
            }
            else
            {
                model.Status = 0;
            }

            

            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

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
            ViewBag.donorItemList = donorItemList;
            var foodItemCatories = OpenFoodApi.GetFoodItemCatogoriesList();
            var foodlist = new List<SelectListItem>();

            foodItemCatories.Insert(0, selectvaule);
            ViewBag.FoodItemCatogoriesList = foodItemCatories;

            foodlist.Insert(0, selectvaule);
            ViewBag.FoodItemList = foodlist;

            return PartialView("_AddEditFoodDonation", model);
        }
        [HttpGet]
        public IActionResult BindDonorList(int charityID,int Branchid)
        {
            var donorList = foodbankService.GetDonorList(CurrentUser.FoodbankId,  charityID,  Branchid);

            SelectListItem selectvaule = new SelectListItem();
            selectvaule.Value = "";
            selectvaule.Text = "Select";

            List<SelectListItem> donorItemList = new List<SelectListItem>();
            foreach (var items in donorList)
            {
                donorItemList.Add(new SelectListItem
                {
                    Value = ((int)items.Donor.PersonId).ToString(),
                    Text = ($"{items.Donor.Forenames}{items.Donor.Surname}").ToString(),
                });
            }
            //donorItemList.Insert(0, selectvaule);
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = donorItemList });
        }
        [HttpPost]
        public IActionResult AddFooddonation(FoodDonationDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isExist = model.FoodItemId > 0 ? true : false;
                    FoodItem entity = model.FoodItemId > 0 ? foodbankService.GetFoodDonationById(model.FoodItemId) : new FoodItem();
                    entity.Donorid = model.DonorId;
                    entity.Quntity = model.Quantity;
                    entity.QuantityUnit = model.QuantityUnit;
                    entity.ExpiryDate = Convert.ToDateTime(model.DonationDate);
                    if (!isExist)
                    {
                        entity.AddedDate = DateTime.Now;
                    }
                    entity.Status = model.Status;

                    if (!foodService.GetAllFoodList().Select(x => x.ProductIdApi).Contains(model.ProductApiId.ToString()))
                    {
                        Food obj = new Food()
                        {
                            ProductIdApi = model.ProductApiId.ToString(),
                            Name = model.FoodItemName,
                            CategoryApiId = model.FoodCategoryId
                        };
                        entity.Food = obj;
                    }
                    else
                    {
                        model.FoodIId = Convert.ToInt32(foodService.GetAllFoodList().Where(x => x.ProductIdApi == model.ProductApiId.ToString()).FirstOrDefault().Id);
                    }

                    bool result = foodbankService.SaveFoodDonation(entity);

                    if (result)
                    {
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Donation has been " + (isExist ? "updated" : "added") + " successfully.", IsSuccess = true });
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
            else
            {
                string message = "Please fill all require field.";
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });

            }
        }
        #endregion

        #region Delete Donation
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this donatiion?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Donatiion" },
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
                var donation = foodbankService.GetFoodDonationById(id);
                if (donation != null)
                {
                    foodbankService.DeleteFoodDonation(id);
                }
                ShowSuccessMessage("Success!", "Donation has been deleted successfully.", false);
                return RedirectToAction("myfooddonation", "donation");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("myfooddonation", "donation");
            }
        }
        #endregion

        [HttpGet]
        public IActionResult ViewDonation(int? id = null)
        {
            var donorList = foodbankService.GetDonorList(CurrentUser.FoodbankId,0,0);

            FoodDonationDto model = new FoodDonationDto();
            if (id.HasValue)
            {
                var foodItem = foodbankService.GetFoodDonationById(id.Value);
                if (foodItem.Id > 0)
                {
                    model.DonationDate = (foodItem.ExpiryDate == null ? "" : foodItem.ExpiryDate.Value.Date.ToString("dd/MM/yyyy"));
                    model.Quantity = foodItem.Quntity.Value;
                    model.QuantityUnit = foodItem.QuantityUnit;
                    model.DonorId = foodItem.Donorid.Value;
                    model.FoodIId = foodItem.Food.Id;
                    model.FoodItemId = foodItem.Id;
                    model.Status = foodItem.Status.Value;
                    model.FoodItemName = foodItem.Food.Name;
                    if (foodItem.Donor != null)
                    {
                        BindOrganisationViewBag(CurrentUser.OrganisationID, foodItem.Donor.CharityId ?? 0, foodItem.Donor.BranchId ?? 0);
                    }
                    else
                    {
                        BindOrganisationViewBag(CurrentUser.OrganisationID, 0, 0);
                    }
                }
            }

            List<SelectListItem> donorItemList = new List<SelectListItem>();
            foreach (var items in donorList)
            {
                donorItemList.Add(new SelectListItem
                {
                    Value = ((int)items.Donor.PersonId).ToString(),
                    Text = ($"{items.Donor.Forenames}{items.Donor.Surname}").ToString(),
                });
            }
            ViewBag.donorItemList = donorItemList;

            ViewBag.FoodItemList = foodService.GetAllFoodList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return PartialView("_View", model);
        }

        [HttpPost]
        public IActionResult MyDonationPayment(DataTableServerSide model, int foodbankId, int charitID, int BranchID)
        {
            KeyValuePair<int, List<DonorDonationPaymentDto>> centralOffices = new KeyValuePair<int, List<DonorDonationPaymentDto>>();
            centralOffices = donorservices.GetFoodDonationPaymentByFoodbankId(model, foodbankId,  charitID,  BranchID);
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
    }
}
