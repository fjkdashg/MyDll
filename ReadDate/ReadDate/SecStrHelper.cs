using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
{
    class SecStrHelper
    {
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string codeKey = "fjkdashg"; //8位密钥

        //isSec 算法方向，True加密，False解密

        public string DESLite(Boolean isSec, string str)
        {
            if (isSec)
            {
                //加密字符串
                try
                {
                    byte[] rgbKey = Encoding.UTF8.GetBytes(codeKey.Substring(0, 8));
                    byte[] rgbIV = Keys;
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                    DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                    MemoryStream mStream = new MemoryStream();
                    CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                    cStream.Write(inputByteArray, 0, inputByteArray.Length);
                    cStream.FlushFinalBlock();
                    return Convert.ToBase64String(mStream.ToArray());
                }
                catch
                {
                    return "保存错误" + str;
                }
            }
            else
            {
                //解密字符串
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                else
                {
                    try
                    {
                        byte[] rgbKey = Encoding.UTF8.GetBytes(codeKey);
                        byte[] rgbIV = Keys;
                        byte[] inputByteArray = Convert.FromBase64String(str);
                        DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                        MemoryStream mStream = new MemoryStream();
                        CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                        cStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cStream.FlushFinalBlock();
                        return Encoding.UTF8.GetString(mStream.ToArray());
                    }
                    catch
                    {
                        return "读取失败" + str;
                    }
                }
            }
        }
    }
}

