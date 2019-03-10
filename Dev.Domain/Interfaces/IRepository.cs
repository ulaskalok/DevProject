using Dev.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Domain.Interfaces
{
    public interface IRepository { }
    public interface IRepository<T> : IRepository where T : Entity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> match);
        T Get(Guid Id);
        Guid Add(T Item);
        void Delete(Guid Id);
        void Update(T Item);

        void Dispose();

        Task<int> SaveChanges(CancellationToken cancellationToken);

        Task<int> SaveChanges();
    }
}
