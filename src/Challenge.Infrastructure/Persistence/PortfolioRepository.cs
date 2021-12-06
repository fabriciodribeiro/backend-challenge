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
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IChallengeDBContext _context;

        public PortfolioRepository(IChallengeDBContext context)
        {
            _context = context;
        }

        public async Task<List<Portfolio>> GetAllPortfoliosAsync(CancellationToken cancellationToken)
        {
            return await _context.Portfolios
                .AsNoTracking()
                .Include(t => t.Trades)
                .Include(a => a.User)
                .OrderBy(x => x.Created)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Guid> AddPortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken)
        {
            await _context.Portfolios.AddAsync(portfolio, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return portfolio.Id;
        }

        public async Task<bool> DeletePortfolioAsync(Guid id, CancellationToken cancellationToken)
        {
            bool existsPortfolio = false;

            Portfolio portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            existsPortfolio = portfolio != null;

            if (existsPortfolio)
            {
                IQueryable<Trade> trades = _context.Traders.AsQueryable().Where(t => t.PortfolioId == id);

                if (await trades.AnyAsync(cancellationToken: cancellationToken).ConfigureAwait(false))
                {
                    _context.Traders.RemoveRange(trades);
                }

                _context.Portfolios.Remove(portfolio);
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            return existsPortfolio;
        }

        public async Task<decimal> GetPortifolioBalanceAsync(Guid id, CancellationToken cancellationToken)
        {
            var positiveList = await _context.Traders
                .AsNoTracking()
                .Where(t => t.PortfolioId == id && t.Asset == "cash" && t.Action == Actions.buy)
                .ToListAsync(cancellationToken: cancellationToken);

            var negativeList = await _context.Traders
                .AsNoTracking()
                .Where(t => t.PortfolioId == id && t.Asset != "cash")
                .ToListAsync(cancellationToken: cancellationToken);

            decimal positive = positiveList.Sum(x => Convert.ToInt32(x.MarketValue));
            decimal negative = negativeList.Sum(x => Convert.ToInt32(x.MarketValue)) * -1;

            return positive + negative;
        }
    }
}
