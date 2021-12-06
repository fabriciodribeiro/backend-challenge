using Challenge.Application.Common.ViewModel;
using Challenge.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Portfolis.Command.Delete
{
    public class PortfolioDeletionCommand : IRequest<Result>
    {
        public Guid PortfolioId { get; set; }

        public PortfolioDeletionCommand(Guid id)
        {
            PortfolioId = id;
        }
    }

    public class PertfolioCreationCommandHandler : IRequestHandler<PortfolioDeletionCommand, Result>
    {
        private readonly IPortfolioService _service;

        public PertfolioCreationCommandHandler(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(PortfolioDeletionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.DeletePortfolioAsync(request, cancellationToken);

                var result = Result.Success();

                return result;
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add($"Fail to delete portfolio: { ex.Message }");
                var result = Result.Failure(errors);
                return result;
            }
        }
    }
}
