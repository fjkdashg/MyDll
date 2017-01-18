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
        


        //初始化远程数据链接
        public void InitialMSSQLDB()
        {
            try
            {
                MSSQLConn = new SqlConnection(MSSQLConnSTR);
                MSSQLConn.Open();
            }
            catch (Exception ex)
            {
            }
        }

        //远程数据查询
        public  DataTable SelectDT(string sql)
        {
            DataTable theselect = new DataTable();
            
            SqlCommand Comm = new SqlCommand(sql, MSSQLConn);
            Comm.CommandTimeout = 20;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = Comm;
            sda.Fill(theselect);
            
            return theselect;
        }


        public object ReadSigleValue(string sql)
        {
            SqlCommand Comm1 = new SqlCommand(sql, MSSQLConn);
            Comm1.CommandTimeout = 20;
            // Comm1.ExecuteNonQuery();   //insert  update delete
            object RetrnValue= Comm1.ExecuteScalar();
            return RetrnValue;
        }

        public SqlDataReader ReadSigleRow(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, MSSQLConn);
            comm.CommandTimeout = 20;
            SqlDataReader data;
            data = comm.ExecuteReader();
            return data;
        }

        public  int DoExe(string sql)
        {
            SqlCommand Comm = new SqlCommand(sql, MSSQLConn);
            Comm.CommandTimeout = 20;

            int SQLMessage = Comm.ExecuteNonQuery();
            return SQLMessage;
        }

        public void CloseConn()
        {
            MSSQLConn.Close();
        }
    }
}
