using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunLog
{
    public class LogClass
    {
        public string EditLogReturn(string LogLine)
        {
            try
            {
                string ProPath = System.Environment.CurrentDirectory;
                FileStream fs = new FileStream(@ProPath + "\\Log\\" + DateTime.Now.ToLongDateString().ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now.ToString() + LogLine + "\n");
                sw.Flush();
                sw.Close();
                fs.Close();
                return "OK";
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }

        }


        public void  EditLog(string LogLine)
        {
            
                string ProPath = System.Environment.CurrentDirectory;
                FileStream fs = new FileStream(@ProPath + "\\Log\\" + DateTime.Now.ToLongDateString().ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(DateTime.Now.ToString() + LogLine + "\n");
                sw.Flush();
                sw.Close();
                fs.Close();
       

        }
    }
}
