using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Core.Models;

namespace Challenge.Application.Interfaces.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Guid> AddPortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken);
        Task<List<Portfolio>> GetAllPortfoliosAsync(CancellationToken cancellationToken);
        Task<bool> DeletePortfolioAsync(Guid id, CancellationToken cancellationToken);
    }
}
