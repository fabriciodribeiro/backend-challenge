using Challenge.Application.Interfaces;
using Challenge.Application.Interfaces.Repositories;
using Challenge.Core.Enums;
using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Persistence
{
    public class TradeRepository : ITradeRepository
    {
        private readonly IChallengeDBContext _context;

        public TradeRepository(IChallengeDBContext context)
        {
            _context = context;
        }

        public async Task<List<Trade>> GetAllTradesAsync(CancellationToken cancellationToken)
        {
            return await _context.Traders
                .AsNoTracking()
                .Include(a => a.Portfolio)
                .OrderBy(x => x.Created)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Guid> AddTradeAsync(Trade trade, CancellationToken cancellationToken)
        {
            await _context.Traders.AddAsync(trade, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return trade.Id;
        }

        public async Task<bool> DeleteTradeAsync(Guid id, CancellationToken cancellationToken)
        {
            bool existsTrade = false;

            Trade trade = await _context.Traders
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            existsTrade = trade != null;

            if (existsTrade)
            {
                _context.Traders.Remove(trade);
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            return existsTrade;
        }
    }
}
