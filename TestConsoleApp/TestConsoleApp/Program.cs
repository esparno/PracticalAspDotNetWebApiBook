using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost.fiddler:49741/api/employees";
            using (WebClient client = new WebClient())
            {
                client.DownloadString(uri);
            }
        }
    }
}
