using Dev.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Application.Services
{
    public interface IBaseService<TModel, TViewModel> : IDisposable
                                                                 where TModel : Entity
                                                                 where TViewModel : class
    {
        IQueryable<TViewModel> Get();
        IQueryable<TViewModel> Get(Expression<Func<TModel, bool>> expression);
        TViewModel GetById(Guid id);

        Task<int> Add(TModel model);
        Task<int> Update(TModel model);
        Task<int> Remove(Guid id);
    }
}
