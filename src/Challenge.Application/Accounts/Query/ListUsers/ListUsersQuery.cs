using AutoMapper;
using AutoMapper.QueryableExtensions;
using Challenge.Application.Accounts.ViewModels;
using Challenge.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Accounts.Query.ListUsers
{
    public class ListUsersQuery : IRequest<List<AccountDTO>>
    {
    }

    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, List<AccountDTO>>
    {
        private readonly IChallengeDBContext _context;
        private readonly IMapper _mapper;

        public ListUsersQueryHandler(IChallengeDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AccountDTO>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AsNoTracking()
                .OrderBy(x => x.UserName)
                .ProjectTo<AccountDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
