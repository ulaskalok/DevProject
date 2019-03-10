using FluentValidation;


namespace Dev.Application.Queries.Account
{
    public class LoginAccountQueryValidator : AbstractValidator<LoginAccountQuery>
    {
        public LoginAccountQueryValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(8);
        }
    }
}
