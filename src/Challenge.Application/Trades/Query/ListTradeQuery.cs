using Challenge.Application.Interfaces.Services;
using Challenge.Application.Portfolis.ViewModels;
using Challenge.Application.Trades.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Portfolis.Query
{
    public class ListTradeQuery : IRequest<List<TradeDTO>>
    {
    }

    public class ListTradeQueryHandler : IRequestHandler<ListTradeQuery, List<TradeDTO>>
    {
        private readonly ITradeService _service;

        public ListTradeQueryHandler(ITradeService service)
        {
            _service = service;
        }

        public async Task<List<TradeDTO>> Handle(ListTradeQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTradeListAsync(cancellationToken);
        }
    }
}
