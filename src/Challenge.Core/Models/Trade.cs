using Challenge.Core.Enums;
using System;

namespace Challenge.Core.Models
{
    public class Trade : AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Shares{ get; set; }
        public decimal Price { get; set; }       
        public string Currency { get; set; }
        public decimal MarketValue { get; set; }
        public Actions Action { get; set; }  //TODO: remove this comment: see PriorityLevel (saft)
        public Guid PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
        public Guid Executor { get; set; }
        public Account User { get; set; }
        public string Note { get; set; }
        public string Asset { get; set; }
    }
}
