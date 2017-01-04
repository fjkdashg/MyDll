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
        DateTime AppTime= DateTime.Parse("1900-01-01");
        public Boolean AllowLogin = false;
        

        public string AddLicense(string LicenseKeyStr)
        {
            RegistryKey LMKey = Registry.LocalMachine;
            RegistryKey LicenseKeyEdit = LMKey.CreateSubKey("software\\HiSoft");
            RegistryKey LicenseKey = LMKey.OpenSubKey("SOFTWARE\\HiSoft", true);
            LicenseKey.SetValue("HiStr", LicenseKeyStr);
            return LicenseKey.GetValue("HiStr").ToString();
        }



        public string ReadLicense()
        {
            SecStrHelper SSH = new SecStrHelper();

            string LicenseKeyStr="";
            string LicenseStr="";

            try
            {
                //读取本地许可信息
                RegistryKey LMKey = Registry.LocalMachine;
                RegistryKey LicenseKeyEdit = LMKey.CreateSubKey("software\\HiSoft");
                RegistryKey LicenseKey = LMKey.OpenSubKey("SOFTWARE\\HiSoft", true);
                LicenseKeyStr = LicenseKey.GetValue("HiStr").ToString();
                
                //解密许可信息
                LicenseStr = SSH.DESLite(false, LicenseKeyStr);
                List<string> LicenseInfo = new List<string>(LicenseStr.Split(','));
                //提取许可信息
                AppID = LicenseInfo[2];
                AppTime = DateTime.Parse(LicenseInfo[1].ToString());
                AppMac = LicenseInfo[0];
            }
            catch(Exception ex)
            {
                //
            }

            
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
                return "True,验证成功," + AppID;
            }
        }
    }
}
