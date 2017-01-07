using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReadData
{
    public class HttpWebHelper
    {
        public string HttpGetWeb(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //如果需要POST数据     
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }

                byte[] data = Encoding.GetEncoding("utf-8").GetBytes(buffer.ToString());

                using (Stream tstream = request.GetRequestStream())
                {
                    tstream.Write(data, 0, data.Length);
                }
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //打印返回值  
            Stream stream = response.GetResponseStream();   //获取响应的字符串流  
            StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
            string html = sr.ReadToEnd();   //从头读到尾，放到字符串html  
            return html;
        }
    }
}
