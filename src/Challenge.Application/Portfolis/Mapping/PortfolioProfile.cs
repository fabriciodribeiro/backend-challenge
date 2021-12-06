using AutoMapper;
using Challenge.Application.Portfolis.Command.Creation;
using Challenge.Application.Portfolis.ViewModels;
using Challenge.Core.Models;

namespace Challenge.Application.Portfolis.Mapping
{
    class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioDTO>(MemberList.Source);
            CreateMap<PortfolioCreationCommand, Portfolio>(MemberList.Source);
        }

    }
}
