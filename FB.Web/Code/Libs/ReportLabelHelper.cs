using FB.Core;
using FB.ExportReport;
using FB.ExportReportLabel;
using System;
using System.IO;

namespace FB.Web
{
    public class ReportLabelHelper
    {

        /// <summary>
        /// Set data in PDF
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>bool</returns>
        public static bool SetPDFResult(string key, bool isPreview)
        {
            ExportLabelModel exportDataModel = SiteSessions.GetReportSession<ExportLabelModel>(key);
            string fileNamewithFolder = $"TempPdfFiles/{(exportDataModel.FileName.IsNotNullAndNotEmpty() ? exportDataModel.FileName : Guid.NewGuid().ToString())}.pdf";
            string filePath = Path.Combine(ContextProvider.HostEnvironment.WebRootPath, fileNamewithFolder);
            bool isResult = false;


            exportDataModel.PDFResult = PdfLabelUtil.GeneratePdfLabels(exportDataModel.Labels, exportDataModel.LabelFormat, filePath, isPreview).ToArray();
            exportDataModel.ReportFormat = ReportFormat.PDF;
            if (isPreview)
            {
                exportDataModel.PdfFileLocation = SiteKeys.DomainName + fileNamewithFolder;
                exportDataModel.PDFResult = null;
            }
            SiteSessions.SetReportSession<ExportLabelModel>(key, exportDataModel);
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

        /// <summary>
        /// check if data already exist in session for pdf
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>ResponsePdf</returns>
        public static ResponsePdf ProceedPdf(string key, bool isPreview)
        {
            ResponsePdf result = new ResponsePdf() { IsSuccess = true };
            try
            {
                ExportLabelModel exportDataModel = SiteSessions.GetReportSession<ExportLabelModel>(key);
                result.IsSuccess = ReportLabelHelper.SetPDFResult(key, isPreview);
                exportDataModel = SiteSessions.GetReportSession<ExportLabelModel>(key);
                if (isPreview)
                    result.FilePath = exportDataModel.PdfFileLocation;

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
