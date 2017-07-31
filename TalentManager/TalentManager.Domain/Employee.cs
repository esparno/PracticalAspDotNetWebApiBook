using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManager.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
