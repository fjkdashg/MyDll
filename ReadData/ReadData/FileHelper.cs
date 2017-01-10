using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public class FileHelper
    {
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
        public BackgroundWorker SaveHttpFileWork_Initial()
        {
            BackgroundWorker SaveHttpFileWork = new BackgroundWorker();
            SaveHttpFileWork.WorkerReportsProgress = true;  //允许报告进度
            SaveHttpFileWork.WorkerSupportsCancellation = true;
            SaveHttpFileWork.DoWork += new DoWorkEventHandler(SaveHttpFileWork_DoWork);  //产生新的线程来处理任务
            //SaveHttpFileWork.ProgressChanged += new ProgressChangedEventHandler(BGWDownloading_ProgressChanged);  //当调用ReportProgress会触发该事件
            //SaveHttpFileWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWDownloading_RunWorkerCompleted);
            return SaveHttpFileWork;
        }
        public void SaveHttpFileWork_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker SaveHttpFileWork = sender as BackgroundWorker;
            string[] AppInfo = e.Argument as string[];
            string FileURL = AppInfo[0];
            string FilePath = AppInfo[1];
            Console.WriteLine(FileURL + " | " + FilePath);

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

        //ZIP解压缩
        public string UnZipped(string ZipPath, string ExtPath)
        {
            using (FileStream zipFileToOpen = new FileStream(ZipPath, FileMode.Open))
            using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Read))
            {
                //获取更新包内文件
                foreach (ZipArchiveEntry zipArchiveEntry in archive.Entries)
                {
                    //设置解压路径
                    string zipOutPath = ExtPath + @"\" + zipArchiveEntry.FullName;
                    //判断路径是否合法
                    if (!String.IsNullOrEmpty(Path.GetFileName(zipOutPath)))
                    {
                        //判断解压路径是否存在
                        if (!Directory.Exists(Path.GetDirectoryName(zipOutPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(zipOutPath));
                        }
                        //解压文件
                        using (System.IO.Stream stream = zipArchiveEntry.Open())
                        {
                            System.IO.Stream output = new FileStream(@zipOutPath, FileMode.Create);
                            int b = -1;
                            while ((b = stream.ReadByte()) != -1)
                            {
                                output.WriteByte((byte)b);
                            }
                            output.Close();
                        }
                    }
                }
            }
            return ExtPath;
        }
    }
}
