using Dev.Domain.Core;
using Dev.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Domain.Interfaces
{
    public interface IAuthorize
    {
        AuthorizeModel Authentication(AccountView account);

        AuthorizeModel UpdateToken(string refreshToken);


    }
}
