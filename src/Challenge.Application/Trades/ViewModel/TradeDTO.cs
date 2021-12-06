using Challenge.Application.Accounts.ViewModels;
using Challenge.Core.Enums;
using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Challenge.Application.Trades.ViewModels
{
    public class TradeDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Shares { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public decimal MarketValue { get; set; }
        public Actions Action { get; set; }
        public Guid PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = new Portfolio();
        public Guid Executor { get; set; }
        public string Note { get; set; }
        public string Asset { get; set; }

    }
}
