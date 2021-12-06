using Challenge.Application.Trades.Command.Delete;
using FluentValidation;

namespace Challenge.Application.Portfolis.Command.Delete
{
    public class TradeDeletionCommandValidator : AbstractValidator<TradeDeletionCommand>
    {
        public TradeDeletionCommandValidator()
        {
            RuleFor(v => v.TradeId)
                .NotNull().WithMessage("Trade Id is required.")
                .NotEmpty().WithMessage("Trade id is required.");
        }
    }
}
