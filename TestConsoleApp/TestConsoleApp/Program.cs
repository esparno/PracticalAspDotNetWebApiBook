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
            string uri = "http://localhost:49741/api/employees/12345";
            using (AutoDecompressionWebClient client = new AutoDecompressionWebClient())
            {
                client.Headers.Add("Accept-Encoding", "gzip, deflate;q=0.8");
                Console.WriteLine(client.DownloadString(uri));
                Console.ReadLine();
            }
        }
    }

    class AutoDecompressionWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
    }
}
