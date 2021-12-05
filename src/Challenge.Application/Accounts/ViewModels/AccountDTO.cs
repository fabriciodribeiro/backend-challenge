using Challenge.Core.Models;
using System;
using System.Collections.Generic;

namespace Challenge.Application.Accounts.ViewModels
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<Portfolio> Portfolios { get; private set; } = new List<Portfolio>();
        public IList<Trade> Trades { get; private set; } = new List<Trade>();

    }
}
