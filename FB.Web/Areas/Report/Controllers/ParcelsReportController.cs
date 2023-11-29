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
    public class ParcelsReportController : BaseController
    {
        private readonly IParcelService parcelService;
        public ParcelsReportController(IParcelService _parcelService)
        {
            parcelService = _parcelService;
        }
        public IActionResult Index()
        {
            var enumList = Enum.GetValues(typeof(ParcelStatus));
            List<SelectListItem> statusList = new List<SelectListItem>();
            foreach (var items in enumList)
            {
                statusList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((ParcelStatus)(int)items).GetDescription(),
                });
            }
            ViewBag.Status = statusList;

            var enumListforType = Enum.GetValues(typeof(ParcelTypes));
            List<SelectListItem> parceTypelList = new List<SelectListItem>();
            foreach (var items in enumListforType)
            {
                parceTypelList.Add(new SelectListItem
                {
                    Value = ((int)items).ToString(),
                    Text = ((ParcelTypes)(int)items).GetDescription(),
                });
            }
            ViewBag.ParcelType = parceTypelList;
            return View();
        }

        [HttpPost]
        public IActionResult GetMembersByStepOneWizardValues(ParcelsReportDto model)
        {
            try
            {
                string reportHeader = string.Empty;
                var parcelsDetails = parcelService.GetParcelDetailsForReport(model);

                if (parcelsDetails.Count > 0)
                {
                    #region initiliaze columns list
                    ExportDataModel<ReportParcelsDto> resultData = new ExportDataModel<ReportParcelsDto>();
                    resultData.Data = parcelsDetails;

                    #endregion

                    #region Gererate pdf preview
                    ViewData["ReportTitle"] = reportHeader;
                    resultData.PdfTemplateUrl = this.RenderViewToStringAsync("~/Areas/Report/Views/ParcelsReport/_ReportParcels.cshtml", resultData.Data, ViewData, TempData).Result.ToString();
                    resultData.pdfFooterUrl = SiteKeys.DomainName + "Html/ReportFooter.html";
                    #endregion

                    SiteSessions.SetReportSession<ExportDataModel<ReportParcelsDto>>(ReportType.ParcelsReport.GetDescription(), resultData);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ReportType.ParcelsReport.GetDescription(), IsSuccess = true });
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
