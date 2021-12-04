using Challenge.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Accounts.Commands.Signup
{
    public class AccountSignupCommandValidator : AbstractValidator<AccountSignupCommand>
    {
        private readonly IChallengeDBContext _context;

        public AccountSignupCommandValidator(IChallengeDBContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(100)
                .NotEmpty().WithMessage("Account name is required.");

            RuleFor(v => v.UserName)
                .MaximumLength(100)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(v => v.Password)
                .MaximumLength(2000)
                .NotEmpty();

            RuleFor(v => v.UserName)
                .MaximumLength(100)
                .NotEmpty().WithMessage("Username is required.")
                .MustAsync(BeUniqueUsername).WithMessage("The specified username already exists.");

            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Accounts.AllAsync(a => a.Email != email);
        }

        public async Task<bool> BeUniqueUsername(string userName, CancellationToken cancellationToken)
        {
            return await _context.Accounts.AllAsync(a => a.UserName != userName);
        }
    }
}