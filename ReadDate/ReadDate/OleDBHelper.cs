using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
{
    public class OleDBHelper
    {
        public string Path;
        public string UserName;
        public string PWD;

        public OleDbConnection conn;   //数据库连接
        public Boolean connState = false;

        public string[] InitialConn(string DBType)
        {
            switch (DBType)
            {
                case "Access":
                    try
                    {
                        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";";
                        if (!String.IsNullOrEmpty(PWD))
                        {
                            connStr+="jet oledb: Database Password = " + PWD + "; ";
                        }
                        conn = new OleDbConnection(connStr);
                        conn.Open();
                        connState = true;
                        return new string[] { "true", Path };
                        break;
                    }
                    catch(Exception ex)
                    {
                        return new string[] { "false", Path , ex.Message };
                        break;
                    }
                    break;

                case "Excel":
                    try
                    {
                        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path + ";Extended Properties='Excel 12.0;HDR=YES;'";
                        conn = new OleDbConnection(connStr);
                        conn.Open();
                        connState = true;
                        return new string[] { "true", Path };
                        break;
                    }
                    catch (Exception ex)
                    {
                        return new string[] { "false", Path, ex.Message };
                        break;
                    }
                    break;
                case "ExcelNoHDR":
                    try
                    {
                        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path + ";Extended Properties='Excel 12.0;HDR=NO;'";
                        conn = new OleDbConnection(connStr);
                        conn.Open();
                        connState = true;
                        return new string[] { "true", Path };
                        break;
                    }
                    catch (Exception ex)
                    {
                        return new string[] { "false", Path, ex.Message };
                        break;
                    }
                    break;
                default:
                    return new string[] { "false", Path , "未找到匹配的数据库类型" };
                    break;
            }
        }


        //读取数据库
        public DataTable ReadDT(string SQL)
        {
            DataTable DT=null;
            OleDbDataAdapter ODDA = new OleDbDataAdapter(SQL, conn);
            ODDA.Fill(DT);
            return DT;
        }



        //数据库操作
        public string[] WriteDT(string sql)
        {
            try
            {
                OleDbCommand ODCComm = conn.CreateCommand();
                ODCComm.CommandText = sql;
                int k = ODCComm.ExecuteNonQuery();
                ODCComm.Dispose();
                return new string[] { "true", sql, k.ToString() };
            }
            catch (Exception ex)
            {
                return new string[] { "false", sql, ex.Message };
            }
        }
    }
}
