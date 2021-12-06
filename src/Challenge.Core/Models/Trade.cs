using Challenge.Core.Enums;
using System;
using System.Text.Json.Serialization;

namespace Challenge.Core.Models
{
    public class Trade : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public int Shares{ get; set; }
        public decimal Price { get; set; }       
        public string Currency { get; set; }
        public decimal MarketValue { get; set; }
        public string Action { get; set; }
        public Guid PortfolioId { get; set; }
        [JsonIgnore]
        public Portfolio Portfolio { get; set; }
        public Guid Executor { get; set; }
        public string Note { get; set; }
        public string Asset { get; set; }
    }
}
