using Challenge.Application.Accounts.ViewModels;
using Challenge.Core.Models;
using System;
using System.Collections.Generic;

namespace Challenge.Application.Portfolis.ViewModels
{
    public class PortfolioDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public AccountPortfolioDTO User { get; private set; } = new AccountPortfolioDTO();
        public IList<Trade> Trades { get; private set; } = new List<Trade>();

    }
}
