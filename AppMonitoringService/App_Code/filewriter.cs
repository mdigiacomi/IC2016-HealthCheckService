using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ElasticSearchFacadeSevice.App_Code
{
    public class filewriter
    {
        public static void writelog(string str)
        {
            using (StreamWriter writetext = new StreamWriter("C:\\Projects\\IC2016\\logs\\HealthCheckService.txt", true))
            {
                writetext.WriteLine("------------------");
                writetext.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                writetext.WriteLine(str);
                writetext.WriteLine("------------------");
            }
        }
    }
}