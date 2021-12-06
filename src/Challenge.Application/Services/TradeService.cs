using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Challenge.Core.Models;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Interfaces.Repositories;
using System;
using Challenge.Application.Portfolis.ViewModels;
using Challenge.Application.Trades.Command.Creation;
using Challenge.Application.Trades.ViewModels;
using Challenge.Application.Trades.Command.Delete;

namespace Challenge.Application.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _repository;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TradeDTO>> GetTradeListAsync(CancellationToken cancellationToken)
        {
            List<Trade> tradeList = await _repository
                .GetAllTradesAsync(cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<List<TradeDTO>>(tradeList);
        }

        public async Task<Guid> AddTradeAsync(TradeCreationCommand tradeCommand, CancellationToken cancellationToken)
        {
            Trade trade = _mapper.Map<Trade>(tradeCommand);
            trade.PortfolioId = tradeCommand.PortfolioId;

            return await _repository.AddTradeAsync(trade, cancellationToken);
        }

        public async Task<bool> DeleteTradeAsync(TradeDeletionCommand tradeCommand, CancellationToken cancellationToken)
        {
            return await _repository.DeleteTradeAsync(tradeCommand.TradeId, cancellationToken);
        }
    }
}
