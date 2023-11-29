using System.Collections.Generic;
using System.IO;

namespace FB.ExportReport
{
    public interface IExport<T> where T : class
    {
        List<ColumnInfo> Columns { get; set; }
        Header Header { get; set; }
        Footer Footer { get; set; }
        RowReportStyle RowStyle { get; set; }
        ReportFormat Format { get; set; }
        List<T> Data { get; set; }
        string Template { get; set; }
        bool IsLandScape { get; set; }
        byte[] Result { get; }
        void GenerateReport();
        void Export();       
    }
}
