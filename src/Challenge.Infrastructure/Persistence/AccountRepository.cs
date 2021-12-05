using Challenge.Application.Interfaces;
using Challenge.Application.Interfaces.Repositories;
using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Persistence
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IChallengeDBContext _context;

        public AccountRepository(IChallengeDBContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAllAccountsAsync(CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AsNoTracking()
                .Include(portfolio => portfolio.Portfolios)
                .ThenInclude(x => x.Trades)
                .OrderBy(x => x.UserName)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Guid> AddAccountsAsync(Account account, CancellationToken cancellationToken)
        {
            await _context.Accounts.AddAsync(account, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return account.Id;
        }
    }
}
