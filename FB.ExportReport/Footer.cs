using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace FB.ExportReport
{
    public class Footer
    {
        public BaseReportStyle Style { get; set; }
        public List<FooterValue> Values { get; set; }

        internal void Create(ref IRow footerRow, List<ColumnInfo> columns)
        {
            foreach (var item in Values)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    if (columns[i].Name == item.Column)
                    {
                        footerRow.CreateCell(i).SetCellValue(item.Value ?? string.Empty);
                    }
                }
            }
        }
    }

}
