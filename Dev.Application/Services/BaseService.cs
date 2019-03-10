using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dev.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dev.Domain.Interfaces;

namespace Dev.Application.Services
{
    public class BaseService<TModel, TViewModel> : IBaseService<TModel, TViewModel> where TModel : Entity
                                                                                                          where TViewModel : class
    {
        protected IRepository<TModel> _baseRepository;
        protected IMapper _mapper;
        private IUnitOfWork _uow;
        public BaseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _baseRepository =  _uow.GetRepository<TModel>();
            _mapper = mapper;
        }

        public void Dispose()
        {
            _baseRepository.Dispose();
        }

        public IQueryable<TViewModel> Get()
        {
            return _baseRepository.GetAll().ProjectTo<TViewModel>();
        }

        public IQueryable<TViewModel> Get(Expression<Func<TModel, bool>> expression)
        {
            return _baseRepository.GetAll(expression).ProjectTo<TViewModel>();
        }

        public TViewModel GetById(Guid id)
        {
            var model = _mapper.Map<TViewModel>(_baseRepository.Get(id));
            return model;
        }

        public Task<int> Add(TModel model)
        {
            _baseRepository.Add(model);
            return _uow.SaveChanges();
        }

        public Task<int> Remove(Guid id)
        {
            _baseRepository.Delete(id);
            return _baseRepository.SaveChanges();
        }

        public Task<int> Update(TModel model)
        {
            _baseRepository.Update(model);
            return _baseRepository.SaveChanges();
        }
    }
}
