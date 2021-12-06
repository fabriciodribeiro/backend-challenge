using Challenge.Application.Trades.Command.Creation;
using Challenge.Application.Trades.Command.Delete;
using Challenge.Application.Trades.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Challenge.Application.Interfaces.Services
{
    public interface ITradeService
    {
        Task<Guid> AddTradeAsync(TradeCreationCommand tradeCommand, CancellationToken cancellationToken);
        Task<List<TradeDTO>> GetTradeListAsync(CancellationToken cancellationToken);
        Task<bool> DeleteTradeAsync(TradeDeletionCommand tradeCommand, CancellationToken cancellationToken);
    }
}