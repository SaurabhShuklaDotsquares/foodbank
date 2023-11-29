using System;
using System.Collections.Generic;
using System.IO;

namespace FB.ExportReport.PDF
{
    public class PDF<T> : IExport<T> where T : class
    {
        public List<ColumnInfo> Columns { get; set; }

        public Header Header { get; set; }

        public Footer Footer { get; set; }

        public RowReportStyle RowStyle { get; set; }

        public ReportFormat Format { get; set; }

        public List<T> Data { get; set; }

        public string Template { get; set; }

        public byte[] Result { get; set; }

        public bool IsLandScape { get; set; }

        public void GenerateReport()
        {

        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
