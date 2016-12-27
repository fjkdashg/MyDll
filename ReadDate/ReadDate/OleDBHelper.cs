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
        public string dbPath="";
        public string UserName;
        public string PWD;
        public string[] MSG=new string[5];

        public OleDbConnection conn=new OleDbConnection();   //数据库连接        

        public void FormatPath()
        {
            //dbPath = Path.GetFullPath(dbPath);
            //connString= "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + dbPath + "';";
        }

        public void InitialConn(string DBType)
        {
            //FormatPath();
            switch (DBType)
            {
                case "Access":
                    try
                    {
                        Console.WriteLine(@dbPath);
                        string connStr = "";
                        if (!String.IsNullOrEmpty(PWD))
                        {
                            connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath + ";jet oledb:Database Password="+ PWD + ";";//
                        }
                        else
                        {
                            connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath + ";";
                        }
                        Console.WriteLine(connStr);
                        conn = new OleDbConnection(connStr);
                        conn.Open();
                        //return new string[] { "true", dbPath };
                        break;
                    }
                    catch(Exception ex)
                    {
                        //return new string[] { "false", "Access "+ dbPath, "Access Conn Initial \n" + ex.Message };
                        MSG = new string[] { "false", "Access " + dbPath, "Access Conn Initial \n" + ex.Message };
                        break;
                    }
                    break;

                case "Excel":
                    try
                    {
                        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + dbPath + ";Extended Properties='Excel 12.0;HDR=YES;'";
                        conn = new OleDbConnection(connStr);
                        conn.Open();
                        
                        //return new string[] { "true", dbPath };
                        break;
                    }
                    catch (Exception ex)
                    {
                        //return new string[] { "false", dbPath, ex.Message };
                        MSG = new string[] { "false", dbPath, ex.Message };
                        break;
                    }
                    break;
                case "ExcelNoHDR":
                    try
                    {
                        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + dbPath + ";Extended Properties='Excel 12.0;HDR=NO;'";
                        conn = new OleDbConnection(connStr);
                        conn.Open();
                        
                        //return new string[] { "true", dbPath };
                        break;
                    }
                    catch (Exception ex)
                    {
                        //return new string[] { "false", dbPath, ex.Message };
                        MSG = new string[] { "false", dbPath, ex.Message };
                        break;
                    }
                    break;
                default:
                    //return new string[] { "false", dbPath, "未找到匹配的数据库类型" };
                    MSG = new string[] { "false", dbPath, "未找到匹配的数据库类型" };
                    break;
            }
        }


        //读取数据库
        public DataTable ReadDT(string SQL)
        {
            DataTable DT=new DataTable();
            OleDbDataAdapter ODDA = new OleDbDataAdapter(SQL, conn);
            ODDA.Fill(DT);
            return DT;
        }

        //读取单个值
        public object ReadSingleValue(string sql)
        {
            OleDbCommand ODC = new OleDbCommand(sql, conn);
            OleDbDataReader ODDR = ODC.ExecuteReader();
            if (ODDR.Read())
            {
                return ODDR[0];
            }
            else
            {
                return "Error";
            }
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
