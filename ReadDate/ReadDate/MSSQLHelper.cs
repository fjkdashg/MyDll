using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
{
    class MSSQLHelper
    {
        //远程MSSQLSERVER数据库
        //基本参数
        public static string MSSQLConnSTR = " ";
        public static SqlConnection MSSQLConn = null;


        //初始化远程数据链接
        public static void InitialMSSQLDB()
        {
            try
            {
                MSSQLConn = new SqlConnection(MSSQLConnSTR);
                MSSQLConn.Open();
                MessageBox.Show("远程数据库初始化完成", "远程数据库初始化完成 MSSQLDB", MessageBoxButtons.OK);
                PublicData.LogMessage.EditLog("远程数据库初始化完成");
                MSSQLConn.Close();
                LoginMSSQLServerState = true;
            }
            catch (Exception ex)
            {
                LoginMSSQLServerState = false;
                MessageBox.Show(ex.ToString(), "远程数据库连接失败 MSSQLDB", MessageBoxButtons.OK);
                PublicData.LogMessage.EditLog("远程数据库连接失败，" + MSSQLConnSTR + "|" + ex.ToString());
            }

        }

        //远程数据查询
        public static DataTable MSSQLSelectTB(string sql)
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

        public static int MSSQLDo(string sql)
        {

            return 0;
        }
    }
}
