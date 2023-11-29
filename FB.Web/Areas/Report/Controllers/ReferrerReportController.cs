using FB.Core;
using FB.Dto;
using FB.Service;
using FB.Web.Code;
using FB.Web.Controllers;
using FB.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Areas.Report.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class ReferrerReportController : BaseController
    {
        private readonly IMyReferralService _myReferralService;
        private readonly IFoodbankService _foodbankService;
        private readonly IFamilyService _familyService;
        public ReferrerReportController(IMyReferralService myReferralService, IFoodbankService foodbankService, IFamilyService familyService)
        {
            _myReferralService = myReferralService;
            _foodbankService = foodbankService;
            _familyService = familyService;
        }

       [HttpGet]
        public IActionResult Index()
        {
            var foodBank = _foodbankService.GetFoodbankByUserId(CurrentUser.UserID);
            var reefersData = _myReferralService.GetReferrasByFoodBankId(foodBank.Id).OrderBy(x=>x.Name);
            if (reefersData != null)
            {
                ViewBag.Referrers= reefersData.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
            }
            else
            {
                ViewBag.Referrers=Enumerable.Empty<SelectListItem>().ToList();
            }
            ReferrerReportDto model = new ReferrerReportDto();
            return View(model);
        }

        [HttpPost]
        public IActionResult GetMembersByStepOneWizardValues(ReferrerReportDto model)
        {
            try
            {
                if (model.ReferrersId == 0)
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Please select Referrer.", IsSuccess = false });
                }
                List<SelectListItem> listItems = _myReferralService.GetReferraFamilyDetails(model.ReferrersId).Select(p => new SelectListItem
                {
                    Text = p.Family.FamilyName,
                    Value = p.FamilyId.ToString()
                }).ToList();

                return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = listItems, IsSuccess = true });
            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = $"Error : {ex.Message}", IsSuccess = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ReferrerReportDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!model.FamailyIds.IsNotNullAndNotEmpty())
                    {
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "You must select at least one member.", IsSuccess = false });
                    }
                    string reportHeader = string.Empty;
                    List<ReportReferrerDto> reportFamilyRecords = _familyService.GetFamilyforRefferreport(model);
                    if (reportFamilyRecords.Count > 0)
                    {
                        #region initiliaze columns list
                        ExportDataModel<ReportReferrerDto> resultData = new ExportDataModel<ReportReferrerDto>();
                        resultData.Data = reportFamilyRecords;

                        #endregion

                        #region Gererate pdf preview
                        ViewData["ReportTitle"] = reportHeader;
                        resultData.PdfTemplateUrl = model.IncludeFamailyMemberDetails? this.RenderViewToStringAsync("~/Areas/Report/Views/ReferrerReport/_ReportReferFamilywithMember.cshtml", resultData.Data, ViewData, TempData).Result.ToString():this.RenderViewToStringAsync("~/Areas/Report/Views/ReferrerReport/_ReportReferFamaily.cshtml", resultData.Data, ViewData, TempData).Result.ToString();
                        resultData.pdfFooterUrl = SiteKeys.DomainName + "Html/ReportFooter.html";
                        #endregion

                        SiteSessions.SetReportSession<ExportDataModel<ReportReferrerDto>>(ReportType.ReffersReport.GetDescription(), resultData);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ReportType.ReffersReport.GetDescription(), IsSuccess = true });
                    }

                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = string.Empty, IsSuccess = false });
                }
                catch (Exception ex)
                {
                    string message = ex.GetBaseException().Message;
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
                }
            }
            else
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = Constants.CustomRequiredErrorMessage, IsSuccess = false });
            }

        }
    }
}
