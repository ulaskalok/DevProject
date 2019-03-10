using Dev.Application.Services;
using Dev.Data.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.Application.Commands.Page
{
    public class CreatePageCommandHandler : IRequestHandler<CreatePageCommand, int>
    {
        private readonly IBaseService<Domain.Page,Domain.Models.PageView> _service;
        public CreatePageCommandHandler(IBaseService<Domain.Page, Domain.Models.PageView> service)
        {
            _service = service;
        }

        public async Task<int> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
            var entity = new Dev.Domain.Page() { PageName = request.PageName, PageDesc = request.PageDesc,  PageText = request.PageText };
            return await _service.Add(entity);
        }
    }
}
