using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Domain.Models
{
    public class AccountView
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }
    }
}
