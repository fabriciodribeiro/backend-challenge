using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.Common;
using Challenge.Core.Models;
using System;
using Challenge.Application.Services;
using System.Collections.Generic;
using Challenge.Application.Interfaces.Services;

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
        private readonly IAccountService _service;

        public AccountSignupCommandHandler(IAccountService service)
        {
            _service = service;
        }

        public async Task<(Result Result, Guid UserId)> Handle(AccountSignupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = _service.AddAccountAsync(request, cancellationToken);

                var result = Result.Success();

                return (result, response.Result);
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
