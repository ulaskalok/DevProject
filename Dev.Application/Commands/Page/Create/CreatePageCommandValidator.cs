using FluentValidation;

namespace Dev.Application.Commands.Page
{
    public class CreatePageCommandValidatior : AbstractValidator<CreatePageCommand>
    {
        public CreatePageCommandValidatior()
        {
            RuleFor(x => x.PageName).NotEmpty();
            RuleFor(x => x.PageDesc).MaximumLength(10);
        }
    }
}
