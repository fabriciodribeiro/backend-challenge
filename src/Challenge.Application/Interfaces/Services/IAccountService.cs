using Challenge.Application.Accounts.Commands.Signup;
using Challenge.Application.Accounts.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Guid> AddAccountAsync(AccountSignupCommand accountCommand, CancellationToken cancellationToken);
        Task<List<AccountDTO>> GetAccountListAsync(CancellationToken cancellationToken);
    }
}
