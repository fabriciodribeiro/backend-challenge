using Challenge.Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.Services;

namespace Challenge.Application.Accounts.Commands.Login
{
    public class AccountLoginCommand : IRequest<bool>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class AccountLoginCommandHandler : IRequestHandler<AccountLoginCommand, bool>
    {
        private readonly IChallengeDBContext _context;

        public AccountLoginCommandHandler(IChallengeDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
        {
            var result = _context.Accounts
                .Any(x => x.UserName == request.Username && x.Password == HashService.Cryptograph(request.Password, x.Salt));

            return result;
        }       
    }
}
