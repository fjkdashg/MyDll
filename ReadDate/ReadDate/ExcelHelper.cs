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
        //选择读取Excel到DataTable
        public DataTable ReadExcelToDT(string Path, string DataList)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 12.0;HDR=YES;IMEX=2";
            OleDbConnection conn = new OleDbConnection(strConn);

            //读取数据
            conn.Open();

            //获取Excel Sheet表名称
            DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            string tableName = schemaTable.Rows[0][2].ToString().Trim();

            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            string strExcel = "select " + DataList + " from [" + tableName + "]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds.Tables[0];
        }
    }
}
