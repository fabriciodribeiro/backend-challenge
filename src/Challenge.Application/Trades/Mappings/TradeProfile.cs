using AutoMapper;
using Challenge.Application.Trades.ViewModels;
using Challenge.Core.Models;

namespace Challenge.Application.Trades.Mapping
{
    class TradeProfile : Profile
    {
        public TradeProfile()
        {
            CreateMap<Trade, TradeDTO>(MemberList.Source);
        }

    }
}
