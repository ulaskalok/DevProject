using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Instratructure.Authentication
{
    public class AuthSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessLifetime { get; set; }
        public int RefreshLifetime { get; set; }
        public string Key { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
