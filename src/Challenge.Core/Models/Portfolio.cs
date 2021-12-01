    using System;
using System.Collections.Generic;

namespace Challenge.Core.Models
{
    public class Portfolio : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        public Account User { get; set; }
        public IList<Trade> Trades { get; private set; } = new List<Trade>();
    }
}
