using FB.Core;
using FB.ExportReport;
using FB.ExportReportLabel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web
{
    public class ExportLabelModel
    {
        public ReportType ReportType { get; set; }
        public byte[] Result { get; set; }
        public ReportFormat ReportFormat { get; set; }
        public LabelFormat LabelFormat { get; set; }
        public List<string> Labels { get; set; }

        public string PdfFileLocation { get; set; }
        public string PdfTemplateUrl { get; set; }
        public string PdfHeaderUrl { get; set; }
        public string pdfFooterUrl { get; set; }
        public byte[] PDFResult { get; set; }

        public string FileName { get; set; }

    }
}
