using FB.Core;
using FB.ExportReport;
using FB.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web
{
    public class ReportHelper
    {
        /// <summary>
        /// Set data in excel
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>bool</returns>    
        public static bool SetExcelResult<T>(string key, string fileName = "", bool isHtml = false) where T : class
        {

            bool isResult = true;
            ExportDataModel<T> exportDataModel = SiteSessions.GetReportSession<ExportDataModel<T>>(key);
            if (exportDataModel != null)
            {
                IExport<T> excelReport = new Excel<T>();
                if (isHtml && !string.IsNullOrWhiteSpace(exportDataModel.PdfTemplateUrl))
                {
                    excelReport.Template = exportDataModel.PdfTemplateUrl;
                }
                else
                {
                    excelReport.Format = ReportFormat.Excel;
                    excelReport.Data = exportDataModel.Data;

                    if (exportDataModel.Columns != null)
                    {
                        excelReport.Columns = exportDataModel.Columns;
                    }
                }

                excelReport.GenerateReport();
                exportDataModel.Result = excelReport.Result;
                exportDataModel.ReportFormat = ReportFormat.Excel;

                if (!string.IsNullOrWhiteSpace(fileName))
                    exportDataModel.FileName = fileName;

                SiteSessions.SetReportSession<ExportDataModel<T>>(key, exportDataModel);
                //SiteSessions.SetReportSession(key, exportDataModel);
                if (exportDataModel.Result.Length <= 0)
                {
                    isResult = false;
                }
            }
            return isResult;
        }





        /// <summary>
        /// Set data in PDF
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>bool</returns>
        public static bool SetPDFResult<T>(string key) where T : class
        {
            try
            {
                ExportDataModel<T> exportDataModel = SiteSessions.GetReportSession<ExportDataModel<T>>(key);
                string fileNamewithFolder = $"TempPdfFiles/{(exportDataModel.FileName.IsNotNullAndNotEmpty() ? exportDataModel.FileName : Guid.NewGuid().ToString())}.pdf";
                string filePath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, fileNamewithFolder);
                bool isResult = false;
                PdfDocument document = new PdfDocument()
                {
                    Html = exportDataModel.PdfTemplateUrl,
                    HeaderCenter = exportDataModel.HeaderCenter,
                    FooterUrl = exportDataModel.pdfFooterUrl,
                    FooterLeft = exportDataModel.FooterLeft,
                    FooterLine = exportDataModel.FooterLine,
                    FooterRight = exportDataModel.pdfFooterRight,
                    FooterCenter = exportDataModel.FooterCenter,
                    IsLandScape = exportDataModel.IsLandScape, //? true : ((exportDataModel.Columns != null && exportDataModel.Columns.Count > 6) ? true : false),
                    MarginTop = exportDataModel.MarginTop,
                    MarginBottom = exportDataModel.MarginBottom,
                    MarginLeft = exportDataModel.MarginLeft,
                    MarginRight = exportDataModel.MarginRight,
                    PageSize = exportDataModel.PageSize
                };
                PdfOutput pdfoutput = new PdfOutput() { OutputStream = new MemoryStream(), OutputFilePath = filePath };
                //PdfConvertEnvironment environment = new PdfConvertEnvironment() { WkHtmlToPdfPath = "D:\\local\\MyMembership\\MMO.Web\\bin\\Debug\\netcoreapp3.1\\Rotativa\\Windows\\wkhtmltopdf.exe", TempFolderPath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "TempPdfFiles/"), Timeout = 60000 };
                PdfConvertEnvironment environment = new PdfConvertEnvironment() { WkHtmlToPdfPath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "TempPdfFiles/wkhtmltopdf.exe"), TempFolderPath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, "TempPdfFiles/"), Timeout = 60000 };
                IPdf pdf = new Pdf();
                pdf.Document = document;
                pdf.OutputResult = pdfoutput;
                pdf.ConvertEnvironment = environment;
                pdf.GeneratePDF();
                pdf.OutputResult.OutputStream.Position = 0;
                var memoryStream = new MemoryStream();
                pdf.OutputResult.OutputStream.CopyTo(memoryStream);
                exportDataModel.PDFResult = memoryStream.ToArray();
                exportDataModel.ReportFormat = ReportFormat.PDF;
                exportDataModel.PdfFileLocation = SiteKeys.DomainName + fileNamewithFolder;
                //SiteSessions.SetReportSession(key, exportDataModel);
                SiteSessions.SetReportSession<ExportDataModel<T>>(key, exportDataModel);
                if (exportDataModel.PDFResult != null ? exportDataModel.PDFResult.Length <= 0 : false)
                {
                    isResult = false;
                }
                else
                {
                    isResult = true;
                }
                return isResult;
            }
            catch (Exception ex)
            {

                string abc = ex.Message;
                return false;

            }
        }

        /// <summary>
        /// check if data already exist in session for pdf
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>ResponsePdf</returns>
        public static ResponsePdf ProceedPdf<T>(string key) where T : class
        {
            ResponsePdf result = new ResponsePdf() { IsSuccess = true };
            try
            {
                ExportDataModel<T> exportDataModel = SiteSessions.GetReportSession<ExportDataModel<T>>(key);
                if (exportDataModel != null && exportDataModel.PdfFileLocation != null && exportDataModel.PDFResult != null)
                {
                    exportDataModel.PdfFileLocation = null;
                    exportDataModel.PDFResult = null;
                    //result.FilePath = exportDataModel.PdfFileLocation;
                    //exportDataModel.ReportFormat = ReportFormat.PDF;
                    //SiteSessions.SetReportSession<ExportDataModel<T>>(key, exportDataModel);
                }
                //else
                //{
                result.IsSuccess = ReportHelper.SetPDFResult<T>(key);
                exportDataModel = SiteSessions.GetReportSession<ExportDataModel<T>>(key);
                result.FilePath = exportDataModel.PdfFileLocation;
                //}
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.GetBaseException().Message;
                result.IsSuccess = false;
            }
            return result;
        }
    }
}
