using Dev.Domain.Models;
using MediatR;

namespace Dev.Application.Queries.Account
{
    public class LoginAccountQuery : IRequest<AccountView>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
