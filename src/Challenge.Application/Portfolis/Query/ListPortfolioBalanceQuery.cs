using Challenge.Application.Common.ViewModel;
using Challenge.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Portfolis.Query
{
    public class ListPortfolioBalanceQuery : IRequest<(Result Result, decimal Balance)>
    {
        public Guid PortfolioId { get; set; }

        public ListPortfolioBalanceQuery(Guid id)
        {
            PortfolioId = id;
        }
    }

    public class ListPortfolioBalanceQueryQueryHandler : IRequestHandler<ListPortfolioBalanceQuery, (Result Result, decimal Balance)>
    {
        private readonly IPortfolioService _service;

        public ListPortfolioBalanceQueryQueryHandler(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<(Result Result, decimal Balance)> Handle(ListPortfolioBalanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.GetPortfolioBalanceAsync(request, cancellationToken);

                var result = Result.Success();

                return (result, response);
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add($"Fail to calculate portfolio balance: { ex.Message }");
                var result = Result.Failure(errors);
                return (result, 0);
            }            
        }
    }
}
