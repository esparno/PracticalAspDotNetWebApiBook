using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HelloWebApi.Models;

namespace HelloWebApi.Controllers.Api
{
    public class EmployeesController : ApiController
    {
        private IList<Employee> list = new List<Employee>() {
            new Employee()
            {
                Id = 12345, FirstName = "John", LastName = "Smith"
            },
            new Employee()
            {
                Id = 12346, FirstName = "Jane", LastName = "Doe"
            },
            new Employee()
            {
                Id = 12347, FirstName = "Joseph", LastName = "Law"
            }
        };

        public IEnumerable<Employee> Get()
        {
            return list;
        }

        public Employee Get(int id)
        {
            return list.FirstOrDefault(e => e.Id == id);
        }

        public void Post (Employee employee)
        {
            var maxId = list.Max(e => e.Id);
            employee.Id = maxId + 1;
        }
        public void Put (int id, Employee employee)
        {
            int index = list.ToList().FindIndex(e => e.Id == id);
            list[index] = employee;
        }
        public void Delete (int id)
        {
            Employee employee = Get(id);
            list.Remove(employee);
        }
    }
}
