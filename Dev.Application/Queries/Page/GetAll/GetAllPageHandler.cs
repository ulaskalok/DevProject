using Dev.Application.Services;
using Dev.Domain.Models;
using Dev.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Application.Queries.Page
{
    public class GetAllPageHandler : IRequestHandler<GetAllPageQuery, PageListView>
    {
        private readonly IBaseService<Domain.Page, PageView> _service;
        private readonly ICache _cache;
        public GetAllPageHandler(IBaseService<Domain.Page, PageView> service, ICache cache)
        {
            _service = service;
            _cache = cache;
        }

        public async Task<PageListView> Handle(GetAllPageQuery request, CancellationToken cancellationToken)
        {
            var cached = await _cache.Get<PageListView>(nameof(GetAllPageQuery));

            if (cached != null)
            {
                return cached;
            }

            var model = new PageListView
            {
                Datas = await _service.Get()
                    .ToListAsync(cancellationToken),
            };

            await _cache.Store<PageListView>(nameof(GetAllPageQuery), model);

            return model;
        }
    }
}
