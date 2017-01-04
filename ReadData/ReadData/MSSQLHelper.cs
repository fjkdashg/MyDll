using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public class MSSQLHelper
    {
        //远程MSSQLSERVER数据库
        //基本参数
        public  string MSSQLConnSTR = " ";
        public  SqlConnection MSSQLConn = null;
        public  Boolean LoginState = true;
        


        //初始化远程数据链接
        public void InitialMSSQLDB()
        {
            try
            {
                MSSQLConn = new SqlConnection(MSSQLConnSTR);
                MSSQLConn.Open();
                MSSQLConn.Close();
                LoginState = true;
            }
            catch (Exception ex)
            {
                LoginState = false;
            }
        }

        //远程数据查询
        public  DataTable MSSQLSelectTB(string sql)
        {
            DataTable theselect = new DataTable();
            MSSQLConn.Open();
            SqlCommand Comm = new SqlCommand(sql, MSSQLConn);
            Comm.CommandTimeout = 20;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = Comm;
            sda.Fill(theselect);
            MSSQLConn.Close();
            return theselect;
        }

        public  int MSSQLDo(string sql)
        {
            return 0;
        }

        public void CloseConn()
        {
            MSSQLConn.Close();
            MSSQLConn = null;
        }
    }
}
