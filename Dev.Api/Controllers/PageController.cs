using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev.Domain.Models;
using Dev.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dev.Instratructure.ElasticSearch;
using Nest;
using Dev.Application.Commands.Page;
using Dev.Instratructure.ElasticSearch.Object;

namespace Dev.Api.Controllers
{
    public class PageController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IElasticSearch _elastic;
        public PageController(IMediator mediator, IElasticSearch elastic)
        {
            _mediator = mediator;
            _elastic = elastic;
        }

        //[Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreatePageCommand command)
        {
            var res = await _mediator.Send(command);

            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<PageListView>> GetAll()
        {
            var model = await _mediator.Send(new Application.Queries.Page.GetAllPageQuery());

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<ElasticResult>> Search([FromForm] string text)
        {
            var list = _elastic.Search(text);
            return Ok(list);
        }
    }
}
