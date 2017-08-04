using System;
using System.Linq;
using System.Linq.Expressions;
using Robusta.TalentManager.Domain;

namespace Robusta.TalentManager.Data
{
    public interface IRepository<T> : IDisposable where T : class, IIdentifiable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllEager(params Expression<Func<T, object>>[] includes);
        T Find(int id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
