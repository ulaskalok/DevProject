using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.Domain.Models;
using Dev.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dev.Application.Queries.Account;

namespace Dev.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IAuthorize _authorize;
        public AccountController(IMediator mediator, IAuthorize authorize)
        {
            _mediator = mediator;
            _authorize = authorize;
        }

        [HttpGet]
        public async Task<ActionResult<AccountView>> Login([FromForm]LoginAccountQuery command)
        {
            var model = await _mediator.Send(command);
            var token = _authorize.Authentication(model);

            return Ok(token);
        }

        [HttpGet]
        public async Task<ActionResult<AccountView>> Token([FromForm]string refreshToken)
        {
            var token = _authorize.UpdateToken(refreshToken);
            return Ok(token);
        }
    }
}