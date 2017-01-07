using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
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
            newrow["AppID"] = "GraspNETSaleBillCopyTool";
            newrow["AppName"] = "管家婆服装NET开单辅助工具";
            newrow["AppPath"] = "GraspNETSaleBillCopyTool.exe";
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
            
            //添加数据行3
            newrow = AppList.NewRow();
            newrow["id"] = 3;
            newrow["AppID"] = "GlImportTool";
            newrow["AppName"] = "总账导入工具";
            newrow["AppPath"] = "GlImportTool.exe";
            newrow["UpDataURL"] = "http://s4.hzcrj.com/API/AppInfo.php?AppID=GlImportTool";
            AppList.Rows.Add(newrow);

            //添加数据行4
            newrow = AppList.NewRow();
            newrow["id"] = 4;
            newrow["AppID"] = "TplusWeigh";
            newrow["AppName"] = "T+过磅计量";
            newrow["AppPath"] = "TplusWeigh.exe";
            newrow["UpDataURL"] = "http://s4.hzcrj.com/API/AppInfo.php?AppID=GlImportTool";
            AppList.Rows.Add(newrow);

            return AppList;
        }

        public string GetAppInfo(string SName,string SValue,string TName)
        {
            DataRow[] SelectRow= AppListInitial().Select(SName + " = '" + SValue + " '");
            return SelectRow[0][TName].ToString();
        }
    }
}
