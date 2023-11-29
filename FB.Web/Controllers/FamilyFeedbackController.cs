using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Service;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FB.Web.Controllers
{
    public class FamilyFeedbackController : BaseController
    {
        private readonly IFeedbackService feedbackService;

        public FamilyFeedbackController(IFeedbackService _feedbackService)
        {
            feedbackService = _feedbackService;
           
        }
        public IActionResult Index(string parceltoken)
        {
            try
            {
                if (CurrentUser.IsAuthenticated)
                {
                    return RedirectToAction("index", "home");
                }

                var res = feedbackService.GetParcelDetailsByToken(parceltoken);
                ViewBag.resQuestions = feedbackService.GetQuestionFormByFoodbank(Convert.ToInt32(res.FoodbankId));
                return View(res);
            }
            catch 
            {
                return RedirectToAction("Index","Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(FeedbackDto feedback)
        {
            string[] obj = feedback.dynamicString.Split("#$#;");
            
            if (obj.Length > 0)
            {
              
                foreach (var item in obj)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        string[] field = item.Split(new[] { '_' }, 2);
                        FeedbackFormDetailsDto formid = new FeedbackFormDetailsDto();
                        formid.FeedbackMasterId = Convert.ToInt32(field[0]);
                        formid.Answer = field[1];
                        feedback.FeedbackFormDetails.Add(formid);
                    }
                }
               
            }
            else
            {
               
            }
            var mapres = FeedbackDtoMapper.SaveFeedback(feedback);
            feedbackService.SaveFeedbackForm(mapres);

            ShowSuccessMessage("Success!", "Feedback has been saved successfully.", false);
            return RedirectToAction("Index", "FamilyFeedback", new { parceltoken = feedback.ParcelToken });
            
        }
    }
}
