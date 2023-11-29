using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class ProfessionController : BaseController
    {

        private readonly IProfessionServices _professionServices;
        private readonly IFoodbankService _foodbankService;


        public ProfessionController(IProfessionServices professionServices, IFoodbankService foodbankService) // that is the constructor 
        {
            _professionServices = professionServices;
            _foodbankService = foodbankService;

        }
        public IActionResult Index()
        {
            var foodbank = _foodbankService.GetFoodbankByUserId(CurrentUser.UserID);
            Professiondto model = new Professiondto();
            model.FoodBankId = foodbank.Id;

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DataTableServerSide model, int foodbankId)
        {

            KeyValuePair<int, List<Profession>> list = new KeyValuePair<int, List<Profession>>();
            list = _professionServices.GetProfessionByFoodBankId(model, foodbankId);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = list.Key,
                recordsFiltered = list.Key,
                data = list.Value.Select((c, index) => new List<object> {
                    c.ProfessionId,//0
                    model.start+index+1,//1
                    c.Title,//2
                    c.ModifiedDate?.ToString("dd/MM/yyyy"),//3

                     "<a data-toggle='modal' data-target='#modal-add-Profession'  href=" + Url.Action("AddEditProfession", "Profession", new { id = c.ProfessionId })
                      + " class='btn btn-primary grid-btn btn-sm'> Edit <i class='fa fa-edit'></i></a>&nbsp;" // for Edit button

                    + "<a data-toggle='modal' data-target='#modal-delete-Profession' href=" + Url.Action("Delete", "Profession", new { id = c.ProfessionId })
                    + " class='btn btn-danger grid-btn btn-sm ps3 delete-btn'> Delete <i class='fa fa-trash-o'></i></a>&nbsp;" // for Delete button


                })
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {

                Message = "Are you sure to delete this Profession Name?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Profession Name" },
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
                var Profession = _professionServices.GetProfessionById(id);
                if (Profession != null)
                {
                    _professionServices.DeleteFoodDonation(id);
                }
                ShowSuccessMessage("Success!", "Profession Name has been deleted successfully.", false);
                return RedirectToAction("Index", "Profession");
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                    message = "Error";

                ShowErrorMessage("Success!", message, false);
                return RedirectToAction("Index", "Profession");
            }
        }

        [HttpGet]
        public IActionResult AddEditProfession(int? id = null)
        {
            Professiondto model = new Professiondto();
            if (id.HasValue)
            {
                Profession profession = _professionServices.GetProfessionID(id.Value);
                model.ProfessionId = profession.ProfessionId;
                model.Title = profession.Title;
                model.AddedDate = profession.AddedDate;
                model.ModifiedDate = profession.ModifiedDate;
            }

            model.FoodBankId = _foodbankService.GetFoodbankByUserId(CurrentUser.UserID).Id;
            return PartialView("_AddEditProfession", model);
        }
        [HttpPost]
        public IActionResult AddEditProfession(Professiondto model)
        {
            try
            {
                bool isExist = false;
                var profession = _professionServices.GetProfessionID(model.ProfessionId);

                isExist = profession != null ? true : false; 

                profession = isExist ? profession : new Profession();
                profession.Title = model.Title;
                profession.AddedDate = isExist ? profession.AddedDate : DateTime.Now;
                profession.ModifiedDate = DateTime.Now;
                profession.FoodbankId = isExist ? profession.FoodbankId : model.FoodBankId;

                if (isExist)
                {
                    _professionServices.Update(profession);
                }
                else
                {
                    _professionServices.SaveProfession(profession);
                }
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Profession has been" + (isExist ? "updated" : "added") + "successfully.", IsSuccess = true });

            }
            catch (Exception ex)
            {

                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.Message, IsSuccess = false });
            }
        }
    }
}


