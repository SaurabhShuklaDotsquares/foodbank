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
    public class VolunteerReportsController : BaseController
    {
        private readonly IVolunteerService volunteerService;
        public VolunteerReportsController(IVolunteerService _volunteerService)
        {
            volunteerService = _volunteerService;
        }
        public IActionResult Index()
        {
            ViewBag.VolunteersList = Enumerable.Empty<SelectListItem>().ToList();
            var enumList = Enum.GetValues(typeof(packingordelivery));
            List<SelectListItem> TypeList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                TypeList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((packingordelivery)(int)items).GetDescription(),
                });
            }
            ViewBag.ShiftType = TypeList;
            return View();
        }
        public IActionResult GetVoluneteers(string fromDate, string toDate)
        {
            List<SelectListItem> volunteersList = new List<SelectListItem>();
            volunteersList = volunteerService.GetVolunteersListByDate(fromDate, toDate).Select(c => new SelectListItem { Text = c.Contact.ForeName + c.Contact.Surname, Value = c.Id.ToString() }).ToList();
            return NewtonSoftJsonResult(new RequestOutcome<List<SelectListItem>> { Data = volunteersList.Distinct().OrderBy(x => x.Text).ToList() });
        }

        [HttpPost]
        public IActionResult GetMembersByStepOneWizardValues(VolunteerReportDto model)
        {
            try
            {
                string reportHeader = string.Empty;
                var volunteerDetails = volunteerService.GetVolunteersDetailsForReport(model);

                if (volunteerDetails.Count > 0)
                {
                    #region initiliaze columns list
                    ExportDataModel<ReportVolunterrsDto> resultData = new ExportDataModel<ReportVolunterrsDto>();
                    resultData.Data = volunteerDetails;

                    #endregion

                    #region Gererate pdf preview
                    ViewData["ReportTitle"] = reportHeader;
                    resultData.PdfTemplateUrl = this.RenderViewToStringAsync("~/Areas/Report/Views/VolunteerReports/_ReportVolunteers.cshtml", resultData.Data, ViewData, TempData).Result.ToString();
                    resultData.pdfFooterUrl = SiteKeys.DomainName + "Html/ReportFooter.html";
                    #endregion

                    SiteSessions.SetReportSession<ExportDataModel<ReportVolunterrsDto>>(ReportType.VolunteersReport.GetDescription(), resultData);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ReportType.VolunteersReport.GetDescription(), IsSuccess = true });
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
