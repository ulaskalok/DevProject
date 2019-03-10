using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Domain.Core
{
    public class AuthorizeModel
    {
        public Guid AccountId { get; set; }
        public string Login { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshExpires { get; set; }
    }
}
