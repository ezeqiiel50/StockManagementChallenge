using FluentValidation;

namespace StockManager.Application.UsesCases.Login
{
    public class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.User)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} es obligatorio");

            RuleFor(x => x.Password)
               .NotNull()
               .NotEmpty().WithMessage("{PropertyName} es obligatorio");
        }
    }
}
