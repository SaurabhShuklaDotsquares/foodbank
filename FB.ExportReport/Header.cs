using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace FB.ExportReport
{
    public class Header
    {
        public BaseReportStyle Style { get; set; }

        internal void Create(ref IRow headerRow, List<ColumnInfo> Columns, HSSFCellStyle hStyle)
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(!string.IsNullOrWhiteSpace(Columns[i].Label) ? Columns[i].Label : Columns[i].Name ?? string.Empty);
                headerRow.RowStyle = hStyle;
            }
        }
    }
}
