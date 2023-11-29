using FB.Core;
using FB.ExportReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Models
{
    public class ExportDataModel<T> where T : class
    {
        public ExportDataModel()
        {
            IsLandScape = false;
        }

        public List<ColumnInfo> Columns { get; set; }
        public List<T> Data { get; set; }
        public ReportType ReportType { get; set; }
        public byte[] Result { get; set; }
        public ReportFormat ReportFormat { get; set; }
        public Dictionary<string, dynamic> KeyData { get; set; }
        public bool IsPreview { get; set; }
        public List<string> Labels { get; set; }

        public string PdfFileLocation { get; set; }
        public string PdfTemplateUrl { get; set; }
        public string PdfHeaderUrl { get; set; }
        public string pdfFooterUrl { get; set; }
        public string pdfFooterRight { get; set; }
        public string FooterLeft { get; set; }
        public string FooterCenter { get; set; }
        public bool? FooterLine { get; set; }
        //public MemoryStream PDFResult { get; set; }
        public byte[] PDFResult { get; set; }
        public string MarginTop { get; set; }
        public string MarginBottom { get; set; }
        public string MarginLeft { get; set; }
        public string MarginRight { get; set; }
        public string PageSize { get; set; }
        public bool IsLandScape { get; set; }

        public string FileName { get; set; }
        public string PdfTemplate { get; set; }
        public string FooterLabel { get; set; }
        public string HeaderCenter { get; set; }
        public bool IncludeTelephoneNumber { get; set; }

    }
}
