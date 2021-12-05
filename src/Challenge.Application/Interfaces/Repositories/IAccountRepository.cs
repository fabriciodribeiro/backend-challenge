using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Guid> AddAccountsAsync(Account account, CancellationToken cancellationToken);
        Task<List<Account>> GetAllAccountsAsync(CancellationToken cancellationToken);
    }
}
