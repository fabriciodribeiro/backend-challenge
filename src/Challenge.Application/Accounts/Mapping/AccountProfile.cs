using AutoMapper;
using Challenge.Application.Accounts.Commands.Signup;
using Challenge.Application.Accounts.ViewModels;
using Challenge.Core.Models;

namespace Challenge.Application.Accounts.Mapping
{
    class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDTO>(MemberList.Source);
            CreateMap<AccountSignupCommand, Account>(MemberList.Source);
            CreateMap<Account, AccountPortfolioDTO>(MemberList.Source);
        }
    }
}
