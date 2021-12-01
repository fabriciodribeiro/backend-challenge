using Challenge.Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Accounts.Commands
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
            //var entity = new TodoItem
            //{
            //    ListId = request.ListId,
            //    Title = request.Title,
            //    Done = false
            //};

            //TODO: Validar se o usuario existe e neste caso validar a senha

            var retorno = _context.Accounts.Any(x => x.UserName == request.Username);

            return true;
        }
    }
}
