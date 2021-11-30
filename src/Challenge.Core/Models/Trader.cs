    using System;

namespace Challenge.Core.Models
{
    public class Trader : BaseEntity
    {
        public DateTime Date { get; private set; }

        public Trader(string name)
        {
            Date = DateTime.UtcNow;
        }
    }
}
