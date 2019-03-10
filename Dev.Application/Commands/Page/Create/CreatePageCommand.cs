using MediatR;

namespace Dev.Application.Commands.Page
{
    public class CreatePageCommand : IRequest<int>
    {
        public string PageName { get; set; }

        public string PageDesc { get; set; }

        public string PageText { get; set; }
    }
}
