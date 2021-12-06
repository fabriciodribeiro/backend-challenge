using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.Common.ViewModel;
using System;
using System.Collections.Generic;
using Challenge.Application.Interfaces.Services;

namespace Challenge.Application.Portfolis.Command.Creation
{
    public class PortfolioCreationCommand : IRequest<(Result Result, Guid PortfolioId)>
    {
        public string Name { get; set; }
        public Guid Account { get; set; }

    }

    public class PertfolioCreationCommandHandler : IRequestHandler<PortfolioCreationCommand, (Result Result, Guid UserId)>
    {
        private readonly IPortfolioService _service;

        public PertfolioCreationCommandHandler(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<(Result Result, Guid UserId)> Handle(PortfolioCreationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.AddPortfolioAsync(request, cancellationToken);

                var result = Result.Success();

                return (result, response);
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add($"Fail to create portfolio: { ex.Message }");
                var result = Result.Failure(errors);
                return (result, Guid.Empty);
            }
        }
    }
}