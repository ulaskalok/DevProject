using AutoMapper;
using Dev.Application.Exceptions;
using Dev.Data;
using Dev.Data.Interface;
using Dev.Domain;
using Dev.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected DataContext _context;
        protected DbSet<T> EntitySet;
        public Repository(DataContext context)
        {
            _context = context;
            EntitySet = _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return EntitySet.AsQueryable();
        }
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match);
        }

        public virtual T Get(Guid Id)
        {
            return EntitySet.AsQueryable().Where(t => t.Id == Id).FirstOrDefault<T>();
        }

        public virtual Guid Add(T Item)
        {
            var addedItem = EntitySet.Add(Item);
            return addedItem.Entity.Id;
        }

        public virtual void Delete(Guid Id)
        {
            var foundEntity = Get(Id);
            if (foundEntity == null)
                throw new NotFoundException("Entity not found", Id);
            _context.Entry(foundEntity).State = EntityState.Deleted;
        }

        public virtual void Update(T Item)
        {
            var updateItem = EntitySet.Attach(Item);
            _context.Entry(updateItem).State = EntityState.Modified;
        }

        public virtual async Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<int> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
