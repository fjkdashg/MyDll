using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
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

            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            string ExcelSQL = "select " + DataList + " from [" + ExcelTBName + "]";
            myCommand = new OleDbDataAdapter(ExcelSQL, conn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds.Tables[0];
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
                for (int i = 0; i < TarDT.Columns.Count; i++)
                {
                    if (!(Array.IndexOf(SkepColumns, TarDT.Columns[i].ColumnName) > -1))
                    {
                        Title += TarDT.Columns[i].ColumnName;
                        Values += "'" + DTRow[i].ToString() + "'";
                        if (i != (TarDT.Columns.Count - 1))
                        {
                                Title += ",";
                                Values += ",";
                        }
                    }
                }

                OleDbCommand ODCExcel = conn.CreateCommand();
                ODCExcel.CommandText = "insert into ["+ ExcelTable+"] ("+ Title + ")values("+ Values+")";
                Console.WriteLine("OK - "+ DTRow["科目编码"].ToString()+"  " + DTRow["科目名称"].ToString());
                ODCExcel.ExecuteNonQuery();
                ODCExcel.Dispose();
            }
            return "OK,"+ Path+","+ ExcelTable;
        }
    }
}
