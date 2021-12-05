using Challenge.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Portfolis.Command.Creation
{
    public class PortfolioCreationValidator : AbstractValidator<PortfolioCreationCommand>
    {
        private readonly IChallengeDBContext _context;

        public PortfolioCreationValidator(IChallengeDBContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(50).WithMessage("Portfolio name accepts max lenght of 50")
                .NotEmpty().WithMessage("Portfolio name is required.");

            RuleFor(v => v.Account)                
                .NotEmpty().WithMessage("Account is required.")
                .MustAsync(Exists).WithMessage("The specified account doesn't exists.");
        }

        public async Task<bool> Exists(Guid account, CancellationToken cancellationToken)
        {
            bool result = await _context.Accounts.AllAsync(a => a.Id != account);
            return !result;
        }
    }
}