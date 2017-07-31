using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using TalentManager.Data.Configuration;
using TalentManager.Domain;

namespace TalentManager.Data
{
    public class Context : DbContext, IContext
    {
        public Context() : base("DefaultConnection") { }

        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new EmployeeConfiguration()).Add(new DepartmentConfiguration());
        }
    }
}
