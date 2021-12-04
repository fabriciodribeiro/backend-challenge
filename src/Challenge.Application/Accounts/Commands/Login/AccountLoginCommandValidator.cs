using FluentValidation;

namespace Challenge.Application.Accounts.Commands.Login
{
    public class AccountLoginCommandValidator : AbstractValidator<AccountLoginCommand>
    {
        public AccountLoginCommandValidator()
        {
            RuleFor(v => v.Username)
                .MaximumLength(100)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(v => v.Password)
                .MaximumLength(2000)
                .NotEmpty();
        }
    }
}
