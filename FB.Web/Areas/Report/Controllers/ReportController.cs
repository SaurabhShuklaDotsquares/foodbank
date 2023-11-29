using FB.Core;
using FB.Dto;
using FB.ExportReport;
using FB.Web.Code;
using FB.Web.Controllers;
using FB.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FB.Web.Areas.Report.Controllers
{
    [CustomActionFilterAdminAttribute]
    public class ReportController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Export(IFormCollection FC)
        {
            try
            {
                string key = string.Empty;
                if (!FC["Key"].IsNotNullAndNotEmpty())
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = "Some required things are missing!" });
                }
                else
                {
                    key = Convert.ToString(FC["Key"]);
                }

                bool isPreview = FC["hdnexportbtn"] == "btnPreview";
                string reportFormat = Convert.ToString(FC["rdReportFormat"]);
                string reportOrientation = Convert.ToString(FC["rdReportOrientation"]).Trim();
                string footerName = Convert.ToString(FC["FooterName"]);
                string headerName = Convert.ToString(FC["TitalName"]);
                string fileName = Convert.ToString(FC["FileName"]);
                fileName = fileName.Replace(" ", "");
                object exportDataModel = GetExportDataModel(key);
                if (exportDataModel == null)
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = "Something went wrong, please re-generator report." });
                }

                if (fileName.IsNotNullAndNotEmpty())
                    exportDataModel.SetPropValue("FileName", fileName);
                if (footerName.IsNotNullAndNotEmpty())
                    exportDataModel.SetPropValue("FooterCenter", footerName);
                if (headerName.IsNotNullAndNotEmpty())
                    exportDataModel.SetPropValue("HeaderCenter", headerName);

                exportDataModel.SetPropValue("IsLandScape", reportOrientation == "Landscape");
                SiteSessions.SetReportSession(key, exportDataModel);

                if (!isPreview && (reportFormat == ExportType.Excel.ToString() || reportFormat == ExportType.CSV.ToString() || reportFormat == ExportType.Word.ToString()))
                {
                    return DocumentResult(key, reportFormat);
                }
                else if (isPreview || reportFormat == ExportType.PDF.ToString())
                {
                    return DocumentResult(key, reportFormat, FC["hdnexportbtn"] == "btnPreview");
                }
                else
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = "Please select at least one resource for data exporting." });
                }

            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = ex.Message });
            }
        }

        public IActionResult DocumentResult(string key, string reportFormat, bool isPreview = false)
        {
            bool isResult = false;
            var dtoType = GetReportDtoType(key);

            if (dtoType != null)
            {
                string methodToCall = string.Empty;
                if (!isPreview && reportFormat == ExportType.Excel.ToString())
                {
                    methodToCall = "SetExcelResult";
                }
                else if (isPreview || reportFormat == ExportType.PDF.ToString())
                {
                    methodToCall = "ProceedPdf";
                }
                else if (!isPreview && reportFormat == ExportType.CSV.ToString())
                {
                    methodToCall = "SetCSVResult";
                }
                else if (!isPreview && reportFormat == ExportType.Word.ToString())
                {
                    methodToCall = "SetWordResult";
                }

                if (!string.IsNullOrWhiteSpace(methodToCall))
                {
                    if (isPreview || reportFormat == ExportType.PDF.ToString())
                    {
                        ResponsePdf responsepdf = new ResponsePdf() { IsSuccess = false };

                        MethodInfo methodLogAction = typeof(ReportHelper).GetMethod(methodToCall, BindingFlags.Public | BindingFlags.Static);
                        MethodInfo generic = methodLogAction.MakeGenericMethod(dtoType);
                        responsepdf = (ResponsePdf)generic.Invoke(this, new object[] { key });


                        if (responsepdf.IsSuccess == true)
                        {
                            if (isPreview)
                            {
                                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = responsepdf.FilePath });
                            }
                            else
                            {
                                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = key });
                            }
                        }
                    }
                    else
                    {
                        MethodInfo methodLogAction = typeof(ReportHelper).GetMethod(methodToCall, BindingFlags.Public | BindingFlags.Static);
                        MethodInfo generic = methodLogAction.MakeGenericMethod(dtoType);

                        if (!isPreview && reportFormat == ExportType.Excel.ToString())
                        {
                            isResult = (bool)generic.Invoke(this, new object[] { key, "", true });
                        }
                        else
                        {
                            isResult = (bool)generic.Invoke(this, new object[] { key });
                        }
                    }
                }
            }

            if (isResult)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = key });
            }
            else
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = false, Data = "Some problem occured during report generation." });
            }
        }

        private Type GetReportDtoType(string key)
        {
            Type type = null;

            ReportType reportKey;
            if (Enum.TryParse(key, true, out reportKey))
            {
                switch (reportKey)
                {
                    case ReportType.ReffersReport:
                        type = typeof(ReportReferrerDto);
                        break;
                    case ReportType.FamilyReport:
                        type = typeof(ReportFamilyDto);
                        break;
                    case ReportType.ParcelsReport:
                        type = typeof(ReportParcelsDto);
                        break;
                    case ReportType.GrantorsReport:
                        type = typeof(ReportGarntorsDto);
                        break;
                    case ReportType.VolunteersReport:
                        type = typeof(ReportVolunterrsDto);
                        break;
                    default:
                        break;
                }
            }


            return type;
        }

        private object GetExportDataModel(string key)
        {
            object exportDataModel = null;
            if (Enum.TryParse(key, true, out ReportType reportKey))
            {
                switch (reportKey)
                {
                    case ReportType.ReffersReport:
                        exportDataModel = SiteSessions.GetReportSession<ExportDataModel<ReportReferrerDto>>(ReportType.ReffersReport.GetDescription());
                        break;
                    case ReportType.FamilyReport:
                        exportDataModel = SiteSessions.GetReportSession<ExportDataModel<ReportFamilyDto>>(ReportType.FamilyReport.GetDescription());
                        break;
                    case ReportType.ParcelsReport:
                        exportDataModel = SiteSessions.GetReportSession<ExportDataModel<ReportParcelsDto>>(ReportType.ParcelsReport.GetDescription());
                        break;
                    case ReportType.GrantorsReport:
                        exportDataModel = SiteSessions.GetReportSession<ExportDataModel<ReportGarntorsDto>>(ReportType.GrantorsReport.GetDescription());
                        break;
                    case ReportType.VolunteersReport:
                        exportDataModel = SiteSessions.GetReportSession<ExportDataModel<ReportVolunterrsDto>>(ReportType.VolunteersReport.GetDescription());
                        break;
                    default:
                        break;
                }
            }


            return exportDataModel;
        }


        /// <summary>
        /// Output of resultdata in ExcelSheet
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownloadFile(string key)
        {
            try
            {
                MemoryStream Result = null;
                ReportFormat ReportFormat = ReportFormat.PDF;
                string ReportName = string.Empty;
                string pdffileloaction = string.Empty;
                if (!string.IsNullOrEmpty(key))
                {
                    ReportType reportKey;
                    if (Enum.TryParse(key, true, out reportKey))
                    {
                        switch (reportKey)
                        {
                            case ReportType.ReffersReport:

                                ExportDataModel<ReportReferrerDto> reportReferrer = SiteSessions.GetReportSession<ExportDataModel<ReportReferrerDto>>(key);
                                if (reportReferrer.ReportFormat == ReportFormat.PDF)
                                    Result = new MemoryStream(reportReferrer.PDFResult);
                                else
                                    Result = new MemoryStream(reportReferrer.Result);

                                ReportFormat = reportReferrer.ReportFormat;
                                ReportName = reportReferrer.FileName.IsNotNullAndNotEmpty() ? reportReferrer.FileName : ReportType.ReffersReport.GetDescription();
                                break;
                            case ReportType.FamilyReport:

                                ExportDataModel<ReportFamilyDto> reportFamily = SiteSessions.GetReportSession<ExportDataModel<ReportFamilyDto>>(key);
                                if (reportFamily.ReportFormat == ReportFormat.PDF)
                                    Result = new MemoryStream(reportFamily.PDFResult);
                                else
                                    Result = new MemoryStream(reportFamily.Result);

                                ReportFormat = reportFamily.ReportFormat;
                                ReportName = reportFamily.FileName.IsNotNullAndNotEmpty() ? reportFamily.FileName : ReportType.FamilyReport.GetDescription();
                                break;
                            case ReportType.ParcelsReport:

                                ExportDataModel<ReportParcelsDto> reportParcels = SiteSessions.GetReportSession<ExportDataModel<ReportParcelsDto>>(key);
                                if (reportParcels.ReportFormat == ReportFormat.PDF)
                                    Result = new MemoryStream(reportParcels.PDFResult);
                                else
                                    Result = new MemoryStream(reportParcels.Result);

                                ReportFormat = reportParcels.ReportFormat;
                                ReportName = reportParcels.FileName.IsNotNullAndNotEmpty() ? reportParcels.FileName : ReportType.ParcelsReport.GetDescription();
                                break;
                            case ReportType.GrantorsReport:

                                ExportDataModel<ReportGarntorsDto> reportGrantors = SiteSessions.GetReportSession<ExportDataModel<ReportGarntorsDto>>(key);
                                if (reportGrantors.ReportFormat == ReportFormat.PDF)
                                    Result = new MemoryStream(reportGrantors.PDFResult);
                                else
                                    Result = new MemoryStream(reportGrantors.Result);

                                ReportFormat = reportGrantors.ReportFormat;
                                ReportName = reportGrantors.FileName.IsNotNullAndNotEmpty() ? reportGrantors.FileName : ReportType.GrantorsReport.GetDescription();
                                break;
                            case ReportType.VolunteersReport:

                                ExportDataModel<ReportVolunterrsDto> reportVolunteers = SiteSessions.GetReportSession<ExportDataModel<ReportVolunterrsDto>>(key);
                                if (reportVolunteers.ReportFormat == ReportFormat.PDF)
                                    Result = new MemoryStream(reportVolunteers.PDFResult);
                                else
                                    Result = new MemoryStream(reportVolunteers.Result);

                                ReportFormat = reportVolunteers.ReportFormat;
                                ReportName = reportVolunteers.FileName.IsNotNullAndNotEmpty() ? reportVolunteers.FileName : ReportType.VolunteersReport.GetDescription();
                                break;
                            default:
                                break;
                        }
                    }




                    if (Result != null && Result.Length > 0)
                    {
                        if (ReportFormat == ReportFormat.Excel)
                            return File(Result.ToArray(), "application/vnd.ms-excel", ReportName + "_" + DateTime.Now + ".xls");
                        else if (ReportFormat == ReportFormat.CSV)
                            return File(Result.ToArray(), "text/csv", ReportName + "_" + DateTime.Now + ".csv");
                        else if (ReportFormat == ReportFormat.PDF)
                            return File(Result.ToArray(), "application/pdf", ReportName + "_" + DateTime.Now + ".pdf");
                        else if (ReportFormat == ReportFormat.DBF)
                            return File(Result.ToArray(), "application/dbf", ReportName + ".dbf");
                        else if (ReportFormat == ReportFormat.Word)
                            return File(Result.ToArray(), "application/msword", ReportName + ".doc");
                        // return File(@pdffileloaction, "application/pdf", ReportName + "_" + DateTime.Now + ".pdf");
                    }
                    else
                    {
                        return RedirectToAction("Index", "error");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "error");
                }
            }
            catch
            {
                return RedirectToAction("Index", "error");
            }
            return RedirectToAction("Index", "error");
        }
    }
}
