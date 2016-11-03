using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
{
    class SQLiteHelper
    {
        //本地数据库
        //基本参数
        public static SQLiteConnection SQLiteConn = null;
        public static SQLiteCommand SQLiteComm = null;
        public static string SQLitePath = "Data Source=database.s3db";
        public static string SQLitePWD = ";Password=Hc@3232327";

        //初始化本地数据库
        public static void InitialSQLiteDB()
        {
            try
            {
                SQLiteConn = new SQLiteConnection(SQLitePath + SQLitePWD);
                SQLiteConn.Open();
            }
            catch (Exception ex)
            {
                PublicData.demoMsg("本地数据库连接", ex.ToString());

                SQLiteConn = new SQLiteConnection(SQLitePath);
                SQLiteConn.Open();
                SQLiteConn.ChangePassword("Hc@3232327");
                SQLiteConn.Close();
                SQLiteConn = new SQLiteConnection(SQLitePath + SQLitePWD);
                SQLiteConn.Open();
            }

            SQLiteComm = new SQLiteCommand(SQLiteConn);

            //初始化SQLite设置
            string sql1 = "SELECT name FROM sqlite_master WHERE type = 'table' and name='Soft_User' ORDER BY name";
            DataTable TableIsIn = SQLiteDT(sql1);
            
            if (!(TableIsIn.Rows.Count > 0))
            {
                //初始化系统表
                try
                {
                    SQLiteComm.CommandText = "CREATE TABLE [Soft_User] ([UID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,[UserName] VARCHAR(20)  UNIQUE NULL,[UserPWD] VARCHAR(100)  NULL)";
                    SQLiteComm.ExecuteNonQuery();
                    string pwd = PublicData.DESSec.doDESLite(true, "admin", PublicData.SecKey);
                    SQLiteComm.CommandText = "insert into [Soft_User]([UserName],[UserPWD]) values('admin','" + pwd + "')";
                    SQLiteComm.ExecuteNonQuery();
                    
                    
                }
                catch (Exception ex)
                {
                    PublicData.LogMessage.EditLog("!!!!!本地数据库初始化失败");
                    
                   
                }
            }
        }


        //本地数据库查询
        public static DataTable SQLiteDT(String SQL)
        {
            SQLiteDataAdapter SLDA = new SQLiteDataAdapter(SQL, SQLiteConn);
            DataSet ds = new DataSet();
            SLDA.Fill(ds);
            DataTable DT = ds.Tables[0];
            SLDA = null;
            ds = null;
            return DT;
        }
    }
}
