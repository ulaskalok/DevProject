using Dev.Application.Services;
using Dev.Domain;
using Dev.Domain.Core;
using Dev.Domain.Interfaces;
using Dev.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Dev.Instratructure.Authentication
{
    public class Authorize : IAuthorize
    {
        private readonly IOptions<AuthSettings> _settings;
        private readonly IJwt _jwt;
        private readonly IAccountService _service;
        private List<AuthorizeModel> _tokens;
        public Authorize(IOptions<AuthSettings> settings, IJwt jwt, IAccountService service)
        {
            _settings = settings;
            _jwt = jwt;
            _service = service;
            _tokens = new List<AuthorizeModel>();
        }
        public AuthorizeModel UpdateToken(string refreshToken)
        {
            AuthorizeModel token = _tokens.Find(at => at.RefreshToken == refreshToken);
            if (token == null || token.RefreshExpires <= DateTime.Now) return null;

            Domain.Models.AccountView acc = _service.GetById(token.AccountId);

            if (acc == null)
                return null;

            return Authentication(acc);
        }
        public AuthorizeModel Authentication(Domain.Models.AccountView acc)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimsIdentity.DefaultNameClaimType, acc.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, acc.Role),
                new Claim("id", acc.Id.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            AuthorizeModel model = new AuthorizeModel()
            {
                AccountId = acc.Id,
                AccessToken = _jwt.GetJwt(identity),
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshExpires = DateTime.Now.AddMinutes(_settings.Value.RefreshLifetime)
            };

            return model;
        }
    }
}
