using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunEXE
{
    public class RunEXEProcess
    {
        public void SystemShutdown()
        {
            Process Proc;
            ProcessStartInfo p = new ProcessStartInfo("shutdown.exe", "-s -t 10");
            /*
            p.WorkingDirectory = exepath;//设置此外部程序所在windows目录
            p.WindowStyle = ProcessWindowStyle.Hidden;
            //在调用外部exe程序的时候，控制台窗口不弹出
            //如果想获得当前路径为
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            */
            Proc = System.Diagnostics.Process.Start(p);//调用外部程序
        }

        public void LockScreen()
        {
            Process Proc;
            ProcessStartInfo p = new ProcessStartInfo("rundll32.exe", "user32.dll,LockWorkStation");
            /*
            p.WorkingDirectory = exepath;//设置此外部程序所在windows目录
            p.WindowStyle = ProcessWindowStyle.Hidden;
            //在调用外部exe程序的时候，控制台窗口不弹出
            //如果想获得当前路径为
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            */
            Proc = System.Diagnostics.Process.Start(p);//调用外部程序
        }

        public void ShowWebPage(string webURL)
        {
            Process Proc;
            ProcessStartInfo p = new ProcessStartInfo("iexplorer.exe", webURL);
            /*
            p.WorkingDirectory = exepath;//设置此外部程序所在windows目录
            p.WindowStyle = ProcessWindowStyle.Hidden;
            //在调用外部exe程序的时候，控制台窗口不弹出
            //如果想获得当前路径为
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            */
            Proc = System.Diagnostics.Process.Start(p);//调用外部程序
        }

        public void RunProgram(string ExeName,string ExeInfo)
        {
            Process Proc;
            ProcessStartInfo p = new ProcessStartInfo(ExeName, ExeInfo);
            /*
            p.WorkingDirectory = exepath;//设置此外部程序所在windows目录
            p.WindowStyle = ProcessWindowStyle.Hidden;
            //在调用外部exe程序的时候，控制台窗口不弹出
            //如果想获得当前路径为
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            */
            Proc = System.Diagnostics.Process.Start(p);//调用外部程序
        }

    }
}
