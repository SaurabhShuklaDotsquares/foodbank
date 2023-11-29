using System.Collections.Generic;

namespace FB.ExportReport
{
    public class BaseReportStyle
    {
        public short Boldweight { get; set; }

        public double FontHeight { get; set; }

        public short FontHeightInPoints { get; set; }

        public string FontName { get; set; }

        public bool IsItalic { get; set; }

        public bool IsStrikeout { get; set; }

        public bool IsBold { get; set; }

        private short BoldWeight { get { return IsBold ? (short)700 : (short)400; } }

        public Color ForeColor { get; set; }

        public Color BackColor { get; set; }
    }

    public class RowReportStyle : BaseReportStyle
    {
        public List<int> RowNumbers { get; set; }
    }
}
