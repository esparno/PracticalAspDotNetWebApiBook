using System;
using System.Linq;
using System.Linq.Expressions;
using Robusta.TalentManager.Domain;

namespace Robusta.TalentManager.Data
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
    }
}
