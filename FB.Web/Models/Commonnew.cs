using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using FB.Core;
namespace FB.Web
{
    public static class Commonnew
    {
        public static DataTable ReadBankStCsvToDataTable(string path, int rowIndex = 0, int colIndex = 0)
        {
            string header = "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            DataTable dataTable = new DataTable();
            DataTable dtCsv = new DataTable();
            string sql = @"SELECT * FROM [" + fileName + "]";
            // "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Documents;Extended Properties=\"Text;HDR=Yes\"";

            //  using (OleDbConnection connection = new OleDbConnection(
            //@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
            //";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbConnection connection = new OleDbConnection(
          @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source" + pathOnly +
          ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                dataTable.Locale = CultureInfo.CurrentCulture;

                DataTable dtSchema = new DataTable();
                adapter.FillSchema(dtSchema, SchemaType.Source);

                if (dtSchema != null)
                    writeSchema(dtSchema, pathOnly, fileName);

                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count > 0)
            {
                for (int k = colIndex; k < dataTable.Columns.Count; k++)
                {
                    string col = dataTable.Rows[rowIndex][k].ToString().ReplaceMultiSpaces().Trim().Trim('\"').Trim('\"').Trim('\'');
                    //col = !string.IsNullOrWhiteSpace(col) ? col : "COL" + k;
                    if (!string.IsNullOrWhiteSpace(col))
                        dtCsv.Columns.Add(col);
                }

                for (int j = (rowIndex + 1); j < dataTable.Rows.Count; j++)
                {
                    DataRow dr = dtCsv.NewRow();
                    bool isNotBlank = false;
                    int drCol = 0;
                    for (int k = colIndex; k < dataTable.Columns.Count; k++)
                    {
                        string col = dataTable.Rows[rowIndex][k].ToString().ReplaceMultiSpaces().Trim().Trim('\"').Trim('\"').Trim('\'');
                        if (!string.IsNullOrWhiteSpace(col))
                        {
                            dr[drCol] = dataTable.Rows[j][k].ToString().ReplaceMultiSpaces().Trim().Trim('\"').Trim('\"').Trim('\'');
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(dr[drCol])))
                                isNotBlank = true;
                            drCol++;
                        }
                    }
                    if (isNotBlank)
                        dtCsv.Rows.Add(dr); //add other rows
                }
            }

            return dtCsv;
        }
        private static void writeSchema(DataTable dt, string filePath, string fileName)
        {
            try
            {
                FileStream fsOutput = new FileStream(filePath + "\\schema.ini", FileMode.Create, FileAccess.Write);
                StreamWriter srOutput = new StreamWriter(fsOutput);
                string s1, s2, s3, s4, s5;
                s1 = "[" + fileName + "]";
                s2 = "ColNameHeader=false";
                s3 = "Format=CSVDelimited";
                s4 = "MaxScanRows=50";
                s5 = "CharacterSet=ANSI";
                //s6 = "DateTimeFormat=MM/dd/yyyy";
                srOutput.WriteLine(s1 + '\n' + s2 + '\n' + s3 + '\n' + s4 + '\n' + s5);
                StringBuilder strB = new StringBuilder();
                if (dt != null)
                {
                    for (Int32 ColIndex = 1; ColIndex <= dt.Columns.Count; ColIndex++)
                    {
                        strB.Append("Col" + ColIndex.ToString());
                        strB.Append("=F" + ColIndex.ToString());
                        strB.Append(" Text\n");
                        srOutput.WriteLine(strB.ToString());
                        strB = new StringBuilder();
                    }
                }

                srOutput.Close();
                fsOutput.Close();
            }
            catch (Exception ex)
            {
            }
        }

    }
}
