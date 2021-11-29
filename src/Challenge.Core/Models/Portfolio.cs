    using System;

namespace Challenge.Core.Models
{
    public class Portfolio : BaseEntity
    {
        public DateTime Date { get; private set; }

        public Portfolio(string name)
        {
            Date = DateTime.UtcNow;
        }
    }
}
