using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dev.Infrastructure.Repository;
using Dev.Data;
using Dev.Data.Interface;
using Dev.Domain;
using Dev.Domain.Interfaces;

namespace Dev.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IContextFactory _dbContextFactory;
        protected DataContext _context;
        public UnitOfWork(IContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = dbContextFactory.Create();
        }

        public IRepository<T> GetRepository<T>() where T : Entity
        {
            return new Repository<T>(_context);
        }

        public Task<int> SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}
