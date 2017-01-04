using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public class ExcelHelper
    {
        public string Path = "";
        public OleDbConnection conn = new OleDbConnection();
        public string DefaultTBName = "";

        public void InitialExcelHelper()
        {
            try
            {
                string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path + ";Extended Properties='Excel 12.0;HDR=YES;'";
                conn = new OleDbConnection(connStr);
                conn.Open();
                DefaultTBName= ExcelSchemaInfo().Rows[0][2].ToString().Trim();
            }
            catch
            {

            }
        }

        //获取Excel表信息
        public DataTable ExcelSchemaInfo()
        {
            return conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
        }

        //选择读取Excel到DataTable
        public DataTable ReadExcelToDT(string DataList,string ExcelTBName)
        {
            //获取Excel Sheet表名称
            if (String.IsNullOrEmpty(ExcelTBName))
            {
                ExcelTBName = DefaultTBName;
            }

            //OleDbDataAdapter myCommand = null;
            DataTable dt = new DataTable();
            string ExcelSQL = "select " + DataList + " from [" + ExcelTBName + "]";
            OleDbDataAdapter myCommand = new OleDbDataAdapter(ExcelSQL, conn);
            myCommand.Fill(dt);
            return dt;
        }


        //将表格数据写入Excel
        public string DTImportToExcel(DataTable TarDT,string ExcelTable, string[] TarExcelColName, string[] SkepColumns)
        {
            //获取Excel表名称
            if (String.IsNullOrEmpty(ExcelTable))
            {
                ExcelTable = DefaultTBName;
            }
            
            foreach (DataRow DTRow in TarDT.Rows)
            {
                string Title="";
                string Values="";
                Boolean k = false;
                for (int i = 0; i < TarDT.Columns.Count; i++)
                {
                    if (!(Array.IndexOf(SkepColumns, TarDT.Columns[i].ColumnName) > -1))
                    {
                        if (k)
                        {
                            Title += ",";
                            Values += ",";
                        }
                        k = true;
                        Title += TarDT.Columns[i].ColumnName;
                        Values += "'" + DTRow[i].ToString() + "'";
                    }
                }

                OleDbCommand ODCExcel = conn.CreateCommand();
                ODCExcel.CommandText = "insert into ["+ ExcelTable+"] ("+ Title + ")values("+ Values+")";
                //Console.WriteLine(ODCExcel.CommandText);
                //Console.WriteLine("OK - "+ DTRow["科目编码"].ToString()+"  " + DTRow["科目名称"].ToString());
                ODCExcel.ExecuteNonQuery();
                ODCExcel.Dispose();
            }
            return "OK,"+ Path+","+ ExcelTable;
        }
    }
}
