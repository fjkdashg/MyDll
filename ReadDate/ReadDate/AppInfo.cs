using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
{
    public class AppInfo
    {
        
        public DataTable AppListInitial()
        {
            //表结构
            DataTable AppList = new DataTable();

            AppList.Columns.Add("id", typeof(int));
            AppList.Columns.Add("AppID", typeof(string));
            AppList.Columns.Add("AppName", typeof(string));
            AppList.Columns.Add("AppPath", typeof(string));
            AppList.Columns.Add("UpDataURL", typeof(string));

            //添加数据行1
            DataRow newrow = AppList.NewRow();
            newrow["id"] = 1;
            newrow["AppID"] = "GRASPFZNETCOPYBILL";
            newrow["AppName"] = "管家婆服装NET开单辅助工具";
            newrow["AppPath"] = "服装开单辅助工具.exe";
            newrow["UpDataURL"] = "http://s4.hzcrj.com/API/AppInfo.php?AppID=GRASPFZNETCOPYBILL";

            AppList.Rows.Add(newrow);

            //添加数据行2
            newrow = AppList.NewRow();
            newrow["id"] = 2;
            newrow["AppID"] = "CallListUpdateTool";
            newrow["AppName"] = "通话记录上传工具";
            newrow["AppPath"] = "CallListUpdateTool.exe";
            newrow["UpDataURL"] = "http://s4.hzcrj.com/API/AppInfo.php?AppID=CallListUpdateTool";

            AppList.Rows.Add(newrow);


            return AppList;
        }
        
    }
}
