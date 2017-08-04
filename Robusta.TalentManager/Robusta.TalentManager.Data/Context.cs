using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Robusta.TalentManager.Data.Configuration;

namespace Robusta.TalentManager.Data
{
    public class Context : DbContext, IContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

        public Context() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations
                .Add(new EmployeeConfiguration());
        }
    }
}
