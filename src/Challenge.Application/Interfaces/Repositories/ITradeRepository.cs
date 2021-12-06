using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Interfaces.Repositories
{
    public interface ITradeRepository
    {
        Task<Guid> AddTradeAsync(Trade trade, CancellationToken cancellationToken);
        Task<List<Trade>> GetAllTradesAsync(CancellationToken cancellationToken);
        Task<bool> DeleteTradeAsync(Guid id, CancellationToken cancellationToken);
    }
}
