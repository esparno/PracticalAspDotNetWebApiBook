using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace RequestBinding.Controllers
{
    public class EmployeesController : ApiController
    {
        public void Post(HttpRequestMessage request)
        {
            var content = request.Content.ReadAsStringAsync().Result;
            int id = Int32.Parse(request.RequestUri.Segments.Last());

            Trace.WriteLine(content);
            Trace.WriteLine(id);
        }
    }
}
