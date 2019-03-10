using Dev.Application.Exceptions;
using Dev.Application.Services;
using Dev.Domain.Models;
using Dev.Data.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Dev.Application.Queries.Account
{
    public class LoginAccountQueryHandler : IRequestHandler<LoginAccountQuery, AccountView>
    {
        private readonly IAccountService _service;
        public LoginAccountQueryHandler(IAccountService service)
        {
            _service = service;
        }

        public async Task<AccountView> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
        {
            var find = await _service.Login(request.UserName, request.Password).SingleOrDefaultAsync(cancellationToken);

            if (find == null)
            {
                throw new NotFoundException(nameof(Account), request.UserName);
            }

            return find;
        }
    }
}
