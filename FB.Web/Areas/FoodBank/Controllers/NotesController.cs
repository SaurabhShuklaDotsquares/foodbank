using AutoMapper;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto.Branch;
using FB.Dto.Foodbank;
using FB.ModalMapper;
using FB.ModelMapper;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FB.Web.Areas.FoodBank.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class NotesController : BaseController
    {

      
        private INoteService noteService;
        private readonly IMyReferralService referrerService;
        private readonly ICountryService countryService;
        private readonly IUserService userService;
        private readonly IQuickDonorGiftService quickDonorGiftService;
        private readonly IRoleService roleService;
        private readonly IFoodbankService foodbankService;
        private IGrantorService grantorService;
        private readonly IFamilyService familyService;
        private readonly IAllergiesService allergyService; private ICentralOfficeService centralOfficeService;
        private ICharityService charityService;
        private IBranchService branchService;
        public NotesController(IMyReferralService _referrerService, ICountryService _countryService,
            IUserService _userService, IQuickDonorGiftService _quickDonorGiftService, IRoleService _roleService,
            IFoodbankService _foodbankService, IMyReferralService _ReferralService, IGrantorService _grantorService, IFamilyService _familyService, IAllergiesService _allergyService,
             ICentralOfficeService _centralOfficeService,
            ICharityService _charityService,
            IBranchService _branchService,
            INoteService _noteService
            )
        {
            this.centralOfficeService = _centralOfficeService;
            this.charityService = _charityService;
            this.branchService = _branchService;
            referrerService = _referrerService;
            countryService = _countryService;
            userService = _userService;
            quickDonorGiftService = _quickDonorGiftService;
            roleService = _roleService;
            foodbankService = _foodbankService;

            grantorService = _grantorService; familyService = _familyService;
            noteService = _noteService;
            allergyService = _allergyService;
        }
        //[DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult NoteView(int personId)
        {
            return PartialView("_NoteView", new NoteDto { PersonId = personId });
        }


        /// <summary>
        /// To get the notes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
      //  [DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult GetNotesByPersonId(DataTableServerSide model, int personId)
        {
            KeyValuePair<int, List<NoteDto>> notes = noteService.GetNotes(model, personId, CurrentUser.RoleID, CurrentUser.UserID);
            //var userentity = userService.GetUser(CurrentUser.UserID);
            return Json(new
            {
                draw = model.draw,
                recordsTotal = notes.Key,
                recordsFiltered = notes.Key,
                data = notes.Value.Select(c => new List<object>
                {
                    c.NoteDate,
                    c.Description,
                    c.Privacy,
                    c.Comment,
                    //c.Privacy? (CurrentUser.RoleID== (int)UserRoles.SuperAdmin?c.Comment : ( c.CreatedBy==CurrentUser.UserID ? c.Comment :   userentity.IsPrivateNotesAccess?c.Comment:string.Empty)) : c.Comment,
                    c.CreatedBy,
                    c.NoteId
                })
            });
        }


        /// <summary>
        /// To open the pop up to create and edit the note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
       // [DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult CreateEdit(int personId, int? id = null)
        {
            ViewBag.PrivacyType = Enum.GetValues(typeof(NotesPrivacy)).Cast<NotesPrivacy>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToBoolean(v).ToString()
            }).ToList();

            if (id.HasValue)
            {
                var noteEntity = NoteDtoMapper.Map(noteService.GetNote(id.Value, personId));
                return PartialView("_CreateEdit", noteEntity ?? new NoteDto { PersonId = personId });
            }
            else
            {
                return PartialView("_CreateEdit", new NoteDto { PersonId = personId });
            }
        }

        /// <summary>
        /// To save the contact history
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(NoteDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Dictionary<DataEnityNames, object> dictDataIds = new Dictionary<DataEnityNames, object>
                    //{
                    //    { DataEnityNames.Person, model.PersonId },
                    //    { DataEnityNames.MMONotes, model.NoteId }
                    //};
                    var isAccessible = true;// CheckAuthorisedData(dictDataIds);
                    if (isAccessible)
                    {
                        FamilyNotes entity;
                        if (model.NoteId == 0)
                        {
                            entity = new FamilyNotes
                            {
                                CreatedBy = CurrentUser.UserID
                            };
                        }
                        else
                        {
                            entity = noteService.GetNote(model.NoteId, model.PersonId);
                            if (CurrentUser.RoleID != (int)UserRoles.SuperAdmin && model.Privacy == Convert.ToBoolean(NotesPrivacy.Private))
                            {
                                if (CurrentUser.UserID != entity.CreatedBy)
                                {
                                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "You are unauthorized for update private note.", IsSuccess = false });
                                }
                            }
                        }
                        entity.FamilyId = model.PersonId;
                        entity.Privacy = model.Privacy;
                        entity.Description = model.Description;
                        entity.Comment = model.Privacy ? EncryptionUtils.Encrypt(model.Comment, "") : model.Comment;
                        entity.NoteDate = model.NoteDate.ToDateTimeNullable();
                         noteService.Save(entity, model.NoteId == 0);

                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = model.NoteId > 0 ? "Note updated successfully." : "Note added successfully.", IsSuccess = true });
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
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }
        }

        /// <summary>
        /// To open the popup to delete note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        //[DataActionFilter("id", DataEnityNames.MMONotes)]
       // [DataActionFilter("personId", DataEnityNames.Person)]
        public IActionResult Delete(int personId, int id)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure you want to delete this note?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Heading = "Delete Note " },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        /// <summary>
        /// To delete the contact history
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FC"></param>
        /// <returns></returns>
        [HttpPost]
        //[DataActionFilter("id", DataEnityNames.MMONotes)]
        //[DataActionFilter("personId", DataEnityNames.Person)]
        public string Delete(int personId, int id, IFormCollection FC)
        {
            string message;
            try
            {
                noteService.Delete(id);
                message = "Success";
            }
            catch (Exception ex)
            {
                message = ex.GetBaseException().Message;
            }
            return message;
        }


        protected override void Dispose(bool disposing)
        {
            if (centralOfficeService != null)
            {
                centralOfficeService.Dispose();
                centralOfficeService = null;
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


            base.Dispose(disposing);
        }
    }
}
