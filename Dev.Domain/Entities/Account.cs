using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Domain
{
    public class Account : Entity
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
