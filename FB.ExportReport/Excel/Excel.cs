using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace FB.ExportReport
{
    public class Excel<T> : IExport<T> where T : class
    {
        public List<ColumnInfo> Columns { get; set; }

        public Header Header { get; set; }

        public Footer Footer { get; set; }

        public RowReportStyle RowStyle { get; set; }

        public ReportFormat Format { get; set; }

        public List<T> Data { get; set; }

        public string Template { get; set; }

        public byte[] Result { get; private set; }

        private ISheet Sheet { get; set; }

        public bool IsLandScape { get; set; }

        public Excel()
        {
            Header = new Header();
        }

        public void GenerateReport()
        {
            if (!string.IsNullOrWhiteSpace(Template))
            {
                var strBody = new StringBuilder();         
                strBody.Append(Template);
                Result = Encoding.ASCII.GetBytes(strBody.ToString());
                //Result = new MemoryStream(data, 0, data.Length);
            }
            else
            {
                MemoryStream output = new MemoryStream();
                if (Format == ReportFormat.Excel)
                {
                    var workbook = new HSSFWorkbook();
                    Sheet = workbook.CreateSheet();
                    Sheet.SetColumnWidth(0, 30 * 256);

                    IRow rowHeader = Sheet.CreateRow(0);

                    if (Columns == null || Columns.Count == 0)
                    {
                        Columns = new List<ColumnInfo>();
                        Columns = Utility.GetPropertyNames(Data);
                    }
                    else
                    {
                        var properties = Utility.GetPropertyNames(Data).Select(p => p.Name).ToList();
                        List<ColumnInfo> NewColumns = new List<ColumnInfo>();
                        foreach (var item in Columns)
                        {
                            if (properties.Contains(item.Name))
                            {
                                NewColumns.Add(item);
                            }
                        }

                        Columns = NewColumns;
                    }


                    HSSFCellStyle hStyle = (HSSFCellStyle)workbook.CreateCellStyle();

                    if (Header.Style != null)
                    {
                        HSSFFont hFont = (HSSFFont)workbook.CreateFont();

                        hFont.FontHeightInPoints = 20;
                        hFont.FontName = "Calibri";
                        hFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                        hFont.Color = (short)11;

                        hStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        hStyle.SetFont(hFont);
                    }

                    this.Header.Create(ref rowHeader, Columns, hStyle);
                    Sheet.CreateFreezePane(0, 1, 0, 1);

                    int rowNum = 1;
                    foreach (var item in Data)
                    {
                        var newRow = Sheet.CreateRow(rowNum++);
                        for (int i = 0; i < Columns.Count; i++)
                        {
                            try
                            {

                                var val = item.GetType().GetProperty(Columns[i].Name).GetValue(item, null);
                                var type = item.GetType().GetProperty(Columns[i].Name).PropertyType;

                                if (type == typeof(decimal) || type == typeof(decimal?) || type == typeof(double) || type == typeof(double?) || type == typeof(float) || type == typeof(float?))
                                    newRow.CreateCell(i).SetCellValue(Convert.ToDouble(val));
                                else if (type == typeof(int) || type == typeof(long) || type == typeof(int?) || type == typeof(long?))
                                    newRow.CreateCell(i).SetCellValue(Convert.ToInt64(val));
                                else
                                    newRow.CreateCell(i).SetCellValue(Convert.ToString(val));

                                Sheet.AutoSizeColumn(i, true);
                            }
                            catch { }
                        }
                    }

                    if (Footer != null)
                    {
                        IRow rowFooter = Sheet.CreateRow(rowNum);
                        this.Footer.Create(ref rowFooter, Columns);
                    }

                    workbook.Write(output);
                }
                Result = output.ToArray();
            }     
           
        }

        public void Export()
        {
            throw new NotImplementedException();
        }
    }
}
