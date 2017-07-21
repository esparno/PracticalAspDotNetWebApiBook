using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HelloWebApi.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.Tracing;


namespace HelloWebApi.Controllers.Api
{
    public class EmployeesController : ApiController
    {
        private IList<Employee> list = new List<Employee>() {
            new Employee()
            {
                Id = 12345, FirstName = "John", LastName = "Smith", Department=2
            },
            new Employee()
            {
                Id = 12346, FirstName = "Jane", LastName = "Doe", Department = 3
            },
            new Employee()
            {
                Id = 12347, FirstName = "Joseph", LastName = "Law", Department = 2
            }
        };
        private readonly ITraceWriter traceWriter = null;
        public EmployeesController()
        {
            this.traceWriter = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        }
        //public IEnumerable<Employee> Get([FromUri]Filter filter) 
        //{
        //    return list.Where(e => e.Department == filter.Department && e.LastName.ToUpper() == filter.LastName.ToUpper()); 
        //}
        public IEnumerable<Employee> Get()
        {
            IEnumerable<Employee> employees = null;
            if (traceWriter != null)
            {
                traceWriter.TraceBeginEnd(Request, TraceCategories.FormattingCategory, System.Web.Http.Tracing.TraceLevel.Info, "EmployeesController", "Get",
                    beginTrace: (tr) =>
                        {
                            tr.Message = "Entering Get";
                        },
                    execute: () =>
                        {
                            System.Threading.Thread.Sleep(1000);
                            employees = list;
                        },
                    endTrace: (tr) =>
                            {
                                tr.Message = "Leaving Get";
                            },
                    errorTrace: null);
            }
            return employees;
        }

        public Employee Get(int id)
        {
           var employee = list.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            if (traceWriter != null)
            {
                traceWriter.Info(Request, "EmployeesController", String.Format("Getting employee {0}", id));
                traceWriter.Trace(Request, "System.Web.Http.Controllers", System.Web.Http.Tracing.TraceLevel.Info, (traceRecord) =>
                    {
                        traceRecord.Message = String.Format("Getting employee {0}", id);
                        traceRecord.Operation = "Get(int)";
                        traceRecord.Operator = "EmployeeController";
                    });
            }
            return employee;
        }

        public HttpResponseMessage Post (Employee employee)
        {
            var maxId = list.Max(e => e.Id);
            employee.Id = maxId + 1;
            list.Add(employee);
            var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);
            string uri = Url.Link("DefaultApi", new { id = employee.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        public HttpResponseMessage Put (int id, Employee employee)
        {
            int index = list.ToList().FindIndex(e => e.Id == id);
            if (index >= 0)
            {
                list[index] = employee;
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                var maxId = list.Max(e => e.Id);
                employee.Id = maxId + 1;
                list.Add(employee);
                var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);
                string uri = Url.Link("DefaultApi", new { id = employee.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            
        }
        public HttpResponseMessage Post(int id, Employee employee)
        {
            int index = list.ToList().FindIndex(e => e.Id == id);
            if (index >= 0)
            {
                list[index] = employee;
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                var maxId = list.Max(e => e.Id);
                employee.Id = maxId + 1;
                list.Add(employee);
                var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employee);
                string uri = Url.Link("DefaultApi", new { id = employee.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

        }
        public HttpResponseMessage Patch(int id, Delta<Employee> deltaEmployee)
        {
            var employee = list.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            deltaEmployee.Patch(employee);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
        public void Delete (int id)
        {
            Employee employee = Get(id);
            list.Remove(employee);
        }
        //public IEnumerable<Employee> GetByDepartment(int department)
        //{
        //    int[] validDepartments = { 1, 2, 3, 5, 8, 13 };
        //    if (!validDepartments.Any(d => d == department))
        //    {
        //        var response = new HttpResponseMessage()
        //        {
        //            StatusCode = (HttpStatusCode)442,
        //            ReasonPhrase = "Invalid Department"
        //        };
        //        throw new HttpResponseException(response);
        //    }
        //    return list.Where(e => e.Department == department);
        //}
    }
}
