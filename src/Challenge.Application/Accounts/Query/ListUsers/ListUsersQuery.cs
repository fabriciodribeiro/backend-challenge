using Challenge.Application.Accounts.ViewModels;
using Challenge.Application.Interfaces.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Accounts.Query.ListUsers
{
    public class ListUsersQuery : IRequest<List<AccountDTO>>
    {
    }

    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, List<AccountDTO>>
    {
        private readonly IAccountService _service;

        public ListUsersQueryHandler(IAccountService service)
        {
            _service = service;
        }

        public async Task<List<AccountDTO>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAccountListAsync(cancellationToken);
        }
    }
}
