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
using FB.Web.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Globalization;

namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class FeedbackController : BaseController
    {
        private IFeedbackService feedbackService;
        private IFoodbankService foodbankService;
        public FeedbackController(IFeedbackService _feedbackService,IFoodbankService _foodbankService)
        {
            feedbackService = _feedbackService;
            foodbankService = _foodbankService;

        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// To get the listing of memership type for server side data table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetUserDefinedFields(DataTableServerSide model)
        {
            List<UserDataAccessDto> urser = new List<UserDataAccessDto>();
            var userDefinedFields = feedbackService.GetUserDefinedFields(model, urser, CurrentUser.FoodbankId);

            return Json(new
            {
                draw = model.draw,
                recordsTotal = userDefinedFields.Key,
                recordsFiltered = userDefinedFields.Key,
                data = userDefinedFields.Value.Select((c, index) => new List<object> {
                    c.FieldId,
                    model.start+index+1,
                    c.FieldDescription,
                    c.FieldTypeName,
                    c.FieldDefaultValue,
                    c.IsAutoAssignDefaultValue ? "<i class='fa fa-check'></i>" : "<i class='fa fa-close'></i>",


                    "<a data-toggle='modal' data-target='#modal-create-edit-user-defined-field'  href=" + Url.Action("CreateEdit", "Feedback", new { id = c.FieldId })
                    + " class='btn btn-primary grid-btn btn-sm'>Edit <i class='fa fa-edit'></i></a>&nbsp;"
                    + "<a data-toggle='modal' data-target='#modal-delete-user-defined-field' href='" + Url.Action("Delete", "Feedback", new { id = c.FieldId })
                    + "' class='btn btn-danger grid-btn btn-sm ps3 delete-btn'>Delete <i class='fa fa-trash-o'></i></a>" ,
                    c.FieldId
                })
            });
        }


        ///// <summary>
        ///// To get or open the partial view to edit/create membership type
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [DataActionFilter("id", DataEnityNames.MMOUserDefinedField)]
        public IActionResult CreateEdit(int? id = null)
        {
            var FeedbackMasterDTO = new FeedbackMasterDTO();


            FeedbackMasterDTO.IsHouseHold = PersonTypes.Family;

            ViewBag.UserFieldTypes = Enum.GetValues(typeof(UserFieldType)).Cast<UserFieldType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).ToList();

            ViewBag.FamilyOrIndividual = Enum.GetValues(typeof(PersonTypes)).Cast<PersonTypes>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToByte(v).ToString()
            }).Where(x => x.Value == "0").ToList();

            if (id.HasValue)
            {
                var obj = feedbackService.GetUserDefinedField(id.Value);
                var userDefinedFieldEntity = FeedbackDtoMapper.Map(obj);
                return PartialView("_CreateEdit", userDefinedFieldEntity ?? new FeedbackMasterDTO());
            }

            return PartialView("_CreateEdit", FeedbackMasterDTO);
        }

        ///// <summary>
        ///// To save the membership type
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(FeedbackMasterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    switch (model.FieldType)
                    {
                        case (int)UserFieldType.Text:
                            break;
                        case (int)UserFieldType.Numeric:
                            if (!string.IsNullOrEmpty(model.FieldDefaultValue))
                            {
                                Int32 intValue;
                                if (!Int32.TryParse(model.FieldDefaultValue, out intValue))
                                {
                                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please enter valid integer value.", IsSuccess = false });
                                }
                            }
                            break;
                        case (int)UserFieldType.Date:
                            if (!string.IsNullOrEmpty(model.FieldDefaultValue))
                            {
                                DateTime dateValue;
                                //if (!DateTime.TryParse(model.FieldDefaultValue, out date))
                                if (!DateTime.TryParseExact(model.FieldDefaultValue, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateValue))
                                {
                                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please enter valid date value.", IsSuccess = false });
                                }
                            }
                            break;
                        case (int)UserFieldType.List:
                            if (string.IsNullOrEmpty(model.UserDefinedFieldOptionList) || string.IsNullOrEmpty(model.ListFieldDefaultValue))
                            {
                                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "List type Question can't be empty, please add list and select option default value.", IsSuccess = false });
                            }
                            break;
                    }

                    Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>
                    {
                        { DataEnityNames.MMOUserDefinedField, model.FieldId },
                        { DataEnityNames.CentralOffice, model.CentralOfficeId },
                        { DataEnityNames.Charity, model.CharityId },
                        { DataEnityNames.Branch, model.BranchId }
                    };

                    var isAccessible = CheckAuthorisedData(dictDataIds);
                    if (isAccessible)
                    {
                        if (feedbackService.IsExist(model.FieldId, model.FieldDescription.Trim(), model.FieldType, model.CentralOfficeId, model.CharityId, model.BranchId))
                        {
                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Question already exists", IsSuccess = false });
                        }
                        else
                        {

                            FeedbackMaster entity;
                            if (model.FieldId == 0)
                                entity = new FeedbackMaster();
                            else
                            {
                                var userfieldentityoptions = feedbackService.GetUserDefinedFieldOption(model.FieldId);
                                if (userfieldentityoptions.Count > 0)
                                {
                                    foreach (var entityoption in userfieldentityoptions)
                                    {
                                        feedbackService.UserDefinedFDelete(entityoption.OptionId);
                                    }
                                }

                                entity = feedbackService.GetUserDefinedField(model.FieldId);
                            }
                            entity.IsHouseHold = (byte)model.IsHouseHold;
                            entity.FieldDescription = model.FieldDescription;
                            entity.FieldType = model.FieldType;

                            entity.AuditUserId = CurrentUser.UserID;
                            entity.AuditIp = ContextProvider.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                            entity.IsAutoAssignDefaultValue = model.IsAutoAssignDefaultValue;
                            entity.FoodbankId = CurrentUser.FoodbankId;
                            if (model.FieldType == (int)UserFieldType.List)
                            {
                                entity.FieldDefaultValue = model.ListFieldDefaultValue;
                                if (!string.IsNullOrEmpty(model.UserDefinedFieldOptionList))
                                {
                                    string[] options = model.UserDefinedFieldOptionList.Split(',').ToArray();
                                    for (int i = 0; i < options.Length; i++)
                                    {
                                        entity.FeedbackMasterFieldOption.Add(new FeedbackMasterFieldOption
                                        {
                                            OptionValue = options[i].ToString(),
                                        });
                                    }
                                }
                            }
                            else if (model.FieldType == (int)UserFieldType.Logical)
                            {
                                entity.FieldDefaultValue = model.IsLogical.ToString();
                            }
                            else
                            {
                                entity.FieldDefaultValue = !string.IsNullOrWhiteSpace(model.FieldDefaultValue) ? model.FieldDefaultValue : "-";
                            }
                            feedbackService.Save(entity, model.FieldId == 0);

                            if (entity.IsAutoAssignDefaultValue == true)
                            {

                            }

                            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Question saved successfully.", IsSuccess = true });
                        }
                    }
                    else
                    {
                        return RedirectAccessDenied();
                    }
                }
                else
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = Constants.CustomRequiredErrorMessage, IsSuccess = false });
                }
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                if (message.ToLower().Contains("unique key"))
                    message = "Question already exists for Foodbank.";
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
        }

        public bool CheckAuthorisedData(Dictionary<DataEnityNames, object> dictDataIds)
        {
            var isAuthorised = true;

            if (CurrentUser.RoleID == (int)UserRoles.SuperAdmin || CurrentUser.RoleID == (int)UserRoles.Internal)
                return true;

            if (dictDataIds == null)
                return true;

            object dataCheckId = null;

            return isAuthorised;
        }

        ///// <summary>
        ///// To open the modal pop dialog of delete membership type
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [DataActionFilter("id", DataEnityNames.MMOUserDefinedField)]
        public IActionResult Delete(int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure to delete this user defined field type?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete User Defined Field" },
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
        [DataActionFilter("id", DataEnityNames.MMOUserDefinedField)]
        public string Delete(int id, IFormCollection FC)
        {
            string message;
            try
            {
                var userfieldentityoptions = feedbackService.GetUserDefinedFieldOption(id);
                if (userfieldentityoptions.Count > 0)
                {
                    foreach (var entityoption in userfieldentityoptions)
                    {
                        feedbackService.UserDefinedFDelete(entityoption.OptionId);
                    }
                }
                feedbackService.Delete(id);
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

        [HttpGet]
        public IActionResult FeedbackList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FeedbackList(DataTableServerSide model)
        {
            KeyValuePair<int, List<FeedbackDto>> centralOffices = new KeyValuePair<int, List<FeedbackDto>>();
            centralOffices = feedbackService.GetFeedbackListByFoodbank(model, CurrentUser.FoodbankId,0);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = centralOffices.Key,
                recordsFiltered = centralOffices.Key,
                data = centralOffices.Value.Select((c, index) => new List<object> {
                    model.start+index+1,
                     c.DateCompletd?.ToString("dd/MM/yyyy"),
                    c.FamilyName,
                    c.DeliveryDate,
                    c.ParcelTypeName,
                    c.PackingDate,
                     c.Id
                   })
            });

        }
        [HttpGet]
        public IActionResult FeedbackDetails(int Id)
        {
            var res = feedbackService.GetFeedbackDetailsByFeedbackID(Id);
            return View(res);
        }
    

        protected override void Dispose(bool disposing)
        {
            if (feedbackService != null)
            {
                feedbackService.Dispose();
                feedbackService = null;
            }
            if (foodbankService != null)
            {
                foodbankService.Dispose();
                foodbankService = null;
            }
            base.Dispose(disposing);
        }
    }
}
