using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Challenge.Application.Accounts.ViewModels;
using System.Threading;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Interfaces.Repositories;
using Challenge.Core.Models;
using Challenge.Application.Accounts.Commands.Signup;
using System;

namespace Challenge.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AccountDTO>> GetAccountListAsync(CancellationToken cancellationToken)
        {
            List<Account> accountList = await _repository
                .GetAllAccountsAsync(cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<List<AccountDTO>>(accountList);
        }

        public async Task<Guid> AddAccountAsync(AccountSignupCommand accountCommand, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(accountCommand);
            account.Password = HashService.Cryptograph(account.Password, account.Salt);

            return await _repository.AddAccountsAsync(account, cancellationToken);
        }
    }
}
