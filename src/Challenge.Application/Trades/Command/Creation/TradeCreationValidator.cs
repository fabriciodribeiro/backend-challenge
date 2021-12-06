using Challenge.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Trades.Command.Creation
{
    public class TradeCreationValidator : AbstractValidator<TradeCreationCommand>
    {
        private readonly IChallengeDBContext _context;

        public TradeCreationValidator(IChallengeDBContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MaximumLength(50).WithMessage("Portfolio name accepts max lenght of 50")
                .NotEmpty().WithMessage("Portfolio name is required.");

            RuleFor(v => v.Portfolio)
                .NotEmpty().WithMessage("Portfolio is required.")
                .MustAsync(Exists).WithMessage("The specified portfolio doesn't exists.");
        }

        public async Task<bool> Exists(Guid portfolio, CancellationToken cancellationToken)
        {
            bool result = await _context.Portfolios.AllAsync(a => a.Id != portfolio);
            return !result;
        }
    }
}