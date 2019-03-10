using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Application.Services
{
    public interface IAccountService : IBaseService<Domain.Account, Domain.Models.AccountView>
    {
        IQueryable<Domain.Models.AccountView> Login(string username, string password);
    }
}
