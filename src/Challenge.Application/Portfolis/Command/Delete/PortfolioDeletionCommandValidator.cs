using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Portfolis.Command.Delete
{
    public class PortfolioDeletionCommandValidator : AbstractValidator<PortfolioDeletionCommand>
    {
        public PortfolioDeletionCommandValidator()
        {
            RuleFor(v => v.PortfolioId)
                .NotNull().WithMessage("Portfolio name is required.")
                .NotEmpty().WithMessage("Portfolio name is required.");
        }       
    }
}
