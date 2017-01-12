using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public class SQLiteHelper
    {
        //本地数据库
        //基本参数
        public  SQLiteConnection SQLiteConn = null;
        public  SQLiteCommand SQLiteComm = null;
        public  string SQLitePath = "Data Source=database.s3db";
        public  string SQLitePWD = ";Password=Hc@3232327";

        //初始化本地数据库
        public  void InitialSQLiteDB()
        {
            try
            {
                SQLiteConn = new SQLiteConnection(SQLitePath + SQLitePWD);
                SQLiteConn.Open();
            }
            catch (Exception ex)
            {
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
                    SQLiteComm.CommandText = "CREATE TABLE [Soft_User] ([UID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,[UserName] VARCHAR(20)  UNIQUE NULL,[UserPWD] VARCHAR(100)  NULL,[UserAPP] VARCHAR(200)  NULL)";
                    SQLiteComm.ExecuteNonQuery();
                    SecStrHelper SSH = new SecStrHelper();
                    string pwd = SSH.DESLite(true, "admin");
                    SQLiteComm.CommandText = "insert into [Soft_User]([UserName],[UserPWD],[UserAPP]) values('admin','" + pwd + "','ALL')";
                    SQLiteComm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
        }

        //本地数据库查询
        public  DataTable SQLiteDT(string SQL)
        {
            SQLiteDataAdapter SLDA = new SQLiteDataAdapter(SQL, SQLiteConn);
            DataSet ds = new DataSet();
            SLDA.Fill(ds);
            DataTable DT = ds.Tables[0];
            SLDA = null;
            ds = null;
            return DT;
        }

        //本地数据库操作
        public  string SQLiteDO(string SQL)
        {
            SQLiteComm.CommandText = SQL;
            string RT= SQLiteComm.ExecuteNonQuery().ToString();
            return RT;
        }

        //本地用户验证
        public string UserLoginCheck(string UserName, string UserPWD)
        {
            string sql1 = "SELECT [UID],[UserAPP] FROM [Soft_User] WHERE [UserName] = '" + UserName + "' and UserPWD='"+ UserPWD + "' ";
            SQLiteCommand cmd = SQLiteConn.CreateCommand();
            cmd.CommandText = sql1;
            SQLiteDataReader SelectUID = cmd.ExecuteReader();

            if (SelectUID.Read())
            {
                return SelectUID.GetInt16(0).ToString()+","+ SelectUID.GetString(1);
            }
            else
            {
                return "-1,用户名或者密码错误";
            }
        }
    }
}
