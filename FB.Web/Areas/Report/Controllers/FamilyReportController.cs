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
    public class FamilyReportController : BaseController
    {
        private readonly IFoodbankService _foodbankService;
        private readonly IFamilyService _familyService;
        public FamilyReportController(IMyReferralService myReferralService, IFoodbankService foodbankService, IFamilyService familyService)
        {
            _foodbankService = foodbankService;
            _familyService = familyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var enumList = Enum.GetValues(typeof(FamilyReportSatus));
            List<SelectListItem> statusList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                statusList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((FamilyReportSatus)(int)items).GetDescription(),
                });
            }
            ViewBag.Status = statusList;
            return View();
        }

        [HttpPost]
        public IActionResult GetMembersByStepOneWizardValues(FamilyReportDto model)
        {
            try
            {
                List<SelectListItem> listItems = _familyService.GetFamilysForFamilyReport(model).Select(p => new SelectListItem
                {
                    Text = p.FamilyName,
                    Value = p.Id.ToString()
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
        public IActionResult Index(FamilyReportDto model)
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
                    List<ReportFamilyDto> reportFamilyRecords = _familyService.GetFamilysDetailsforFamilyReport(model);
                    if (reportFamilyRecords.Count > 0)
                    {
                        #region initiliaze columns list
                        ExportDataModel<ReportFamilyDto> resultData = new ExportDataModel<ReportFamilyDto>();
                        resultData.Data = reportFamilyRecords;

                        #endregion

                        #region Gererate pdf preview
                        ViewData["ReportTitle"] = reportHeader;
                        resultData.PdfTemplateUrl = this.RenderViewToStringAsync("~/Areas/Report/Views/FamilyReport/_ReportFamaily.cshtml", resultData.Data, ViewData, TempData).Result.ToString();
                        resultData.pdfFooterUrl = SiteKeys.DomainName + "Html/ReportFooter.html";
                        #endregion

                        SiteSessions.SetReportSession<ExportDataModel<ReportFamilyDto>>(ReportType.FamilyReport.GetDescription(), resultData);
                        return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ReportType.FamilyReport.GetDescription(), IsSuccess = true });
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
