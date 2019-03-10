using System.Linq;
using AutoMapper;
using Dev.Domain;
using Dev.Domain.Interfaces;
using Dev.Domain.Models;

namespace Dev.Application.Services
{
    public class AccountService : BaseService<Domain.Account, AccountView>,IAccountService
    {
        public AccountService(IUnitOfWork uow, IMapper mapper) : base(uow,mapper)
        {

        }
        public IQueryable<AccountView> Login(string username, string password)
        {
            return Get(t => t.UserName == username && t.Password == password);
        }
    }
}
