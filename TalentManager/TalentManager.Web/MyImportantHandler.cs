using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TalentManager.Web
{
    public class MyImportantHandler : DelegatingHandler
    {
        private const string REQUEST_HEADER = "X-Name";
        private const string RESPONSE_HEADER = "X-Message";
        private const string NAME = "Voldemort";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string name = String.Empty;

            if (request.Headers.Contains(REQUEST_HEADER))
            {
                name = request.Headers.GetValues(REQUEST_HEADER).First();
            }

            if (NAME.Equals(name, StringComparison.OrdinalIgnoreCase))
                return request.CreateResponse(HttpStatusCode.Forbidden);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.OK &&
                                        !String.IsNullOrEmpty(name))
            {
                response.Headers.Add(RESPONSE_HEADER, String.Format("Hello, {0}. Time is {1}", name,
                                    DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
            }

            return response;
        }
    }
}