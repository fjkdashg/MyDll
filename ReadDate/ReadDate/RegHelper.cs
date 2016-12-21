using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
{
    class RegHelper
    {
        public string RegPath = "software\\HiSoft";

        public string AddLicense(string RegKey, string LicenseKeyStr)
        {
            RegistryKey LMKey = Registry.LocalMachine;
            RegistryKey LicenseKeyEdit = LMKey.CreateSubKey(RegPath);
            RegistryKey LicenseKey = LMKey.OpenSubKey(RegPath, true);
            LicenseKey.SetValue(RegKey, LicenseKeyStr);
            return LicenseKey.GetValue(RegKey).ToString();
        }

        public string ReadLicense(string RegKey)
        {
            RegistryKey LMKey = Registry.LocalMachine;
            RegistryKey LicenseKeyEdit = LMKey.CreateSubKey(RegPath);
            RegistryKey LicenseKey = LMKey.OpenSubKey(RegPath, true);
            return LicenseKey.GetValue(RegKey).ToString();
        }
    }
}
