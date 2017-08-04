using System;
using System.Linq;
using System.Linq.Expressions;
using Robusta.TalentManager.Domain;

namespace Robusta.TalentManager.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        IContext Context { get; }
    }
}
