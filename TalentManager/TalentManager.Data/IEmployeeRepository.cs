using System;
using System.Collections.Generic;
using TalentManager.Domain;

namespace TalentManager.Data
{
    public interface IEmployeeRepository :IDisposable
    {
        IEnumerable<Employee> GetByDepartment(int departmentId);
        Employee Get(int id);
    }
}
