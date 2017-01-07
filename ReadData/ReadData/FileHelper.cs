using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public  class FileHelper
    {
        public int processPer = 0;
        //输出内嵌资源文件
        public string SaveResourceFile(string FileSavePath, Stream SouFS)
        {
            //内嵌文件属性：生成操作->嵌入的资源
            //string ResFile = Assembly.GetExecutingAssembly().GetName().Name.ToString() + ".Resources.TPLUSACCINFO.xls";
            //Assembly app = Assembly.GetExecutingAssembly();
            //Stream SouFS = app.GetManifestResourceStream(ResFile);

            BinaryReader BR = new BinaryReader(SouFS);
            FileStream FS = new FileStream(@FileSavePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter BW = new BinaryWriter(FS);

            byte[] bArr = new byte[512];
            bArr = BR.ReadBytes(bArr.Length);
            while (bArr.Length > 0)
            {
                BW.Write(bArr, 0, bArr.Length);
                bArr = BR.ReadBytes(bArr.Length);
            }
            SouFS.Close();
            
            BR.Close();
            BW.Close();
            FS.Close();
            SouFS.Close();
            return "OK,"+ @FileSavePath;
        }

        //输出网络文件
        public string SaveHttpFile(string URL,string SavePath)
        {

            return "SaveFullPath";
        }
    }
}
