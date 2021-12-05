using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.Portfolis.Command.Creation;
using Challenge.Application.Portfolis.Command.Delete;
using Challenge.Application.Portfolis.ViewModels;

namespace Challenge.Application.Interfaces.Services
{
    public interface IPortfolioService
    {
        Task<Guid> AddPortfolioAsync(PortfolioCreationCommand portfolioCommand, CancellationToken cancellationToken);
        Task<List<PortfolioDTO>> GetPortfoliosListAsync(CancellationToken cancellationToken);
        Task<bool> DeletePortfolioAsync(PortfolioDeletionCommand portfolioCommand, CancellationToken cancellationToken);
    }
}
