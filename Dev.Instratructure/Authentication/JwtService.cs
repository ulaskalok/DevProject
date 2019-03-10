using Dev.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dev.Instratructure.Authentication
{
    public class JwtService : IJwt
    {
        private readonly IOptions<AuthSettings> _settings;
        public JwtService(IOptions<AuthSettings> settings)
        {
            _settings = settings;
        }

        public string GetJwt(ClaimsIdentity identity)
        {
            var now = DateTime.Now;
            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _settings.Value.Issuer,
                    audience: _settings.Value.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.AddMinutes(_settings.Value.AccessLifetime),
                    signingCredentials: new SigningCredentials(_settings.Value.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJwt;
        }
    }
}
