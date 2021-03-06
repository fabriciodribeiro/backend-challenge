using System;
using System.Collections.Generic;

namespace Challenge.Core.Models
{
    public class Account : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
        public IList<Portfolio> Portfolios { get; private set; } = new List<Portfolio>();
    }
}
