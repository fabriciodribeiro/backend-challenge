using FluentValidation;

namespace Challenge.Application.Accounts.Commands
{
    public class AccountLoginCommandValidator : AbstractValidator<AccountLoginCommand>
    {
        public AccountLoginCommandValidator()
        {
            RuleFor(v => v.Username)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(v => v.Password)
                .MaximumLength(2000)
                .NotEmpty();
        }
    }
}
