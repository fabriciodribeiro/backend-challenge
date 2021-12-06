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

            RuleFor(v => v.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(BeAValidDate).WithMessage("Date is invalid.");

            RuleFor(v => v.Executor)
                .NotEmpty().WithMessage("Executor is required.")
                .Must(BeAValidGuid).WithMessage("Executor format is invalid.");

            RuleFor(v => v.PortfolioId)
                .NotEmpty().WithMessage("Portfolio is required.")
                .MustAsync(ExistsPortfolio).WithMessage("The specified portfolio doesn't exists.");

            RuleFor(v => v.Shares)
                .NotEmpty().WithMessage("Shares is required.");

            RuleFor(v => v.Price)
                .NotEmpty().WithMessage("Price is required.");

            RuleFor(v => v.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency format is invalid.");

            RuleFor(v => v.MarketValue)
                .NotEmpty().WithMessage("MarketValue is required.");

            RuleFor(v => v.Action)
                .NotEmpty().WithMessage("Action is required.")
                .IsInEnum();
        }

        public async Task<bool> ExistsPortfolio(Guid portfolio, CancellationToken cancellationToken)
        {
            bool result = await _context.Portfolios.AllAsync(a => a.Id != portfolio);
            return !result;
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool BeAValidGuid(Guid id)
        {
            return !id.Equals(default(Guid));
        }
    }
}