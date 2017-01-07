using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public  class FileHelper
    {
        public BackgroundWorker SaveHttpFileWork = new BackgroundWorker();


        public int processPer = 0;
        //输出内嵌资源文件
        public string SaveResourceFile(string FileSavePath, Stream SouFS)
        {
            //内嵌文件属性：生成操作->嵌入的资源
            //string ResFile = Assembly.GetExecutingAssembly().GetName().Name.ToString() + ".Resources.TPLUSACCINFO.xls";
            //Assembly app = Assembly.GetExecutingAssembly();
            //Stream SouFS = app.GetManifestResourceStream(ResFile);

            //判断文件夹是否存在
            if (!Directory.Exists(Path.GetDirectoryName(FileSavePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FileSavePath));
            }

            BinaryReader BR = new BinaryReader(SouFS);
            Console.WriteLine(@FileSavePath);
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
            return @FileSavePath;
        }

        //输出网络文件
        public void SaveHttpFileWork_Initial()
        {
            SaveHttpFileWork.WorkerReportsProgress = true;  //允许报告进度
            SaveHttpFileWork.WorkerSupportsCancellation = true;
            SaveHttpFileWork.DoWork += new DoWorkEventHandler(SaveHttpFileWork_DoWork);  //产生新的线程来处理任务
            //SaveHttpFileWork.ProgressChanged += new ProgressChangedEventHandler(BGWDownloading_ProgressChanged);  //当调用ReportProgress会触发该事件
            //SaveHttpFileWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWDownloading_RunWorkerCompleted);
        }

        public void SaveHttpFileWork_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] AppInfo = e.Argument as string[];
            string FileURL = AppInfo[0];
            string FilePath =AppInfo[1];

            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }

            HttpWebRequest request = WebRequest.Create(FileURL) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            long fullsize = response.ContentLength;
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            Stream stream = new FileStream(FilePath, FileMode.Create);
            //stream.Length;
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            long loadSize = 0;
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
                loadSize += size;
                SaveHttpFileWork.ReportProgress((int)(loadSize * 100 / fullsize));
            }
            stream.Close();
            responseStream.Close();
            e.Result = FilePath;
        }
    }
}
