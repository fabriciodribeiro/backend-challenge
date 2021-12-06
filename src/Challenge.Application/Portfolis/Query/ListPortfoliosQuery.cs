using Challenge.Application.Interfaces.Services;
using Challenge.Application.Portfolis.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Portfolis.Query
{
    public class ListPortfoliosQuery : IRequest<List<PortfolioDTO>>
    {
    }

    public class ListPortfoliosQueryHandler : IRequestHandler<ListPortfoliosQuery, List<PortfolioDTO>>
    {
        private readonly IPortfolioService _service;

        public ListPortfoliosQueryHandler(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<List<PortfolioDTO>> Handle(ListPortfoliosQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPortfoliosListAsync(cancellationToken);
        }
    }
}
