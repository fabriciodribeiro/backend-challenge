using Challenge.Application.Common.ViewModel;
using Challenge.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Trades.Command.Delete
{
    public class TradeDeletionCommand : IRequest<Result>
    {
        public Guid TradeId { get; set; }

        public TradeDeletionCommand(Guid id)
        {
            TradeId = id;
        }
    }

    public class PertfolioCreationCommandHandler : IRequestHandler<TradeDeletionCommand, Result>
    {
        private readonly ITradeService _service;

        public PertfolioCreationCommandHandler(ITradeService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(TradeDeletionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.DeleteTradeAsync(request, cancellationToken);

                var result = Result.Success();

                return result;
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add($"Fail to delete trade: { ex.Message }");
                var result = Result.Failure(errors);
                return result;
            }
        }
    }
}
