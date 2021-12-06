using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.Common.ViewModel;
using System;
using System.Collections.Generic;
using Challenge.Application.Interfaces.Services;
using Challenge.Core.Enums;
using System.Text.Json.Serialization;

namespace Challenge.Application.Trades.Command.Creation
{
    public class TradeCreationCommand : IRequest<(Result Result, Guid TradeId)>
    {
        public DateTime Date { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public decimal MarketValue { get; set; }
        public Actions Action { get; set; }
        [JsonIgnore]
        public Guid PortfolioId { get; set; }
        public Guid Executor { get; set; }
        public string Note { get; set; }
        public string Asset { get; set; }
    }

    public class TradeCreationCommandHandler : IRequestHandler<TradeCreationCommand, (Result Result, Guid TradeId)>
    {
        private readonly ITradeService _service;

        public TradeCreationCommandHandler(ITradeService service)
        {
            _service = service;
        }

        public async Task<(Result Result, Guid TradeId)> Handle(TradeCreationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _service.AddTradeAsync(request, cancellationToken);

                var result = Result.Success();

                return (result, response);
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add($"Fail to create trade: { ex.Message }");
                var result = Result.Failure(errors);
                return (result, Guid.Empty);
            }
        }
    }
}