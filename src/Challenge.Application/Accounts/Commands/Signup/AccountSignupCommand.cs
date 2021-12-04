using Challenge.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.Common;
using Challenge.Core.Models;
using System;
using Challenge.Application.Services;
using System.Collections.Generic;

namespace Challenge.Application.Accounts.Commands.Signup
{
    public class AccountSignupCommand : IRequest<(Result Result, Guid UserId)>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AccountSignupCommandHandler : IRequestHandler<AccountSignupCommand, (Result Result, Guid UserId)>
    {
        private readonly IChallengeDBContext _context;

        public AccountSignupCommandHandler(IChallengeDBContext context)
        {
            _context = context;
        }

        //public async Task<bool> Handle(AccountLoginCommand request, CancellationToken cancellationToken)
        //{
        //    var result = _context.Accounts
        //        .Any(x => x.UserName == request.Username && x.Password == HashService.Cryptograph(request.Password, x.Salt));

        //    return result;
        //}

        public async Task<(Result Result, Guid UserId)> Handle(AccountSignupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var salt = Guid.NewGuid().ToString().Replace("-", "");
                var account = new Account
                {
                    Name = request.Name,
                    UserName = request.UserName,
                    Salt = salt,
                    Password = HashService.Cryptograph(request.Password, salt),
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync(cancellationToken);

                var result = Result.Success();

                return (result, account.Id);
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add($"Fail to create account: { ex.Message }");
                var result = Result.Failure(errors);
                return (result, Guid.Empty);
            }
        }
    }
}
