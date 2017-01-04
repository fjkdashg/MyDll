using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public class LicenseHelper
    {
        string AppMac = "";
        string AppID = "";
        DateTime AppTime;
        public Boolean AllowLogin = false;
        

        public string AddLicense(string LicenseKeyStr)
        {
            RegistryKey LMKey = Registry.LocalMachine;
            RegistryKey LicenseKeyEdit = LMKey.CreateSubKey("SOFTWARE\\HiSoft");
            RegistryKey LicenseKey = LMKey.OpenSubKey("SOFTWARE\\HiSoft", true);
            LicenseKey.SetValue("HiStr", LicenseKeyStr);
            return LicenseKey.GetValue("HiStr").ToString();
        }



        public string ReadLicense()
        {
            //读取本地许可信息
            RegistryKey LMKey = Registry.LocalMachine;
            RegistryKey LicenseKeyEdit = LMKey.CreateSubKey("software\\HiSoft");
            RegistryKey LicenseKey = LMKey.OpenSubKey("SOFTWARE\\HiSoft", true);
            string LicenseKeyStr = LicenseKey.GetValue("HiStr").ToString();
            //解密许可信息
            SecStrHelper SSH = new SecStrHelper();
            string LicenseStr = SSH.DESLite(false, LicenseKeyStr);
            List<string> LicenseInfo = new List<string>(LicenseStr.Split(','));
            //提取许可信息
            AppID = LicenseInfo[2];
            AppTime = DateTime.Parse(LicenseInfo[1].ToString());
            AppMac = LicenseInfo[0];
            //提取硬件信息
            HardwareHelper HwH = new HardwareHelper();
            string ThisMAC = SSH.DESLite(true, HwH.GetMacAddress());

            //许可验证
            if (AppMac != ThisMAC)
            {
                AllowLogin = false;
                return "False,未注册," + ThisMAC;
            }
            else if (AppTime < DateTime.Now)
            {
                AllowLogin = false;
                return "False,注册过期," + AppTime;
            }
            else
            {
                AllowLogin = true;
                return "True,验证成功," + AppID + "," + LicenseStr;
            }
        }
    }
}
