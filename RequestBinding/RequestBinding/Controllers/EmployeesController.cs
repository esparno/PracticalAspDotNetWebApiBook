using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using RequestBinding.Models;
using System.Net.Http.Formatting;

namespace RequestBinding.Controllers
{
    public class EmployeesController : ApiController
    {
        //public void Post(int id, Employee employee)
        //{
        //    //var content = request.Content.ReadAsStringAsync().Result;
        //    //var content = req.Content.ReadAsAsync<Employee>().Result;
        //    //int id = Int32.Parse(req.RequestUri.Segments.Last());

        //    //Trace.WriteLine(content.Id);
        //    //Trace.WriteLine(content.FirstName);
        //    //Trace.WriteLine(content.LastName);
        //    //Trace.WriteLine(id);


        //    Trace.WriteLine(employee.Id);
        //    Trace.WriteLine(employee.FirstName);
        //    Trace.WriteLine(employee.LastName);
        //    Trace.WriteLine(id);
        //}

        // Binding an Http Request to Simple Types
        //public void Post(int id, string firstName, [FromBody]int locationid, Guid guid)
        //{
        //    Trace.WriteLine(id);
        //    Trace.WriteLine(firstName);
        //    Trace.WriteLine(locationid);
        //    Trace.WriteLine(guid);
        //}


        public void Post(FormDataCollection data)
        {
            Trace.WriteLine(data.Get("firstname"));
            Trace.WriteLine(data.Get("lastname"));
        }
    }
}
