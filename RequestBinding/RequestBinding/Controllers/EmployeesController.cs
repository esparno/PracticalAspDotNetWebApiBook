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

        // bind form data
        //public void Post(FormDataCollection data)
        //{
        //    Trace.WriteLine(data.Get("firstname"));
        //    Trace.WriteLine(data.Get("lastname"));
        //}

        // bind form data to custom class
        public int Post(Employee employee)
        {
            return new Random().Next();
        }

        //public HttpResponseMessage Get(Shift shift)
        //{
        //    var response = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent(shift.Date.ToShortDateString())
        //    };
        //    return response;
        //}

        public HttpResponseMessage Get([System.Web.Http.ModelBinding.ModelBinder]IEnumerable<string> ifmatch)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(String.Join(" ", ifmatch))
            };
            return response;
        }
    }
}
