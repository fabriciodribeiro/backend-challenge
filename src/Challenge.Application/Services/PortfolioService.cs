using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Challenge.Core.Models;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Interfaces.Repositories;
using System;
using Challenge.Application.Portfolis.ViewModels;
using Challenge.Application.Portfolis.Command.Creation;
using Challenge.Application.Portfolis.Command.Delete;

namespace Challenge.Application.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _repository;
        private readonly IMapper _mapper;

        public PortfolioService(IPortfolioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PortfolioDTO>> GetPortfoliosListAsync(CancellationToken cancellationToken)
        {
            List<Portfolio> portfolioList = await _repository
                .GetAllPortfoliosAsync(cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<List<PortfolioDTO>>(portfolioList);
        }

        public async Task<Guid> AddPortfolioAsync(PortfolioCreationCommand portfolioCommand, CancellationToken cancellationToken)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(portfolioCommand);
            portfolio.AccountId = portfolioCommand.Account;

            return await _repository.AddPortfolioAsync(portfolio, cancellationToken);
        }

        public async Task<bool> DeletePortfolioAsync(PortfolioDeletionCommand portfolioCommand, CancellationToken cancellationToken)
        {
            return await _repository.DeletePortfolioAsync(portfolioCommand.PortfolioId, cancellationToken);
        }
    }
}
