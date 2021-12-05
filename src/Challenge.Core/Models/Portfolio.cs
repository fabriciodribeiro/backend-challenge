using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Challenge.Core.Models
{
    public class Portfolio : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public Account User { get; set; }
        public IList<Trade> Trades { get; private set; } = new List<Trade>();
    }
}
