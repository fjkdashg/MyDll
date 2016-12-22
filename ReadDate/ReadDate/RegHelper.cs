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
        public string RegPath = "SOFTWARE\\HiSoft";

        public string AddReg(string RegKey, string RegKeyStr)
        {
            RegistryKey LMKey = Registry.LocalMachine;
            RegistryKey LicenseKeyEdit = LMKey.CreateSubKey(RegPath);
            RegistryKey LicenseKey = LMKey.OpenSubKey(RegPath, true);
            LicenseKey.SetValue(RegKey, RegKeyStr);
            return LicenseKey.GetValue(RegKey).ToString();
        }

        public string ReadReg(string RegKey)
        {
            RegistryKey LMKey = Registry.LocalMachine;
            RegistryKey LicenseKeyEdit = LMKey.CreateSubKey(RegPath);
            RegistryKey LicenseKey = LMKey.OpenSubKey(RegPath, true);
            return LicenseKey.GetValue(RegKey).ToString();
        }
    }
}
