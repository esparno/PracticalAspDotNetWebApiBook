using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Robusta.TalentManager.Data;
using Robusta.TalentManager.Domain;
using Robusta.TalentManager.WebApi.Dto;

namespace Robusta.TalentManager.WebApi.Core.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IUnitOfWork uow = null;
        private readonly IRepository<Employee> repository = null;

        public EmployeesController(IUnitOfWork uow, IRepository<Employee> repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            var employee = repository.Find(id);
            if (employee == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");

                throw new HttpResponseException(response);
            }

            return Request.CreateResponse<EmployeeDto>(
                                HttpStatusCode.OK,
                                         Mapper.Map<Employee, EmployeeDto>(employee));
        }

        public HttpResponseMessage Post(EmployeeDto employeeDto)
        {
            var employee = Mapper.Map<EmployeeDto, Employee>(employeeDto);

            repository.Insert(employee);
            uow.Save();

            var response = Request.CreateResponse<Employee>(
                                            HttpStatusCode.Created,
                                                    employee);

            string uri = Url.Link("DefaultApi", new { id = employee.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (repository != null)
                repository.Dispose();

            if (uow != null)
                uow.Dispose();

            base.Dispose(disposing);
        }
    }
}
