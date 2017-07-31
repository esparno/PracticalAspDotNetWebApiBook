using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using TalentManager.Data;
using TalentManager.Domain;

namespace TalentManager.Web.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeRepository repository = null;

        public EmployeesController()
        {
            this.repository = new EmployeeRepository();
        }

        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public HttpResponseMessage Get(int id)
        {
            var employee = repository.Get(id);
            if (employee == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");

                throw new HttpResponseException(response);
            }

            return Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);
        }

        public HttpResponseMessage GetByDepartment(int departmentId)
        {
            var employees = repository.GetByDepartment(departmentId);
            if (employees != null && employees.Any())
            {
                return Request.CreateResponse<IEnumerable<Employee>>(HttpStatusCode.OK, employees);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        protected override void Dispose(bool disposing)
        {
            if (repository != null)
                repository.Dispose();

            base.Dispose(disposing);
        }



















        //private IContext context = null;

        //public EmployeesController()
        //{
        //    context = new Context();
        //}
        //public EmployeesController(IContext context)
        //{
        //    this.context = context;
        //}
        //public HttpResponseMessage Get(int id)
        //{
        //    var employee = context.Employees.FirstOrDefault(e => e.Id == id);
        //    if(employee == null)
        //    {
        //        var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee Not Found");
        //        throw new HttpResponseException(response);
        //    }
        //    return Request.CreateResponse<Employee>(HttpStatusCode.OK, employee);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (context != null && context is IDisposable)
        //    {
        //        ((IDisposable)context).Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
