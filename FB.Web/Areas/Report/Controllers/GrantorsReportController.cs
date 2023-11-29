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
    public class GrantorsReportController : BaseController
    {
        private readonly IGrantorService grantorService;
        public GrantorsReportController(IGrantorService _grantorService)
        {
            grantorService = _grantorService;
        }
        public IActionResult Index()
        {
            List<SelectListItem> grantorList = new List<SelectListItem>();
            var datafromDb = grantorService.GetAllGrantor(CurrentUser.FoodbankId);
            foreach (var items in datafromDb)
            {
                grantorList.Add(new SelectListItem
                {
                    Value = items.Id.ToString(),
                    Text = items.ForeName + items.SurName,
                });
            }
            ViewBag.grantorList = grantorList;
            return View();
        }

        [HttpPost]
        public IActionResult GetMembersByStepOneWizardValues(GrantorReportDto model)
        {
            try
            {
                string reportHeader = string.Empty;
                var granotrDetails = grantorService.GetGrantorDetails(model);

                if (granotrDetails.Count > 0)
                {
                    #region initiliaze columns list
                    ExportDataModel<ReportGarntorsDto> resultData = new ExportDataModel<ReportGarntorsDto>();
                    resultData.Data = granotrDetails;

                    #endregion

                    #region Gererate pdf preview
                    ViewData["ReportTitle"] = reportHeader;
                    resultData.PdfTemplateUrl = this.RenderViewToStringAsync("~/Areas/Report/Views/GrantorsReport/_ReportGrantors.cshtml", resultData.Data, ViewData, TempData).Result.ToString();
                    resultData.pdfFooterUrl = SiteKeys.DomainName + "Html/ReportFooter.html";
                    #endregion

                    SiteSessions.SetReportSession<ExportDataModel<ReportGarntorsDto>>(ReportType.GrantorsReport.GetDescription(), resultData);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ReportType.GrantorsReport.GetDescription(), IsSuccess = true });
                }

                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "No Parcel available", IsSuccess = false });
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = message, IsSuccess = false });
            }

        }

    }
}
